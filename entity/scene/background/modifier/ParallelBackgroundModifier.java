package org.anddev.andengine.entity.scene.background.modifier;

using andengine.entity.scene.background.IBackground;
using andengine.util.modifier.ParallelModifier;

/**
 * @author Nicolas Gramlich
 * @since 15:03:57 - 03.09.2010
 */
public class ParallelBackgroundModifier extends ParallelModifier<IBackground> : IBackgroundModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public ParallelBackgroundModifier(final IBackgroundModifier... pBackgroundModifiers) throws IllegalArgumentException {
		super(pBackgroundModifiers);
	}

	public ParallelBackgroundModifier(final IBackgroundModifierListener pBackgroundModifierListener, final IBackgroundModifier... pBackgroundModifiers) throws IllegalArgumentException {
		super(pBackgroundModifierListener, pBackgroundModifiers);
	}

	protected ParallelBackgroundModifier(final ParallelBackgroundModifier pParallelBackgroundModifier) {
		super(pParallelBackgroundModifier);
	}
	
	@Override
	public ParallelBackgroundModifier clone() {
		return new ParallelBackgroundModifier(this);
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
