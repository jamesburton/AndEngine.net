namespace andengine.audio.sound
{
    using andengine.audio;
    //using BaseAudioManager = andengine.audio.BaseAudioManager;

    using AudioManager = Android.Media.AudioManager;
    using SoundPool = Android.Media.SoundPool;

    /**
     * @author Nicolas Gramlich
     * @since 13:22:59 - 11.03.2010
     */
    public class SoundManager : andengine.audio.BaseAudioManager<Sound>
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static readonly int MAX_SIMULTANEOUS_STREAMS_DEFAULT = 5;

        // ===========================================================
        // Fields
        // ===========================================================

        private SoundPool mSoundPool;

        // ===========================================================
        // Constructors
        // ===========================================================

        public SoundManager()
        {
            this(MAX_SIMULTANEOUS_STREAMS_DEFAULT);
        }

        public SoundManager(int pMaxSimultaneousStreams)
        {
            this.mSoundPool = new SoundPool(pMaxSimultaneousStreams, AudioManager.STREAM_MUSIC, 0);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        public SoundPool getSoundPool()
        {
            return this.mSoundPool;
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public new void releaseAll()
        {
            base.releaseAll();

            this.mSoundPool.Release();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}