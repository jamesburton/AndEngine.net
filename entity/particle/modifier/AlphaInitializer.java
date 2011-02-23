package org.anddev.andengine.entity.particle.modifier;

using andengine.entity.particle.Particle;


/**
 * @author Nicolas Gramlich
 * @since 18:53:41 - 02.10.2010
 */
public class AlphaInitializer extends BaseSingleValueInitializer {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public AlphaInitializer(final float pAlpha) {
		super(pAlpha, pAlpha);
	}

	public AlphaInitializer(final float pMinAlpha, final float pMaxAlpha) {
		super(pMinAlpha, pMaxAlpha);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected void onInitializeParticle(final Particle pParticle, final float pAlpha) {
		pParticle.setAlpha(pAlpha);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
