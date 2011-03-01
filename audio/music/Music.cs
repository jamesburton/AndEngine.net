namespace andengine.audio.music
{

    using BaseAudioEntity = andengine.audio.BaseAudioEntity;

    using MediaPlayer = Android.Media.MediaPlayer;
    using OnCompletionListener = Android.Media.MediaPlayer.IOnCompletionListener;

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

        protected new MusicManager getAudioManager()
        {
            return (MusicManager)base.getAudioManager();
        }

        public override void play()
        {
            this.mMediaPlayer.Start();
        }

        public override void stop()
        {
            this.mMediaPlayer.Stop();
        }

        public override void resume()
        {
            this.mMediaPlayer.Start();
        }

        public override void pause()
        {
            this.mMediaPlayer.Pause();
        }

        public override void release()
        {
            this.mMediaPlayer.Release();
        }

        public override void setLooping(bool pLooping)
        {
            this.mMediaPlayer.Looping = pLooping;
        }

        public new void setVolume(float pLeftVolume, float pRightVolume)
        {
            base.setVolume(pLeftVolume, pRightVolume);

            float masterVolume = this.getAudioManager().getMasterVolume();
            float actualLeftVolume = pLeftVolume * masterVolume;
            float actualRightVolume = pRightVolume * masterVolume;

            this.mMediaPlayer.SetVolume(actualLeftVolume, actualRightVolume);
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
            this.mMediaPlayer.SeekTo(pMilliseconds);
        }

        public void setOnCompletionListener(OnCompletionListener pOnCompletionListener)
        {
            this.mMediaPlayer.SetOnCompletionListener(pOnCompletionListener);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}