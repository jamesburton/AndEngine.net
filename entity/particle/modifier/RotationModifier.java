package org.anddev.andengine.entity.particle.modifier;

using andengine.entity.particle.Particle;

/**
 * @author Nicolas Gramlich
 * @since 10:36:18 - 29.06.2010
 */
public class RotationModifier extends BaseSingleValueSpanModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public RotationModifier(final float pFromRotation, final float pToRotation, final float pFromTime, final float pToTime) {
		super(pFromRotation, pToRotation, pFromTime, pToTime);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected void onSetInitialValue(final Particle pParticle, final float pRotation) {
		pParticle.setRotation(pRotation);
	}

	@Override
	protected void onSetValue(final Particle pParticle, final float pRotation) {
		pParticle.setRotation(pRotation);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
