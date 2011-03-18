namespace andengine.ui.activity;

using andengine.opengl.view.RenderSurfaceView;

/**
 * @author Nicolas Gramlich
 * @since 10:18:50 - 06.10.2010
 */
public abstract class LayoutGameActivity extends BaseGameActivity {
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

	protected abstract int getLayoutID();
	protected abstract int getRenderSurfaceViewID();

	@Override
	protected void onSetContentView() {
		super.setContentView(this.getLayoutID());

		this.mRenderSurfaceView = (RenderSurfaceView) this.findViewById(this.getRenderSurfaceViewID());
		
		this.mRenderSurfaceView.setEGLConfigChooser(false);
		this.mRenderSurfaceView.setRenderer(this.mEngine);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
