namespace andengine.examples {

using Engine = andengine.engine.Engine;
import org.anddev.andengine.engine.camera.Camera;
using EngineOptions = andengine.engine.options.EngineOptions;
using ScreenOrientation = andengine.engine.options.EngineOptions.ScreenOrientation;
using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
using Scene = andengine.entity.scene.Scene;
using IOnSceneTouchListener = andengine.entity.scene.Scene.IOnSceneTouchListener;
using ColorBackground = andengine.entity.scene.background.ColorBackground;
import org.anddev.andengine.entity.sprite.AnimatedSprite;
using FPSLogger = andengine.entity.util.FPSLogger;
using TouchEvent = andengine.input.touch.TouchEvent;
using Texture = andengine.opengl.texture.Texture;
using TextureOptions = andengine.opengl.texture.TextureOptions;
using TextureRegionFactory = andengine.opengl.texture.region.TextureRegionFactory;
import org.anddev.andengine.opengl.texture.region.TiledTextureRegion;

using Toast = Android.Widget.Toast;

/**
 * @author Nicolas Gramlich
 * @since 12:14:22 - 30.06.2010
 */
public class UpdateTextureExample : BaseExample {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final int CAMERA_WIDTH = 480;
	private static final int CAMERA_HEIGHT = 320;

	// ===========================================================
	// Fields
	// ===========================================================

	private Camera mCamera;
	private Texture mTexture;
	private TiledTextureRegion mFaceTextureRegion;

	private boolean mToggleBox = true;

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
		Toast.makeText(this, "Touch the screen to update (redraw) an existing Texture with every touch!", Toast.LENGTH_LONG).show();
		this.mCamera = new Camera(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);
		return new Engine(new EngineOptions(true, ScreenOrientation.LANDSCAPE, new RatioResolutionPolicy(CAMERA_WIDTH, CAMERA_HEIGHT), this.mCamera));
	}

	@Override
	public void onLoadResources() {
		this.mTexture = new Texture(64, 32, TextureOptions.BILINEAR_PREMULTIPLYALPHA);
		this.mFaceTextureRegion = TextureRegionFactory.createTiledFromAsset(this.mTexture, this, "gfx/face_box_tiled.png", 0, 0, 2, 1);

		this.mEngine.getTextureManager().loadTexture(this.mTexture);
	}

	@Override
	public Scene onLoadScene() {
		this.mEngine.registerUpdateHandler(new FPSLogger());

		final Scene scene = new Scene(1);
		scene.setBackground(new ColorBackground(0.09804f, 0.6274f, 0.8784f));

		/* Calculate the coordinates for the face, so its centered on the camera. */
		final int x = (CAMERA_WIDTH - this.mFaceTextureRegion.getTileWidth()) / 2;
		final int y = (CAMERA_HEIGHT - this.mFaceTextureRegion.getTileHeight()) / 2;

		/* Create the face and add it to the scene. */
		final AnimatedSprite face = new AnimatedSprite(x, y, this.mFaceTextureRegion);
		face.animate(100);
		scene.getLastChild().attachChild(face);

		scene.setOnSceneTouchListener(new IOnSceneTouchListener() {
			@Override
			public boolean onSceneTouchEvent(final Scene pScene, final TouchEvent pSceneTouchEvent) {
				if(pSceneTouchEvent.isActionDown()) {
					UpdateTextureExample.this.toggle();
				}
				return true;
			}
		});

		return scene;
	}

	@Override
	public void onLoadComplete() {

	}

	// ===========================================================
	// Methods
	// ===========================================================

	private void toggle() {
		this.mTexture.clearTextureSources();
		this.mToggleBox = !this.mToggleBox;
		TextureRegionFactory.createTiledFromAsset(this.mTexture, this, this.mToggleBox ? "gfx/face_box_tiled.png" : "gfx/face_circle_tiled.png", 0, 0, 2, 1);
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
