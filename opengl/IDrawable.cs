namespace andengine.opengl
{

    //import javax.microedition.khronos.opengles.GL10;
    using OpenTK.Graphics.ES11;
    // TODO: Check the above conversion

    //using andengine.engine.camera.Camera;
    using andengine.engine.camera;

    /**
     * @author Nicolas Gramlich
     * @since 10:50:58 - 08.08.2010
     */
    public interface IDrawable
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ void onDraw(/* final */ GL10 pGL, /* final */ Camera pCamera);
    }
}