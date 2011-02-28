namespace andengine.sensor.location
{

    using Location = Android.Locations.Location;
    using LocationListener = Android.Locations.LocationListener;
    using Bundle = Android.OS.Bundle;

    /**
     * @author Nicolas Gramlich
     * @since 10:39:23 - 31.10.2010
     */
    public interface ILocationListener
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /**
         * @see {@link LocationListener#onProviderEnabled(String)}
         */
        void onLocationProviderEnabled();

        /**
         * @see {@link LocationListener#onLocationChanged(Location)}
         */
        void onLocationChanged(Location pLocation);

        void onLocationLost();

        /**
         * @see {@link LocationListener#onProviderDisabled(String)}
         */
        void onLocationProviderDisabled();

        /**
         * @see {@link LocationListener#onStatusChanged(String, int, android.os.Bundle)}
         */
        void onLocationProviderStatusChanged(LocationProviderStatus pLocationProviderStatus, Bundle pBundle);
    }
}
