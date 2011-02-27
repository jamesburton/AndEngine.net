namespace andengine.sensor
{

    //import java.util.Arrays;
    using System.Collections;
    using System;

    /**
     * @author Nicolas Gramlich
     * @since 16:50:44 - 10.03.2010
     */
    public class BaseSensorData
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected /* final */ readonly float[] mValues;
        protected int mAccuracy;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseSensorData(/* final */ int pValueCount)
        {
            this.mValues = new float[pValueCount];
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public float[] getValues()
        {
            return this.mValues;
        }

        public void setValues(/* final */ float[] pValues)
        {
            /* final */
            float[] values = this.mValues;
            for (int i = pValues.Length - 1; i >= 0; i--)
            {
                values[i] = pValues[i];
            }
        }

        public void setAccuracy(/* final */ int pAccuracy)
        {
            this.mAccuracy = pAccuracy;
        }

        public int getAccuracy()
        {
            return this.mAccuracy;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override String ToString()
        {
            //return "Values: " + Arrays.toString(this.mValues);
            return "Values: " + this.mValues.ToString();
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}