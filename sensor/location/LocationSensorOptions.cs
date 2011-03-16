using Android.Locations;

namespace andengine.sensor.location
{

    using TimeConstants = andengine.util.constants.TimeConstants;

    using Criteria = Android.Locations.Criteria;

    using Accuracy = Android.Locations.Accuracy;

    /**
     * @author Nicolas Gramlich
     * @since 11:02:12 - 31.10.2010
     */
    public class LocationSensorOptions : Criteria
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly long MINIMUMTRIGGERTIME_DEFAULT = 1 * TimeConstants.MILLISECONDSPERSECOND;
        private static /* final */ readonly long MINIMUMTRIGGERDISTANCE_DEFAULT = 10;

        // ===========================================================
        // Fields
        // ===========================================================

        private bool mEnabledOnly = true;

        private long mMinimumTriggerTime = MINIMUMTRIGGERTIME_DEFAULT;
        private long mMinimumTriggerDistance = MINIMUMTRIGGERDISTANCE_DEFAULT;

        // ===========================================================
        // Constructors
        // ===========================================================

        /**
         * @see {@link LocationSensorOptions#setAccuracy(int)},
         *      {@link LocationSensorOptions#setAltitudeRequired(boolean)},
         *      {@link LocationSensorOptions#setBearingRequired(boolean)},
         *      {@link LocationSensorOptions#setCostAllowed(boolean)},
         *      {@link LocationSensorOptions#setEnabledOnly(boolean)},
         *      {@link LocationSensorOptions#setMinimumTriggerDistance(long)},
         *      {@link LocationSensorOptions#setMinimumTriggerTime(long)},
         *      {@link LocationSensorOptions#setPowerRequirement(int)},
         *      {@link LocationSensorOptions#setSpeedRequired(boolean)}.
         */
        public LocationSensorOptions()
        {

        }

        /**
         * @param pAccuracy
         * @param pAltitudeRequired
         * @param pBearingRequired
         * @param pCostAllowed
         * @param pPowerRequirement
         * @param pSpeedRequired
         * @param pEnabledOnly
         * @param pMinimumTriggerTime
         * @param pMinimumTriggerDistance
         */
        //public LocationSensorOptions(int pAccuracy, bool pAltitudeRequired, bool pBearingRequired, bool pCostAllowed, int pPowerRequirement, bool pSpeedRequired, bool pEnabledOnly, long pMinimumTriggerTime, long pMinimumTriggerDistance)
        public LocationSensorOptions(Accuracy pAccuracy, bool pAltitudeRequired, bool pBearingRequired, bool pCostAllowed, Power pPowerRequirement, bool pSpeedRequired, bool pEnabledOnly, long pMinimumTriggerTime, long pMinimumTriggerDistance)
        {
            this.mEnabledOnly = pEnabledOnly;
            this.mMinimumTriggerTime = pMinimumTriggerTime;
            this.mMinimumTriggerDistance = pMinimumTriggerDistance;

            //this.SetAccuracy(pAccuracy);
            this.Accuracy = pAccuracy;
            this.AltitudeRequired = pAltitudeRequired;
            this.BearingRequired = pBearingRequired;
            this.CostAllowed = pCostAllowed;
            this.PowerRequirement = pPowerRequirement;
            this.SpeedRequired = pSpeedRequired;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public void setEnabledOnly(bool pEnabledOnly)
        {
            this.mEnabledOnly = pEnabledOnly;
        }

        public bool isEnabledOnly()
        {
            return this.mEnabledOnly;
        }

        public long getMinimumTriggerTime()
        {
            return this.mMinimumTriggerTime;
        }

        public void setMinimumTriggerTime(long pMinimumTriggerTime)
        {
            this.mMinimumTriggerTime = pMinimumTriggerTime;
        }

        public long getMinimumTriggerDistance()
        {
            return this.mMinimumTriggerDistance;
        }

        public void setMinimumTriggerDistance(long pMinimumTriggerDistance)
        {
            this.mMinimumTriggerDistance = pMinimumTriggerDistance;
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