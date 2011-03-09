namespace andengine.entity.scene.background
{

    //import static org.anddev.andengine.util.constants.ColorConstants.COLOR_FACTOR_INT_TO_FLOAT;
    using ColorConstants = andengine.util.constants.ColorConstants;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;

    using Camera = andengine.engine.camera.Camera;

    /**
     * @author Nicolas Gramlich
     * @since 13:45:24 - 19.07.2010
     */
    public class ColorBackground : BaseBackground
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private float mRed = 0.0f;
        private float mGreen = 0.0f;
        private float mBlue = 0.0f;
        private float mAlpha = 1.0f;

        private bool mColorEnabled = true;

        // ===========================================================
        // Constructors
        // ===========================================================

        protected ColorBackground()
        {

        }

        public ColorBackground(float pRed, float pGreen, float pBlue)
        {
            this.mRed = pRed;
            this.mGreen = pGreen;
            this.mBlue = pBlue;
        }

        public ColorBackground(float pRed, float pGreen, float pBlue, float pAlpha)
        {
            this.mRed = pRed;
            this.mGreen = pGreen;
            this.mBlue = pBlue;
            this.mAlpha = pAlpha;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        /**
         * Sets the color using the arithmetic scheme (0.0f - 1.0f RGB triple).
         * @param pRed The red color value. Should be between 0.0 and 1.0, inclusive.
         * @param pGreen The green color value. Should be between 0.0 and 1.0, inclusive.
         * @param pBlue The blue color value. Should be between 0.0 and 1.0, inclusive.
         */
        public override void SetColor(float pRed, float pGreen, float pBlue)
        {
            this.mRed = pRed;
            this.mGreen = pGreen;
            this.mBlue = pBlue;
        }

        /**
         * Sets the color using the arithmetic scheme (0.0f - 1.0f RGB quadruple).
         * @param pRed The red color value. Should be between 0.0 and 1.0, inclusive.
         * @param pGreen The green color value. Should be between 0.0 and 1.0, inclusive.
         * @param pBlue The blue color value. Should be between 0.0 and 1.0, inclusive.
         * @param pAlpha The alpha color value. Should be between 0.0 and 1.0, inclusive.
         */
        public override void SetColor(float pRed, float pGreen, float pBlue, float pAlpha)
        {
            this.SetColor(pRed, pGreen, pBlue);
            this.mAlpha = pAlpha;
        }

        /**
         * Sets the color using the digital 8-bit per channel scheme (0 - 255 RGB triple).
         * @param pRed The red color value. Should be between 0 and 255, inclusive.
         * @param pGreen The green color value. Should be between 0 and 255, inclusive.
         * @param pBlue The blue color value. Should be between 0 and 255, inclusive.
         */
        public void SetColor(int pRed, int pGreen, int pBlue) /* throws IllegalArgumentException */ {
            this.SetColor(pRed / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT, pGreen / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT, pBlue / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT);
        }

        /**
         * Sets the color using the digital 8-bit per channel scheme (0 - 255 RGB quadruple).
         * @param pRed The red color value. Should be between 0 and 255, inclusive.
         * @param pGreen The green color value. Should be between 0 and 255, inclusive.
         * @param pBlue The blue color value. Should be between 0 and 255, inclusive.
         */
        public void SetColor(int pRed, int pGreen, int pBlue, int pAlpha) /* throws IllegalArgumentException */ {
            this.SetColor(pRed / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT, pGreen / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT, pBlue / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT, pAlpha / ColorConstants.COLOR_FACTOR_INT_TO_FLOAT);
        }

        public void SetColorEnabled(bool pColorEnabled)
        {
            this.mColorEnabled = pColorEnabled;
        }

        public bool IsColorEnabled()
        {
            return this.mColorEnabled;
        }

        public bool ColorEnabled { get { return IsColorEnabled(); } set { SetColorEnabled(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void OnDraw(GL10 pGL, Camera pCamera)
        {
            if (this.mColorEnabled)
            {
                pGL.GlClearColor(this.mRed, this.mGreen, this.mBlue, this.mAlpha);
                pGL.GlClear(Javax.Microedition.Khronos.Opengles.GL10Consts.GlColorBufferBit);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}