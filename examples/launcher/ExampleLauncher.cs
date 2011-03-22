using Android.Widget;

namespace andengine.examples.launcher
{

    //import java.util.Arrays;
    using Arrays = Java.Util.Arrays;

    // TODO: Re-add references as code is converted
    //using R = andengine.net.examples.Resource;
    using R = andengine.net.examples.Resource;
    //import org.anddev.andengine.util.Debug;
    using Debug = andengine.util.Debug;

    //using AlertDialog = Android.App.AlertDialog;
    using AlertDialog = Android.App.AlertDialog;
    //import android.app.Dialog;
    using Dialog = Android.App.Dialog;
    //import android.app.ExpandableListActivity;
    using ExpandableListActivity = Android.App.ExpandableListActivity;
    //import android.content.Context;
    using Context = Android.Content.Context;
    //import android.content.Intent;
    using Intent = Android.Content.Intent;
    using SharedPreferences = Android.Content.ISharedPreferences;
    //import android.content.pm.PackageInfo;
    using PackageInfo = Android.Content.PM.PackageInfo;
    //import android.content.pm.PackageManager;
    using PackageManager = Android.Content.PM.PackageManager;
    //import android.net.Uri;
    using Uri = Android.Net.Uri;
    //import android.os.Bundle;
    using Bundle = Android.OS.Bundle;
    //import android.view.View;
    using View = Android.Views.View;
    //import android.view.View.OnClickListener;
    using OnClickListener = Android.Views.View.IOnClickListener;
    //import android.widget.ExpandableListView;
    using ExpandableListView = Android.Widget.ExpandableListView;
    using Toast = Android.Widget.Toast;

    using String = System.String;
    using FileCreationMode = Android.Content.FileCreationMode;
    using StringBuilder = System.Text.StringBuilder;
    using Math = System.Math;

    /**
     * @author Nicolas Gramlich
     * @since 22:56:46 - 16.06.2010
     */
    public class ExampleLauncher : ExpandableListActivity
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly String PREF_LAST_APP_LAUNCH_VERSIONCODE_ID = "last.app.launch.versioncode";

        private static /* final */ readonly int DIALOG_FIRST_APP_LAUNCH = 0;
        private static /* final */ readonly int DIALOG_NEW_IN_THIS_VERSION = DIALOG_FIRST_APP_LAUNCH + 1;
        private static /* final */ readonly int DIALOG_BENCHMARKS_SUBMIT_PLEASE = DIALOG_NEW_IN_THIS_VERSION + 1;

        // ===========================================================
        // Fields
        // ===========================================================

        private ExpandableExampleLauncherListAdapter mExpandableExampleLauncherListAdapter;

        private int mVersionCodeCurrent;

        private int mVersionCodeLastLaunch;

        // ===========================================================
        // Constructors
        // ===========================================================

        public void OnCreate(/* final */ Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            
            this.SetContentView(R.Layout.list_examples);

            this.mExpandableExampleLauncherListAdapter = new ExpandableExampleLauncherListAdapter(this);

            this.SetListAdapter(this.mExpandableExampleLauncherListAdapter);

            /* TODO: Consider re-adding this if we 
            this.findViewById(R.id.btn_get_involved).setOnClickListener(new OnClickListener() {
                @Override
                public void onClick(final View pView) {
                    ExampleLauncher.this.startActivity(new Intent(Intent.ACTION_VIEW, Uri.parse("http://www.andengine.org")));
                }
            });
            */

            /* final */
            SharedPreferences prefs = this.GetPreferences(FileCreationMode.Private);

            this.mVersionCodeCurrent = this.GetVersionCode();
            this.mVersionCodeLastLaunch = prefs.GetInt(PREF_LAST_APP_LAUNCH_VERSIONCODE_ID, -1);

            if (this.IsFirstTime("first.app.launch"))
            {
                this.ShowDialog(DIALOG_FIRST_APP_LAUNCH);
            }
            else if (this.mVersionCodeLastLaunch != -1 && this.mVersionCodeLastLaunch < this.mVersionCodeCurrent)
            {
                this.ShowDialog(DIALOG_NEW_IN_THIS_VERSION);
            }
            else if (IsFirstTime("please.submit.benchmarks"))
            {
                this.ShowDialog(DIALOG_BENCHMARKS_SUBMIT_PLEASE);
            }

            prefs.Edit().PutInt(PREF_LAST_APP_LAUNCH_VERSIONCODE_ID, this.mVersionCodeCurrent).Commit();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        protected override Dialog OnCreateDialog(/* final */ int pId) {
		//switch(pId) {
			//case DIALOG_FIRST_APP_LAUNCH:
            if(pId == DIALOG_FIRST_APP_LAUNCH) {
				return new AlertDialog.Builder(this)
					.SetTitle( R.String.dialog_first_app_launch_title)
					.SetMessage(R.String.dialog_first_app_launch_message)
					.SetIcon(Android.Resource.Drawable.IcDialogInfo)
					.SetPositiveButton(R.String.ok, null)
					.Create();
			//case DIALOG_BENCHMARKS_SUBMIT_PLEASE:
            } else if(pId == DIALOG_BENCHMARKS_SUBMIT_PLEASE) {
				return new AlertDialog.Builder(this)
					.SetTitle(R.String.dialog_benchmarks_submit_please_title)
					.SetMessage(R.String.dialog_benchmarks_submit_please_message)
					.SetIcon(Android.Resource.Drawable.IcDialogInfo)
					.SetPositiveButton(Android.Resource.String.Ok, null)
					.Create();
			//case DIALOG_NEW_IN_THIS_VERSION:
            } else if(pId == DIALOG_NEW_IN_THIS_VERSION) {
				/* final */ int[] versionCodes = this.Resources.GetIntArray(R.Array.new_in_version_versioncode);
				/* final */ int versionDescriptionsStartIndex = Math.Max(0, Arrays.BinarySearch(versionCodes, this.mVersionCodeLastLaunch) + 1);
				
				/* final */ String[] versionDescriptions = this.Resources.GetStringArray(R.Array.new_in_version_changes);
				
				/* final */ StringBuilder sb = new StringBuilder();
				for(int i = versionDescriptions.Length - 1; i >= versionDescriptionsStartIndex; i--) {
					sb.Append("--------------------------\n");
					sb.Append(">>>  Version: " + versionCodes[i] + "\n");
					sb.Append("--------------------------\n");
					sb.Append(versionDescriptions[i]);
					
					if(i > versionDescriptionsStartIndex){
						sb.Append("\n\n");
					}
				}
				
				return new AlertDialog.Builder(this)
					.SetTitle(R.String.dialog_new_in_this_version_title)
					.SetMessage(sb.ToString())
					.SetIcon(Android.R.Drawable.ic_dialog_info)
					.SetPositiveButton(Android.R.String.ok, null)
					.Create();
			//default:
            } else {
				return base.OnCreateDialog(pId);
		}
	}

        public override void OnGroupExpand(int pGroupPosition)
        {
            if(this.mExpandableExampleLauncherListAdapter.getGroup(pGroupPosition) == ExampleGroup.BENCHMARK)
            {
                Toast.MakeText(this, "When running a benchmark, a dialog with the results will appear after some seconds.",ToastLength.Short).Show();
            }
            base.OnGroupExpand(pGroupPosition);
        }

        public bool OnChildClick(/* final */ ExpandableListView pParent, /* final */ View pV, /* final */ int pGroupPosition, /* final */ int pChildPosition, /* final */ long pId)
        {
            /* final */
            Example example = this.mExpandableExampleLauncherListAdapter.getChild(pGroupPosition, pChildPosition);

            this.StartActivity(new Intent(this, example.ExampleType));

            return base.OnChildClick(pParent, pV, pGroupPosition, pChildPosition, pId);
        }

        // ===========================================================
        // Methods
        // ===========================================================

        public bool IsFirstTime(/* final */ String pKey)
        {
            /* final */
            SharedPreferences prefs = this.GetPreferences(FileCreationMode.Private);
            if (prefs.GetBoolean(pKey, true))
            {
                prefs.Edit().PutBoolean(pKey, false).Commit();
                return true;
            }
            return false;
        }

        public int GetVersionCode()
        {
            try
            {
                /* final */
                PackageInfo pi = this.PackageManager.GetPackageInfo(this.PackageName, 0);
                return pi.VersionCode;
            }
            catch (/* final */ PackageManager.NameNotFoundException e)
            {
                Debug.E("Package name not found", e);
                return -1;
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}