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

        Sound(SoundManager pSoundManager, int pSoundID)
            : base(pSoundManager)
        {
            this.mSoundID = pSoundID;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public void setLoopCount(int pLoopCount)
        {
            this.mLoopCount = pLoopCount;
            if (this.mStreamID != 0)
            {
                this.getAudioManager().getSoundPool().setLoop(this.mStreamID, pLoopCount);
            }
        }

        public void setRate(float pRate)
        {
            this.mRate = pRate;
            if (this.mStreamID != 0)
            {
                this.getAudioManager().getSoundPool().setRate(this.mStreamID, pRate);
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        protected override SoundManager getAudioManager()
        {
            return (SoundManager)base.getAudioManager();
        }

        public override void play()
        {
            float masterVolume = this.getMasterVolume();
            float leftVolume = this.mLeftVolume * masterVolume;
            float rightVolume = this.mRightVolume * masterVolume;
            this.mStreamID = this.getAudioManager().getSoundPool().play(this.mSoundID, leftVolume, rightVolume, 1, this.mLoopCount, this.mRate);
        }

        public override void stop()
        {
            if (this.mStreamID != 0)
            {
                this.getAudioManager().getSoundPool().stop(this.mStreamID);
            }
        }

        public override void resume()
        {
            if (this.mStreamID != 0)
            {
                this.getAudioManager().getSoundPool().resume(this.mStreamID);
            }
        }

        public override void pause()
        {
            if (this.mStreamID != 0)
            {
                this.getAudioManager().getSoundPool().pause(this.mStreamID);
            }
        }

        public override void release()
        {

        }

        public void setLooping(bool pLooping)
        {
            this.setLoopCount((pLooping) ? -1 : 0);
        }

        public override void setVolume(float pLeftVolume, float pRightVolume)
        {
            base.setVolume(pLeftVolume, pRightVolume);
            if (this.mStreamID != 0)
            {
                float masterVolume = this.getMasterVolume();
                float leftVolume = this.mLeftVolume * masterVolume;
                float rightVolume = this.mRightVolume * masterVolume;

                this.getAudioManager().getSoundPool().setVolume(this.mStreamID, leftVolume, rightVolume);
            }
        }

        public override void onMasterVolumeChanged(float pMasterVolume)
        {
            this.setVolume(this.mLeftVolume, this.mRightVolume);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}