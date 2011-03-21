using System.Security;
using Android.Content.PM;
using Android.Media;
using Android.OS;
using Android.Views;

namespace andengine.ui.activity
{

    //import static android.view.ViewGroup.LayoutParams.FILL_PARENT;
    //using LayoutParams = Android.Views.ViewGroup.LayoutParams;

    using MusicManager = andengine.audio.music.MusicManager;
    using SoundManager = andengine.audio.sound.SoundManager;
    using Engine = andengine.engine.Engine;
    using EngineOptions = andengine.engine.options.EngineOptions;
    using Scene = andengine.entity.scene.Scene;
    using RenderSurfaceView = andengine.opengl.view.RenderSurfaceView;
    using AccelerometerSensorOptions = andengine.sensor.accelerometer.AccelerometerSensorOptions;
    using IAccelerometerListener = andengine.sensor.accelerometer.IAccelerometerListener;
    using ILocationListener = andengine.sensor.location.ILocationListener;
    using LocationSensorOptions = andengine.sensor.location.LocationSensorOptions;
    using IOrientationListener = andengine.sensor.orientation.IOrientationListener;
    using OrientationSensorOptions = andengine.sensor.orientation.OrientationSensorOptions;
    using IGameInterface = andengine.ui.IGameInterface;
    using Debug = andengine.util.Debug;

    using Context = Android.Content.Context;
    using ActivityInfo = Android.Content.PM.ActivityInfo;
    using AudioManager = Android.Media.AudioManager;
    using Bundle = Android.OS.Bundle;
    using PowerManager = Android.OS.PowerManager;
    using WakeLock = Android.OS.PowerManager.WakeLock;
    using Gravity = Android.Views.Gravity;
    using Window = Android.Views.Window;
    using WindowManager = Android.Views.IWindowManager;
    //using Runnable = andengine.engine.handler.runnable.Runnable;
    using Runnable = Java.Lang.IRunnable;
    using WindowManagerFlags = Android.Views.WindowManagerFlags;
    using LayoutParams = Android.Widget.FrameLayout.LayoutParams;

    /**
     * @author Nicolas Gramlich
     * @since 11:27:06 - 08.03.2010
     */
    public abstract class BaseGameActivity : BaseActivity, IGameInterface
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        protected Engine mEngine;
        private WakeLock mWakeLock;
        protected RenderSurfaceView mRenderSurfaceView;
        protected bool mHasWindowFocused;
        private bool mPaused;
        private bool mGameLoaded;

        // ===========================================================
        // Constructors
        // ===========================================================

        protected override void OnCreate(Bundle pSavedInstanceState)
        {
            base.OnCreate(pSavedInstanceState);
            this.mPaused = true;

            this.mEngine = this.OnLoadEngine();

            this.ApplyEngineOptions(this.mEngine.GetEngineOptions());

            this.OnSetContentView();
        }

        protected override void OnResume()
        {
            base.OnResume();

            if (this.mPaused && this.mHasWindowFocused)
            {
                this.DoResume();
            }
        }

        public override void OnWindowFocusChanged(bool pHasWindowFocus)
        {
            base.OnWindowFocusChanged(pHasWindowFocus);

            if (pHasWindowFocus)
            {
                if (this.mPaused)
                {
                    this.DoResume();
                }
                this.mHasWindowFocused = true;
            }
            else
            {
                if (!this.mPaused)
                {
                    this.DoPause();
                }
                this.mHasWindowFocused = false;
            }
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (!this.mPaused)
            {
                this.DoPause();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            this.mEngine.InterruptUpdateThread();

            this.OnUnloadResources();
        }

        public abstract Engine OnLoadEngine();
        public abstract void OnLoadResources();

        public /*override*/ void OnUnloadResources()
        {
            if (this.mEngine.GetEngineOptions().NeedsMusic())
            {
                this.GetMusicManager().ReleaseAll();
            }
            if (this.mEngine.GetEngineOptions().NeedsSound())
            {
                this.GetSoundManager().ReleaseAll();
            }
        }

        public abstract Scene OnLoadScene();
        public abstract void OnLoadComplete();

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public Engine GetEngine()
        {
            return this.mEngine;
        }

        public SoundManager GetSoundManager()
        {
            return this.mEngine.GetSoundManager();
        }

        public MusicManager GetMusicManager()
        {
            return this.mEngine.GetMusicManager();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /*override*/ void OnGameResumed()
        {

        }

        public /*override*/ void OnGamePaused()
        {

        }

        // ===========================================================
        // Methods
        // ===========================================================

        private void DoResume()
        {
            if (!this.mGameLoaded)
            {
                this.OnLoadResources();
                Scene scene = this.OnLoadScene();
                this.mEngine.OnLoadComplete(scene);
                this.OnLoadComplete();
                this.mGameLoaded = true;
            }

            this.mPaused = false;
            this.AcquireWakeLock(this.mEngine.GetEngineOptions().GetWakeLockOptions());
            this.mEngine.OnResume();

            this.mRenderSurfaceView.OnResume();
            this.mEngine.Start();
            this.OnGameResumed();
        }

        private void DoPause()
        {
            this.mPaused = true;
            this.ReleaseWakeLock();

            this.mEngine.OnPause();
            this.mEngine.Stop();
            this.mRenderSurfaceView.OnPause();
            this.OnGamePaused();
        }

        public void RunOnUpdateThread(Runnable pRunnable)
        {
            this.mEngine.RunOnUpdateThread(pRunnable);
        }

        protected void OnSetContentView()
        {
            this.mRenderSurfaceView = new RenderSurfaceView(this);
            this.mRenderSurfaceView.SetEGLConfigChooser(false);
            this.mRenderSurfaceView.SetRenderer(this.mEngine);

            this.SetContentView(this.mRenderSurfaceView, this.CreateSurfaceViewLayoutParams());
        }

        private void AcquireWakeLock(WakeLockFlags pWakeLockOptions)
        {
            PowerManager pm = (PowerManager)this.GetSystemService(Context.PowerService);
            this.mWakeLock = pm.NewWakeLock(pWakeLockOptions | WakeLockFlags.OnAfterRelease, "AndEngine");
            try
            {
                this.mWakeLock.Acquire();
            }
            catch (SecurityException e)
            {
                Debug.E("You have to add\n\t<uses-permission android:name=\"android.permission.WAKE_LOCK\"/>\nto your AndroidManifest.xml !", e);
            }
        }

        private void ReleaseWakeLock()
        {
            if (this.mWakeLock != null && this.mWakeLock.IsHeld)
            {
                this.mWakeLock.Release();
            }
        }

        private void ApplyEngineOptions(EngineOptions pEngineOptions)
        {
            if (pEngineOptions.IsFullscreen())
            {
                this.RequestFullscreen();
            }

            if (pEngineOptions.NeedsMusic() || pEngineOptions.NeedsSound())
            {
                /*this.SetVolumeControlStream(AudioManager.STREAM_MUSIC);*/
                this.VolumeControlStream = (int) Stream.Music;
            }

            switch (pEngineOptions.GetScreenOrientation())
            {
                case EngineOptions.ScreenOrientationOptions.LANDSCAPE:
                    this.SetRequestedOrientation(ScreenOrientation.Landscape);
                    break;
                case EngineOptions.ScreenOrientationOptions.PORTRAIT:
                    this.SetRequestedOrientation(ScreenOrientation.Portrait);
                    break;
            }
        }

        protected LayoutParams CreateSurfaceViewLayoutParams()
        {
            LayoutParams layoutParams = new LayoutParams(LayoutParams.FillParent, LayoutParams.FillParent);
            layoutParams.Gravity = (int) GravityFlags.Center;
            return layoutParams;
        }

        private void RequestFullscreen()
        {
            Window window = this.Window;
            window.AddFlags(WindowManagerFlags.Fullscreen);
            window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
            window.RequestFeature(WindowFeatures.NoTitle);
        }

        protected void EnableVibrator()
        {
            this.mEngine.EnableVibrator(this);
        }

        /**
         * @see {@link Engine#enableLocationSensor(Context, ILocationListener, LocationSensorOptions)}
         */
        protected void EnableLocationSensor(ILocationListener pLocationListener, LocationSensorOptions pLocationSensorOptions)
        {
            this.mEngine.EnableLocationSensor(this, pLocationListener, pLocationSensorOptions);
        }

        /**
         * @see {@link Engine#disableLocationSensor(Context)}
         */
        protected void DisableLocationSensor()
        {
            this.mEngine.DisableLocationSensor(this);
        }

        /**
         * @see {@link Engine#enableAccelerometerSensor(Context, IAccelerometerListener)}
         */
        protected bool EnableAccelerometerSensor(IAccelerometerListener pAccelerometerListener)
        {
            return this.mEngine.EnableAccelerometerSensor(this, pAccelerometerListener);
        }

        /**
         * @see {@link Engine#enableAccelerometerSensor(Context, IAccelerometerListener, AccelerometerSensorOptions)}
         */
        protected bool EnableAccelerometerSensor(IAccelerometerListener pAccelerometerListener, AccelerometerSensorOptions pAccelerometerSensorOptions)
        {
            return this.mEngine.EnableAccelerometerSensor(this, pAccelerometerListener, pAccelerometerSensorOptions);
        }

        /**
         * @see {@link Engine#disableAccelerometerSensor(Context)}
         */
        protected bool DisableAccelerometerSensor()
        {
            return this.mEngine.DisableAccelerometerSensor(this);
        }

        /**
         * @see {@link Engine#enableOrientationSensor(Context, IOrientationListener)}
         */
        protected bool EnableOrientationSensor(IOrientationListener pOrientationListener)
        {
            return this.mEngine.EnableOrientationSensor(this, pOrientationListener);
        }

        /**
         * @see {@link Engine#enableOrientationSensor(Context, IOrientationListener, OrientationSensorOptions)}
         */
        protected bool EnableOrientationSensor(IOrientationListener pOrientationListener, OrientationSensorOptions pLocationSensorOptions)
        {
            return this.mEngine.EnableOrientationSensor(this, pOrientationListener, pLocationSensorOptions);
        }

        /**
         * @see {@link Engine#disableOrientationSensor(Context)}
         */
        protected bool DisableOrientationSensor()
        {
            return this.mEngine.DisableOrientationSensor(this);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}