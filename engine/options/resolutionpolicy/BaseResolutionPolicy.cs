using System;
using andengine.opengl.view;
using Android.Views;

namespace andengine.engine.options.resolutionpolicy
{

/**
 * @author Nicolas Gramlich
 * @since 22:46:43 - 06.10.2010
 */
public abstract class BaseResolutionPolicy : IResolutionPolicy {
	// ===========================================================
	// Constants
	// ===========================================================

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

	protected static void throwOnNotMeasureSpecEXACTLY(int pWidthMeasureSpec, int pHeightMeasureSpec) {
		MeasureSpecMode specWidthMode = View.MeasureSpec.GetMode(pWidthMeasureSpec);
		MeasureSpecMode specHeightMode = View.MeasureSpec.GetMode(pHeightMeasureSpec);

		if (specWidthMode != MeasureSpecMode.Exactly || specHeightMode != MeasureSpecMode.Exactly) {
			throw new InvalidOperationException("This IResolutionPolicy requires MeasureSpec.EXACTLY ! That means ");
		}
	}

    public abstract void OnMeasure(RenderSurfaceView pRenderSurfaceView, int pWidthMeasureSpec, int pHeightMeasureSpec);
    
    // ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}