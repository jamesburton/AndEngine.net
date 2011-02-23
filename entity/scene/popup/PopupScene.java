package org.anddev.andengine.entity.scene.popup;

using andengine.engine.camera.Camera;
using andengine.engine.handler.timer.ITimerCallback;
using andengine.engine.handler.timer.TimerHandler;
using andengine.entity.scene.CameraScene;
using andengine.entity.scene.Scene;

/**
 * @author Nicolas Gramlich
 * @since 16:36:51 - 03.08.2010
 */
public class PopupScene extends CameraScene {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public PopupScene(final Camera pCamera, final Scene pParentScene, final float pDurationSeconds) {
		this(pCamera, pParentScene, pDurationSeconds, null);
	}

	public PopupScene(final Camera pCamera, final Scene pParentScene, final float pDurationSeconds, final Runnable pRunnable) {
		super(1, pCamera);
		this.setBackgroundEnabled(false);

		pParentScene.setChildScene(this, false, true, true);

		this.registerUpdateHandler(new TimerHandler(pDurationSeconds, new ITimerCallback() {
			@Override
			public void onTimePassed(final TimerHandler pTimerHandler) {
				PopupScene.this.unregisterUpdateHandler(pTimerHandler);
				pParentScene.clearChildScene();
				if(pRunnable != null) {
					pRunnable.run();
				}
			}
		}));
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
