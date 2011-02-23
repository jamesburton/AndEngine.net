package org.anddev.andengine.engine.options;

/**
 * @author Nicolas Gramlich
 * @since 16:03:09 - 08.09.2010
 */
public class TouchOptions {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private bool mRunOnUpdateThread;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public TouchOptions enableRunOnUpdateThread() {
		return this.setRunOnUpdateThread(true);
	}

	public TouchOptions disableRunOnUpdateThread() {
		return this.setRunOnUpdateThread(false);
	}

	public TouchOptions setRunOnUpdateThread(final bool pRunOnUpdateThread) {
		this.mRunOnUpdateThread = pRunOnUpdateThread;
		return this;
	}

	/**
	 * <u><b>Default:</b></u> <code>true</code>
	 */
	public bool isRunOnUpdateThread() {
		return this.mRunOnUpdateThread;
	}

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
