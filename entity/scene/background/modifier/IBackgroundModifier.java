package org.anddev.andengine.entity.scene.background.modifier;

using andengine.entity.scene.background.IBackground;
using andengine.util.modifier.IModifier;

/**
 * @author Nicolas Gramlich
 * @since 14:55:54 - 03.09.2010
 */
public interface IBackgroundModifier extends IModifier<IBackground> {
	// ===========================================================
	// Final Fields
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	public static interface IBackgroundModifierListener extends IModifierListener<IBackground>{
		// ===========================================================
		// Final Fields
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================
	}
}
