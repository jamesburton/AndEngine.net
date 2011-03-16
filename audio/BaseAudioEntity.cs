namespace andengine.audio
{
    //using IAudioEntity = andengine.audio.IAudioEntity;

    /**
     * @author Nicolas Gramlich
     * @since 16:35:37 - 13.06.2010
     */
    public abstract class BaseAudioEntity : IAudioEntity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //private final IAudioManager<? extends IAudioEntity> mAudioManager;
        private readonly IAudioManager mAudioManager;

        protected float mLeftVolume = 1.0f;
        protected float mRightVolume = 1.0f;

        // ===========================================================
        // Constructors
        // ===========================================================

        //public BaseAudioEntity(final IAudioManager<? extends IAudioEntity> pAudioManager) {
        public BaseAudioEntity(IAudioManager pAudioManager)
        {
            this.mAudioManager = pAudioManager;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        //protected IAudioManager<? extends IAudioEntity> getAudioManager() {
        protected IAudioManager GetAudioManager()
        {
            return this.mAudioManager;
        }
        public IAudioManager AudioManager { get { return GetAudioManager(); } }

        public float GetActualLeftVolume()
        {
            //return this.mLeftVolume * this.getMasterVolume();
            return this.mLeftVolume * this.MasterVolume;
        }

        public float ActualLeftVolume { get { return GetActualLeftVolume(); } }

        public float GetActualRightVolume()
        {
            //return this.mRightVolume * this.getMasterVolume();
            return this.mRightVolume * this.MasterVolume;
        }

        public float ActualRightVolume { get { return GetActualRightVolume(); } }

        protected float GetMasterVolume()
        {
            //return this.mAudioManager.GetMasterVolume();
            return this.mAudioManager.MasterVolume;
        }

        protected float MasterVolume { get { return GetMasterVolume(); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public /* override */ float GetVolume()
        {
            return (this.mLeftVolume + this.mRightVolume) * 0.5f;
        }

        public float Volume { get { return GetVolume(); } set { SetVolume(value); } }

        public /* override */ float GetLeftVolume()
        {
            return this.mLeftVolume;
        }

        public float LeftVolume { get { return GetLeftVolume(); } }

        public /* override */ float GetRightVolume()
        {
            return this.mRightVolume;
        }

        public float RightVolume { get { return GetRightVolume(); } }

        public /* override final */ void SetVolume(/* final */ float pVolume)
        {
            this.SetVolume(pVolume, pVolume);
        }

        public /* override */ void SetVolume(/* final */ float pLeftVolume, /* final */ float pRightVolume)
        {
            this.mLeftVolume = pLeftVolume;
            this.mRightVolume = pRightVolume;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        // NB: Filling in missing interface methods with abstract stubs
        public abstract void OnMasterVolumeChanged(float volume);
        public abstract void Pause();
        public abstract void Play();
        public abstract void Release();
        public abstract void Resume();
        public abstract void SetLooping(bool looping);
        public abstract void Stop();
    }
}