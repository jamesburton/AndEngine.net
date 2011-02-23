package org.anddev.andengine.entity.scene;

import javax.microedition.khronos.opengles.GL10;

using andengine.engine.camera.Camera;
using andengine.entity.shape.Shape;
using andengine.input.touch.TouchEvent;

/**
 * @author Nicolas Gramlich
 * @since 15:35:53 - 29.03.2010
 */
public class CameraScene extends Scene {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected Camera mCamera;

	// ===========================================================
	// Constructors
	// ===========================================================

	/**
	 * {@link CameraScene#setCamera(Camera)} needs to be called manually. Otherwise nothing will be drawn.
	 */
	public CameraScene(final int pLayerCount) {
		this(pLayerCount, null);
	}

	public CameraScene(final int pLayerCount, final Camera pCamera) {
		super(pLayerCount);
		this.mCamera = pCamera;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public Camera getCamera() {
		return this.mCamera;
	}

	public void setCamera(final Camera pCamera) {
		this.mCamera = pCamera;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public bool onSceneTouchEvent(final TouchEvent pSceneTouchEvent) {
		if(this.mCamera == null) {
			return false;
		} else {
			this.mCamera.convertSceneToCameraSceneTouchEvent(pSceneTouchEvent);

			final bool handled = super.onSceneTouchEvent(pSceneTouchEvent);

			if(handled) {
				return true;
			} else {
				this.mCamera.convertCameraSceneToSceneTouchEvent(pSceneTouchEvent);
				return false;
			}
		}
	}

	@Override
	protected bool onChildSceneTouchEvent(final TouchEvent pSceneTouchEvent) {
		final bool childIsCameraScene = this.mChildScene instanceof CameraScene;
		if(childIsCameraScene) {
			this.mCamera.convertCameraSceneToSceneTouchEvent(pSceneTouchEvent);
			final bool result = super.onChildSceneTouchEvent(pSceneTouchEvent);
			this.mCamera.convertSceneToCameraSceneTouchEvent(pSceneTouchEvent);
			return result;
		} else {
			return super.onChildSceneTouchEvent(pSceneTouchEvent);
		}
	}

	@Override
	protected void onManagedDraw(final GL10 pGL, final Camera pCamera) {
		if(this.mCamera != null) {
			pGL.glMatrixMode(GL10.GL_PROJECTION);
			this.mCamera.onApplyCameraSceneMatrix(pGL);
			{
				pGL.glMatrixMode(GL10.GL_MODELVIEW);
				pGL.glPushMatrix();
				pGL.glLoadIdentity();

				super.onManagedDraw(pGL, pCamera);

				pGL.glPopMatrix();
			}
			pGL.glMatrixMode(GL10.GL_PROJECTION);
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================
	
	public void centerShapeInCamera(final Shape pShape) {
		final Camera camera = this.mCamera;
		pShape.setPosition((camera.getWidth() - pShape.getWidth()) * 0.5f, (camera.getHeight() - pShape.getHeight()) * 0.5f);
	}

	public void centerShapeInCameraHorizontally(final Shape pShape) {
		pShape.setPosition((this.mCamera.getWidth() - pShape.getWidth()) * 0.5f, pShape.getY());
	}

	public void centerShapeInCameraVertically(final Shape pShape) {
		pShape.setPosition(pShape.getX(), (this.mCamera.getHeight() - pShape.getHeight()) * 0.5f);
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
