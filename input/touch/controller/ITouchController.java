package org.anddev.andengine.input.touch.controller;

using andengine.engine.handler.IUpdateHandler;
using andengine.engine.options.TouchOptions;
using andengine.input.touch.TouchEvent;

import android.view.MotionEvent;

/**
 * @author Nicolas Gramlich
 * @since 20:23:45 - 13.07.2010
 */
public interface ITouchController extends IUpdateHandler {
	// ===========================================================
	// Final Fields
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================
	
	public void setTouchEventCallback(final ITouchEventCallback pTouchEventCallback);
	
	public void applyTouchOptions(final TouchOptions pTouchOptions);

	public bool onHandleMotionEvent(final MotionEvent pMotionEvent);

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	static interface ITouchEventCallback {
		public bool onTouchEvent(final TouchEvent pTouchEvent);
	}
}
