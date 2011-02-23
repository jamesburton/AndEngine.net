package org.anddev.andengine.opengl.texture.region;

import javax.microedition.khronos.opengles.GL11;

using andengine.opengl.texture.Texture;
using andengine.opengl.texture.region.buffer.BaseTextureRegionBuffer;
using andengine.opengl.texture.region.buffer.TextureRegionBuffer;

/**
 * @author Nicolas Gramlich
 * @since 14:29:59 - 08.03.2010
 */
public class TextureRegion extends BaseTextureRegion {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public TextureRegion(final Texture pTexture, final int pTexturePositionX, final int pTexturePositionY, final int pWidth, final int pHeight) {
		super(pTexture, pTexturePositionX, pTexturePositionY, pWidth, pHeight);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public TextureRegionBuffer getTextureBuffer() {
		return (TextureRegionBuffer) this.mTextureRegionBuffer;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public TextureRegion clone() {
		return new TextureRegion(this.mTexture, this.mTexturePositionX, this.mTexturePositionY, this.mWidth, this.mHeight);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	protected BaseTextureRegionBuffer onCreateTextureRegionBuffer() {
		return new TextureRegionBuffer(this, GL11.GL_STATIC_DRAW);
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
