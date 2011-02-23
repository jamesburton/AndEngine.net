package org.anddev.andengine.entity.scene.background.modifier;

using andengine.entity.scene.background.IBackground;
using andengine.util.modifier.LoopModifier;

/**
 * @author Nicolas Gramlich
 * @since 15:03:53 - 03.09.2010
 */
public class LoopBackgroundModifier extends LoopModifier<IBackground> : IBackgroundModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public LoopBackgroundModifier(final IBackgroundModifier pBackgroundModifier) {
		super(pBackgroundModifier);
	}

	public LoopBackgroundModifier(final IBackgroundModifierListener pBackgroundModifierListener, final int pLoopCount, final ILoopBackgroundModifierListener pLoopModifierListener, final IBackgroundModifier pBackgroundModifier) {
		super(pBackgroundModifierListener, pLoopCount, pLoopModifierListener, pBackgroundModifier);
	}

	public LoopBackgroundModifier(final IBackgroundModifierListener pBackgroundModifierListener, final int pLoopCount, final IBackgroundModifier pBackgroundModifier) {
		super(pBackgroundModifierListener, pLoopCount, pBackgroundModifier);
	}

	public LoopBackgroundModifier(final int pLoopCount, final IBackgroundModifier pBackgroundModifier) {
		super(pLoopCount, pBackgroundModifier);
	}

	protected LoopBackgroundModifier(final LoopBackgroundModifier pLoopBackgroundModifier) {
		super(pLoopBackgroundModifier);
	}
	
	@Override
	public LoopBackgroundModifier clone() {
		return new LoopBackgroundModifier(this);
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

	public interface ILoopBackgroundModifierListener extends ILoopModifierListener<IBackground> {
		// ===========================================================
		// Final Fields
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================
	}
}
