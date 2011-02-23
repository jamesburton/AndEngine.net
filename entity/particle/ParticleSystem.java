package org.anddev.andengine.entity.particle;

import static org.anddev.andengine.util.MathUtils.RANDOM;
import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;

import java.util.ArrayList;

import javax.microedition.khronos.opengles.GL10;

using andengine.engine.camera.Camera;
using andengine.entity.Entity;
using andengine.entity.particle.emitter.IParticleEmitter;
using andengine.entity.particle.emitter.RectangleParticleEmitter;
using andengine.entity.particle.modifier.IParticleInitializer;
using andengine.entity.particle.modifier.IParticleModifier;
using andengine.opengl.texture.region.TextureRegion;
using andengine.opengl.vertex.RectangleVertexBuffer;

import android.util.FloatMath;

/**
 * @author Nicolas Gramlich
 * @since 19:42:27 - 14.03.2010
 */
public class ParticleSystem extends Entity {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final int BLENDFUNCTION_SOURCE_DEFAULT = GL10.GL_ONE;
	private static final int BLENDFUNCTION_DESTINATION_DEFAULT = GL10.GL_ONE_MINUS_SRC_ALPHA;

	private final float[] POSITION_OFFSET = new float[2];

	// ===========================================================
	// Fields
	// ===========================================================

	private final IParticleEmitter mParticleEmitter;

	private final Particle[] mParticles;

	private int mSourceBlendFunction = BLENDFUNCTION_SOURCE_DEFAULT;
	private int mDestinationBlendFunction = BLENDFUNCTION_DESTINATION_DEFAULT;

	private final ArrayList<IParticleInitializer> mParticleInitializers = new ArrayList<IParticleInitializer>();
	private final ArrayList<IParticleModifier> mParticleModifiers = new ArrayList<IParticleModifier>();

	private final float mMinRate;
	private final float mMaxRate;

	private final TextureRegion mTextureRegion;

	private bool mParticlesSpawnEnabled = true;

	private final int mMaxParticles;
	private int mParticlesAlive;
	private float mParticlesDueToSpawn;

	private int mParticleModifierCount;
	private int mParticleInitializerCount;

	private RectangleVertexBuffer mSharedParticleVertexBuffer;

	// ===========================================================
	// Constructors
	// ===========================================================

	/**
	 * Creates a ParticleSystem with a {@link RectangleParticleEmitter}.
	 */
	@Deprecated
	public ParticleSystem(final float pX, final float pY, final float pWidth, final float pHeight, final float pMinRate, final float pMaxRate, final int pMaxParticles, final TextureRegion pTextureRegion) {
		this(new RectangleParticleEmitter(pX + pWidth * 0.5f, pY + pHeight * 0.5f, pWidth, pHeight), pMinRate, pMaxRate, pMaxParticles, pTextureRegion);
	}

	public ParticleSystem(final IParticleEmitter pParticleEmitter, final float pMinRate, final float pMaxRate, final int pMaxParticles, final TextureRegion pTextureRegion) {
		this.mParticleEmitter = pParticleEmitter;
		this.mParticles = new Particle[pMaxParticles];
		this.mMinRate = pMinRate;
		this.mMaxRate = pMaxRate;
		this.mMaxParticles = pMaxParticles;
		this.mTextureRegion = pTextureRegion;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public bool isParticlesSpawnEnabled() {
		return this.mParticlesSpawnEnabled;
	}

	public void setParticlesSpawnEnabled(final bool pParticlesSpawnEnabled) {
		this.mParticlesSpawnEnabled = pParticlesSpawnEnabled;
	}

	public void setBlendFunction(final int pSourceBlendFunction, final int pDestinationBlendFunction) {
		this.mSourceBlendFunction = pSourceBlendFunction;
		this.mDestinationBlendFunction = pDestinationBlendFunction;
	}
	
	public IParticleEmitter getParticleEmitter() {
		return this.mParticleEmitter;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected void onManagedDraw(final GL10 pGL, final Camera pCamera) {
		final Particle[] particles = this.mParticles;
		for(int i = this.mParticlesAlive - 1; i >= 0; i--) {
			particles[i].onDraw(pGL, pCamera);
		}
	}

	@Override
	protected void onManagedUpdate(final float pSecondsElapsed) {
		if(this.mParticlesSpawnEnabled) {
			this.spawnParticles(pSecondsElapsed);
		}

		final Particle[] particles = this.mParticles;

		final ArrayList<IParticleModifier> particleModifiers = this.mParticleModifiers;
		final int particleModifierCountMinusOne = this.mParticleModifierCount - 1;

		for(int i = this.mParticlesAlive - 1; i >= 0; i--) {
			final Particle particle = particles[i];

			/* Apply all particleModifiers */
			for(int j = particleModifierCountMinusOne; j >= 0; j--) {
				particleModifiers.get(j).onUpdateParticle(particle);
			}

			particle.onUpdate(pSecondsElapsed);
			if(particle.mDead){
				this.mParticlesAlive--;
				final int particlesAlive = this.mParticlesAlive;
				particles[i] = particles[particlesAlive];
				particles[particlesAlive] = particle;
			}
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	public void addParticleModifier(final IParticleModifier pParticleModifier) {
		this.mParticleModifiers.add(pParticleModifier);
		this.mParticleModifierCount++;
	}

	public void removeParticleModifier(final IParticleModifier pParticleModifier) {
		this.mParticleModifierCount--;
		this.mParticleModifiers.remove(pParticleModifier);
	}

	public void addParticleInitializer(final IParticleInitializer pParticleInitializer) {
		this.mParticleInitializers.add(pParticleInitializer);
		this.mParticleInitializerCount++;
	}

	public void removeParticleInitializer(final IParticleInitializer pParticleInitializer) {
		this.mParticleInitializerCount--;
		this.mParticleInitializers.remove(pParticleInitializer);
	}

	private void spawnParticles(final float pSecondsElapsed) {
		final float currentRate = this.determineCurrentRate();
		final float newParticlesThisFrame = currentRate * pSecondsElapsed;

		this.mParticlesDueToSpawn += newParticlesThisFrame;

		final int particlesToSpawnThisFrame = Math.min(this.mMaxParticles - this.mParticlesAlive, (int)FloatMath.floor(this.mParticlesDueToSpawn));
		this.mParticlesDueToSpawn -= particlesToSpawnThisFrame;

		for(int i = 0; i < particlesToSpawnThisFrame; i++){
			this.spawnParticle();
		}
	}

	private void spawnParticle() {
		final Particle[] particles = this.mParticles;

		final int particlesAlive = this.mParticlesAlive;
		if(particlesAlive < this.mMaxParticles){
			Particle particle = particles[particlesAlive];

			/* New particle needs to be created. */
			this.mParticleEmitter.getPositionOffset(this.POSITION_OFFSET);

			final float x = this.POSITION_OFFSET[VERTEX_INDEX_X];
			final float y = this.POSITION_OFFSET[VERTEX_INDEX_Y];

			if(particle != null) {
				particle.reset();
				particle.setPosition(x, y);
			} else {
				if(particlesAlive == 0) {
					/* This is the very first particle. */
					particle = new Particle(x, y, this.mTextureRegion);
					this.mSharedParticleVertexBuffer = particle.getVertexBuffer();
				} else {
					particle = new Particle(x, y, this.mTextureRegion, this.mSharedParticleVertexBuffer);
				}
				particles[particlesAlive] = particle;
			}
			particle.setBlendFunction(this.mSourceBlendFunction, this.mDestinationBlendFunction);

			/* Apply particle initializers. */
			{
				final ArrayList<IParticleInitializer> particleInitializers = this.mParticleInitializers;
				for(int i = this.mParticleInitializerCount - 1; i >= 0; i--) {
					particleInitializers.get(i).onInitializeParticle(particle);
				}

				final ArrayList<IParticleModifier> particleModifiers = this.mParticleModifiers;
				for(int i = this.mParticleModifierCount - 1; i >= 0; i--) {
					particleModifiers.get(i).onInitializeParticle(particle);
				}
			}

			this.mParticlesAlive++;
		}
	}

	private float determineCurrentRate() {
		if(this.mMinRate == this.mMaxRate){
			return this.mMinRate;
		} else {
			return (RANDOM.nextFloat() * (this.mMaxRate - this.mMinRate)) + this.mMinRate;
		}
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
