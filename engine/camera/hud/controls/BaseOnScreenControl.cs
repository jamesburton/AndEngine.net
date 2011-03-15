namespace andengine.engine.camera.hud.controls
{

//import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    using Constants = andengine.util.constants.Constants;
//import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;

using Camera = andengine.engine.camera.Camera;
using HUD = andengine.engine.camera.hud.HUD;
using ITimerCallback = andengine.engine.handler.timer.ITimerCallback;
using TimerHandler = andengine.engine.handler.timer.TimerHandler;
using ILayer = andengine.entity.layer.ILayer;
using Scene = andengine.entity.scene.Scene;
using IOnSceneTouchListener = andengine.entity.scene.Scene.IOnSceneTouchListener;
using Sprite = andengine.entity.sprite.Sprite;
using TouchEvent = andengine.input.touch.TouchEvent;
using TextureRegion = andengine.opengl.texture.region.TextureRegion;
using MathUtils = andengine.util.MathUtils;

using MotioEvent = Android.Views.MotionEvent;

/**
 * @author Nicolas Gramlich
 * @since 10:43:09 - 11.07.2010
 */
public abstract class BaseOnScreenControl : HUD, IOnSceneTouchListener {
	// ===========================================================
	// Constants
	// ===========================================================

	private static /* final */ readonly int INVALID_POINTER_ID = -1;

	// ===========================================================
	// Fields
	// ===========================================================

	private /* final */ readonly Sprite mControlBase;
	private /* final */ readonly Sprite mControlKnob;

	private float mControlValueX;
	private float mControlValueY;

	private /* final */ readonly IOnScreenControlListener mOnScreenControlListener;

	private int mActivePointerID = INVALID_POINTER_ID;

	// ===========================================================
	// Constructors
	// ===========================================================

	public BaseOnScreenControl(/* final */ int pX, /* final */ int pY, /* final */ Camera pCamera, /* final */ TextureRegion pControlBaseTextureRegion, /* final */ TextureRegion pControlKnobTextureRegion, /* final */ float pTimeBetweenUpdates, /* final */ IOnScreenControlListener pOnScreenControlListener) {
		this.SetCamera(pCamera);

		this.mOnScreenControlListener = pOnScreenControlListener;
		/* Create the control base. */
        
		this.mControlBase = new Sprite(pX, pY, pControlBaseTextureRegion) {
			public override bool OnAreaTouched(/* final */ TouchEvent pSceneTouchEvent, /* final */ float pTouchAreaLocalX, /* final */ float pTouchAreaLocalY) {
				return BaseOnScreenControl.this.OnHandleControlBaseTouched(pSceneTouchEvent, pTouchAreaLocalX, pTouchAreaLocalY);
			}
		};

		/* Create the control knob. */
		this.mControlKnob = new Sprite(0, 0, pControlKnobTextureRegion);
		this.onHandleControlKnobReleased();

		/* Register listeners and add objects to this HUD. */
		this.setOnSceneTouchListener(this);
		this.registerTouchArea(this.mControlBase);
		this.registerUpdateHandler(new TimerHandler(pTimeBetweenUpdates, true, new ITimerCallback() {
			public override void OnTimePassed(final TimerHandler pTimerHandler) {
				BaseOnScreenControl.this.mOnScreenControlListener.onControlChange(BaseOnScreenControl.this, BaseOnScreenControl.this.mControlValueX, BaseOnScreenControl.this.mControlValueY);
			}
		}));

		/* final */ ILayer bottomLayer = this.getBottomLayer();
		bottomLayer.AddEntity(this.mControlBase);
		bottomLayer.AddEntity(this.mControlKnob);

		this.SetTouchAreaBindingEnabled(true);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public Sprite GetControlBase() {
		return this.mControlBase;
	}
    public Sprite ControlBase { get { GetControlBase(); } }

	public Sprite GetControlKnob() {
		return this.mControlKnob;
	}
    public Sprite ControlKnob { get { return GetControlKnob(); } }

	public IOnScreenControlListener GetOnScreenControlListener() {
		return this.mOnScreenControlListener;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public boolean onSceneTouchEvent(final Scene pScene, final TouchEvent pSceneTouchEvent) {
		final int pointerID = pSceneTouchEvent.getPointerID();
		if(pointerID == this.mActivePointerID) {
			this.onHandleControlBaseLeft();

			switch(pSceneTouchEvent.getAction()) {
				case MotionEvent.ACTION_UP:
				case MotionEvent.ACTION_CANCEL:
					this.mActivePointerID = INVALID_POINTER_ID;
			}
		}
		return false;
	}

	// ===========================================================
	// Methods
	// ===========================================================

	public void refreshControlKnobPosition() {
		this.onUpdateControlKnob(this.mControlValueX * 0.5f, this.mControlValueY * 0.5f);
	}

	/**
	 *  When the touch happened outside of the bounds of this OnScreenControl.
	 * */
	protected void onHandleControlBaseLeft() {
		this.onUpdateControlKnob(0, 0);
	}

	/**
	 * When the OnScreenControl was released.
	 */
	protected void onHandleControlKnobReleased() {
		this.onUpdateControlKnob(0, 0);
	}

	private void updateControlKnob(final TouchEvent pSceneTouchEvent, final float pTouchAreaLocalX, final float pTouchAreaLocalY) {
		final Sprite controlBase = this.mControlBase;

		final float relativeX = MathUtils.bringToBounds(0, controlBase.getWidth(), pTouchAreaLocalX) / controlBase.getWidth() - 0.5f;
		final float relativeY = MathUtils.bringToBounds(0, controlBase.getHeight(), pTouchAreaLocalY) / controlBase.getHeight() - 0.5f;

		this.onUpdateControlKnob(relativeX, relativeY);
	}

	/**
	 * @param pRelativeX from <code>-0.5</code> (left) to <code>0.5</code> (right).
	 * @param pRelativeY from <code>-0.5</code> (top) to <code>0.5</code> (bottom).
	 */
	protected void onUpdateControlKnob(final float pRelativeX, final float pRelativeY) {
		final Sprite controlBase = this.mControlBase;
		final Sprite controlKnob = this.mControlKnob;

		this.mControlValueX = 2 * pRelativeX;
		this.mControlValueY = 2 * pRelativeY;

		final float[] controlBaseSceneCenterCoordinates = controlBase.getSceneCenterCoordinates();
		final float x = controlBaseSceneCenterCoordinates[VERTEX_INDEX_X] - controlKnob.getWidth() * 0.5f + pRelativeX * controlBase.getWidthScaled();
		final float y = controlBaseSceneCenterCoordinates[VERTEX_INDEX_Y] - controlKnob.getHeight() * 0.5f + pRelativeY * controlBase.getHeightScaled();

		controlKnob.setPosition(x, y);
	}

	protected boolean onHandleControlBaseTouched(final TouchEvent pSceneTouchEvent, final float pTouchAreaLocalX, final float pTouchAreaLocalY) {
		final int pointerID = pSceneTouchEvent.getPointerID();

		switch(pSceneTouchEvent.getAction()) {
			case MotionEvent.ACTION_DOWN:
				if(this.mActivePointerID == INVALID_POINTER_ID) {
					this.mActivePointerID = pointerID;
					this.updateControlKnob(pSceneTouchEvent, pTouchAreaLocalX, pTouchAreaLocalY);
					return true;
				}
				break;
			case MotionEvent.ACTION_UP:
			case MotionEvent.ACTION_CANCEL:
				if(this.mActivePointerID == pointerID) {
					this.mActivePointerID = INVALID_POINTER_ID;
					this.onHandleControlKnobReleased();
					return true;
				}
				break;
			default:
				if(this.mActivePointerID == pointerID) {
					this.updateControlKnob(pSceneTouchEvent, pTouchAreaLocalX, pTouchAreaLocalY);
					return true;
				}
				break;
		}
		return true;
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

	public static interface IOnScreenControlListener {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================

		/**
		 * @param pBaseOnScreenControl
		 * @param pValueX between <code>-1</code> (left) to <code>1</code> (right).
		 * @param pValueY between <code>-1</code> (up) to <code>1</code> (down).
		 */
		public void onControlChange(final BaseOnScreenControl pBaseOnScreenControl, final float pValueX, final float pValueY);
	}
}
}