namespace andengine.examples {

using Engine = andengine.engine.Engine;
import org.anddev.andengine.engine.camera.Camera;
using EngineOptions = andengine.engine.options.EngineOptions;
using ScreenOrientation = andengine.engine.options.EngineOptions.ScreenOrientation;
using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
using IEntity = andengine.entity.IEntity;
using Scene = andengine.entity.scene.Scene;
using ColorBackground = andengine.entity.scene.background.ColorBackground;
using Sprite = andengine.entity.sprite.Sprite;
using FPSLogger = andengine.entity.util.FPSLogger;
using Texture = andengine.opengl.texture.Texture;
import org.anddev.andengine.opengl.texture.Texture.ITextureStateListener;
using TextureOptions = andengine.opengl.texture.TextureOptions;
using TextureRegion = andengine.opengl.texture.region.TextureRegion;
using TextureRegionFactory = andengine.opengl.texture.region.TextureRegionFactory;
import org.anddev.andengine.opengl.texture.source.ITextureSource;

using Toast = Android.Widget.Toast;

/**
 * @author Nicolas Gramlich
 * @since 11:54:51 - 03.04.2010
 */
public class ImageFormatsExample : BaseExample {
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
	private TextureRegion mPngTextureRegion;
	private TextureRegion mJpgTextureRegion;
	private TextureRegion mGifTextureRegion;
	private TextureRegion mBmpTextureRegion;

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
		Toast.makeText(this, "GIF is not supported yet. Use PNG instead, it's the better format anyway!", Toast.LENGTH_LONG).show();
		this.mCamera = new Camera(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);
		return new Engine(new EngineOptions(true, ScreenOrientation.LANDSCAPE, new RatioResolutionPolicy(CAMERA_WIDTH, CAMERA_HEIGHT), this.mCamera));
	}

	@Override
	public void onLoadResources() {
		this.mTexture = new Texture(128, 128, TextureOptions.BILINEAR_PREMULTIPLYALPHA, new ITextureStateListener.TextureStateAdapter() {
			@Override
			public void onTextureSourceLoadExeption(final Texture pTexture, final ITextureSource pTextureSource, final Throwable pThrowable) {
				ImageFormatsExample.this.runOnUiThread(new Runnable() {
					@Override
					public void run() {
						Toast.makeText(ImageFormatsExample.this, "Failed loading TextureSource: " + pTextureSource.toString(), Toast.LENGTH_LONG).show();
					}
				});
			}
		});

		this.mPngTextureRegion = TextureRegionFactory.createFromAsset(this.mTexture, this, "gfx/imageformat_png.png", 0, 0);
		this.mJpgTextureRegion = TextureRegionFactory.createFromAsset(this.mTexture, this, "gfx/imageformat_jpg.jpg", 49, 0);
		this.mGifTextureRegion = TextureRegionFactory.createFromAsset(this.mTexture, this, "gfx/imageformat_gif.gif", 0, 49);
		this.mBmpTextureRegion = TextureRegionFactory.createFromAsset(this.mTexture, this, "gfx/imageformat_bmp.bmp", 49, 49);

		this.mEngine.getTextureManager().loadTexture(this.mTexture);
	}

	@Override
	public Scene onLoadScene() {
		this.mEngine.registerUpdateHandler(new FPSLogger());

		final Scene scene = new Scene(1);
		scene.setBackground(new ColorBackground(0.09804f, 0.6274f, 0.8784f));

		/* Create the icons and add them to the scene. */
		final IEntity lastChild = scene.getLastChild();

		lastChild.attachChild(new Sprite(160 - 24, 106 - 24, this.mPngTextureRegion));
		lastChild.attachChild(new Sprite(160 - 24, 213 - 24, this.mJpgTextureRegion));
		lastChild.attachChild(new Sprite(320 - 24, 106 - 24, this.mGifTextureRegion));
		lastChild.attachChild(new Sprite(320 - 24, 213 - 24, this.mBmpTextureRegion));

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
