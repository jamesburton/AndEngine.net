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

        public static /* sealed */ float RadToDeg(/* final */ float pRad)
        {
            return MathConstants.RAD_TO_DEG * pRad;
        }

        public static /* sealed */ float DegToRad(float pDegree)
        {
            return MathConstants.DEG_TO_RAD * pDegree;
        }

        public static /* sealed */ int RandomSign()
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

        public static /* sealed */ float Random(float pMin, float pMax)
        {
            return pMin + ((float)RANDOM.NextDouble()) * (pMax - pMin);
        }

        /** 
         * @param pMin inclusive!
         * @param pMax inclusive!
         * @return
         */
        public static /* sealed */ int Random(int pMin, int pMax)
        {
            return pMin + RANDOM.Next(pMax - pMin + 1);
        }

        public static /* sealed */ bool IsPowerOfTwo(int n)
        {
            return ((n != 0) && (n & (n - 1)) == 0);
        }

        public static /* sealed */ int NextPowerOfTwo(int n)
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

        public static /* sealed */ int Sum(int[] pValues)
        {
            int sum = 0;
            for (int i = pValues.Length - 1; i >= 0; i--)
            {
                sum += pValues[i];
            }

            return sum;
        }

        public static /* sealed */ void ArraySumInternal(int[] pValues)
        {
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pValues[i] = pValues[i - 1] + pValues[i];
            }
        }

        public static /* sealed */ void ArraySumInternal(long[] pValues)
        {
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pValues[i] = pValues[i - 1] + pValues[i];
            }
        }

        public static /* sealed */ void ArraySumInternal(long[] pValues, long pFactor)
        {
            pValues[0] = pValues[0] * pFactor;
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pValues[i] = pValues[i - 1] + pValues[i] * pFactor;
            }
        }

        public static /* sealed */ void ArraySumInto(long[] pValues, long[] pTargetValues, long pFactor)
        {
            pTargetValues[0] = pValues[0] * pFactor;
            int valueCount = pValues.Length;
            for (int i = 1; i < valueCount; i++)
            {
                pTargetValues[i] = pTargetValues[i - 1] + pValues[i] * pFactor;
            }
        }

        public static /* sealed */ float ArraySum(float[] pValues)
        {
            float sum = 0;
            int valueCount = pValues.Length;
            for (int i = 0; i < valueCount; i++)
            {
                sum += pValues[i];
            }
            return sum;
        }

        public static /* sealed */ float ArrayAverage(float[] pValues)
        {
            return MathUtils.ArraySum(pValues) / pValues.Length;
        }

        public static float[] RotateAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY)
        {
            if (pRotation != 0)
            {
                float rotationRad = MathUtils.DegToRad(pRotation);
                float sinRotationRad = FloatMath.Sin(rotationRad);
                float cosRotationInRad = FloatMath.Cos(rotationRad);

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

        public static float[] ScaleAroundCenter(float[] pVertices, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            if (pScaleX != 1 || pScaleY != 1)
            {
                for (int i = pVertices.Length - 2; i >= 0; i -= 2)
                {
                    pVertices[i] = pScaleCenterX + (pVertices[i] - pScaleCenterX) * pScaleX;
                    pVertices[i + 1] = pScaleCenterY + (pVertices[i + 1] - pScaleCenterY) * pScaleY;
                }
            }

            return pVertices;
        }

        public static float[] RotateAndScaleAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            MathUtils.RotateAroundCenter(pVertices, pRotation, pRotationCenterX, pRotationCenterY);
            return MathUtils.ScaleAroundCenter(pVertices, pScaleX, pScaleY, pScaleCenterX, pScaleCenterY);
        }

        public static float[] RevertScaleAroundCenter(float[] pVertices, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            return MathUtils.ScaleAroundCenter(pVertices, 1 / pScaleX, 1 / pScaleY, pScaleCenterX, pScaleCenterY);
        }

        public static float[] RevertRotateAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY)
        {
            return MathUtils.RotateAroundCenter(pVertices, -pRotation, pRotationCenterX, pRotationCenterY);
        }

        public static float[] RevertRotateAndScaleAroundCenter(float[] pVertices, float pRotation, float pRotationCenterX, float pRotationCenterY, float pScaleX, float pScaleY, float pScaleCenterX, float pScaleCenterY)
        {
            MathUtils.RevertScaleAroundCenter(pVertices, pScaleX, pScaleY, pScaleCenterX, pScaleCenterY);
            return MathUtils.RevertRotateAroundCenter(pVertices, pRotation, pRotationCenterX, pRotationCenterY);
        }

        public static int BringToBounds(int pMinValue, int pMaxValue, int pValue)
        {
            return Math.Max(pMinValue, Math.Min(pMaxValue, pValue));
        }

        public static float BringToBounds(float pMinValue, float pMaxValue, float pValue)
        {
            return Math.Max(pMinValue, Math.Min(pMaxValue, pValue));
        }

        public static float Distance(float pX1, float pY1, float pX2, float pY2)
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