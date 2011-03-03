namespace andengine.audio.sound
{

    using IOException = Java.IO.IOException;

    using Context = Android.Content.Context;
    //using Java.Lang;
    using String = System.String;
    using IllegalStateException = Java.Lang.IllegalStateException;

    /**
     * @author Nicolas Gramlich
     * @since 14:23:03 - 11.03.2010
     */
    public class SoundFactory
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static String sAssetBasePath = "";

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        /**
         * @param pAssetBasePath must end with '<code>/</code>' or have <code>.length() == 0</code>.
         */
        public static void SetAssetBasePath(String pAssetBasePath)
        {
            if (pAssetBasePath.EndsWith("/") || pAssetBasePath.Length == 0)
            {
                SoundFactory.sAssetBasePath = pAssetBasePath;
            }
            else
            {
                throw new IllegalStateException("pAssetBasePath must end with '/' or be lenght zero.");
            }
        }

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static Sound CreateSoundFromPath(SoundManager pSoundManager, Context pContext, String pPath) /* throws IOException */ {
            //int soundID = pSoundManager.getSoundPool().load(pPath, 1);
            int soundID = pSoundManager.SoundPool.Load(pPath, 1);
            Sound sound = new Sound(pSoundManager, soundID);
            pSoundManager.Add(sound);
            return sound;
        }

        public static Sound CreateSoundFromAsset(SoundManager pSoundManager, Context pContext, String pAssetPath) /* throws IOException */ {
            //int soundID = pSoundManager.getSoundPool().load(pContext.getAssets().openFd(SoundFactory.sAssetBasePath + pAssetPath), 1);
            int soundID = pSoundManager.SoundPool.Load(pContext.Assets.OpenFd(SoundFactory.sAssetBasePath + pAssetPath), 1);
            Sound sound = new Sound(pSoundManager, soundID);
            pSoundManager.Add(sound);
            return sound;
        }

        public static Sound CreateSoundFromResource(SoundManager pSoundManager, Context pContext, int pSoundResID)
        {
            //int soundID = pSoundManager.getSoundPool().load(pContext, pSoundResID, 1);
            int soundID = pSoundManager.SoundPool.Load(pContext, pSoundResID, 1);
            Sound sound = new Sound(pSoundManager, soundID);
            pSoundManager.Add(sound);
            return sound;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}