package org.anddev.andengine.entity.shape.modifier;

using andengine.entity.shape.IShape;
using andengine.util.modifier.IModifier;

/**
 * @author Nicolas Gramlich
 * @since 11:17:50 - 19.03.2010
 */
public interface IShapeModifier extends IModifier<IShape> {
	// ===========================================================
	// Final Fields
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	public static interface IShapeModifierListener extends IModifierListener<IShape>{
		// ===========================================================
		// Final Fields
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================
	}
}
