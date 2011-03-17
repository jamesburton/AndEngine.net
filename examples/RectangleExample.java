namespace andengine.examples {

using Engine = andengine.engine.Engine;
import org.anddev.andengine.engine.camera.Camera;
using EngineOptions = andengine.engine.options.EngineOptions;
using ScreenOrientation = andengine.engine.options.EngineOptions.ScreenOrientation;
using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
import org.anddev.andengine.entity.Entity;
import org.anddev.andengine.entity.primitive.Rectangle;
using Scene = andengine.entity.scene.Scene;
using IOnSceneTouchListener = andengine.entity.scene.Scene.IOnSceneTouchListener;
using ColorBackground = andengine.entity.scene.background.ColorBackground;
using FPSLogger = andengine.entity.util.FPSLogger;
import org.anddev.andengine.entity.util.ScreenCapture;
import org.anddev.andengine.entity.util.ScreenCapture.IScreenCaptureCallback;
using TouchEvent = andengine.input.touch.TouchEvent;
import org.anddev.andengine.util.FileUtils;

using Toast = Android.Widget.Toast;

/**
 * @author Nicolas Gramlich
 * @since 11:54:51 - 03.04.2010
 */
public class RectangleExample : BaseExample {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final int CAMERA_WIDTH = 720;
	private static final int CAMERA_HEIGHT = 480;

	// ===========================================================
	// Fields
	// ===========================================================

	private Camera mCamera;

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public Engine onLoadEngine() {
		this.mCamera = new Camera(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);
		return new Engine(new EngineOptions(true, ScreenOrientation.LANDSCAPE, new RatioResolutionPolicy(CAMERA_WIDTH, CAMERA_HEIGHT), this.mCamera));
	}

	@Override
	public void onLoadResources() {

	}

	@Override
	public Scene onLoadScene() {
		this.mEngine.registerUpdateHandler(new FPSLogger());

		final Scene scene = new Scene(2);
		final ScreenCapture screenCapture = new ScreenCapture();
		scene.attachChild(screenCapture);
		scene.setOnSceneTouchListener(new IOnSceneTouchListener() {
			@Override
			public boolean onSceneTouchEvent(final Scene pScene, final TouchEvent pSceneTouchEvent) {
				if(pSceneTouchEvent.isActionDown()) {
					screenCapture.capture(180, 60, 360, 360, FileUtils.getAbsolutePathOnExternalStorage(RectangleExample.this, "Screen_" + System.currentTimeMillis() + ".png"), new IScreenCaptureCallback() {
						@Override
						public void onScreenCaptured(final String pFilePath) {
							RectangleExample.this.runOnUiThread(new Runnable() {
								@Override
								public void run() {
									Toast.makeText(RectangleExample.this, "Screenshot: " + pFilePath + " taken!", Toast.LENGTH_SHORT).show();
								}
							});
						}

						@Override
						public void onScreenCaptureFailed(final String pFilePath, final Exception pException) {
							RectangleExample.this.runOnUiThread(new Runnable() {
								@Override
								public void run() {
									Toast.makeText(RectangleExample.this, "FAILED capturing Screenshot: " + pFilePath + " !", Toast.LENGTH_SHORT).show();
								}
							});
						}
					});
				}
				return true;
			}
		});

		scene.setBackground(new ColorBackground(0, 0, 0));

		/* Create the rectangles. */
		final Rectangle rect1 = this.makeColoredRectangle(-180, -180, 1, 0, 0);
		final Rectangle rect2 = this.makeColoredRectangle(0, -180, 0, 1, 0);
		final Rectangle rect3 = this.makeColoredRectangle(0, 0, 0, 0, 1);
		final Rectangle rect4 = this.makeColoredRectangle(-180, 0, 1, 1, 0);

		final Entity rectangleGroup = new Entity(CAMERA_WIDTH / 2, CAMERA_HEIGHT / 2);

		rectangleGroup.attachChild(rect1);
		rectangleGroup.attachChild(rect2);
		rectangleGroup.attachChild(rect3);
		rectangleGroup.attachChild(rect4);

		scene.getFirstChild().attachChild(rectangleGroup);

		return scene;
	}

	private Rectangle makeColoredRectangle(final float pX, final float pY, final float pRed, final float pGreen, final float pBlue) {
		final Rectangle coloredRect = new Rectangle(pX, pY, 180, 180);
		coloredRect.setColor(pRed, pGreen, pBlue);
		return coloredRect;
	}

	@Override
	public void onLoadComplete() {

	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
