using System;
using andengine.engine.camera;
using andengine.entity.primitive;
using Android.Content.PM;

namespace andengine.examples
{

    using Engine = andengine.engine.Engine;
    //import org.anddev.andengine.engine.camera.Camera;
    using EngineOptions = andengine.engine.options.EngineOptions;
    //using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
    //import org.anddev.andengine.entity.primitive.Line;
    using Scene = andengine.entity.scene.Scene;
    using ColorBackground = andengine.entity.scene.background.ColorBackground;
    using FPSLogger = andengine.entity.util.FPSLogger;

    /**
     * @author Nicolas Gramlich
     * @since 11:54:51 - 03.04.2010
     */
    public class LineExample : BaseExample
    {
        // ===========================================================
        // Constants
        // ===========================================================

        /* Initializing the Random generator produces a comparable result over different versions. */
        private static int RANDOM_SEED = 1234567890;

        private static int CAMERA_WIDTH = 720;
        private static int CAMERA_HEIGHT = 480;

        private static int LINE_COUNT = 100;

        // ===========================================================
        // Fields
        // ===========================================================

        private Camera mCamera;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================


        public override Engine OnLoadEngine()
        {
            this.mCamera = new Camera(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);
            return new Engine(new EngineOptions(true, EngineOptions.ScreenOrientationOptions.LANDSCAPE, new RatioResolutionPolicy(CAMERA_WIDTH, CAMERA_HEIGHT), this.mCamera));
        }


        public override void OnLoadResources()
        {

        }


        public override Scene OnLoadScene()
        {
            this.mEngine.RegisterUpdateHandler(new FPSLogger());

            Scene scene = new Scene(1);
            scene.Background = new ColorBackground(0.09804f, 0.6274f, 0.8784f);

            Random random = new Random(RANDOM_SEED);

            for (int i = 0; i < LINE_COUNT; i++)
            {
                float x1 = (float)(random.NextDouble() * CAMERA_WIDTH);
                float x2 = (float)(random.NextDouble() * CAMERA_WIDTH);
                float y1 = (float)(random.NextDouble() * CAMERA_HEIGHT);
                float y2 = (float)(random.NextDouble() * CAMERA_HEIGHT);
                float lineWidth = (float)(random.NextDouble() * 5);

                Line line = new Line(x1, y1, x2, y2, lineWidth);

                line.SetColor((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());

                scene.getLastChild().attachChild(line);
            }

            return scene;
        }


        public override void OnLoadComplete()
        {

        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }

}