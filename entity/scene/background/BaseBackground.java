package org.anddev.andengine.entity.scene.background;

using andengine.util.modifier.IModifier;
using andengine.util.modifier.ModifierList;



/**
 * @author Nicolas Gramlich
 * @since 14:08:17 - 19.07.2010
 */
public abstract class BaseBackground : IBackground {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final ModifierList<IBackground> mBackgroundModifiers = new ModifierList<IBackground>(this);

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
	public void addBackgroundModifier(final IModifier<IBackground> pBackgroundModifier) {
		this.mBackgroundModifiers.add(pBackgroundModifier);
	}

	@Override
	public bool removeBackgroundModifier(final IModifier<IBackground> pBackgroundModifier) {
		return this.mBackgroundModifiers.remove(pBackgroundModifier);
	}

	@Override
	public void clearBackgroundModifiers() {
		this.mBackgroundModifiers.clear();
	}

	@Override
	public void onUpdate(final float pSecondsElapsed) {
		this.mBackgroundModifiers.onUpdate(pSecondsElapsed);
	}

	@Override
	public void reset() {
		this.mBackgroundModifiers.reset();
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
