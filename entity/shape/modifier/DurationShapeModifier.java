package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.BaseDurationModifier;

/**
 * @author Nicolas Gramlich
 * @since 16:10:42 - 19.03.2010
 */
public abstract class DurationShapeModifier extends BaseDurationModifier<IShape> : IShapeModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public DurationShapeModifier() {
		super();
	}

	public DurationShapeModifier(final float pDuration) {
		super(pDuration);
	}

	public DurationShapeModifier(final float pDuration, final IShapeModifierListener pShapeModifierListener) {
		super(pDuration, pShapeModifierListener);
	}

	protected DurationShapeModifier(final DurationShapeModifier pDurationShapeModifier) {
		super(pDurationShapeModifier);
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
