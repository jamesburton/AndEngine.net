namespace andengine.examples {

import java.util.Random;

using Engine = andengine.engine.Engine;
import org.anddev.andengine.engine.camera.Camera;
using EngineOptions = andengine.engine.options.EngineOptions;
using ScreenOrientation = andengine.engine.options.EngineOptions.ScreenOrientation;
using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
import org.anddev.andengine.entity.primitive.Line;
using Scene = andengine.entity.scene.Scene;
using ColorBackground = andengine.entity.scene.background.ColorBackground;
using FPSLogger = andengine.entity.util.FPSLogger;

/**
 * @author Nicolas Gramlich
 * @since 11:54:51 - 03.04.2010
 */
public class LineExample : BaseExample {
	// ===========================================================
	// Constants
	// ===========================================================

	/* Initializing the Random generator produces a comparable result over different versions. */
	private static final long RANDOM_SEED = 1234567890;

	private static final int CAMERA_WIDTH = 720;
	private static final int CAMERA_HEIGHT = 480;

	private static final int LINE_COUNT = 100;

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

		final Scene scene = new Scene(1);
		scene.setBackground(new ColorBackground(0.09804f, 0.6274f, 0.8784f));

		final Random random = new Random(RANDOM_SEED);

		for(int i = 0; i < LINE_COUNT; i++) {
			final float x1 = random.nextFloat() * CAMERA_WIDTH;
			final float x2 = random.nextFloat() * CAMERA_WIDTH;
			final float y1 = random.nextFloat() * CAMERA_HEIGHT;
			final float y2 = random.nextFloat() * CAMERA_HEIGHT;
			final float lineWidth = random.nextFloat() * 5;

			final Line line = new Line(x1, y1, x2, y2, lineWidth);

			line.setColor(random.nextFloat(), random.nextFloat(), random.nextFloat());

			scene.getLastChild().attachChild(line);
		}

		return scene;
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
