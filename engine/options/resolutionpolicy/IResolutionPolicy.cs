namespace andengine.engine.options.resolutionpolicy
{

    using RenderSurfaceView = andengine.opengl.view.RenderSurfaceView;

    /**
     * @author Nicolas Gramlich
     * @since 11:02:35 - 29.03.2010
     */
    public interface IResolutionPolicy
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        void onMeasure(RenderSurfaceView pRenderSurfaceView, int pWidthMeasureSpec, int pHeightMeasureSpec);
    }
}
