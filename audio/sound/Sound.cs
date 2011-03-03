namespace andengine.audio.sound
{

    using BaseAudioEntity = andengine.audio.BaseAudioEntity;

    /**
     * @author Nicolas Gramlich
     * @since 13:22:15 - 11.03.2010
     */
    public class Sound : BaseAudioEntity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly int mSoundID;
        private int mStreamID = 0;

        private int mLoopCount = 0;
        private float mRate = 1.0f;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Sound(SoundManager pSoundManager, int pSoundID)
            : base(pSoundManager)
        {
            this.mSoundID = pSoundID;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public void SetLoopCount(int pLoopCount)
        {
            this.mLoopCount = pLoopCount;
            if (this.mStreamID != 0)
            {
                //this.getAudioManager().getSoundPool().SetLoop(this.mStreamID, pLoopCount);
                this.AudioManager.SoundPool.SetLoop(this.mStreamID, pLoopCount);
            }
        }
        public int LoopCount { set { SetLoopCount(value); } }

        public void SetRate(float pRate)
        {
            this.mRate = pRate;
            if (this.mStreamID != 0)
            {
                //this.getAudioManager().getSoundPool().SetRate(this.mStreamID, pRate);
                this.AudioManager.SoundPool.SetRate(this.mStreamID, pRate);
            }
        }

        public float Rate { set { SetRate(value); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        protected new SoundManager GetAudioManager()
        {
            return (SoundManager)base.GetAudioManager();
        }
        protected new SoundManager AudioManager { get { return GetAudioManager(); } }

        public override void Play()
        {
            //float masterVolume = this.getMasterVolume();
            float masterVolume = this.MasterVolume;
            float leftVolume = this.mLeftVolume * masterVolume;
            float rightVolume = this.mRightVolume * masterVolume;
            //this.mStreamID = this.getAudioManager().getSoundPool().play(this.mSoundID, leftVolume, rightVolume, 1, this.mLoopCount, this.mRate);
            this.mStreamID = this.AudioManager.SoundPool.Play(this.mSoundID, leftVolume, rightVolume, 1, this.mLoopCount, this.mRate);
        }

        public override void Stop()
        {
            if (this.mStreamID != 0)
            {
                //this.getAudioManager().getSoundPool().Stop(this.mStreamID);
                this.AudioManager.SoundPool.Stop(this.mStreamID);
            }
        }

        public override void Resume()
        {
            if (this.mStreamID != 0)
            {
                //this.getAudioManager().getSoundPool().Resume(this.mStreamID);
                this.AudioManager.SoundPool.Resume(this.mStreamID);
            }
        }

        public override void Pause()
        {
            if (this.mStreamID != 0)
            {
                //this.getAudioManager().getSoundPool().Pause(this.mStreamID);
                this.AudioManager.SoundPool.Pause(this.mStreamID);
            }
        }

        public override void Release()
        {

        }

        public override void SetLooping(bool pLooping)
        {
            this.SetLoopCount((pLooping) ? -1 : 0);
        }

        public new void SetVolume(float pLeftVolume, float pRightVolume)
        {
            base.SetVolume(pLeftVolume, pRightVolume);
            if (this.mStreamID != 0)
            {
                //float masterVolume = this.getMasterVolume();
                float masterVolume = this.MasterVolume;
                float leftVolume = this.mLeftVolume * masterVolume;
                float rightVolume = this.mRightVolume * masterVolume;

                //this.getAudioManager().getSoundPool().setVolume(this.mStreamID, leftVolume, rightVolume);
                this.AudioManager.SoundPool.SetVolume(this.mStreamID, leftVolume, rightVolume);
            }
        }

        public override void OnMasterVolumeChanged(float pMasterVolume)
        {
            this.SetVolume(this.mLeftVolume, this.mRightVolume);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}