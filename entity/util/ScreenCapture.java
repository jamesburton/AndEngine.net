package org.anddev.andengine.entity.util;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;

import javax.microedition.khronos.opengles.GL10;

using andengine.engine.camera.Camera;
using andengine.entity.Entity;
using andengine.entity.util.ScreenGrabber.IScreenGrabberCallback;
using andengine.util.Debug;
using andengine.util.StreamUtils;

import android.graphics.Bitmap;
import android.graphics.Bitmap.CompressFormat;

/**
 * @author Nicolas Gramlich
 * @since 15:11:50 - 15.03.2010
 */
public class ScreenCapture extends Entity : IScreenGrabberCallback {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private String mFilePath;

	private final ScreenGrabber mScreenGrabber = new ScreenGrabber();

	private IScreenshotCallback mScreenCaptureCallback;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected void onManagedDraw(final GL10 pGL, final Camera pCamera) {
		this.mScreenGrabber.onManagedDraw(pGL, pCamera);
	}

	@Override
	protected void onManagedUpdate(final float pSecondsElapsed) {
		/* Nothing */
	}

	@Override
	public void reset() {
		/* Nothing */
	}

	@Override
	public void onScreenGrabbed(final Bitmap pBitmap) {
		try {
			ScreenCapture.saveCapture(pBitmap, this.mFilePath);
		} catch (final FileNotFoundException e) {
			this.mScreenCaptureCallback.onScreenCaptureFailed(this.mFilePath, e);
		}
	}

	@Override
	public void onScreenGrabFailed(final Exception pException) {
		this.mScreenCaptureCallback.onScreenCaptureFailed(this.mFilePath, pException);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	public void capture(final int pCaptureWidth, final int pCaptureHeight, final String pFilePath, final IScreenshotCallback pScreenCaptureCallback) {
		this.capture(0, 0, pCaptureWidth, pCaptureHeight, pFilePath, pScreenCaptureCallback);
	}

	public void capture(final int pCaptureX, final int pCaptureY, final int pCaptureWidth, final int pCaptureHeight, final String pFilePath, final IScreenshotCallback pScreenshotCallback) {
		this.mFilePath = pFilePath;
		this.mScreenCaptureCallback = pScreenshotCallback;
		this.mScreenGrabber.grab(pCaptureX, pCaptureY, pCaptureWidth, pCaptureHeight, this);
	}

	private static void saveCapture(final Bitmap pBitmap, final String pFilePath) throws FileNotFoundException {
		FileOutputStream fos = null;
		try {
			fos = new FileOutputStream(pFilePath);
			pBitmap.compress(CompressFormat.PNG, 100, fos);

		} catch (final FileNotFoundException e) {
			StreamUtils.flushCloseStream(fos);
			Debug.e("Error saving file to: " + pFilePath, e);
			throw e;
		}
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	public static interface IScreenshotCallback {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================

		public void onScreenCaptured(final String pFilePath);
		public void onScreenCaptureFailed(final String pFilePath, final Exception pException);
	}
}
