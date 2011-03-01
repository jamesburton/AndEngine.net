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
    using SensorDelay = andengine.sensor.SensorDelay;
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
    using Java.Lang;
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
    public class Engine : SensorEventListener, OnTouchListener /* NB: Is actually IOnTouchListener */, ITouchEventCallback, /* TimeConstants, */ LocationListener
    {
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

            TextureRegionFactory.setAssetBasePath("");
            SoundFactory.setAssetBasePath("");
            MusicFactory.setAssetBasePath("");
            FontFactory.setAssetBasePath("");

            BufferObjectManager.setActiveInstance(this.mBufferObjectManager);

            this.mEngineOptions = pEngineOptions;
            this.setTouchController(new SingleTouchController());
            this.mCamera = pEngineOptions.getCamera();

            if (this.mEngineOptions.needsSound())
            {
                this.mSoundManager = new SoundManager();
            }

            if (this.mEngineOptions.needsMusic())
            {
                this.mMusicManager = new MusicManager();
            }

            if (this.mEngineOptions.hasLoadingScreen())
            {
                this.initLoadingScreen();
            }

            this.mUpdateThread.Start();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool isRunning()
        {
            return this.mRunning;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]
        public /* synchronized */ void start()
        {
            if (!this.mRunning)
            {
                //this.mLastTick = System.nanoTime();
                this.mLastTick = System.DateTime.Now.Ticks;
                this.mRunning = true;
            }
        }

        //public synchronized void stop() {
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void stop()
        {
            if (this.mRunning)
            {
                this.mRunning = false;
            }
        }

        public Scene getScene()
        {
            return this.mScene;
        }

        public void setScene(/* final */ Scene pScene)
        {
            this.mScene = pScene;
        }

        public EngineOptions getEngineOptions()
        {
            return this.mEngineOptions;
        }

        public andengine.engine.camera.Camera getCamera()
        {
            return this.mCamera;
        }

        public float getSecondsElapsedTotal()
        {
            return this.mSecondsElapsedTotal;
        }

        public void setSurfaceSize(/* final */ int pSurfaceWidth, /* final */ int pSurfaceHeight)
        {
            //		Debug.w("SurfaceView size changed to (width x height): " + pSurfaceWidth + " x " + pSurfaceHeight, new Exception());
            this.mSurfaceWidth = pSurfaceWidth;
            this.mSurfaceHeight = pSurfaceHeight;
        }

        public int getSurfaceWidth()
        {
            return this.mSurfaceWidth;
        }

        public int getSurfaceHeight()
        {
            return this.mSurfaceHeight;
        }

        public ITouchController getTouchController()
        {
            return this.mTouchController;
        }

        public void setTouchController(/* final */ ITouchController pTouchController)
        {
            this.mTouchController = pTouchController;
            this.mTouchController.applyTouchOptions(this.mEngineOptions.getTouchOptions());
            this.mTouchController.setTouchEventCallback(this);
        }

        public AccelerometerData getAccelerometerData()
        {
            return this.mAccelerometerData;
        }

        public OrientationData getOrientationData()
        {
            return this.mOrientationData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IllegalStateException" />
        public SoundManager getSoundManager() /* throws IllegalStateException */ {
            if (this.mSoundManager != null)
            {
                return this.mSoundManager;
            }
            else
            {
                throw new IllegalStateException("To enable the SoundManager, check the EngineOptions!");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        /// <exception cref="IllegalStateException" />
        public MusicManager getMusicManager() /* throws IllegalStateException */ {
            if (this.mMusicManager != null)
            {
                return this.mMusicManager;
            }
            else
            {
                throw new IllegalStateException("To enable the MusicManager, check the EngineOptions!");
            }
        }

        public TextureManager getTextureManager()
        {
            return this.mTextureManager;
        }

        public FontManager getFontManager()
        {
            return this.mFontManager;
        }

        public void clearUpdateHandlers()
        {
            this.mUpdateHandlers.Clear();
        }

        public void registerUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Add(pUpdateHandler);
        }

        public void unregisterUpdateHandler(/* final */ IUpdateHandler pUpdateHandler)
        {
            this.mUpdateHandlers.Remove(pUpdateHandler);
        }

        public bool isMethodTracing()
        {
            return this.mIsMethodTracing;
        }

        public void startMethodTracing(/* final */ String pTraceFileName)
        {
            if (!this.mIsMethodTracing)
            {
                this.mIsMethodTracing = true;
                Android.OS.Debug.StartMethodTracing(new Java.Lang.String(pTraceFileName));
            }
        }

        public void stopMethodTracing()
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

        public override void onAccuracyChanged(/* final */ Sensor pSensor, /* final */ int pAccuracy)
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

        public override void onSensorChanged(/* final */ SensorEvent pEvent)
        {
            if (this.mRunning)
            {
                switch (pEvent.Sensor.Type)
                {
                    case SensorType.Accelerometer:
                        this.mAccelerometerData.setValues(pEvent.Values);
                        this.mAccelerometerListener.onAccelerometerChanged(this.mAccelerometerData);
                        break;
                    case SensorType.Orientation:
                        this.mOrientationData.setValues(pEvent.Values);
                        this.mOrientationListener.onOrientationChanged(this.mOrientationData);
                        break;
                }
            }
        }

        public override void onLocationChanged(/* final */ Location pLocation)
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

        public override void onProviderDisabled(/* final */ String pProvider)
        {
            this.mLocationListener.OnProviderDisabled(pProvider);
        }

        public override void onProviderEnabled(/* final */ String pProvider)
        {
            this.mLocationListener.OnProviderEnabled(pProvider);
        }

        public void onStatusChanged(/* final */ String pProvider, /* final */ int  pStatus, /* final */ Bundle pExtras)
        {
            onStatusChanged(pProvider, (LocationProviderStatus)pStatus, pExtras);
        }

        public void onStatusChanged(/* final */ String pProvider, /* final int */ LocationProviderStatus pStatus, /* final */ Bundle pExtras)
        {
            switch (pStatus)
            {
                //case LocationProvider.AVAILABLE:
                case LocationProviderStatus.AVAILABLE:
                    this.mLocationListener.OnStatusChanged(LocationProviderStatus.AVAILABLE, pExtras);
                    break;
                //case LocationProvider.OUT_OF_SERVICE:
                case LocationProviderStatus.OUT_OF_SERVICE:
                    this.mLocationListener.OnStatusChanged(LocationProviderStatus.OUT_OF_SERVICE, pExtras);
                    break;
                //case LocationProvider.TEMPORARILY_UNAVAILABLE:
                case LocationProviderStatus.TEMPORARILY_UNAVAILABLE:
                    this.mLocationListener.OnStatusChanged(LocationProviderStatus.TEMPORARILY_UNAVAILABLE, pExtras);
                    break;
            }
        }

        public bool onTouch(/* final */ View pView, /* final */ MotionEvent pSurfaceMotionEvent)
        {
            if (this.mRunning)
            {
                /* final */
                bool handled = this.mTouchController.onHandleMotionEvent(pSurfaceMotionEvent);
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
                    Debug.e(e);
                }
                return handled;
            }
            else
            {
                return false;
            }
        }

        public override bool onTouchEvent(/* final */ TouchEvent pSurfaceTouchEvent)
        {
            /*
             * Let the engine determine which scene and camera this event should be
             * handled by.
             */
            /* final */
            Scene scene = this.getSceneFromSurfaceTouchEvent(pSurfaceTouchEvent);
            /* final */
            Camera camera = this.getCameraFromSurfaceTouchEvent(pSurfaceTouchEvent);

            this.convertSurfaceToSceneTouchEvent(camera, pSurfaceTouchEvent);

            if (this.onTouchHUD(camera, pSurfaceTouchEvent))
            {
                return true;
            }
            else
            {
                /* If HUD didn't handle it, Scene may handle it. */
                return this.onTouchScene(scene, pSurfaceTouchEvent);
            }
        }

        protected bool onTouchHUD(/* final */ andengine.engine.camera.Camera pCamera, /* final */ TouchEvent pSceneTouchEvent)
        {
            if (pCamera.hasHUD())
            {
                return pCamera.getHUD().onSceneTouchEvent(pSceneTouchEvent);
            }
            else
            {
                return false;
            }
        }

        protected bool onTouchScene(/* final */ Scene pScene, /* final */ TouchEvent pSceneTouchEvent)
        {
            if (pScene != null)
            {
                return pScene.onSceneTouchEvent(pSceneTouchEvent);
            }
            else
            {
                return false;
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void runOnUpdateThread(/* final */ IRunnable pRunnable)
        {
            this.mUpdateThreadRunnableHandler.postRunnable(pRunnable);
        }

        public void interruptUpdateThread()
        {
            this.mUpdateThread.Interrupt();
        }

        private void initLoadingScreen()
        {
            /* final */
            ITextureSource loadingScreenTextureSource = this.mEngineOptions.getLoadingScreenTextureSource();
            /* final */
            Texture loadingScreenTexture = TextureFactory.createForTextureSourceSize(loadingScreenTextureSource);
            /* final */
            TextureRegion loadingScreenTextureRegion = TextureRegionFactory.createFromSource(loadingScreenTexture, loadingScreenTextureSource, 0, 0);
            this.setScene(new SplashScene(this.getCamera(), loadingScreenTextureRegion));
        }

        public void onResume()
        {
            this.mTextureManager.reloadTextures();
            this.mFontManager.reloadFonts();
            BufferObjectManager.setActiveInstance(this.mBufferObjectManager);
            this.mBufferObjectManager.reloadBufferObjects();
        }

        public void onPause()
        {

        }

        protected andengine.engine.camera.Camera getCameraFromSurfaceTouchEvent(/* final */ TouchEvent pTouchEvent)
        {
            return this.getCamera();
        }

        protected Scene getSceneFromSurfaceTouchEvent(/* final */ TouchEvent pTouchEvent)
        {
            return this.mScene;
        }

        protected void convertSurfaceToSceneTouchEvent(/* final */ andengine.engine.camera.Camera pCamera, /* final */ TouchEvent pSurfaceTouchEvent)
        {
            pCamera.convertSurfaceToSceneTouchEvent(pSurfaceTouchEvent, this.mSurfaceWidth, this.mSurfaceHeight);
        }

        public void onLoadComplete(/* final */ Scene pScene)
        {
            /* TODO Unload texture from loading-screen. */
            if (this.mEngineOptions.hasLoadingScreen())
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
                this.registerUpdateHandler(EngineTimerUpdateHandler);
            }
            else
            {
                this.setScene(pScene);
            }
        }

        protected class EngineTimer : ITimerCallback
        {
            protected Engine Engine;
            public EngineTimer(Engine Engine) { this.Engine = Engine; }
            public override void onTimePassed(/* final */ TimerHandler pTimerHandler)
            {
                this.Engine.unregisterUpdateHandler(pTimerHandler);
                this.Engine.setScene(pScene);
            }
        }

        void onTickUpdate() /* throws InterruptedException */ {
            if (this.mRunning)
            {
                /* final */
                long secondsElapsed = this.getNanosecondsElapsed();

                this.onUpdate(secondsElapsed);

                this.yieldDraw();
            }
            else
            {
                this.yieldDraw();

                Thread.sleep(16);
            }
        }

        private void yieldDraw() /* throws InterruptedException */ {
            /* TODO: Verify this change ... maybe the static class needs to become non-static
            // final State threadLocker = this.mThreadLocker;
            threadLocker.notifyCanDraw();
            threadLocker.waitUntilCanUpdate();
            */
            State.notifyCanDraw();
            State.waitUntilCanUpdate();
        }

        protected void onUpdate(/* final */ long pNanosecondsElapsed) /* throws InterruptedException */ {
            /* final */
            float pSecondsElapsed = (float)pNanosecondsElapsed / TimeConstants.NANOSECONDSPERSECOND;

            this.mSecondsElapsedTotal += pSecondsElapsed;
            this.mLastTick += pNanosecondsElapsed;

            this.mTouchController.onUpdate(pSecondsElapsed);
            this.updateUpdateHandlers(pSecondsElapsed);
            this.onUpdateScene(pSecondsElapsed);
        }

        protected void onUpdateScene(/* final */ float pSecondsElapsed)
        {
            if (this.mScene != null)
            {
                this.mScene.onUpdate(pSecondsElapsed);
            }
        }

        protected void updateUpdateHandlers(/* final */ float pSecondsElapsed)
        {
            this.mUpdateThreadRunnableHandler.onUpdate(pSecondsElapsed);
            this.mUpdateHandlers.onUpdate(pSecondsElapsed);
            this.getCamera().onUpdate(pSecondsElapsed);
        }

        public void onDrawFrame(/* final */ GL10 pGL) /* throws InterruptedException */ {
            /*
            final State threadLocker = this.mThreadLocker;

            threadLocker.waitUntilCanDraw();
            //*/
            State.waitUntilCanDraw();

            this.mTextureManager.updateTextures(pGL);
            this.mFontManager.updateFonts(pGL);
            if (GLHelper.EXTENSIONS_VERTEXBUFFEROBJECTS)
            {
                this.mBufferObjectManager.updateBufferObjects((GL11)pGL);
            }

            this.onDrawScene(pGL);

            threadLocker.notifyCanUpdate();
        }

        protected void onDrawScene(/* final */ GL10 pGL)
        {
            /* final */
            Camera camera = this.getCamera();

            this.mScene.onDraw(pGL, camera);

            camera.onDrawHUD(pGL);
        }

        private long getNanosecondsElapsed()
        {
            /* final */
            //long now = System.nanoTime();
            long now = System.DateTime.Now.Ticks;

            return this.calculateNanosecondsElapsed(now, this.mLastTick);
        }

        protected long calculateNanosecondsElapsed(/* final */ long pNow, /* final */ long pLastTick)
        {
            return pNow - pLastTick;
        }

        public bool enableVibrator(/* final */ Context pContext)
        {
            this.mVibrator = (Vibrator)pContext.GetSystemService(Context.VibratorService);
            return this.mVibrator != null;
        }

        public void vibrate(/* final */ long pMilliseconds) /* throws IllegalStateException */ {
            if (this.mVibrator != null)
            {
                this.mVibrator.Vibrate(pMilliseconds);
            }
            else
            {
                throw new IllegalStateException("You need to enable the Vibrator before you can use it!");
            }
        }

        public void vibrate(/* final */ long[] pPattern, /* final */ int pRepeat) /* throws IllegalStateException */ {
            if (this.mVibrator != null)
            {
                this.mVibrator.Vibrate(pPattern, pRepeat);
            }
            else
            {
                throw new IllegalStateException("You need to enable the Vibrator before you can use it!");
            }
        }

        public void enableLocationSensor(/* final */ Context pContext, /* final */ ILocationListener pLocationListener, /* final */ LocationSensorOptions pLocationSensorOptions)
        {
            this.mLocationListener = pLocationListener;

            /* final */
            LocationManager locationManager = (LocationManager)pContext.GetSystemService(Context.LocationService);
            /* final */
            String locationProvider = locationManager.GetBestProvider(pLocationSensorOptions, pLocationSensorOptions.isEnabledOnly());
            locationManager.RequestLocationUpdates(locationProvider, pLocationSensorOptions.getMinimumTriggerTime(), pLocationSensorOptions.getMinimumTriggerDistance(), this);

            this.onLocationChanged(locationManager.GetLastKnownLocation(locationProvider));
        }

        public void disableLocationSensor(/* final */ Context pContext)
        {
            /* final */
            LocationManager locationManager = (LocationManager)pContext.GetSystemService(Context.LocationService);
            locationManager.RemoveUpdates(this);
        }

        /**
         * @see {@link Engine#enableAccelerometerSensor(Context, IAccelerometerListener, AccelerometerSensorOptions)}
         */
        public bool enableAccelerometerSensor(/* final */ Context pContext, /* final */ IAccelerometerListener pAccelerometerListener)
        {
            return this.enableAccelerometerSensor(pContext, pAccelerometerListener, new AccelerometerSensorOptions(SENSORDELAY_DEFAULT));
        }

        /**
         * @return <code>true</code> when the sensor was successfully enabled, <code>false</code> otherwise.
         */
        public bool enableAccelerometerSensor(/* final */ Context pContext, /* final */ IAccelerometerListener pAccelerometerListener, /* final */ AccelerometerSensorOptions pAccelerometerSensorOptions)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.isSensorSupported(sensorManager, SensorType.Accelerometer))
            {
                this.mAccelerometerListener = pAccelerometerListener;

                if (this.mAccelerometerData == null)
                {
                    this.mAccelerometerData = new AccelerometerData();
                }

                this.registerSelfAsSensorListener(sensorManager, SensorType.Accelerometer, pAccelerometerSensorOptions.getSensorDelay());

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
        public bool disableAccelerometerSensor(/* final */ Context pContext)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.isSensorSupported(sensorManager, SensorType.Accelerometer))
            {
                this.unregisterSelfAsSensorListener(sensorManager, SensorType.Accelerometer);
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
        public bool enableOrientationSensor(/* final */ Context pContext, /* final */ IOrientationListener pOrientationListener)
        {
            return this.enableOrientationSensor(pContext, pOrientationListener, new OrientationSensorOptions(SENSORDELAY_DEFAULT));
        }

        /**
         * @return <code>true</code> when the sensor was successfully enabled, <code>false</code> otherwise.
         */
        public bool enableOrientationSensor(/* final */ Context pContext, /* final */ IOrientationListener pOrientationListener, /* final */ OrientationSensorOptions pOrientationSensorOptions)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.isSensorSupported(sensorManager, SensorType.Orientation))
            {
                this.mOrientationListener = pOrientationListener;

                if (this.mOrientationData == null)
                {
                    this.mOrientationData = new OrientationData();
                }

                this.registerSelfAsSensorListener(sensorManager, SensorType.Orientation, pOrientationSensorOptions.getSensorDelay());

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
        public bool disableOrientationSensor(/* final */ Context pContext)
        {
            /* final */
            SensorManager sensorManager = (SensorManager)pContext.GetSystemService(Context.SensorService);
            if (this.isSensorSupported(sensorManager, SensorType.Orientation))
            {
                this.unregisterSelfAsSensorListener(sensorManager, SensorType.Orientation);
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool isSensorSupported(/* final */ SensorManager pSensorManager, /* final int */ SensorType pType)
        {
            return pSensorManager.GetSensorList(pType).Count > 0;
        }

        private void registerSelfAsSensorListener(/* final */ SensorManager pSensorManager, /* final int */ SensorType pType, /* final */ Android.Hardware.SensorDelay pSensorDelay)
        {
            /* final */
            Sensor sensor = pSensorManager.GetSensorList(pType)[0];
            pSensorManager.RegisterListener(this, sensor, pSensorDelay/*.getDelay()*/);
        }

        private void unregisterSelfAsSensorListener(/* final */ SensorManager pSensorManager, /* final int */ SensorType pType)
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
                thread = new Java.Lang.Thread(run);
                //thread = new Thread(new ThreadStart(run));
            }

            public override void run()
            {
                try
                {
                    while (true)
                    {
                        //Engine.this.onTickUpdate();
                        Engine.Instance.onTickUpdate();
                    }
                }
                catch (/* final */ InterruptedException e)
                {
                    Debug.d("UpdateThread interrupted. Don't worry - this Exception is most likely expected!", e);
                    //this.interrupt();
                    Interrupt();
                }
            }
        }

        private static class State
        {
            static bool mDrawing = false;

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static /* synchronized */ void notifyCanDraw()
            {
                // Debug.d(">>> notifyCanDraw");
                mDrawing = true;
                this.notifyAll();
                // Debug.d("<<< notifyCanDraw");
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static /* synchronized */ void notifyCanUpdate()
            {
                // Debug.d(">>> notifyCanUpdate");
                mDrawing = false;
                this.notifyAll();
                // Debug.d("<<< notifyCanUpdate");
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static /* synchronized */ void waitUntilCanDraw() /* throws InterruptedException */ {
                // Debug.d(">>> waitUntilCanDraw");
                while (mDrawing == false)
                {
                    this.wait();
                }
                // Debug.d("<<< waitUntilCanDraw");
            }

            [MethodImpl(MethodImplOptions.Synchronized)]
            public static /* synchronized */ void waitUntilCanUpdate() /* throws InterruptedException */ {
                // Debug.d(">>> waitUntilCanUpdate");
                while (mDrawing == true)
                {
                    this.wait();
                }
                // Debug.d("<<< waitUntilCanUpdate");
            }
        }
    }
}