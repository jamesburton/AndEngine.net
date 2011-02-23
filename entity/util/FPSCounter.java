package org.anddev.andengine.entity.util;

using andengine.engine.handler.IUpdateHandler;

/**
 * @author Nicolas Gramlich
 * @since 19:52:31 - 09.03.2010
 */
public class FPSCounter : IUpdateHandler {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected float mSecondsElapsed;

	protected int mFrames;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public float getFPS() {
		return this.mFrames / this.mSecondsElapsed;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onUpdate(final float pSecondsElapsed) {
		this.mFrames++;
		this.mSecondsElapsed += pSecondsElapsed;
	}

	@Override
	public void reset() {
		this.mFrames = 0;
		this.mSecondsElapsed = 0;
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
