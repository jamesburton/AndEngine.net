package org.anddev.andengine.input.touch.detector;

using andengine.entity.scene.Scene;
using andengine.entity.scene.Scene.IOnSceneTouchListener;
using andengine.input.touch.TouchEvent;

/**
 * @author Nicolas Gramlich
 * @since 15:59:00 - 05.11.2010
 */
public abstract class BaseDetector : IOnSceneTouchListener {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private bool mEnabled = true;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public bool isEnabled() {
		return this.mEnabled;
	}

	public void setEnabled(final bool pEnabled) {
		this.mEnabled = pEnabled;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	protected abstract bool onManagedTouchEvent(TouchEvent pSceneTouchEvent);

	@Override
	public bool onSceneTouchEvent(final Scene pScene, final TouchEvent pSceneTouchEvent) {
		return this.onTouchEvent(pSceneTouchEvent);
	}

	public final bool onTouchEvent(final TouchEvent pSceneTouchEvent) {
		if(this.mEnabled) {
			return this.onManagedTouchEvent(pSceneTouchEvent);
		} else {
			return false;
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
