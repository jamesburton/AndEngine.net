namespace andengine.entity
{

    using andengine.engine.handler/*.IUpdateHandler*/;
    using andengine.opengl/*.IDrawable*/;


    /**
     * @author Nicolas Gramlich
     * @since 11:20:25 - 08.03.2010
     */
    public interface IEntity : IDrawable, IUpdateHandler
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ int GetZIndex();
        /* public */ void SetZIndex(/* final */ int pZIndex);
    }
}
