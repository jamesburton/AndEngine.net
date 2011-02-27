namespace andengine.audio.music
{

    using BaseAudioEntity = andengine.audio.BaseAudioEntity;

    using MediaPlayer = Android.Media.MediaPlayer;
    //using OnCompletionListener = Android.Media.MediaPlayer.OnCompletionListener;

    /**
     * @author Nicolas Gramlich
     * @since 14:53:12 - 13.06.2010
     */
    public class Music : BaseAudioEntity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly MediaPlayer mMediaPlayer;

        // ===========================================================
        // Constructors
        // ===========================================================

        public Music(MusicManager pMusicManager, MediaPlayer pMediaPlayer)
            : base(pMusicManager)
        {
            this.mMediaPlayer = pMediaPlayer;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public bool isPlaying()
        {
            return this.mMediaPlayer.IsPlaying;
        }

        public MediaPlayer getMediaPlayer()
        {
            return this.mMediaPlayer;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override MusicManager getAudioManager()
        {
            return (MusicManager)base.getAudioManager();
        }

        public override void play()
        {
            this.mMediaPlayer.start();
        }

        public override void stop()
        {
            this.mMediaPlayer.stop();
        }

        public override void resume()
        {
            this.mMediaPlayer.start();
        }

        public override void pause()
        {
            this.mMediaPlayer.pause();
        }

        public override void release()
        {
            this.mMediaPlayer.release();
        }

        public override void setLooping(bool pLooping)
        {
            this.mMediaPlayer.setLooping(pLooping);
        }

        public override void setVolume(float pLeftVolume, float pRightVolume)
        {
            base.setVolume(pLeftVolume, pRightVolume);

            float masterVolume = this.getAudioManager().getMasterVolume();
            float actualLeftVolume = pLeftVolume * masterVolume;
            float actualRightVolume = pRightVolume * masterVolume;

            this.mMediaPlayer.setVolume(actualLeftVolume, actualRightVolume);
        }

        public override void onMasterVolumeChanged(float pMasterVolume)
        {
            this.setVolume(this.mLeftVolume, this.mRightVolume);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void seekTo(int pMilliseconds)
        {
            this.mMediaPlayer.seekTo(pMilliseconds);
        }

        public void setOnCompletionListener(OnCompletionListener pOnCompletionListener)
        {
            this.mMediaPlayer.setOnCompletionListener(pOnCompletionListener);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}