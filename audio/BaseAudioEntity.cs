namespace andengine.audio
{

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
        private sealed IAudioManager<IAudioEntity> mAudioManager;

        protected float mLeftVolume = 1.0f;
        protected float mRightVolume = 1.0f;

        // ===========================================================
        // Constructors
        // ===========================================================

        //public BaseAudioEntity(final IAudioManager<? extends IAudioEntity> pAudioManager) {
        public BaseAudioEntity(IAudioManager<IAudioEntity> pAudioManager)
        {
            this.mAudioManager = pAudioManager;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        //protected IAudioManager<? extends IAudioEntity> getAudioManager() {
        protected IAudioManager<IAudioEntity> getAudioManager()
        {
            return this.mAudioManager;
        }

        public float getActualLeftVolume()
        {
            return this.mLeftVolume * this.getMasterVolume();
        }

        public float getActualRightVolume()
        {
            return this.mRightVolume * this.getMasterVolume();
        }

        protected float getMasterVolume()
        {
            return this.mAudioManager.getMasterVolume();
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override float getVolume()
        {
            return (this.mLeftVolume + this.mRightVolume) * 0.5f;
        }

        public override float getLeftVolume()
        {
            return this.mLeftVolume;
        }

        public override float getRightVolume()
        {
            return this.mRightVolume;
        }

        public override /* final */ sealed void setVolume(/* final */ float pVolume)
        {
            this.setVolume(pVolume, pVolume);
        }

        public override void setVolume(/* final */ float pLeftVolume, /* final */ float pRightVolume)
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
    }
}