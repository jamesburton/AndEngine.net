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

        void addBackgroundModifier(andengine.util.modifier.IModifier<IBackground> pBackgroundModifier);
        bool removeBackgroundModifier(andengine.util.modifier.IModifier<IBackground> pBackgroundModifier);
        void clearBackgroundModifiers();

        void setColor(float pRed, float pGreen, float pBlue);
        void setColor(float pRed, float pGreen, float pBlue, float pAlpha);
    }
}