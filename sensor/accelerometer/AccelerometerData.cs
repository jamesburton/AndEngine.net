namespace andengine.sensor.accelerometer
{

    //import java.util.Arrays;
    using System.Collections;

    //using andengine.sensor.BaseSensorData;
    using BaseSensorData = andengine.sensor.BaseSensorData;

    //import android.hardware.SensorManager;
    using SensorManager = Android.Hardware.SensorManager;
    using System;

    /**
     * @author Nicolas Gramlich
     * @since 16:50:44 - 10.03.2010
     */
    public class AccelerometerData : BaseSensorData
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

        public AccelerometerData() : base(3)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float getX()
        {
            //return this.mValues[SensorManager.DATA_X];
            return this.mValues[0];
        }

        public float getY()
        {
            //return this.mValues[SensorManager.DATA_Y];
            return this.mValues[1];
        }

        public float getZ()
        {
            //return this.mValues[SensorManager.DATA_Z];
            return this.mValues[2];
        }

        public void setX(/* final */ float pX)
        {
            //this.mValues[SensorManager.DATA_X] = pX;
            this.mValues[0] = pX;
        }

        public void setY(/* final */ float pY)
        {
            //this.mValues[SensorManager.DATA_Y] = pY;
            this.mValues[1] = pY;
        }

        public void setZ(/* final */ float pZ)
        {
            //this.mValues[SensorManager.DATA_Z]  = pZ;
            this.mValues[2] = pZ;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override String ToString()
        {
            //return "Accelerometer: " + Arrays.toString(this.mValues);
            return "Accelerometer: " + this.mValues.ToString();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}