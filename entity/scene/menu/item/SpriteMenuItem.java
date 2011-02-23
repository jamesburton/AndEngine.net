package org.anddev.andengine.entity.scene.menu.item;

using andengine.entity.sprite.Sprite;
using andengine.opengl.texture.region.TextureRegion;

/**
 * @author Nicolas Gramlich
 * @since 20:15:20 - 01.04.2010
 */
public class SpriteMenuItem extends Sprite : IMenuItem {
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

	public SpriteMenuItem(final int pID, final TextureRegion pTextureRegion) {
		super(0, 0, pTextureRegion);
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
