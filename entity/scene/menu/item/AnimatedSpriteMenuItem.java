package org.anddev.andengine.entity.scene.menu.item;

using andengine.entity.sprite.AnimatedSprite;
using andengine.opengl.texture.region.TiledTextureRegion;

/**
 * @author Nicolas Gramlich
 * @since 15:44:39 - 07.07.2010
 */
public class AnimatedSpriteMenuItem extends AnimatedSprite : IMenuItem {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final int mID;

	// ===========================================================
	// Constructors
	// ===========================================================

	public AnimatedSpriteMenuItem(final int pID, final TiledTextureRegion pTiledTextureRegion) {
		super(0, 0, pTiledTextureRegion);
		this.mID = pID;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public int getID() {
		return this.mID;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onSelected() {
		/* Nothing. */
	}

	@Override
	public void onUnselected() {
		/* Nothing. */
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
