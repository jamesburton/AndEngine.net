package org.anddev.andengine.entity.scene;

using andengine.engine.camera.Camera;
using andengine.entity.shape.modifier.ScaleModifier;
using andengine.entity.sprite.Sprite;
using andengine.opengl.texture.region.TextureRegion;
using andengine.util.modifier.ease.IEaseFunction;

/**
 * @author Nicolas Gramlich
 * @since 09:45:02 - 03.05.2010
 */
public class SplashScene extends Scene {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public SplashScene(final Camera pCamera, final TextureRegion pTextureRegion) {
		this(pCamera, pTextureRegion, -1, 1, 1);
	}

	public SplashScene(final Camera pCamera, final TextureRegion pTextureRegion, final float pDuration, final float pScaleFrom, final float pScaleTo) {
		super(1);

		final Sprite loadingScreenSprite = new Sprite(pCamera.getMinX(), pCamera.getMinY(), pCamera.getWidth(), pCamera.getHeight(), pTextureRegion);
		if(pScaleFrom != 1 || pScaleTo != 1) {
			loadingScreenSprite.setScale(pScaleFrom);
			loadingScreenSprite.addShapeModifier(new ScaleModifier(pDuration, pScaleFrom, pScaleTo, IEaseFunction.DEFAULT));
		}

		this.getTopLayer().addEntity(loadingScreenSprite);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
