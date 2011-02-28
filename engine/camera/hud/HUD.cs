namespace andengine.engine.camera.hud
{

    using Camera = andengine.engine.camera.Camera;
    using CameraScene = andengine.entity.scene.CameraScene;
    using Scene = andengine.entity.scene.Scene;

    /**
     * While you can add a {@link HUD} to {@link Scene}, you should not do so.
     * {@link HUD}s are meant to be added to {@link Camera}s via {@link Camera#setHUD(HUD)}.
     * 
     * @author Nicolas Gramlich
     * @since 14:13:13 - 01.04.2010
     */
    public class HUD : CameraScene
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

        public HUD()
            : this(1)
        {

            this.setBackgroundEnabled(false);
        }

        public HUD(int pLayerCount)
            : base(pLayerCount)
        {

            this.setBackgroundEnabled(false);
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