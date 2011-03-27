using andengine.util;
using andengine.util.constants;

namespace andengine.entity.util
{

    using System;
/**
 * @author Nicolas Gramlich
 * @since 19:52:31 - 09.03.2010
 */
public class FPSLogger : AverageFPSCounter {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected float mShortestFrame = float.MaxValue;
	protected float mLongestFrame = float.MinValue;

	// ===========================================================
	// Constructors
	// ===========================================================

	public FPSLogger() : base()
    {
	}

	public FPSLogger(float pAverageDuration) : base(pAverageDuration)
    {
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	protected override void OnHandleAverageDurationElapsed(float pFPS) {
		this.onLogFPS();
		
        this.mLongestFrame = float.MinValue;
		this.mShortestFrame = float.MaxValue;
	}

	public override void OnUpdate(float pSecondsElapsed) {
		base.OnUpdate(pSecondsElapsed);

		this.mShortestFrame = Math.Min(this.mShortestFrame, pSecondsElapsed);
		this.mLongestFrame = Math.Max(this.mLongestFrame, pSecondsElapsed);
	}

	public override void Reset() {
		base.Reset();

		this.mShortestFrame = float.MaxValue;
		this.mLongestFrame = float.MinValue;
	}

	// ===========================================================
	// Methods
	// ===========================================================

	protected void onLogFPS() {
		Debug.D(String.Format("FPS: {0:f2} (MIN: {1:f0} ms | MAX: {2:f0} ms)",
				this.mFrames / this.mSecondsElapsed,
				this.mShortestFrame * TimeConstants.MILLISECONDSPERSECOND,
                this.mLongestFrame * TimeConstants.MILLISECONDSPERSECOND));
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}