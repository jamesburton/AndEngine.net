namespace andengine.audio.music
{

    using File = Java.IO.File;
    using FileInputStream = Java.IO.FileInputStream;
    using IOException = Java.IO.IOException;

    using Context = Android.Content.Context;
    using AssetFileDescriptor = Android.Content.Res.AssetFileDescriptor;
    using MediaPlayer = Android.Media.MediaPlayer;
    //using String = Java.Lang.String;
    //using Java.Lang;
    using String = System.String;
    using IllegalStateException = Java.Lang.IllegalStateException;

    /**
     * @author Nicolas Gramlich
     * @since 15:05:49 - 13.06.2010
     */
    public class MusicFactory
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private static String sAssetBasePath = string.Empty;

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
                MusicFactory.sAssetBasePath = pAssetBasePath;
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

        public static Music CreateMusicFromFile(MusicManager pMusicManager, Context pContext, File pFile) /* throws IOException */ {
            MediaPlayer mediaPlayer = new MediaPlayer();

            mediaPlayer.SetDataSource(new FileInputStream(pFile).FD);
            mediaPlayer.Prepare();

            Music music = new Music(pMusicManager, mediaPlayer);
            pMusicManager.Add(music);

            return music;
        }

        public static Music CreateMusicFromAsset(MusicManager pMusicManager, Context pContext, String pAssetPath) /*throws IOException */ {
            MediaPlayer mediaPlayer = new MediaPlayer();

            //AssetFileDescriptor assetFileDescritor = pContext.getAssets().openFd(MusicFactory.sAssetBasePath + pAssetPath);
            AssetFileDescriptor assetFileDescritor = pContext.Assets.OpenFd(MusicFactory.sAssetBasePath + pAssetPath);
            mediaPlayer.SetDataSource(assetFileDescritor.FileDescriptor, assetFileDescritor.StartOffset, assetFileDescritor.Length);
            mediaPlayer.Prepare();

            Music music = new Music(pMusicManager, mediaPlayer);
            pMusicManager.Add(music);

            return music;
        }

        public static Music createMusicFromResource(MusicManager pMusicManager, Context pContext, int pMusicResID) /* throws IOException */ {
            MediaPlayer mediaPlayer = MediaPlayer.Create(pContext, pMusicResID);
            mediaPlayer.Prepare();

            Music music = new Music(pMusicManager, mediaPlayer);
            pMusicManager.Add(music);

            return music;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}