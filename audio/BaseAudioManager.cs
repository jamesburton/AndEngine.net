namespace andengine.audio
{
    using System.Collections.Generic;
    //public class ArrayList<T> : List<T> { public ArrayList() : base() { } }

    //import java.util.ArrayList;

    /**
     * @author Nicolas Gramlich
     * @since 18:07:02 - 13.06.2010
     */
    //public abstract class BaseAudioManager<T extends IAudioEntity> : IAudioManager<T> {
    public abstract class BaseAudioManager<T> : IAudioManager<T> where T : IAudioEntity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //protected /* final */ sealed ArrayList<T> mAudioEntities = new ArrayList<T>();
        protected readonly List<T> mAudioEntities = new List<T>();

        protected float mMasterVolume = 1.0f;

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public float GetMasterVolume()
        {
            return this.mMasterVolume;
        }

        public void SetMasterVolume(/* final */ float pMasterVolume)
        {
            this.mMasterVolume = pMasterVolume;

            /* final */
            //ArrayList<T> audioEntities = this.mAudioEntities;
            List<T> audioEntities = this.mAudioEntities;
            for (int i = audioEntities.Count - 1; i >= 0; i--)
            {
                /* final */
                T audioEntity = audioEntities[i];

                audioEntity.OnMasterVolumeChanged(pMasterVolume);
            }
        }

        public float MasterVolume { get { return GetMasterVolume(); } set { SetMasterVolume(value); } }

        public void Add(/* final */ T pAudioEntity)
        {
            this.mAudioEntities.Add(pAudioEntity);
        }

        public /* override */ void ReleaseAll()
        {
            // final ArrayList<T> audioEntities = this.mAudioEntities;
            List<T> audioEntities = this.mAudioEntities;
            for (int i = audioEntities.Count - 1; i >= 0; i--)
            {
                /* final */
                T audioEntity = audioEntities[i];

                audioEntity.Stop();
                audioEntity.Release();
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}