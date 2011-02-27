namespace andengine.engine.handler.timer
{

    /**
     * @author Nicolas Gramlich
     * @since 16:23:25 - 12.03.2010
     */
    public interface ITimerCallback
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        void onTimePassed(TimerHandler pTimerHandler);
    }
}