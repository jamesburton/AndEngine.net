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

        void Play();
        void Pause();
        void Resume();
        void Stop();

        //float GetVolume();
        //void SetVolume(/* final */ float pVolume);
        float Volume { get; set; }

        //float getLeftVolume();
        float LeftVolume { get; }
        //float getRightVolume();
        float RightVolume { get; }
        void SetVolume(/* final */ float pLeftVolume, /* final */ float pRightVolume);

        void OnMasterVolumeChanged(/* final */ float pMasterVolume);

        void SetLooping(/* final */ bool pLooping);

        void Release();
    }
}