namespace andengine.util
{

    //import java.util.Random;
    using Random = System.Random;

    //using andengine.util.constants.MathConstants;
    using MathConstants = andengine.util.constants.MathConstants;

    //import android.util.FloatMath;
    using FloatMath = Android.Util.FloatMath;
    using System;

    /**
     * @author Nicolas Gramlich
     * @since 20:42:15 - 17.12.2009
     */
    public class MathUtils //: MathConstants
    {
        // ===========================================================
        // Constants
        // ===========================================================

        //public static Random RANDOM = new Random(System.nanoTime());
        public static Random RANDOM = new Random((int)DateTime.Now.Ticks);

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

        // ===========================================================
        // Methods
        // ===========================================================

        public static /* sealed */ float radToDeg(/* final */ float pRad)
        {
            return MathConstants.RAD_TO_DEG * pRad;
        }

        public static /* sealed */ float degToRad(float pDegree)
        {
            return MathConstants.DEG_TO_RAD * pDegree;
        }

        public static /* sealed */ int randomSign()
        {
            //if(RANDOM.nextBoolean()) {
            if (RANDOM.Next(0, 2) == 1)
            {
                return 1;
            }
            else
            {
                return -1;
            }
        }

        public static /* sealed */ float random(float pMin, float pMax)
        {
            return pMin + ((float)RANDOM.NextDouble()) * (pMax - pMin);
        }

        /** 
         * @param pMin inclusive!
         * @param pMax inclusive!
         * @return
         */
        public static /* sealed */ int random(int pMin, int pMax)
        {
            return pMin + RANDOM.Next(pMax - pMin + 1);
        }

        public static /* sealed */ bool isPowerOfTwo(int n)
        {
            return ((n != 0) && (n & (n - 1)) == 0);
        }

        public static /* sealed */ int nextPowerOfTwo(int n)
        {
            int k = n;

            if (k == 0)
            {
                return 1;
            }

            k--;

            for (int i = 1; i < 32; i <<= 1)
            {
                k = k | k >> i;
            }

            return k + 1;
        }

        public static /* sealed */ int sum(int[] pValues)
        {
            int sum = 0;
            for (int i = pValues.Length - 1; i >= 0; i--)
            {
                sum += pValues[i];
            }

            return sum;
        }

        public static /* sealed */ void arraySumInternal(int[] pValues)
        {
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pValues[i] = pValues[i - 1] + pValues[i];
            }
        }

        public static /* sealed */ void arraySumInternal(long[] pValues)
        {
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pValues[i] = pValues[i - 1] + pValues[i];
            }
        }

        public static /* sealed */ void arraySumInternal(long[] pValues, long pFactor)
        {
            pValues[0] = pValues[0] * pFactor;
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pValues[i] = pValues[i - 1] + pValues[i] * pFactor;
            }
        }

        public static /* sealed */ void arraySumInto(long[] pValues, long[] pTargetValues, long pFactor)
        {
            pTargetValues[0] = pValues[0] * pFactor;
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pTargetValues[i] = pTargetValues[i - 1] + pValues[i] * pFactor;
            }
        }

        public static /* sealed */ float arraySum(float[] pValues)
        {
            float sum = 0;
            int valueCount = pValues.Length;
            for (int i = 0; i < valueCount; i++)
            {
                sum += pValues[i];
            }
            return sum;
        }

        public static /* sealed */ float arrayAverage(float[] pValues)
        {
            return MathUtils.arraySum(pValues) / pValues.Length;
        }

        public static float[] rotateAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY)
        {
            if (pRotation != 0)
            {
                float rotationRad = MathUtils.degToRad(pRotation);
                float sinRotationRad = FloatMath.sin(rotationRad);
                float cosRotationInRad = FloatMath.cos(rotationRad);

                for (int i = pVertices.Length - 2; i >= 0; i -= 2)
                {
                    float pX = pVertices[i];
                    float pY = pVertices[i + 1];
                    pVertices[i] = pRotationCenterX + (cosRotationInRad * (pX - pRotationCenterX) - sinRotationRad * (pY - pRotationCenterY));
                    pVertices[i + 1] = pRotationCenterY + (sinRotationRad * (pX - pRotationCenterX) + cosRotationInRad * (pY - pRotationCenterY));
                }
            }
            return pVertices;
        }

        public static float[] scaleAroundCenter(float[] pVertices, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            if (pScaleX != 1 || pScaleY != 1)
            {
                for (int i = pVertices.length - 2; i >= 0; i -= 2)
                {
                    pVertices[i] = pScaleCenterX + (pVertices[i] - pScaleCenterX) * pScaleX;
                    pVertices[i + 1] = pScaleCenterY + (pVertices[i + 1] - pScaleCenterY) * pScaleY;
                }
            }

            return pVertices;
        }

        public static float[] rotateAndScaleAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            MathUtils.rotateAroundCenter(pVertices, pRotation, pRotationCenterX, pRotationCenterY);
            return MathUtils.scaleAroundCenter(pVertices, pScaleX, pScaleY, pScaleCenterX, pScaleCenterY);
        }

        public static float[] revertScaleAroundCenter(float[] pVertices, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            return MathUtils.scaleAroundCenter(pVertices, 1 / pScaleX, 1 / pScaleY, pScaleCenterX, pScaleCenterY);
        }

        public static float[] revertRotateAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY)
        {
            return MathUtils.rotateAroundCenter(pVertices, -pRotation, pRotationCenterX, pRotationCenterY);
        }

        public static float[] revertRotateAndScaleAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            MathUtils.revertScaleAroundCenter(pVertices, pScaleX, pScaleY, pScaleCenterX, pScaleCenterY);
            return MathUtils.revertRotateAroundCenter(pVertices, pRotation, pRotationCenterX, pRotationCenterY);
        }

        public static int bringToBounds(int pMinValue, int pMaxValue, int pValue)
        {
            return Math.Max(pMinValue, Math.Min(pMaxValue, pValue));
        }

        public static float bringToBounds(float pMinValue, float pMaxValue, float pValue)
        {
            return Math.Max(pMinValue, Math.Min(pMaxValue, pValue));
        }

        public static float distance(float pX1, float pY1, float pX2, float pY2)
        {
            float dX = pX2 - pX1;
            float dY = pY2 - pY1;
            return FloatMath.Sqrt((dX * dX) + (dY * dY));
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}