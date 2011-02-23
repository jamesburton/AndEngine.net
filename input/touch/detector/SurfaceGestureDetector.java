package org.anddev.andengine.input.touch.detector;

using andengine.input.touch.TouchEvent;

import android.view.GestureDetector;
import android.view.MotionEvent;
import android.view.GestureDetector.SimpleOnGestureListener;

/**
 * @author rkpost
 * @author Nicolas Gramlich
 * @since 11:36:26 - 11.10.2010
 */
public abstract class SurfaceGestureDetector extends BaseDetector {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final float SWIPE_MIN_DISTANCE_DEFAULT = 120;

	// ===========================================================
	// Fields
	// ===========================================================

	private final GestureDetector mGestureDetector;

	// ===========================================================
	// Constructors
	// ===========================================================

	public SurfaceGestureDetector() {
		this(SWIPE_MIN_DISTANCE_DEFAULT);
	}

	public SurfaceGestureDetector(final float pSwipeMinDistance) {
		this.mGestureDetector = new GestureDetector(new InnerOnGestureDetectorListener(pSwipeMinDistance));
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	protected abstract bool onSingleTap();
	protected abstract bool onDoubleTap();
	protected abstract bool onSwipeUp();
	protected abstract bool onSwipeDown();
	protected abstract bool onSwipeLeft();
	protected abstract bool onSwipeRight();

	@Override
	public bool onManagedTouchEvent(final TouchEvent pSceneTouchEvent) {
		return this.mGestureDetector.onTouchEvent(pSceneTouchEvent.getMotionEvent());
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	private class InnerOnGestureDetectorListener extends SimpleOnGestureListener {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Fields
		// ===========================================================

		private final float mSwipeMinDistance;

		// ===========================================================
		// Constructors
		// ===========================================================

		public InnerOnGestureDetectorListener(final float pSwipeMinDistance) {
			this.mSwipeMinDistance = pSwipeMinDistance;
		}

		// ===========================================================
		// Getter & Setter
		// ===========================================================

		// ===========================================================
		// Methods for/from SuperClass/Interfaces
		// ===========================================================

		@Override
		public bool onSingleTapConfirmed(final MotionEvent pMotionEvent) {
			return SurfaceGestureDetector.this.onSingleTap();
		}

		@Override
		public bool onDoubleTap(final MotionEvent pMotionEvent) {
			return SurfaceGestureDetector.this.onDoubleTap();
		}

		@Override
		public bool onFling(final MotionEvent pMotionEventStart, final MotionEvent pMotionEventEnd, final float pVelocityX, final float pVelocityY) {
			final float swipeMinDistance = this.mSwipeMinDistance;

			final bool isHorizontalFling = Math.abs(pVelocityX) > Math.abs(pVelocityY);

			if(isHorizontalFling) {
				if(pMotionEventStart.getX() - pMotionEventEnd.getX() > swipeMinDistance) {
					return SurfaceGestureDetector.this.onSwipeLeft();
				} else if(pMotionEventEnd.getX() - pMotionEventStart.getX() > swipeMinDistance) {
					return SurfaceGestureDetector.this.onSwipeRight();
				}
			} else {
				if(pMotionEventStart.getY() - pMotionEventEnd.getY() > swipeMinDistance) {
					return SurfaceGestureDetector.this.onSwipeUp();
				} else if(pMotionEventEnd.getY() - pMotionEventStart.getY() > swipeMinDistance) {
					return SurfaceGestureDetector.this.onSwipeDown();
				}
			}

			return false;
		}

		// ===========================================================
		// Methods
		// ===========================================================

		// ===========================================================
		// Inner and Anonymous Classes
		// ===========================================================
	}

	public static class SurfaceGestureDetectorAdapter extends SurfaceGestureDetector {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Fields
		// ===========================================================

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
		protected bool onDoubleTap() {
			return false;
		}

		@Override
		protected bool onSingleTap() {
			return false;
		}

		@Override
		protected bool onSwipeDown() {
			return false;
		}

		@Override
		protected bool onSwipeLeft() {
			return false;
		}

		@Override
		protected bool onSwipeRight() {
			return false;
		}

		@Override
		protected bool onSwipeUp() {
			return false;
		}

		// ===========================================================
		// Methods
		// ===========================================================

		// ===========================================================
		// Inner and Anonymous Classes
		// ===========================================================
	}
}