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
            this.InitBlendFunction();
        }

        public BaseSprite(float pX, float pY, float pWidth, float pHeight, BaseTextureRegion pTextureRegion, RectangleVertexBuffer pRectangleVertexBuffer)
            : base(pX, pY, pWidth, pHeight, pRectangleVertexBuffer)
        {

            this.mTextureRegion = pTextureRegion;
            this.InitBlendFunction();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public BaseTextureRegion GetTextureRegion()
        {
            return this.mTextureRegion;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void Reset()
        {
            base.Reset();

            this.InitBlendFunction();
        }

        protected override void OnInitDraw(GL10 pGL)
        {
            base.OnInitDraw(pGL);
            GLHelper.EnableTextures(pGL);
            GLHelper.EnableTexCoordArray(pGL);
        }

        protected override void OnApplyTransformations(GL10 pGL)
        {
            base.OnApplyTransformations(pGL);

            this.mTextureRegion.OnApply(pGL);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        private void InitBlendFunction()
        {
            if (this.mTextureRegion.GetTexture().GetTextureOptions().mPreMultipyAlpha)
            {
                this.SetBlendFunction(BLENDFUNCTION_SOURCE_PREMULTIPLYALPHA_DEFAULT, BLENDFUNCTION_DESTINATION_PREMULTIPLYALPHA_DEFAULT);
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}
