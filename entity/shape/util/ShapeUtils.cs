namespace andengine.entity.shape.util
{

    using IShape = andengine.entity.shape.IShape;
    using MathUtils = andengine.util.MathUtils;

    using FloatMath = Android.Util.FloatMath;

    /**
     * @author Nicolas Gramlich
     * @since 01:09:43 - 06.10.2010
     */
    public class ShapeUtils
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

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        /**
         * Not tested for now!
         */
        // @Deprecated
        /// <summary>
        /// Deprecated and untested
        /// </summary>
        /// <param name="IShape"></param>
        public void setVelocityRespectingRotation(/* final */ IShape pShape, /* final */ float pVelocityX, /* final */ float pVelocityY)
        {
            /* final */
            float rotation = pShape.getRotation();
            /* final */
            float rotationRad = MathUtils.degToRad(rotation);

            /* final */
            float sin = FloatMath.Sin(rotationRad);
            /* final */
            float cos = FloatMath.Cos(rotationRad);

            /* final */
            float velocityX = sin * -pVelocityY + cos * pVelocityX;
            /* final */
            float velocityY = cos * pVelocityY + sin * pVelocityX;

            pShape.setVelocity(velocityX, velocityY);
        }

        /**
         * Not tested for now!
         */
        //@Deprecated
        /// <summary>
        /// Decprecated and untested
        /// </summary>
        /// <param name="IShape"></param>
        public void accelerateRespectingRotation(/* final */ IShape pShape, /* final */ float pAccelerationX, /* final */ float pAccelerationY)
        {
            /* final */
            float rotation = pShape.getRotation();
            /* final */
            float rotationRad = MathUtils.degToRad(rotation);

            /* final */
            float sin = FloatMath.Sin(rotationRad);
            /* final */
            float cos = FloatMath.Cos(rotationRad);

            /* final */
            float accelerationX = sin * -pAccelerationY + cos * pAccelerationX;
            /* final */
            float accelerationY = cos * pAccelerationY + sin * pAccelerationX;

            pShape.setAcceleration(accelerationX, accelerationY);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}