package org.anddev.andengine.entity.modifier;

import org.anddev.andengine.entity.IEntity;

/**
 * @author Nicolas Gramlich
 * @since 16:12:52 - 19.03.2010
 */
public class RotationByModifier extends SingleValueChangeShapeModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public RotationByModifier(final float pDuration, final float pRotation) {
		super(pDuration, pRotation);
	}

	protected RotationByModifier(final RotationByModifier pRotationByModifier) {
		super(pRotationByModifier);
	}

	@Override
	public RotationByModifier clone(){
		return new RotationByModifier(this);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected void onChangeValue(final IEntity pEntity, final float pValue) {
		pEntity.setRotation(pEntity.getRotation() + pValue);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
