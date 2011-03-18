using System;
using System.Linq;

namespace andengine.engine
{

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    //import javax.microedition.khronos.opengles.GL11;
    using GL11 = Javax.Microedition.Khronos.Opengles.IGL11;

    using MusicFactory = andengine.audio.music.MusicFactory;
    using MusicManager = andengine.audio.music.MusicManager;
    using System.Runtime.CompilerServices;
    using SoundFactory = andengine.audio.sound.SoundFactory;
    using SoundManager = andengine.audio.sound.SoundManager;
    using Camera = andengine.engine.camera.Camera;
    using IUpdateHandler = andengine.engine.handler.IUpdateHandler;
    using UpdateHandlerList = andengine.engine.handler.UpdateHandlerList;
    using RunnableHandler = andengine.engine.handler.runnable.RunnableHandler;
    using ITimerCallback = andengine.engine.handler.timer.ITimerCallback;
    using TimerHandler = andengine.engine.handler.timer.TimerHandler;
    using EngineOptions = andengine.engine.options.EngineOptions;
    using Scene = andengine.entity.scene.Scene;
    using SplashScene = andengine.entity.scene.SplashScene;
    using TouchEvent = andengine.input.touch.TouchEvent;
    using ITouchController = andengine.input.touch.controller.ITouchController;
    using SingleTouchController = andengine.input.touch.controller.SingleTouchControler;
    using ITouchEventCallback = andengine.input.touch.controller./*ITouchController.*/ITouchEventCallback;
    using BufferObjectManager = andengine.opengl.buffer.BufferObjectManager;
    using FontFactory = andengine.opengl.font.FontFactory;
    using FontManager = andengine.opengl.font.FontManager;
    using Texture = andengine.opengl.texture.Texture;
    using TextureFactory = andengine.opengl.texture.TextureFactory;
    using TextureManager = andengine.opengl.texture.TextureManager;
    using TextureRegion = andengine.opengl.texture.region.TextureRegion;
    using TextureRegionFactory = andengine.opengl.texture.region.TextureRegionFactory;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    using GLHelper = andengine.opengl.util.GLHelper;
    using AccelerometerData = andengine.sensor.accelerometer.AccelerometerData;
    using AccelerometerSensorOptions = andengine.sensor.accelerometer.AccelerometerSensorOptions;
    using IAccelerometerListener = andengine.sensor.accelerometer.IAccelerometerListener;
    using ILocationListener = andengine.sensor.location.ILocationListener;
    using LocationProviderStatus = andengine.sensor.location.LocationProviderStatus;
    using LocationSensorOptions = andengine.sensor.location.LocationSensorOptions;
    using IOrientationListener = andengine.sensor.orientation.IOrientationListener;
    using OrientationData = andengine.sensor.orientation.OrientationData;
    //using OrientationSensorOptions = andengine.sensor.orientation.OrientationSensorOptions;
    using OrientationSensorOptions = andengine.sensor.orientation.OrientationSensorOptions;
    using Debug = andengine.util.Debug;
    using TimeConstants = andengine.util.constants.TimeConstants;

    using Context = Android.Content.Context;
    //using Android.Content;
    using Sensor = Android.Hardware.Sensor;
    using SensorEvent = Android.Hardware.SensorEvent;
    using SensorEventListener = Android.Hardware.ISensorEventListener;
    using SensorManager = Android.Hardware.SensorManager;
    //using Android.Locations;
    using Location = Android.Locations.Location;
    using LocationListener = Android.Locations.ILocationListener;
    using LocationManager = Android.Locations.LocationManager;
    using LocationProvider = Android.Locations.LocationProvider;
    //using Android.OS;
    using Bundle = Android.OS.Bundle;
    using Vibrator = Android.OS.Vibrator;
    //using Android.Views;

    //using Java.Lang;
    using String = System.String;
    using IllegalStateException = Java.Lang.IllegalStateException;
    using IRunnable = Java.Lang.IRunnable;
    using InterruptedException = Java.Lang.InterruptedException;

    //import android.view.MotionEvent;
    //import android.view.View;
    //import android.view.View.OnTouchListener;
    using MotionEvent = Android.Views.MotionEvent;
    using View = Android.Views.View;
    using OnTouchListener = Android.Views.View.IOnTouchListener;
    using Android.Hardware;
    using System.Threading;

    /**
     * @author Nicolas Gramlich
     * @since 12:21:31 - 08.03.2010
     */
    // TODO: Check the implications of removing class(es) from the list (TimeConstants - no longer an interface, so add class to usage)
    public class Engine : Java.Lang.Object, SensorEventListener, OnTouchListener /* NB: Is actually IOnTouchListener */, ITouchEventCallback, /* TimeConstants, */ LocationListener
    {
        #region Interface bindings
        //void ITouchEventCallback.OnTouchEvent(TouchEvent pSurfaceTouchEvent) { OnTouchEvent(pSurfaceTouchEvent); }
        void Android.Locations.ILocationListener.OnLocationChanged(Location pLocation) { OnLocationChanged(pLocation); }
        //void ILocationListener.OnProviderDisabled(System.String pProvider) { OnProviderDisabled(pProvider); }
        //void Android.Locations.ILocationListener.OnProviderDisabled(System.String pProvider) { OnProviderDisabled(pProvider); }
        void Android.Locations.ILocationListener.OnProviderDisabled(System.String pProvider) { OnProviderDisabled(pProvider); }
        void Android.Locations.ILocationListener.OnProviderEnabled(System.String pProvider) { OnProviderEnabled(pProvider); }
        void Android.Locations.ILocationListener.OnStatusChanged(string pProvider, int pStatus, Bundle pExtras) { OnStatusChanged(pProvider, pStatus, pExtras); }
        #endregion

        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly float LOADING_SCREEN_DURATION_DEFAULT = 2;

        private static /* final */ readonly Android.Hardware.SensorDelay SENSORDELAY_DEFAULT = Android.Hardware.SensorDelay.Game;

        // ===========================================================
        // Fields
        // ===========================================================

        private bool mRunning = false;

        private long mLastTick = -1;
        private float mSecondsElapsedTotal = 0;

        //private /* final */ readonly State mThreadLocker = new State();
        private readonly State mThreadLocker = new State();

        private /* final */ readonly UpdateThread mUpdateThread = new UpdateThread();

        private /* final */ readonly RunnableHandler mUpdateThreadRunnableHandler = new RunnableHandler();

        private /* final */ readonly EngineOptions mEngineOptions;
        protected /* final */ readonly andengine.engine.camera.Camera mCamera;

        private ITouchController mTouchController;

        private SoundManager mSoundManager;
        private MusicManager mMusicManager;
        private /* final */ readonly TextureManager mTextureManager = new TextureManager();
        private /* final */ readonly BufferObjectManager mBufferObjectManager = new BufferObjectManager();
        private /* final */ readonly FontManager mFontManager = new FontManager();

        protected Scene mScene;

        private Vibrator mVibrator;

        private ILocationListener mLocationListener;
        private Location mLocation;

        private IAccelerometerListener mAccelerometerListener;
        private AccelerometerData mAccelerometerData;

        private IOrientationListener mOrientationListener;
        private OrientationData mOrientationData;

        private /* final */ readonly UpdateHandlerList mUpdateHandlers = new UpdateHandlerList();
        private static readonly object _methodLock = new object();

        protected int mSurfaceWidth = 1; // 1 to prevent accidental DIV/0
        protected int mSurfaceHeight = 1; // 1 to prevent accidental DIV/0

        private bool mIsMethodTracing;

        // ===========================================================
        // Constructors
        // ===========================================================

        public static Engine Instance;
        public Engine(/* final */ EngineOptions pEngineOptions)
        {
            Engine.Instance = this;

            //TextureRegionFactory.setAssetBasePath("");
            TextureRegionFactory.SetAssetBasePath("");
            //SoundFactory.setAssetBasePath("");
            SoundFactory.SetAssetBasePath("");
            //MusicFactory.setAssetBasePath("");
            MusicFactory.SetAssetBasePath("");
            //FontFactory.setAssetBasePath("");
            FontFactory.setAssetBasePath("");

            //BufferObjectManager.setActiveInstance(this.mBufferObjectManager);
            BufferObjectManager.SetActiveInstance(this.mBufferObjectManager);

            this.mEngineOptions = pEngineOptions;
            //this.SetTouchController(new SingleTouchController());
            this.TouchController = new SingleTouchController();
            //this.mCamera = pEngineOptions.getCamera();
            this.mCamera = pEngineOptions.GetCamera();

            if (this.mEngineOptions.NeedsSound())
            {
                this.mSoundManager = new SoundManager();
            }

            if (this.mEngineOptions.NeedsMusic())
            {
                this.mMusicManager = new MusicManager();
            }

            if (this.mEngineOptions.HasLoadingScreen())
            {
                this.InitLoadingScreen();
            }

            this.mUpdateThread.Start();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool IsRunning()
        {
            return this.mRunning;
        }
        public bool Running { get { return IsRunning(); } }

        public /* synchronized */ void Start()
        {
            lock (_methodLock)
            {
                if (!this.mRunning)
                {
                    //this.mLastTick = System.nanoTime();
                    this.mLastTick = DateTime.Now.Ticks;
                    this.mRunning = true;
                }
            }
        }

        //public synchronized void stop() {
        public void Stop()
        {
            lock (_methodLock)
            {
                if (this.mRunning)
                {
                    this.mRunning = false;
                }
            }
        }

        public Scene GetScene()
        {
            return this.mScene;
        }

        public void SetScene(/* final */ Scene pScene)
        {
            this.mScene = pScene;
        }

        public Scene Scene { get { return GetScene(); } set { SetScene(value); } }

        public EngineOptions GetEngineOptions()
        {
            return this.mEngineOptions;
        }

        public EngineOptions EngineOptions { get { return GetEngineOptions(); } }

        public virtual Camera GetCamera()
        {
            return this.mCamera;
        }

        public Camera Camera { get { return GetCamera(); } }

        public float GetSecondsElapsedTotal()
        {
            return this.mSecondsElapsedTotal;
        }

        public float SecondsElapsedTotal { get { return GetSecondsElapsedTotal(); } }

        public void SetSurfaceSize(/* final */ int pSurfaceWidth, /* final */ int pSurfaceHeight)
        {
            //		Debug.w("SurfaceView size changed to (width x height): " + pSurfaceWidth + " x " + pSurfaceHeight, new Exception());
            this.mSurfaceWidth = pSurfaceWidth;
            this.mSurfaceHeight = pSurfaceHeight;
        }

        public int GetSurfaceWidth()
        {
            return this.mSurfaceWidth;
        }

        public int SurfaceWidth { get { return GetSurfaceWidth(); } }

        public int GetSurfaceHeight()
        {
            return this.mSurfaceHeight;
        }

        public int SurfaceHeight { get { return GetSurfaceHeight(); } }

        public ITouchController TouchController { get { return GetTouchController(); } set { SetTouchController(value); } }

        public ITouchController GetTouchController()
        {
            return this.mTouchController;
        }

        public void SetTouchController(/* final */ ITouchController pTouchController)
        {
            this.mTouchController = pTouchController;
            this.mTouchController.ApplyTouchOptions(this.mEngineOptions.GetTouchOptions());
            this.mTouchController.SetTouchEventCallback(this);
        }

        public AccelerometerData AccelerometerData { get { return GetAccelerometerData(); } }

        public AccelerometerData GetAccelerometerData()
        {
            return this.mAccelerometerData;
        }

        public OrientationData OrientationData { get { return GetOrientationData(); } }

        public OrientationData GetOrientationData()
        {
            return this.mOrientationData;
        }

        public SoundManager SoundManager { get { return GetSoundManager(); } }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IllegalStateException" />
        public SoundManager GetSoundManager() /* throws IllegalStateException */ {
            if (this.mSoundManager != null)
            {
                return this.mSoundManager;
            }
            else
            {
                throw new IllegalStateException("To enable the SoundManager, check the EngineOptions!");
            }
        }

        public MusicManager MusicManager { get { return GetMusicManager(); } }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IllegalStateException" />
        public MusicManager GetMusicManager() /* throws IllegalStateException */ {
            if (this.mMusicManager != null)
            {
                return this.mMusicManager;
            }
            else
            {
                throw new IllegalStateException("To enable the MusicManager, check the EngineOptions!");
            }
        }

        public TextureManager TextureManager { get { return GetTextureManager(); } }
        public TextureManager GetTextureManager()
        {
            return this.mTextureManager;
        }

        public FontManager FontManager { get { return GetFontManager(); } }
        public FontManager GetFontManager()
        {
            return this.mFontManager;
        }

        public void ClearUpdateHandlers()
        {
            this.mUpdateHandlers.Clear();
        }

        public void RegisterUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Add(pUpdateHandler);
        }

        public void UnregisterUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Remove(pUpdateHandler);
        }

        public bool IsMethodTracing()
        {
            return this.mIsMethodTracing;
        }
        public bool MethodTracing { get { return IsMethodTracing(); } }

        public void StartMethodTracing(/* final */ String pTraceFileName)
        {
            if (!this.mIsMethodTracing)
            {
                this.mIsMethodTracing = true;
                //Android.OS.Debug.StartMethodTracing(new Java.Lang.String(pTraceFileName));
                Android.OS.Debug.StartMethodTracing(pTraceFileName.ToString());
            }
        }

        public void StopMethodTracing()
        {
            if (this.mIsMethodTracing)
            {
                Android.OS.Debug.StopMethodTracing();
                this.mIsMethodTracing = false;
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ void OnAccuracyChanged(/* final */ Sensor pSensor, /* final */ int pAccuracy)
        {
            if (this.mRunning)
            {
                switch (pSensor.Type)
                {
                    case SensorType.Accelerometer:
                        this.mAccelerometerData.setAccuracy(pAccuracy);
                        this.mAccelerometerListener.onAccelerometerChanged(this.mAccelerometerData);
                        break;
                }
            }
        }

        public /* override */ void OnSensorChanged(/* final */ SensorEvent pEvent)
        {
            if (this.mRunning)
            {
                switch (pEvent.Sensor.Type)
                {
                    case SensorType.Accelerometer:
                        this.mAccelerometerData.setValues(pEvent.Values.ToArray());
                        this.mAccelerometerListener.onAccelerometerChanged(this.mAccelerometerData);
                        break;
                    case SensorType.Orientation:
                        this.mOrientationData.setValues(pEvent.Values.ToArray());
                        this.mOrientationListener.onOrientationChanged(this.mOrientationData);
                        break;
                }
            }
        }

        public /* override */ void OnLocationChanged(/* final */ Location pLocation)
        {
            if (this.mLocation == null)
            {
                this.mLocation = pLocation;
            }
            else
            {
                if (pLocation == null)
                {
                    this.mLocationListener.onLocationLost();
                }
                else
                {
                    this.mLocation = pLocation;
                    this.mLocationListener.onLocationChanged(pLocation);
                }
            }
        }

        public /* override */ void OnProviderDisabled(/* final */ String pProvider)
        {
            //TODO: The following method call had a parameter in the Java code. Why has it been removed?
            this.mLocationListener.onLocationProviderDisabled();/* pProvider */
        }

        public /* override */ void OnProviderEnabled(/* final */ String pProvider)
        {
            //TODO: The following method call had a parameter in the Java code. Why has it been removed?
            this.mLocationListener.onLocationProviderEnabled(); /* pProvider */
        }

        public void OnStatusChanged(/* final */ String pProvider, /* final */ int  pStatus, /* final */ Bundle pExtras)
        {
            OnStatusChanged(pProvider, (LocationProviderStatus)pStatus, pExtras);
        }

        public void OnStatusChanged(/* final */ String pProvider, /* final int */ LocationProviderStatus pStatus, /* final */ Bundle pExtras)
        {
            switch (pStatus)
            {
                //case LocationProvider.AVAILABLE:
                case LocationProviderStatus.AVAILABLE:
                    this.mLocationListener.onLocationProviderStatusChanged(LocationProviderStatus.AVAILABLE, pExtras);
                    break;
                //case LocationProvider.OUT_OF_SERVICE:
                case LocationProviderStatus.OUT_OF_SERVICE:
                    this.mLocationListener.onLocationProviderStatusChanged(LocationProviderStatus.OUT_OF_SERVICE, pExtras);
                    break;
                //case LocationProvider.TEMPORARILY_UNAVAILABLE:
                case LocationProviderStatus.TEMPORARILY_UNAVAILABLE:
                    this.mLocationListener.onLocationProviderStatusChanged(LocationProviderStatus.TEMPORARILY_UNAVAILABLE, pExtras);
                    break;
            }
        }

        public bool OnTouch(/* final */ View pView, /* final */ MotionEvent pSurfaceMotionEvent)
        {
            if (this.mRunning)
            {
                /* final */
                bool handled = this.mTouchController.OnHandleMotionEvent(pSurfaceMotionEvent);
                try
                {
                    /*
                     * As a human cannot interact 1000x per second, we pause the
                     * UI-Thread for a little.
                     */
                    Thread.Sleep(20); // TODO Maybe this can be removed, when TouchEvents are handled on the UpdateThread!
                }
                catch (/* final */ InterruptedException e)
                {
                    Debug.E(e);
                }
                return handled;
            }
            else
            {
                return false;
            }
        }

        public /* override */ bool OnTouchEvent(/* final */ TouchEvent pSurfaceTouchEvent)
        {
            /*
             * Let the engine determine which scene and camera this event should be
             * handled by.
             */
            /* final */
            Scene scene = this.GetSceneFromSurfaceTouchEvent(pSurfaceTouchEvent);
            /* final */
            Camera camera = this.GetCameraFromSurfaceTouchEvent(pSurfaceTouchEvent);

            this.ConvertSurfaceToSceneTouchEvent(camera, pSurfaceTouchEvent);

            if (this.OnTouchHUD(camera, pSurfaceTouchEvent))
            {
                return true;
            }
            else
            {
                /* If HUD didn't handle it, Scene may handle it. */
                return this.OnTouchScene(scene, pSurfaceTouchEvent);
            }
        }

        protected bool OnTouchHUD(/* final */ Camera pCamera, /* final */ TouchEvent pSceneTouchEvent)
        {
            if (pCamera.HasHUD())
                return pCamera.GetHUD().OnSceneTouchEvent(pSceneTouchEvent);
            else
            {
                return false;
            }
        }

        protected bool OnTouchScene(/* final */ Scene pScene, /* final */ TouchEvent pSceneTouchEvent)
        {
            if (pScene != null)
            {
                return pScene.OnSceneTouchEvent(pSceneTouchEvent);
            }
            else
            {
                return false;
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void RunOnUpdateThread(/* final */ IRunnable pRunnable)
        {
            this.mUpdateThreadRunnableHandler.PostRunnable(pRunnable);
        }

        public void InterruptUpdateThread()
        {
            this.mUpdateThread.Interrupt();
        }

        private void InitLoadingScreen()
        {
            /* final */
            ITextureSource loadingScreenTextureSource = this.mEngineOptions.GetLoadingScreenTextureSource();
            /* final */
            Texture loadingScreenTexture = TextureFactory.CreateForTextureSourceSize(loadingScreenTextureSource);
            /* final */
            TextureRegion loadingScreenTextureRegion = TextureRegionFactory.CreateFromSource(loadingScreenTexture, loadingScreenTextureSource, 0, 0);
            this.SetScene(new SplashScene(this.GetCamera(), loadingScreenTextureRegion));
        }

        public void OnResume()
        {
            this.mTextureManager.reloadTextures();
            this.mFontManager.ReloadFonts();
            BufferObjectManager.SetActiveInstance(this.mBufferObjectManager);
            this.mBufferObjectManager.ReloadBufferObjects();
        }

        public void OnPause()
        {

        }

        protected virtual Camera GetCameraFromSurfaceTouchEvent(/* final */ TouchEvent pTouchEvent)
        {
            return this.GetCamera();
        }

        protected Scene GetSceneFromSurfaceTouchEvent(/* final */ TouchEvent pTouchEvent)
        {
            return this.mScene;
        }

        protected virtual void ConvertSurfaceToSceneTouchEvent(/* final */ Camera pCamera, /* final */ TouchEvent pSurfaceTouchEvent)
        {
            pCamera.ConvertSurfaceToSceneTouchEvent(pSurfaceTouchEvent, this.mSurfaceWidth, this.mSurfaceHeight);
        }

        public void OnLoadComplete(/* final */ Scene pScene)
        {
            /* TODO Unload texture from loading-screen. */
            if (this.mEngineOptions.HasLoadingScreen())
            {
                /*
                this.registerUpdateHandler(new TimerHandler(LOADING_SCREEN_DURATION_DEFAULT, new ITimerCallback() {
                    //@Override
                    public override void onTimePassed(/* final * / TimerHandler pTimerHandler) {
                        Engine.this.unregisterUpdateHandler(pTimerHandler);
                        Engine.this.setScene(pScene);
                    }
                }));
                */
                IUpdateHandler EngineTimerUpdateHandler = new TimerHandler(LOADING_SCREEN_DURATION_DEFAULT,
                    new EngineTimer(this));
                this.RegisterUpdateHandler(EngineTimerUpdateHandler);
            }
            else
            {
                this.SetScene(pScene);
            }
        }

        protected class EngineTimer : ITimerCallback
        {
            protected Engine Engine;
            public EngineTimer(Engine Engine) { this.Engine = Engine; }
            public /* override */ void OnTimePassed(/* final */ TimerHandler pTimerHandler)
            {
                this.Engine.UnregisterUpdateHandler(pTimerHandler);
                this.Engine.SetScene(Engine.Scene /* pScene */);
            }
        }

        void OnTickUpdate() /* throws InterruptedException */ {
            if (this.mRunning)
            {
                /* final */
                long secondsElapsed = this.GetNanosecondsElapsed();

                this.OnUpdate(secondsElapsed);

                this.YieldDraw();
            }
            else
            {
                this.YieldDraw();

                Thread.Sleep(16);
            }
        }

        private void YieldDraw() /* throws InterruptedException */ {
            /* TODO: Verify this change ... maybe the static class needs to become non-static
            // final State threadLocker = this.mThreadLocker;
            threadLocker.notifyCanDraw();
            threadLocker.waitUntilCanUpdate();
            */
            State State = this.mThreadLocker;
            State.NotifyCanDraw();
            State.WaitUntilCanUpdate();
        }

        protected void OnUpdate(/* final */ long pNanosecondsElapsed) /* throws InterruptedException */ {
            /* final */
            float pSecondsElapsed = (float)pNanosecondsElapsed / TimeConstants.NANOSECONDSPERSECOND;

            this.mSecondsElapsedTotal += pSecondsElapsed;
            this.mLastTick += pNanosecondsElapsed;

            this.mTouchController.OnUpdate(pSecondsElapsed);
            this.UpdateUpdateHandlers(pSecondsElapsed);
            this.OnUpdateScene(pSecondsElapsed);
        }

        protected void OnUpdateScene(/* final */ float pSecondsElapsed)
        {
            if (this.mScene != null)
            {
                this.mScene.OnUpdate(pSecondsElapsed);
            }
        }

        protected virtual void UpdateUpdateHandlers(/* final */ float pSecondsElapsed)
        {
            this.mUpdateThreadRunnableHandler.OnUpdate(pSecondsElapsed);
            this.mUpdateHandlers.OnUpdate(pSecondsElapsed);
            this.GetCamera().OnUpdate(pSecondsElapsed);
        }

        public void OnDrawFrame(/* final */ GL10 pGL) /* throws InterruptedException */ {
            /*
            final State threadLocker = this.mThreadLocker;

            threadLocker.waitUntilCanDraw();
            //*/
            //State.WaitUntilCanDraw();
            State threadLocker = this.mThreadLocker;

            this.mTextureManager.updateTextures(pGL);
            this.mFontManager.UpdateFonts(pGL);
            if (GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS)
            {
                this.mBufferObjectManager.UpdateBufferObjects((GL11)pGL);
            }

            this.OnDrawScene(pGL);

            threadLocker.NotifyCanUpdate();
            threadLocker.NotifyCanUpdate();
        }

        protected virtual void OnDrawScene(/* final */ GL10 pGL)
        {
            /* final */
            Camera camera = this.GetCamera();

            this.mScene.OnDraw(pGL, camera);

            camera.OnDrawHUD(pGL);
        }

        private long GetNanosecondsElapsed()
        {
            /* final */
            //long now = System.nanoTime();
            long now = System.DateTime.Now.Ticks;

            return this.CalculateNanosecondsElapsed(now, this.mLastTick);
        }

        protected long CalculateNanosecondsElapsed(/* final */ long pNow, /* final */ long pLastTick)
        {
            return pNow - pLastTick;
        }

        public bool EnableVibrator(/* final */ Context pContext)
        {
            this.mVibrator = (Vibrator)pContext.GetSystemService(Context.VibratorService);
            return this.mVibrator != null;
        }

        public void Vibrate(/* final */ long pMilliseconds) /* throws IllegalStateException */ {
            if (this.mVibrator != null)
            {
                this.mVibrator.Vibrate(pMilliseconds);
            }
            else
            {
                throw new IllegalStateException("You need to enable the Vibrator before you can use it!");
            }
        }

        public void Vibrate(/* final */ long[] pPattern, /* final */ int pRepeat) /* throws IllegalStateException */ {
            if (this.mVibrator != null)
            {
                this.mVibrator.Vibrate(pPattern, pRepeat);
            }
            else
            {
                throw new IllegalStateException("You need to enable the Vibrator before you can use it!");
            }
        }

        public void EnableLocationSensor(/* final */ Context pContext, /* final */ ILocationListener pLocationListener, /* final */ LocationSensorOptions pLocationSensorOptions)
        {
            this.mLocationListener = pLocationListener;

            /* final */
            LocationManager locationManager = (LocationManager)pContext.GetSystemService(Context.LocationService);
            /* final */
            String locationProvider = locationManager.GetBestProvider(pLocationSensorOptions, pLocationSensorOptions.isEnabledOnly());
            locationManager.RequestLocationUpdates(locationProvider, pLocationSensorOptions.getMinimumTriggerTime(), pLocationSensorOptions.getMinimumTriggerDistance(), this);

            this.OnLocationChanged(locationManager.GetLastKnownLocation(locationProvider));
        }

        public void DisableLocationSensor(/* final */ Context pContext)
        {
            /* final */
            LocationManager locationManager = (LocationManager)pContext.GetSystemService(Context.LocationService);
            locationManager.RemoveUpdates(this);
        }

        /**
         * @see {@link Engine#enableAccelerometerSensor(Context, IAccelerometerListener, AccelerometerSensorOptions)}
         */
        public bool EnableAccelerometerSensor(/* final */ Context pContext, /* final */ IAccelerometerListener pAccelerometerListener)
        {
            return this.EnableAccelerometerSensor(pContext, pAccelerometerListener, new AccelerometerSensorOptions(SENSORDELAY_DEFAULT));
        }

        /**
         * @return <code>true</code> when the sensor was successfully enabled, <code>false</code> otherwise.
         */
        public bool EnableAccelerometerSensor(/* final */ Context pContext, /* final */ IAccelerometerListener pAccelerometerListener, /* final */ AccelerometerSensorOptions pAccelerometerSensorOptions)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            //if (this.isSensorSupported(sensorManager, SensorType.Accelerometer))
            if (this.IsSensorSupported(sensorManager, SensorType.Accelerometer))
            {
                this.mAccelerometerListener = pAccelerometerListener;

                if (this.mAccelerometerData == null)
                {
                    this.mAccelerometerData = new AccelerometerData();
                }

                this.RegisterSelfAsSensorListener(sensorManager, SensorType.Accelerometer, pAccelerometerSensorOptions.getSensorDelay());

                return true;
            }
            else
            {
                return false;
            }
        }


        /**
         * @return <code>true</code> when the sensor was successfully disabled, <code>false</code> otherwise.
         */
        public bool DisableAccelerometerSensor(/* final */ Context pContext)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.IsSensorSupported(sensorManager, SensorType.Accelerometer))
            {
                this.UnregisterSelfAsSensorListener(sensorManager, SensorType.Accelerometer);
                return true;
            }
            else
            {
                return false;
            }
        }

        /**
         * @see {@link Engine#enableOrientationSensor(Context, IOrientationListener, OrientationSensorOptions)}
         */
        public bool EnableOrientationSensor(/* final */ Context pContext, /* final */ IOrientationListener pOrientationListener)
        {
            return this.EnableOrientationSensor(pContext, pOrientationListener, new OrientationSensorOptions(SENSORDELAY_DEFAULT));
        }

        /**
         * @return <code>true</code> when the sensor was successfully enabled, <code>false</code> otherwise.
         */
        public bool EnableOrientationSensor(/* final */ Context pContext, /* final */ IOrientationListener pOrientationListener, /* final */ OrientationSensorOptions pOrientationSensorOptions)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.IsSensorSupported(sensorManager, SensorType.Orientation))
            {
                this.mOrientationListener = pOrientationListener;

                if (this.mOrientationData == null)
                {
                    this.mOrientationData = new OrientationData();
                }

                this.RegisterSelfAsSensorListener(sensorManager, SensorType.Orientation, pOrientationSensorOptions.getSensorDelay());

                return true;
            }
            else
            {
                return false;
            }
        }


        /**
         * @return <code>true</code> when the sensor was successfully disabled, <code>false</code> otherwise.
         */
        public bool DisableOrientationSensor(/* final */ Context pContext)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.IsSensorSupported(sensorManager, SensorType.Orientation))
            {
                this.UnregisterSelfAsSensorListener(sensorManager, SensorType.Orientation);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool IsSensorSupported(/* final */ SensorManager pSensorManager, /* final int */ SensorType pType)
        {
            return pSensorManager.GetSensorList(pType).Count > 0;
        }

        private void RegisterSelfAsSensorListener(/* final */ SensorManager pSensorManager, /* final int */ SensorType pType, /* final */ Android.Hardware.SensorDelay pSensorDelay)
        {
            /* final */
            Sensor sensor = pSensorManager.GetSensorList(pType)[0];
            pSensorManager.RegisterListener(this, sensor, pSensorDelay/*.getDelay()*/);
        }

        private void UnregisterSelfAsSensorListener(/* final */ SensorManager pSensorManager, /* final int */ SensorType pType)
        {
            /* final */
            Sensor sensor = pSensorManager.GetSensorList(pType)[0];
            pSensorManager.UnregisterListener(this, sensor);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        private class UpdateThread : Java.Lang.Thread
        {
            Java.Lang.Thread thread;
            public UpdateThread()
                : base("UpdateThread")
            {
                thread = new Java.Lang.Thread(Run);
                //thread = new Thread(new ThreadStart(run));
            }

            public override void Run()
            {
                try
                {
                    while (true)
                    {
                        //Engine.this.onTickUpdate();
                        Engine.Instance.OnTickUpdate();
                    }
                }
                catch (/* final */ InterruptedException e)
                {
                    Debug.D("UpdateThread interrupted. Don't worry - this Exception is most likely expected!", e);
                    //this.interrupt();
                    Interrupt();
                }
            }
        }

        private /* static */ class State /* Adding Java.Lang.Object base class to add threaded functionality from it */ : Java.Lang.Object
        {
            static bool mDrawing = false;

            public /* synchronized */ void NotifyCanDraw()
            {
                lock (_methodLock)
                {
                    // Debug.d(">>> notifyCanDraw");
                    mDrawing = true;
                    this.NotifyAll();
                    // Debug.d("<<< notifyCanDraw");
                }
            }

            public /* synchronized */ void NotifyCanUpdate()
            {
                lock (_methodLock)
                {
                    // Debug.d(">>> notifyCanUpdate");
                    mDrawing = false;
                    this.NotifyAll();
                    // Debug.d("<<< notifyCanUpdate");
                }
            }

            public /* synchronized */ void WaitUntilCanDraw() /* throws InterruptedException */ {
                lock (_methodLock)
                {
                    // Debug.d(">>> waitUntilCanDraw");
                    while (mDrawing == false)
                    {
                        this.Wait();
                    }
                    // Debug.d("<<< waitUntilCanDraw");
                }
            }

            public /* synchronized */ void WaitUntilCanUpdate() /* throws InterruptedException */ {
                lock (_methodLock)
                {
                    // Debug.d(">>> waitUntilCanUpdate");
                    while (mDrawing == true)
                    {
                        this.Wait();
                    }
                    // Debug.d("<<< waitUntilCanUpdate");
                }
            }
        }
    }
}