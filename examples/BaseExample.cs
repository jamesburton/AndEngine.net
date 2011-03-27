using System;
using andengine.ui.activity;
using Android.Views;

namespace andengine.examples
{

    /*import org.anddev.andengine.ui.activity.BaseGameActivity;

    import android.view.Menu;
    import android.view.MenuItem;*/
    using MenuConsts = Android.Views.MenuConsts;

    /**
     * @author Nicolas Gramlich
     * @since 22:10:28 - 11.04.2010
     */
    public abstract class BaseExample : BaseGameActivity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static int MENU_TRACE = MenuConsts.First;

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


        public bool OnCreateOptionsMenu(IMenu pMenu)
        {
            //pMenu.Add(MenuConsts.None, MENU_TRACE, MenuConsts.None, "Start Method Tracing");
            //pMenu.Add(MenuConsts.None, MENU_TRACE, MenuConsts.None, (Java.Lang.ICharSequence)"Start Method Tracing");
            pMenu.Add(MenuConsts.None, MENU_TRACE, MenuConsts.None, (Java.Lang.ICharSequence) new Android.Text.SpannableString("Start Method Tracing"));
            return base.OnCreateOptionsMenu(pMenu);
        }


        public bool OnPrepareOptionsMenu(IMenu pMenu)
        {
            //pMenu.FindItem(MENU_TRACE).SetTitle(this.mEngine.MethodTracing ? "Stop Method Tracing" : "Start Method Tracing");
            pMenu.FindItem(MENU_TRACE).SetTitle(this.mEngine.MethodTracing ? (Java.Lang.ICharSequence) new Android.Text.SpannableString("Stop Method Tracing") : (Java.Lang.ICharSequence) new Android.Text.SpannableString("Start Method Tracing"));
            return base.OnPrepareOptionsMenu(pMenu);
        }


        public bool OnMenuItemSelected(int pFeatureId, IMenuItem pItem)
        {
            if (pItem.ItemId == MENU_TRACE)
            {
                if (this.mEngine.MethodTracing)
                {
                    this.mEngine.StopMethodTracing();
                }
                else
                {
                    this.mEngine.StartMethodTracing("AndEngine_" + DateTime.Now.Ticks + ".trace");
                }
                return true;
            }
            else
            {
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