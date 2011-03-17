namespace andengine.examples {

import javax.microedition.khronos.opengles.GL10;

using Engine = andengine.engine.Engine;
import org.anddev.andengine.engine.camera.Camera;
using EngineOptions = andengine.engine.options.EngineOptions;
using ScreenOrientation = andengine.engine.options.EngineOptions.ScreenOrientation;
using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
import org.anddev.andengine.entity.particle.ParticleSystem;
import org.anddev.andengine.entity.particle.emitter.CircleOutlineParticleEmitter;
import org.anddev.andengine.entity.particle.initializer.AlphaInitializer;
import org.anddev.andengine.entity.particle.initializer.ColorInitializer;
import org.anddev.andengine.entity.particle.initializer.RotationInitializer;
import org.anddev.andengine.entity.particle.initializer.VelocityInitializer;
import org.anddev.andengine.entity.particle.modifier.AlphaModifier;
import org.anddev.andengine.entity.particle.modifier.ColorModifier;
import org.anddev.andengine.entity.particle.modifier.ExpireModifier;
import org.anddev.andengine.entity.particle.modifier.ScaleModifier;
using Scene = andengine.entity.scene.Scene;
using IOnSceneTouchListener = andengine.entity.scene.Scene.IOnSceneTouchListener;
using FPSLogger = andengine.entity.util.FPSLogger;
using TouchEvent = andengine.input.touch.TouchEvent;
using Texture = andengine.opengl.texture.Texture;
using TextureOptions = andengine.opengl.texture.TextureOptions;
using TextureRegion = andengine.opengl.texture.region.TextureRegion;
using TextureRegionFactory = andengine.opengl.texture.region.TextureRegionFactory;

using Toast = Android.Widget.Toast;

/**
 * @author Nicolas Gramlich
 * @since 11:54:51 - 03.04.2010
 */
public class ParticleSystemSimpleExample : BaseExample {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final int CAMERA_WIDTH = 720;
	private static final int CAMERA_HEIGHT = 480;

	// ===========================================================
	// Fields
	// ===========================================================

	private Camera mCamera;
	private Texture mTexture;
	private TextureRegion mParticleTextureRegion;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public Engine onLoadEngine() {
		Toast.makeText(this, "Touch the screen to move the particlesystem.", Toast.LENGTH_LONG).show();
		this.mCamera = new Camera(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);
		return new Engine(new EngineOptions(true, ScreenOrientation.LANDSCAPE, new RatioResolutionPolicy(CAMERA_WIDTH, CAMERA_HEIGHT), this.mCamera));
	}

	@Override
	public void onLoadResources() {
		this.mTexture = new Texture(32, 32, TextureOptions.BILINEAR_PREMULTIPLYALPHA);

		this.mParticleTextureRegion = TextureRegionFactory.createFromAsset(this.mTexture, this, "gfx/particle_point.png", 0, 0);

		this.mEngine.getTextureManager().loadTexture(this.mTexture);
	}

	@Override
	public Scene onLoadScene() {
		this.mEngine.registerUpdateHandler(new FPSLogger());

		final Scene scene = new Scene(1);

		final CircleOutlineParticleEmitter particleEmitter = new CircleOutlineParticleEmitter(CAMERA_WIDTH * 0.5f, CAMERA_HEIGHT * 0.5f + 20, 80);
		final ParticleSystem particleSystem = new ParticleSystem(particleEmitter, 60, 60, 360, this.mParticleTextureRegion);

		scene.setOnSceneTouchListener(new IOnSceneTouchListener() {
			@Override
			public boolean onSceneTouchEvent(final Scene pScene, final TouchEvent pSceneTouchEvent) {
				particleEmitter.setCenter(pSceneTouchEvent.getX(), pSceneTouchEvent.getY());
				return true;
			}
		});

		particleSystem.addParticleInitializer(new ColorInitializer(1, 0, 0));
		particleSystem.addParticleInitializer(new AlphaInitializer(0));
		particleSystem.setBlendFunction(GL10.GL_SRC_ALPHA, GL10.GL_ONE);
		particleSystem.addParticleInitializer(new VelocityInitializer(-2, 2, -20, -10));
		particleSystem.addParticleInitializer(new RotationInitializer(0.0f, 360.0f));

		particleSystem.addParticleModifier(new ScaleModifier(1.0f, 2.0f, 0, 5));
		particleSystem.addParticleModifier(new ColorModifier(1, 1, 0, 0.5f, 0, 0, 0, 3));
		particleSystem.addParticleModifier(new ColorModifier(1, 1, 0.5f, 1, 0, 1, 4, 6));
		particleSystem.addParticleModifier(new AlphaModifier(0, 1, 0, 1));
		particleSystem.addParticleModifier(new AlphaModifier(1, 0, 5, 6));
		particleSystem.addParticleModifier(new ExpireModifier(6, 6));

		scene.getLastChild().attachChild(particleSystem);

		return scene;
	}

	@Override
	public void onLoadComplete() {

	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
