namespace andengine.sensor.orientation
{

    //using java.util.Arrays;
    using System.Collections.Generic;

    using BaseSensorData = andengine.sensor.BaseSensorData;

    using SensorManager = Android.Hardware.SensorManager;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 11:30:33 - 25.05.2010
     */
    public class OrientationData : BaseSensorData
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        public OrientationData()
            : base(3)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float getRoll()
        {
            //return base.mValues[SensorManager.DATA_Z];
            return base.mValues[2];
        }

        public float getPitch()
        {
            //return base.mValues[SensorManager.DATA_Y];
            return base.mValues[1];
        }

        public float getYaw()
        {
            //return base.mValues[SensorManager.DATA_X];
            return base.mValues[0];
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override String toString()
        {
            //return "Orientation: " + Arrays.toString(this.mValues);
            return new Java.Lang.String(System.String.Format("Orientation: Roll {0}, Pitch {1}, Yaw {2}", getRoll(), getPitch(), getYaw()));
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
