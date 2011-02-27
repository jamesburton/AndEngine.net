namespace andengine.audio.sound
{

    using IOException = Java.IO.IOException;

    using Context = Android.Content.Context;
    using Java.Lang;

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
        public static void setAssetBasePath(String pAssetBasePath)
        {
            if (pAssetBasePath.endsWith("/") || pAssetBasePath.length() == 0)
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

        public static Sound createSoundFromPath(SoundManager pSoundManager, Context pContext, String pPath) /* throws IOException */ {
            int soundID = pSoundManager.getSoundPool().load(pPath, 1);
            Sound sound = new Sound(pSoundManager, soundID);
            pSoundManager.add(sound);
            return sound;
        }

        public static Sound createSoundFromAsset(SoundManager pSoundManager, Context pContext, String pAssetPath) /* throws IOException */ {
            int soundID = pSoundManager.getSoundPool().load(pContext.getAssets().openFd(SoundFactory.sAssetBasePath + pAssetPath), 1);
            Sound sound = new Sound(pSoundManager, soundID);
            pSoundManager.add(sound);
            return sound;
        }

        public static Sound createSoundFromResource(SoundManager pSoundManager, Context pContext, int pSoundResID)
        {
            int soundID = pSoundManager.getSoundPool().load(pContext, pSoundResID, 1);
            Sound sound = new Sound(pSoundManager, soundID);
            pSoundManager.add(sound);
            return sound;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}