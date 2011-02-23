package org.anddev.andengine.input.touch.detector;

using andengine.engine.handler.IUpdateHandler;
using andengine.engine.handler.timer.ITimerCallback;
using andengine.engine.handler.timer.TimerHandler;
using andengine.entity.scene.Scene;
using andengine.input.touch.TouchEvent;

import android.os.SystemClock;
import android.speech.tts.TextToSpeech.Engine;
import android.view.MotionEvent;

/**
 * Note: Needs to be registered as an {@link IUpdateHandler} to the {@link Engine} or {@link Scene} to work properly.
 * 
 * @author Nicolas Gramlich
 * @since 20:49:25 - 23.08.2010
 */
public class HoldDetector extends BaseDetector : IUpdateHandler {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final long TRIGGER_HOLD_MINIMUM_MILLISECONDS_DEFAULT = 200;
	private static final float TRIGGER_HOLD_MAXIMUM_DISTANCE_DEFAULT = 10;

	private static final float TIME_BETWEEN_UPDATES_DEFAULT = 0.1f;

	// ===========================================================
	// Fields
	// ===========================================================

	private long mTriggerHoldMinimumMilliseconds;
	private float mTriggerHoldMaximumDistance;
	private final IHoldDetectorListener mHoldDetectorListener;

	private long mDownTimeMilliseconds = Long.MIN_VALUE;

	private float mDownX;
	private float mDownY;

	private float mHoldX;
	private float mHoldY;

	private bool mMaximumDistanceExceeded = false;

	private bool mTriggerOnHold = false;
	private bool mTriggerOnHoldFinished = false;

	private final TimerHandler mTimerHandler;

	// ===========================================================
	// Constructors
	// ===========================================================

	public HoldDetector(final IHoldDetectorListener pClickDetectorListener) {
		this(TRIGGER_HOLD_MINIMUM_MILLISECONDS_DEFAULT, TRIGGER_HOLD_MAXIMUM_DISTANCE_DEFAULT, TIME_BETWEEN_UPDATES_DEFAULT, pClickDetectorListener);
	}

	public HoldDetector(final long pTriggerHoldMinimumMilliseconds, final float pTriggerHoldMaximumDistance, final float pTimeBetweenUpdates, final IHoldDetectorListener pClickDetectorListener) {
		this.mTriggerHoldMinimumMilliseconds = pTriggerHoldMinimumMilliseconds;
		this.mTriggerHoldMaximumDistance = pTriggerHoldMaximumDistance;
		this.mHoldDetectorListener = pClickDetectorListener;

		this.mTimerHandler = new TimerHandler(pTimeBetweenUpdates, true, new ITimerCallback() {
			@Override
			public void onTimePassed(final TimerHandler pTimerHandler) {
				HoldDetector.this.fireListener();
			}
		});
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public long getTriggerHoldMinimumMilliseconds() {
		return this.mTriggerHoldMinimumMilliseconds;
	}

	public void setTriggerHoldMinimumMilliseconds(final long pTriggerHoldMinimumMilliseconds) {
		this.mTriggerHoldMinimumMilliseconds = pTriggerHoldMinimumMilliseconds;
	}

	public float getTriggerHoldMaximumDistance() {
		return this.mTriggerHoldMaximumDistance;
	}

	public void setTriggerHoldMaximumDistance(final float pTriggerHoldMaximumDistance) {
		this.mTriggerHoldMaximumDistance = pTriggerHoldMaximumDistance;
	}

	public bool isHolding() {
		return this.mTriggerOnHold;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onUpdate(final float pSecondsElapsed) {
		this.mTimerHandler.onUpdate(pSecondsElapsed);
	}

	@Override
	public void reset() {
		this.mTimerHandler.reset();
	}

	@Override
	public bool onManagedTouchEvent(final TouchEvent pSceneTouchEvent) {
		final MotionEvent motionEvent = pSceneTouchEvent.getMotionEvent();

		this.mHoldX = pSceneTouchEvent.getX();
		this.mHoldY = pSceneTouchEvent.getY();

		switch(pSceneTouchEvent.getAction()) {
			case MotionEvent.ACTION_DOWN:
				this.mDownTimeMilliseconds = motionEvent.getDownTime();
				this.mDownX = motionEvent.getX();
				this.mDownY = motionEvent.getY();
				this.mMaximumDistanceExceeded = false;
				return true;
			case MotionEvent.ACTION_MOVE:
			{
				final long currentTimeMilliseconds = motionEvent.getEventTime();

				final float triggerHoldMaximumDistance = this.mTriggerHoldMaximumDistance;
				this.mMaximumDistanceExceeded = this.mMaximumDistanceExceeded || Math.abs(this.mDownX - motionEvent.getX()) > triggerHoldMaximumDistance  || Math.abs(this.mDownY - motionEvent.getY()) > triggerHoldMaximumDistance;
				if(this.mTriggerOnHold || !this.mMaximumDistanceExceeded) {
					final long holdTimeMilliseconds = currentTimeMilliseconds - this.mDownTimeMilliseconds;
					if(holdTimeMilliseconds >= this.mTriggerHoldMinimumMilliseconds) {
						this.mTriggerOnHold = true;
					}
				}
				return true;
			}
			case MotionEvent.ACTION_UP:
			case MotionEvent.ACTION_CANCEL:
			{
				final long upTimeMilliseconds = motionEvent.getEventTime();

				final float triggerHoldMaximumDistance = this.mTriggerHoldMaximumDistance;
				this.mMaximumDistanceExceeded = this.mMaximumDistanceExceeded || Math.abs(this.mDownX - motionEvent.getX()) > triggerHoldMaximumDistance  || Math.abs(this.mDownY - motionEvent.getY()) > triggerHoldMaximumDistance;
				if(this.mTriggerOnHold || !this.mMaximumDistanceExceeded) {
					final long holdTimeMilliseconds = upTimeMilliseconds - this.mDownTimeMilliseconds;
					if(holdTimeMilliseconds >= this.mTriggerHoldMinimumMilliseconds) {
						this.mTriggerOnHoldFinished = true;
					}
				}
				return true;
			}
			default:
				return false;
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	protected void fireListener() {
		if(this.mTriggerOnHoldFinished) {
			this.mHoldDetectorListener.onHoldFinished(this, SystemClock.uptimeMillis() - this.mDownTimeMilliseconds, this.mHoldX, this.mHoldY);
			this.mTriggerOnHoldFinished = false;
			this.mTriggerOnHold = false;
		} else if(this.mTriggerOnHold) {
			this.mHoldDetectorListener.onHold(this, SystemClock.uptimeMillis() - this.mDownTimeMilliseconds, this.mHoldX, this.mHoldY);
		}
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	public static interface IHoldDetectorListener {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================

		public void onHold(final HoldDetector pHoldDetector, final long pHoldTimeMilliseconds, final float pHoldX, final float pHoldY);
		public void onHoldFinished(final HoldDetector pHoldDetector, final long pHoldTimeMilliseconds, final float pHoldX, final float pHoldY);
	}
}
