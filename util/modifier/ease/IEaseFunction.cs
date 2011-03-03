namespace andengine.util.modifier.ease
{

    /**
     * @author Gil, Nicolas Gramlich
     * @since 17:13:17 - 26.07.2010
     */
    public /* interface */ abstract class IEaseFunction
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        public static /* final */ readonly IEaseFunction DEFAULT = EaseLinear.GetInstance();

        // ===========================================================
        // Methods
        // ===========================================================

        public abstract float GetPercentageDone(float pSecondsElapsed, float pDuration, float pMinValue, float pMaxValue);
    }
}
