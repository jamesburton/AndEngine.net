package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.BaseModifier;

/**
 * @author Nicolas Gramlich
 * @since 10:53:16 - 03.09.2010
 */
public abstract class ShapeModifier extends BaseModifier<IShape> : IShapeModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public ShapeModifier() {
		super();
	}

	public ShapeModifier(final IShapeModifierListener pShapeModifierListener) {
		super(pShapeModifierListener);
	}

	protected ShapeModifier(final ShapeModifier pShapeModifier) {
		super(pShapeModifier);
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
