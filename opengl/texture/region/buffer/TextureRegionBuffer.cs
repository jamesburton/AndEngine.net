namespace andengine.opengl.texture.region.buffer
{

    using TextureRegion = andengine.opengl.texture.region.TextureRegion;

    public class TextureRegionBuffer : BaseTextureRegionBuffer
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

        public TextureRegionBuffer(TextureRegion pTextureRegion, int pDrawType)
            : base(pTextureRegion, pDrawType)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public override TextureRegion GetTextureRegion()
        {
            return (TextureRegion)base.GetTextureRegion();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected /* override */ virtual float GetX1()
        {
            TextureRegion textureRegion = this.GetTextureRegion();
            return (float)textureRegion.GetTexturePositionX() / textureRegion.GetTexture().GetWidth();
        }
        protected float X1 { get { return GetX1(); } }

        protected /* override */ virtual float GetX2()
        {
            TextureRegion textureRegion = this.GetTextureRegion();
            return (float)(textureRegion.GetTexturePositionX() + textureRegion.GetWidth()) / textureRegion.GetTexture().GetWidth();
        }
        protected float X2 { get { return GetX2(); } }

        protected /* override */ virtual float GetY1()
        {
            TextureRegion textureRegion = this.GetTextureRegion();
            return (float)textureRegion.GetTexturePositionY() / textureRegion.GetTexture().GetHeight();
        }
        protected float Y1 { get { return GetY1(); } }

        protected /* override */ virtual float GetY2()
        {
            TextureRegion textureRegion = this.GetTextureRegion();
            return (float)(textureRegion.GetTexturePositionY() + textureRegion.GetHeight()) / textureRegion.GetTexture().GetHeight();
        }
        protected float Y2 { get { return GetY2(); } }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }

}