namespace andengine.opengl
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    //using andengine.engine.camera.Camera;
    using Camera = andengine.engine.camera.Camera;

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

        /* public */ void OnDraw(/* final */ GL10 pGL, /* final */ Camera pCamera);
    }
}