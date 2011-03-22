using System;
using andengine.ui.activity;
using Android.Views;

namespace andengine.examples
{

    /*import org.anddev.andengine.ui.activity.BaseGameActivity;

    import android.view.Menu;
    import android.view.MenuItem;*/

    /**
     * @author Nicolas Gramlich
     * @since 22:10:28 - 11.04.2010
     */
    public abstract class BaseExample : BaseGameActivity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static int MENU_TRACE = Menu.FIRST;

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================


        public bool onCreateOptionsMenu(IMenu pMenu)
        {
            pMenu.Add(Menu.NONE, MENU_TRACE, Menu.NONE, "Start Method Tracing");
            return base.OnCreateOptionsMenu(pMenu);
        }


        public bool onPrepareOptionsMenu(IMenu pMenu)
        {
            pMenu.FindItem(MENU_TRACE).SetTitle(this.mEngine.IsMethodTracing() ? "Stop Method Tracing" : "Start Method Tracing");
            return base.OnPrepareOptionsMenu(pMenu);
        }


        public bool onMenuItemSelected(int pFeatureId, IMenuItem pItem)
        {
            switch (pItem.ItemId)
            {
                case MENU_TRACE:
                    if (this.mEngine.IsMethodTracing())
                    {
                        this.mEngine.StopMethodTracing();
                    }
                    else
                    {
                        this.mEngine.StartMethodTracing("AndEngine_" + DateTime.Now.Ticks + ".trace");
                    }
                    return true;
                default:
                    return base.OnMenuItemSelected(pFeatureId, pItem);
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