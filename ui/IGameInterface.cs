namespace andengine.ui
{

    using Engine = andengine.engine.Engine;
    using Scene = andengine.entity.scene.Scene;

    /**
     * @author Nicolas Gramlich
     * @since 12:03:08 - 14.03.2010
     */
    public interface IGameInterface
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        Engine OnLoadEngine();
        void OnLoadResources();
        void OnUnloadResources();
        Scene OnLoadScene();
        void OnLoadComplete();

        void OnGamePaused();
        void OnGameResumed();
    }
}
