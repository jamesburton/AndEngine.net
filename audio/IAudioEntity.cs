namespace andengine.audio
{

    /**
     * @author Nicolas Gramlich
     * @since 14:53:29 - 13.06.2010
     */
    public interface IAudioEntity
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        void play();
        void pause();
        void resume();
        void stop();

        float getVolume();
        void setVolume(/* final */ float pVolume);

        float getLeftVolume();
        float getRightVolume();
        void setVolume(/* final */ float pLeftVolume, /* final */ float pRightVolume);

        void onMasterVolumeChanged(/* final */ float pMasterVolume);

        void setLooping(/* final */ bool pLooping);

        void release();
    }
}