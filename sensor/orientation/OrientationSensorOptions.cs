namespace andengine.sensor.orientation
{

    using SensorDelay = andengine.sensor.SensorDelay;

    /**
     * @author Nicolas Gramlich
     * @since 11:12:36 - 31.10.2010
     */
    public class OrientationSensorOptions
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        readonly SensorDelay mSensorDelay;

        // ===========================================================
        // Constructors
        // ===========================================================

        public OrientationSensorOptions(SensorDelay pSensorDelay)
        {
            this.mSensorDelay = pSensorDelay;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public SensorDelay getSensorDelay()
        {
            return this.mSensorDelay;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}