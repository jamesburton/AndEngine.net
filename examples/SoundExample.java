namespace andengine.examples {

import java.io.IOException;

import org.anddev.andengine.audio.sound.Sound;
import org.anddev.andengine.audio.sound.SoundFactory;
using Engine = andengine.engine.Engine;
import org.anddev.andengine.engine.camera.Camera;
using EngineOptions = andengine.engine.options.EngineOptions;
using ScreenOrientation = andengine.engine.options.EngineOptions.ScreenOrientation;
using RatioResolutionPolicy = andengine.engine.options.resolutionpolicy.RatioResolutionPolicy;
using Scene = andengine.entity.scene.Scene;
import org.anddev.andengine.entity.scene.Scene.IOnAreaTouchListener;
import org.anddev.andengine.entity.scene.Scene.ITouchArea;
using ColorBackground = andengine.entity.scene.background.ColorBackground;
using Sprite = andengine.entity.sprite.Sprite;
using FPSLogger = andengine.entity.util.FPSLogger;
using TouchEvent = andengine.input.touch.TouchEvent;
using Texture = andengine.opengl.texture.Texture;
using TextureOptions = andengine.opengl.texture.TextureOptions;
using TextureRegion = andengine.opengl.texture.region.TextureRegion;
using TextureRegionFactory = andengine.opengl.texture.region.TextureRegionFactory;
import org.anddev.andengine.util.Debug;

using Toast = Android.Widget.Toast;

/**
 * @author Nicolas Gramlich
 * @since 11:54:51 - 11.06.2010
 */
public class SoundExample : BaseExample {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final int CAMERA_WIDTH = 720;
	private static final int CAMERA_HEIGHT = 480;

	// ===========================================================
	// Fields
	// ===========================================================

	private Texture mTexture;
	private TextureRegion mTankTextureRegion;

	private Sound mExplosionSound;

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
		Toast.makeText(this, "Touch the tank to hear an explosion sound.", Toast.LENGTH_LONG).show();
		final Camera camera = new Camera(0, 0, CAMERA_WIDTH, CAMERA_HEIGHT);
		return new Engine(new EngineOptions(true, ScreenOrientation.LANDSCAPE, new RatioResolutionPolicy(CAMERA_WIDTH, CAMERA_HEIGHT), camera).setNeedsSound(true));
	}

	@Override
	public void onLoadResources() {
		this.mTexture = new Texture(128, 256, TextureOptions.BILINEAR_PREMULTIPLYALPHA);
		TextureRegionFactory.setAssetBasePath("gfx/");
		this.mTankTextureRegion = TextureRegionFactory.createFromAsset(this.mTexture, this, "tank.png", 0, 0);

		SoundFactory.setAssetBasePath("mfx/");
		try {
			this.mExplosionSound = SoundFactory.createSoundFromAsset(this.mEngine.getSoundManager(), this, "explosion.ogg");
		} catch (final IOException e) {
			Debug.e(e);
		}

		this.mEngine.getTextureManager().loadTexture(this.mTexture);
	}

	@Override
	public Scene onLoadScene() {
		this.mEngine.registerUpdateHandler(new FPSLogger());

		final Scene scene = new Scene(1);
		scene.setBackground(new ColorBackground(0.09804f, 0.6274f, 0.8784f));

		final int x = (CAMERA_WIDTH - this.mTankTextureRegion.getWidth()) / 2;
		final int y = (CAMERA_HEIGHT - this.mTankTextureRegion.getHeight()) / 2;
		final Sprite tank = new Sprite(x, y, this.mTankTextureRegion);
		scene.getLastChild().attachChild(tank);

		scene.registerTouchArea(tank);
		scene.setOnAreaTouchListener(new IOnAreaTouchListener() {
			@Override
			public boolean onAreaTouched(final TouchEvent pSceneTouchEvent, final ITouchArea pTouchArea, final float pTouchAreaLocalX, final float pTouchAreaLocalY) {
				if(pSceneTouchEvent.isActionDown()) {
					SoundExample.this.mExplosionSound.play();
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

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
