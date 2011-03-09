namespace andengine.entity.scene.background
{

    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    using IDrawable = andengine.opengl.IDrawable;
    //using IModifier = andengine.util.modifier.IModifier;

    /**
     * @author Nicolas Gramlich
     * @since 13:47:41 - 19.07.2010
     */
    public interface IBackground : IDrawable, IUpdateHandler
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        void AddBackgroundModifier(andengine.util.modifier.IModifier<IBackground> pBackgroundModifier);
        bool RemoveBackgroundModifier(andengine.util.modifier.IModifier<IBackground> pBackgroundModifier);
        void ClearBackgroundModifiers();

        void SetColor(float pRed, float pGreen, float pBlue);
        void SetColor(float pRed, float pGreen, float pBlue, float pAlpha);
    }
}