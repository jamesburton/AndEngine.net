namespace andengine.util.constants
{

    /**
     * @author Nicolas Gramlich
     * @since 16:49:25 - 26.07.2010
     */
    //public interface MathConstants
    public static class MathConstants
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        //public static /* final */ sealed float PI = (float)Math.PI;
        public static /* final */ readonly float PI = (float)System.Math.PI;

        public static float _2PI = PI * 2.0f;
        public static float _HALF_PI = PI * 0.5f;

        public static /* final */ sealed float DEG_TO_RAD = PI / 180.0f;
        public static /* final */ sealed float RAD_TO_DEG = 180.0f / PI;

        // ===========================================================
        // Methods
        // ===========================================================
    }
}