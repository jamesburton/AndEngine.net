namespace andengine.util.modifier.ease
{

    /**
     * @author Gil, Nicolas Gramlich
     * @since 17:13:17 - 26.07.2010
     */
    public /* interface */ class IEaseFunction
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        public static /* final */ readonly IEaseFunction DEFAULT = EaseLinear.getInstance();

        // ===========================================================
        // Methods
        // ===========================================================

        public abstract float getPercentageDone(float pSecondsElapsed, float pDuration, float pMinValue, float pMaxValue);
    }
}
