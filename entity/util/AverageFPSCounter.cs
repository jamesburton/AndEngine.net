using andengine.util.constants;

namespace andengine.entity.util
{


/**
 * @author Nicolas Gramlich
 * @since 19:52:31 - 09.03.2010
 */
public abstract class AverageFPSCounter : FPSCounter {
	// ===========================================================
	// Constants
	// ===========================================================

	private static float AVERAGE_DURATION_DEFAULT = 5;

	// ===========================================================
	// Fields
	// ===========================================================

	protected float mAverageDuration;

	// ===========================================================
	// Constructors
	// ===========================================================

	public AverageFPSCounter() :
		this(AVERAGE_DURATION_DEFAULT)
    {
	}

	public AverageFPSCounter(float pAverageDuration) {
		this.mAverageDuration = pAverageDuration;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	protected abstract void OnHandleAverageDurationElapsed(float pFPS);

	public override void OnUpdate(float pSecondsElapsed) {
		base.OnUpdate(pSecondsElapsed);

		if(this.mSecondsElapsed > this.mAverageDuration){
			this.OnHandleAverageDurationElapsed(this.getFPS());

			this.mSecondsElapsed -= this.mAverageDuration;
			this.mFrames = 0;
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}