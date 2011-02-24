namespace andengine.engine.options
{

    //import android.os.PowerManager;
    using PowerManager = Android.OS.PowerManager;

    /**
     * @author Nicolas Gramlich
     * @since 19:45:23 - 10.07.2010
     */
    /*
public enum WakeLockOptions {
	// ===========================================================
	// Elements
	// ===========================================================

	FULL(PowerManager.FULL_WAKE_LOCK),
	SCREEN_BRIGHT(PowerManager.SCREEN_BRIGHT_WAKE_LOCK),
	SCREEN_DIM(PowerManager.SCREEN_DIM_WAKE_LOCK);

	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final int mFlag;

	// ===========================================================
	// Constructors
	// ===========================================================

	private WakeLockOptions(final int pFlag) {
		this.mFlag = pFlag;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public int getFlag() {
		return this.mFlag;
	}

	// ===========================================================
	// Methods from SuperClass/Interfaces
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
    */

    /*
    public class WakeLockOptions
    {
        public int const FULL = new WakeLockOptions(PowerManager.WakeLock.FULL_WAKE_LOCK);
    }
     */
    // TODO: Verify if the WakeLockOptions enum is a good conversion
    public enum WakeLockOptions
    {
        FULL = 0,
        SCREEN_BRIGHT = 1,
        SCREEN_DIM = 2
    }
}