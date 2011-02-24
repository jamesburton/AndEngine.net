namespace andengine.util
{

    using Constants = andengine.util.constants.Constants;

    //import android.content.Context;
    //import android.content.SharedPreferences;
    //import android.content.SharedPreferences.Editor;
    using Context = Android.Content.Context;
    using SharedPreferences = Android.Content.ISharedPreferences;
    using Editor = Android.Content.ISharedPreferencesEditor;
    using System;

    /**
     * @author Nicolas Gramlich
     * @since 18:55:12 - 02.08.2010
     */
    public class SimplePreferences /*: Constants*/ {
        // ===========================================================
        // Constants
        // ===========================================================

        private static readonly String PREFERENCES_NAME = null;

        // ===========================================================
        // Fields
        // ===========================================================

        private static SharedPreferences INSTANCE;
        private static Editor EDITORINSTANCE;

        // ===========================================================
        // Constructors
        // ===========================================================

        public static SharedPreferences getInstance(Context ctx)
        {
            if (SimplePreferences.INSTANCE == null)
            {
                SimplePreferences.INSTANCE = ctx.GetSharedPreferences(SimplePreferences.PREFERENCES_NAME, /*Context.MODE_PRIVATE*/ Android.Content.FileCreationMode.Private);
            }
            return SimplePreferences.INSTANCE;
        }

        public static Editor getEditorInstance(Context ctx)
        {
            if (SimplePreferences.EDITORINSTANCE == null)
            {
                SimplePreferences.EDITORINSTANCE = SimplePreferences.getInstance(ctx).Edit();
            }
            return SimplePreferences.EDITORINSTANCE;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static int incrementAccessCount(Context pCtx, String pKey)
        {
            return SimplePreferences.incrementAccessCount(pCtx, pKey, 1);
        }

        public static int incrementAccessCount(Context pCtx, String pKey, int pIncrement)
        {
            SharedPreferences prefs = SimplePreferences.getInstance(pCtx);
            int accessCount = prefs.GetInt(pKey, 0);

            int newAccessCount = accessCount + pIncrement;
            prefs.Edit().PutInt(pKey, newAccessCount).Commit();

            return newAccessCount;
        }

        public static int getAccessCount(Context pCtx, String pKey)
        {
            return SimplePreferences.getInstance(pCtx).GetInt(pKey, 0);
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}