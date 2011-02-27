namespace andengine.entity.scene
{

    using Camera = andengine.engine.camera.Camera;
    using ScaleModifier = andengine.entity.shape.modifier.ScaleModifier;
    using Sprite = andengine.entity.sprite.Sprite;
    using TextureRegion = andengine.opengl.texture.region.TextureRegion;
    using IEaseFunction = andengine.util.modifier.ease.IEaseFunction;

    /**
     * @author Nicolas Gramlich
     * @since 09:45:02 - 03.05.2010
     */
    public class SplashScene : Scene
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

        public SplashScene(Camera pCamera, TextureRegion pTextureRegion)
            : base(1)
        {
            Init(pCamera, pTextureRegion, -1, 1, 1);
        }

        public SplashScene(Camera pCamera, TextureRegion pTextureRegion, float pDuration, float pScaleFrom, float pScaleTo)
            : base(1)
        {
            Init(pCamera, pTextureRegion, pDuration, pScaleFrom, pScaleTo);
        }

        protected void Init(Camera pCamera, TextureRegion pTextureRegion, float pDuration, float pScaleFrom, float pScaleTo)
        {
            Sprite loadingScreenSprite = new Sprite(pCamera.getMinX(), pCamera.getMinY(), pCamera.getWidth(), pCamera.getHeight(), pTextureRegion);
            if (pScaleFrom != 1 || pScaleTo != 1)
            {
                loadingScreenSprite.setScale(pScaleFrom);
                loadingScreenSprite.addShapeModifier(new ScaleModifier(pDuration, pScaleFrom, pScaleTo, IEaseFunction.DEFAULT));
            }

            this.getTopLayer().addEntity(loadingScreenSprite);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}