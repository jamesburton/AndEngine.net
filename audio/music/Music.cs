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

        public bool IsPlaying()
        {
            return this.mMediaPlayer.IsPlaying;
        }

        public MediaPlayer GetMediaPlayer()
        {
            return this.mMediaPlayer;
        }

        public MediaPlayer MediaPlayer { get { return GetMediaPlayer(); } }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected new MusicManager GetAudioManager()
        {
            //return (MusicManager)base.getAudioManager();
            return (MusicManager)base.AudioManager;
        }
        protected new MusicManager AudioManager { get { return GetAudioManager(); } }

        public override void Play()
        {
            this.mMediaPlayer.Start();
        }

        public override void Stop()
        {
            this.mMediaPlayer.Stop();
        }

        public override void Resume()
        {
            this.mMediaPlayer.Start();
        }

        public override void Pause()
        {
            this.mMediaPlayer.Pause();
        }

        public override void Release()
        {
            this.mMediaPlayer.Release();
        }

        public override void SetLooping(bool pLooping)
        {
            this.mMediaPlayer.Looping = pLooping;
        }

        public new void SetVolume(float pLeftVolume, float pRightVolume)
        {
            base.SetVolume(pLeftVolume, pRightVolume);

            //float masterVolume = this.getAudioManager().getMasterVolume();
            float masterVolume = this.AudioManager.MasterVolume;
            float actualLeftVolume = pLeftVolume * masterVolume;
            float actualRightVolume = pRightVolume * masterVolume;

            this.mMediaPlayer.SetVolume(actualLeftVolume, actualRightVolume);
        }

        public override void OnMasterVolumeChanged(float pMasterVolume)
        {
            this.SetVolume(this.mLeftVolume, this.mRightVolume);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public void SeekTo(int pMilliseconds)
        {
            this.mMediaPlayer.SeekTo(pMilliseconds);
        }

        public void SetOnCompletionListener(OnCompletionListener pOnCompletionListener)
        {
            this.mMediaPlayer.SetOnCompletionListener(pOnCompletionListener);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}