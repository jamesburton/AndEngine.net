using System;
using andengine.opengl.view;
using Android.Views;

namespace andengine.engine.options.resolutionpolicy {

/**
 * @author Nicolas Gramlich
 * @since 11:23:00 - 29.03.2010
 */
public class RatioResolutionPolicy : BaseResolutionPolicy {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private float mRatio;

	// ===========================================================
	// Constructors
	// ===========================================================

	public RatioResolutionPolicy(float pRatio) {
		this.mRatio = pRatio;
	}

	public RatioResolutionPolicy(float pWidthRatio, float pHeightRatio) {
		this.mRatio = pWidthRatio / pHeightRatio;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	public override void OnMeasure(RenderSurfaceView pRenderSurfaceView, int pWidthMeasureSpec, int pHeightMeasureSpec) {
		BaseResolutionPolicy.throwOnNotMeasureSpecEXACTLY(pWidthMeasureSpec, pHeightMeasureSpec);

		int specWidth = View.MeasureSpec.GetSize(pWidthMeasureSpec);
		int specHeight = View.MeasureSpec.GetSize(pHeightMeasureSpec);

		float desiredRatio = this.mRatio;
		float realRatio = (float)specWidth / specHeight;

		int measuredWidth;
		int measuredHeight;
		if(realRatio < desiredRatio) {
			measuredWidth = specWidth;
			measuredHeight = (int) Math.Round(measuredWidth / desiredRatio);
		} else {
			measuredHeight = specHeight;
			measuredWidth = (int) Math.Round(measuredHeight * desiredRatio);
		}

		pRenderSurfaceView.SetMeasuredDimensionProxy(measuredWidth, measuredHeight);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}