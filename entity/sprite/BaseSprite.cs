namespace andengine.entity.sprite
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using BaseRectangle = andengine.entity.primitive.BaseRectangle;
    using BaseTextureRegion = andengine.opengl.texture.region.BaseTextureRegion;
    using GLHelper = andengine.opengl.util.GLHelper;
    using RectangleVertexBuffer = andengine.opengl.vertex.RectangleVertexBuffer;

    /**
     * @author Nicolas Gramlich
     * @since 11:38:53 - 08.03.2010
     */
    public abstract class BaseSprite : BaseRectangle
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected readonly BaseTextureRegion mTextureRegion;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseSprite(float pX, float pY, float pWidth, float pHeight, BaseTextureRegion pTextureRegion)
            : base(pX, pY, pWidth, pHeight)
        {
            this.mTextureRegion = pTextureRegion;
            this.initBlendFunction();
        }

        public BaseSprite(float pX, float pY, float pWidth, float pHeight, BaseTextureRegion pTextureRegion, RectangleVertexBuffer pRectangleVertexBuffer)
            : base(pX, pY, pWidth, pHeight, pRectangleVertexBuffer)
        {

            this.mTextureRegion = pTextureRegion;
            this.initBlendFunction();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public BaseTextureRegion getTextureRegion()
        {
            return this.mTextureRegion;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void reset()
        {
            base.reset();

            this.initBlendFunction();
        }

        protected override void onInitDraw(GL10 pGL)
        {
            base.onInitDraw(pGL);
            GLHelper.enableTextures(pGL);
            GLHelper.enableTexCoordArray(pGL);
        }

        protected override void onApplyTransformations(GL10 pGL)
        {
            base.onApplyTransformations(pGL);

            this.mTextureRegion.onApply(pGL);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        private void initBlendFunction()
        {
            if (this.mTextureRegion.getTexture().getTextureOptions().mPreMultipyAlpha)
            {
                this.setBlendFunction(BLENDFUNCTION_SOURCE_PREMULTIPLYALPHA_DEFAULT, BLENDFUNCTION_DESTINATION_PREMULTIPLYALPHA_DEFAULT);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
