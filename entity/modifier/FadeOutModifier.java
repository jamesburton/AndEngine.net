package org.anddev.andengine.entity.modifier;

import org.anddev.andengine.util.modifier.ease.IEaseFunction;


/**
 * @author Nicolas Gramlich
 * @since 19:03:12 - 08.06.2010
 */
public class FadeOutModifier extends AlphaModifier {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public FadeOutModifier(final float pDuration) {
		super(pDuration, 1.0f, 0.0f, IEaseFunction.DEFAULT);
	}

	public FadeOutModifier(final float pDuration, final IEaseFunction pEaseFunction) {
		super(pDuration, 1.0f, 0.0f, pEaseFunction);
	}

	public FadeOutModifier(final float pDuration, final IEntityModifierListener pEntityModifierListener) {
		super(pDuration, 1.0f, 0.0f, pEntityModifierListener, IEaseFunction.DEFAULT);
	}

	public FadeOutModifier(final float pDuration, final IEntityModifierListener pEntityModifierListener, final IEaseFunction pEaseFunction) {
		super(pDuration, 1.0f, 0.0f, pEntityModifierListener, pEaseFunction);
	}

	protected FadeOutModifier(final FadeOutModifier pFadeOutModifier) {
		super(pFadeOutModifier);
	}

	@Override
	public FadeOutModifier clone() {
		return new FadeOutModifier(this);
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
