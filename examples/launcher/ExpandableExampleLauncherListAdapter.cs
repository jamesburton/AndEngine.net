using Android.Content;
using Android.Views;
using Android.Widget;
using Java.Lang;
using R = andengine.net.examples.Resource;

namespace andengine.examples.launcher {
/*import android.content.Context;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.BaseExpandableListAdapter;
import android.widget.TextView;*/

/**
 * @author Nicolas Gramlich
 * @since 20:43:54 - 16.06.2010
 */
public class ExpandableExampleLauncherListAdapter : BaseExpandableListAdapter {
	// ===========================================================
	// Constants
	// ===========================================================

	private static ExampleGroup[] EXAMPLEGROUPS = {
		ExampleGroup.SIMPLE,
		/*ExampleGroup.MODIFIER_AND_ANIMATION,
		ExampleGroup.TOUCH,
		ExampleGroup.PARTICLESYSTEM,
		ExampleGroup.MULTIPLAYER,
		ExampleGroup.PHYSICS,
		ExampleGroup.TEXT,
		ExampleGroup.AUDIO,
		ExampleGroup.ADVANCED,
		ExampleGroup.BACKGROUND,
		ExampleGroup.OTHER,
		ExampleGroup.APP,
		ExampleGroup.GAME,
		ExampleGroup.BENCHMARK*/
	};

	// ===========================================================
	// Fields
	// ===========================================================

	private Context mContext;

	// ===========================================================
	// Constructors
	// ===========================================================

	public ExpandableExampleLauncherListAdapter(Context pContext) {
		this.mContext = pContext;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================


    public override Object GetChild(int pGroupPosition, int pChildPosition)
    {
		return EXAMPLEGROUPS[pGroupPosition].EXAMPLES[pChildPosition];
	}


    public override long GetChildId(int pGroupPosition, int pChildPosition)
    {
		return pChildPosition;
	}


    public override int GetChildrenCount(int pGroupPosition)
    {
		return EXAMPLEGROUPS[pGroupPosition].EXAMPLES.Length;
	}

	
	public override  View GetChildView(int pGroupPosition, int pChildPosition, bool pIsLastChild, View pConvertView, ViewGroup pParent) {
		View childView;
		if (pConvertView != null){
			childView = pConvertView;
		}else{
			childView = LayoutInflater.From(this.mContext).Inflate(R.Layout.listrow_example, null);
		}

		((TextView)childView.FindViewById(R.Id.tv_listrow_example_name)).SetText(((Example)this.GetChild(pGroupPosition, pChildPosition)).NAMERESID);
		return childView;
	}


    public override View GetGroupView(int pGroupPosition, bool pIsExpanded, View pConvertView, ViewGroup pParent)
    {
		View groupView;
		if (pConvertView != null){
			groupView = pConvertView;
		}else{
			groupView = LayoutInflater.From(this.mContext).Inflate(R.Layout.listrow_examplegroup, null);
		}

		((TextView)groupView.FindViewById(R.Id.tv_listrow_examplegroup_name)).SetText(((ExampleGroup)this.GetGroup(pGroupPosition)).NAMERESID);
		return groupView;
	}


    public override Object GetGroup(int pGroupPosition)
    {
		return EXAMPLEGROUPS[pGroupPosition];
	}


    public override int GroupCount
    {
        get { return EXAMPLEGROUPS.Length; }
	}


    public override long GetGroupId(int pGroupPosition)
    {
		return pGroupPosition;
	}


    public override bool IsChildSelectable(int pGroupPosition, int pChildPosition)
    {
		return true;
	}

	
	public override bool HasStableIds {
        get { return true; }
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}