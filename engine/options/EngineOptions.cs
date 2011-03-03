namespace andengine.engine.options
{

    using Camera = andengine.engine.camera.Camera;
    using IResolutionPolicy = andengine.engine.options.resolutionpolicy.IResolutionPolicy;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;

    /**
     * @author  Nicolas Gramlich
     * @since  15:59:52 - 09.03.2010
     */
    public class EngineOptions
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private /* final */ readonly bool mFullscreen;
        private /* final */ readonly /* ScreenOrientation */ ScreenOrientationOptions mScreenOrientation;
        private /* final */ readonly IResolutionPolicy mResolutionPolicy;
        private /* final */ readonly Camera mCamera;

        private /* final */ readonly TouchOptions mTouchOptions = new TouchOptions();
        private /* final */ readonly RenderOptions mRenderOptions = new RenderOptions();

        private ITextureSource mLoadingScreenTextureSource;
        private bool mNeedsSound;
        private bool mNeedsMusic;
        private WakeLockOptions mWakeLockOptions = WakeLockOptions.SCREEN_BRIGHT;

        // ===========================================================
        // Constructors
        // ===========================================================

        public EngineOptions(/* final */ bool pFullscreen, /* final ScreenOrientation */ ScreenOrientationOptions pScreenOrientation, /* final */ IResolutionPolicy pResolutionPolicy, /* final */ Camera pCamera)
        {
            this.mFullscreen = pFullscreen;
            this.mScreenOrientation = pScreenOrientation;
            this.mResolutionPolicy = pResolutionPolicy;
            this.mCamera = pCamera;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public TouchOptions GetTouchOptions()
        {
            return this.mTouchOptions;
        }

        public TouchOptions TouchOptions { get { return GetTouchOptions(); } }

        public RenderOptions GetRenderOptions()
        {
            return this.mRenderOptions;
        }

        public RenderOptions RenderOptions { get { return GetRenderOptions(); } }

        public bool IsFullscreen()
        {
            return this.mFullscreen;
        }

        public ScreenOrientationOptions GetScreenOrientation()
        {
            return this.mScreenOrientation;
        }

        public ScreenOrientationOptions ScreenOrientation { get { return GetScreenOrientation(); } }

        public IResolutionPolicy GetResolutionPolicy()
        {
            return this.mResolutionPolicy;
        }

        public IResolutionPolicy ResolutionPolicy { get { return GetResolutionPolicy(); } }

        public Camera GetCamera()
        {
            return this.mCamera;
        }

        public Camera Camera { get { return GetCamera(); } }

        public bool HasLoadingScreen()
        {
            return this.mLoadingScreenTextureSource != null;
        }

        public ITextureSource GetLoadingScreenTextureSource()
        {
            return this.mLoadingScreenTextureSource;
        }

        public EngineOptions SetLoadingScreenTextureSource(/* final */ ITextureSource pLoadingScreenTextureSource)
        {
            this.mLoadingScreenTextureSource = pLoadingScreenTextureSource;
            return this;
        }

        public ITextureSource LoadingScreenTextureSource { get { return GetLoadingScreenTextureSource(); } set { SetLoadingScreenTextureSource(value); } }

        public bool NeedsSound()
        {
            return this.mNeedsSound;
        }

        public EngineOptions SetNeedsSound(/* final */ bool pNeedsSound)
        {
            this.mNeedsSound = pNeedsSound;
            return this;
        }

        public bool NeedsMusic()
        {
            return this.mNeedsMusic;
        }

        public EngineOptions SetNeedsMusic(/* final */ bool pNeedsMusic)
        {
            this.mNeedsMusic = pNeedsMusic;
            return this;
        }

        public WakeLockOptions GetWakeLockOptions()
        {
            return this.mWakeLockOptions;
        }

        public EngineOptions SetWakeLockOptions(/* final */ WakeLockOptions pWakeLockOptions)
        {
            this.mWakeLockOptions = pWakeLockOptions;
            return this;
        }

        public WakeLockOptions WakeLockOptions { get { return GetWakeLockOptions(); } set { SetWakeLockOptions(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        public /* static */ enum /* ScreenOrientation */ ScreenOrientationOptions
        {
            // ===========================================================
            // Elements
            // ===========================================================

            LANDSCAPE,
            PORTRAIT
        }
    }
}