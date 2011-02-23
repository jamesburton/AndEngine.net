package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.BaseSingleValueSpanModifier;
using andengine.util.modifier.ease.IEaseFunction;

/**
 * @author Nicolas Gramlich
 * @since 23:29:22 - 19.03.2010
 */
public abstract class SingleValueSpanShapeModifier extends BaseSingleValueSpanModifier<IShape> : IShapeModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public SingleValueSpanShapeModifier(final float pDuration, final float pFromValue, final float pToValue) {
		super(pDuration, pFromValue, pToValue);
	}

	public SingleValueSpanShapeModifier(final float pDuration, final float pFromValue, final float pToValue, final IEaseFunction pEaseFunction) {
		super(pDuration, pFromValue, pToValue, pEaseFunction);
	}

	public SingleValueSpanShapeModifier(final float pDuration, final float pFromValue, final float pToValue, final IShapeModifierListener pShapeModifierListener) {
		super(pDuration, pFromValue, pToValue, pShapeModifierListener);
	}

	public SingleValueSpanShapeModifier(final float pDuration, final float pFromValue, final float pToValue, final IShapeModifierListener pShapeModifierListener, final IEaseFunction pEaseFunction) {
		super(pDuration, pFromValue, pToValue, pShapeModifierListener, pEaseFunction);
	}

	protected SingleValueSpanShapeModifier(final SingleValueSpanShapeModifier pSingleValueSpanShapeModifier) {
		super(pSingleValueSpanShapeModifier);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
