using Android.Util;

namespace andengine.util
{

    /**
     * <p>This class is basically a java-space replacement for the native {@link android.graphics.Matrix} class.</p>
     * 
     * <p>Math taken from <a href="http://www.senocular.com/flash/tutorials/transformmatrix/">senocular.com</a>.</p>
     * 
     * This class represents an affine transformation with the following matrix:
     * <pre> [ a , b , 0 ]
     * [ c , d , 0 ]
     * [ tx, ty, 1 ]</pre>
     * where:
     * <ul>
     *  <li><b>a</b> is the <b>x scale</b></li>
     *  <li><b>b</b> is the <b>y skew</b></li>
     *  <li><b>c</b> is the <b>x skew</b></li>
     *  <li><b>d</b> is the <b>y scale</b></li>
     *  <li><b>tx</b> is the <b>x translation</b></li>
     *  <li><b>ty</b> is the <b>y translation</b></li>
     * </ul>
     *
     * <p>TODO Think if that caching of Transformation through the TransformationPool really needs to be thread-safe or if one simple reused static Transform object is enough.</p>
     * 
     * @author Nicolas Gramlich
     * @since 15:47:18 - 23.12.2010
     */
    public class Transformation
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private float a; /* x scale */
        private float b; /* y skew */
        private float c; /* x skew */
        private float d; /* y scale */
        private float tx; /* x translation */
        private float ty; /* y translation */

        // ===========================================================
        // Constructors
        // ===========================================================

        public Transformation()
        {
            this.a = 1.0f;
            this.d = 1.0f;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override string ToString()
        {
            return "Transformation{[" + this.a + ", " + this.c + ", " + this.tx + "][" + this.b + ", " + this.d + ", " + this.ty + "][0.0, 0.0, 1.0]}";
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void reset()
        {
            this.setToIdentity();
        }

        public void setToIdentity()
        {
            this.a = 1.0f;
            this.d = 1.0f;

            this.b = 0.0f;
            this.c = 0.0f;
            this.tx = 0.0f;
            this.ty = 0.0f;
        }

        public void preTranslate(float pX, float pY)
        {
            Transformation transformation = TransformationPool.obtain();
            this.preConcat(transformation.setToTranslate(pX, pY));
            TransformationPool.recycle(transformation);
        }

        public void postTranslate(float pX, float pY)
        {
            Transformation transformation = TransformationPool.obtain();
            this.postConcat(transformation.setToTranslate(pX, pY));
            TransformationPool.recycle(transformation);
        }

        public Transformation setToTranslate(float pX, float pY)
        {
            this.a = 1;
            this.b = 0;
            this.c = 0;
            this.d = 1;
            this.tx = pX;
            this.ty = pY;

            return this;
        }

        public void preScale(float pScaleX, float pScaleY)
        {
            Transformation transformation = TransformationPool.obtain();
            this.preConcat(transformation.setToScale(pScaleX, pScaleY));
            TransformationPool.recycle(transformation);
        }

        public void postScale(float pScaleX, float pScaleY)
        {
            Transformation transformation = TransformationPool.obtain();
            this.postConcat(transformation.setToScale(pScaleX, pScaleY));
            TransformationPool.recycle(transformation);
        }

        public Transformation setToScale(float pScaleX, float pScaleY)
        {
            this.a = pScaleX;
            this.b = 0;
            this.c = 0;
            this.d = pScaleY;
            this.tx = 0;
            this.ty = 0;

            return this;
        }

        public void preRotate(float pAngle)
        {
            Transformation transformation = TransformationPool.obtain();
            this.preConcat(transformation.setToRotate(pAngle));
            TransformationPool.recycle(transformation);
        }

        public void postRotate(float pAngle)
        {
            Transformation transformation = TransformationPool.obtain();
            this.postConcat(transformation.setToRotate(pAngle));
            TransformationPool.recycle(transformation);
        }

        public Transformation setToRotate(float pAngle)
        {
            float angleRad = MathUtils.DegToRad(pAngle);

            float sin = FloatMath.Sin(angleRad);
            float cos = FloatMath.Cos(angleRad);

            this.a = cos;
            this.b = sin;
            this.c = -sin;
            this.d = cos;
            this.tx = 0;
            this.ty = 0;

            return this;
        }

        public void postConcat(Transformation pTransformation)
        {
            float a1 = this.a;
            float a2 = pTransformation.a;

            float b1 = this.b;
            float b2 = pTransformation.b;

            float c1 = this.c;
            float c2 = pTransformation.c;

            float d1 = this.d;
            float d2 = pTransformation.d;

            float tx1 = this.tx;
            float tx2 = pTransformation.tx;

            float ty1 = this.ty;
            float ty2 = pTransformation.ty;

            this.a = a1 * a2 + b1 * c2;
            this.b = a1 * b2 + b1 * d2;
            this.c = c1 * a2 + d1 * c2;
            this.d = c1 * b2 + d1 * d2;
            this.tx = tx1 * a2 + ty1 * c2 + tx2;
            this.ty = tx1 * b2 + ty1 * d2 + ty2;
        }

        public void preConcat(Transformation pTransformation)
        {
            float a1 = pTransformation.a;
            float a2 = this.a;

            float b1 = pTransformation.b;
            float b2 = this.b;

            float c1 = pTransformation.c;
            float c2 = this.c;

            float d1 = pTransformation.d;
            float d2 = this.d;

            float tx1 = pTransformation.tx;
            float tx2 = this.tx;

            float ty1 = pTransformation.ty;
            float ty2 = this.ty;

            this.a = a1 * a2 + b1 * c2;
            this.b = a1 * b2 + b1 * d2;
            this.c = c1 * a2 + d1 * c2;
            this.d = c1 * b2 + d1 * d2;
            this.tx = tx1 * a2 + ty1 * c2 + tx2;
            this.ty = tx1 * b2 + ty1 * d2 + ty2;
        }

        public void transform(float[] pVertices)
        {
            int count = pVertices.Length / 2;
            int i = 0;
            int j = 0;
            while (--count >= 0)
            {
                float x = pVertices[i++];
                float y = pVertices[i++];
                pVertices[j++] = x * this.a + y * this.c + this.tx;
                pVertices[j++] = x * this.b + y * this.d + this.ty;
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}