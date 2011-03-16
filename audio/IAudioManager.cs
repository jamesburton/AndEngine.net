namespace andengine.audio
{
/**
 * @author Nicolas Gramlich
 * @since 15:02:06 - 13.06.2010
 */
    //NOTE: Generic parameter removed due to lack of covariance around casting a BaseAudioManager<Music> to IAudioManager<IAudioEntity>
    public interface IAudioManager/*<T> where T : IAudioEntity*/
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        // public float getMasterVolume();
        // public void setMasterVolume(/* final */ float pMasterVolume);
        float MasterVolume { get; set; }

        /* public */ void Add(/* final T */ object pAudioEntity);

        /* public */ void ReleaseAll();
    }
}