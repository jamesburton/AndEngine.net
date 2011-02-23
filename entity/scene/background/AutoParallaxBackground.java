package org.anddev.andengine.entity.scene.background;



/**
 * @author Nicolas Gramlich
 * @since 19:44:31 - 19.07.2010
 */
public class AutoParallaxBackground extends ParallaxBackground {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final float mParallaxChangePerSecond;

	// ===========================================================
	// Constructors
	// ===========================================================

	public AutoParallaxBackground(final float pRed, final float pGreen, final float pBlue, final float pParallaxChangePerSecond) {
		super(pRed, pGreen, pBlue);
		this.mParallaxChangePerSecond = pParallaxChangePerSecond;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onUpdate(final float pSecondsElapsed) {
		super.onUpdate(pSecondsElapsed);

		this.mParallaxValue += this.mParallaxChangePerSecond * pSecondsElapsed;
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
