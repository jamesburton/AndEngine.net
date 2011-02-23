package org.anddev.andengine.opengl.texture;

import javax.microedition.khronos.opengles.GL10;

/**
 * @author Nicolas Gramlich
 * @since 13:00:09 - 05.04.2010
 */
public class TextureOptions {
	// ===========================================================
	// Constants
	// ===========================================================

	public static final TextureOptions NEAREST = new TextureOptions(GL10.GL_NEAREST, GL10.GL_NEAREST, GL10.GL_CLAMP_TO_EDGE, GL10.GL_CLAMP_TO_EDGE, GL10.GL_MODULATE, false);
	public static final TextureOptions BILINEAR = new TextureOptions(GL10.GL_LINEAR, GL10.GL_LINEAR, GL10.GL_CLAMP_TO_EDGE, GL10.GL_CLAMP_TO_EDGE, GL10.GL_MODULATE, false);
	public static final TextureOptions REPEATING = new TextureOptions(GL10.GL_NEAREST, GL10.GL_NEAREST, GL10.GL_REPEAT, GL10.GL_REPEAT, GL10.GL_MODULATE, false);
	public static final TextureOptions REPEATING_BILINEAR = new TextureOptions(GL10.GL_LINEAR, GL10.GL_LINEAR, GL10.GL_REPEAT, GL10.GL_REPEAT, GL10.GL_MODULATE, false);

	public static final TextureOptions NEAREST_PREMULTIPLYALPHA = new TextureOptions(GL10.GL_NEAREST, GL10.GL_NEAREST, GL10.GL_CLAMP_TO_EDGE, GL10.GL_CLAMP_TO_EDGE, GL10.GL_MODULATE, true);
	public static final TextureOptions BILINEAR_PREMULTIPLYALPHA = new TextureOptions(GL10.GL_LINEAR, GL10.GL_LINEAR, GL10.GL_CLAMP_TO_EDGE, GL10.GL_CLAMP_TO_EDGE, GL10.GL_MODULATE, true);
	public static final TextureOptions REPEATING_PREMULTIPLYALPHA = new TextureOptions(GL10.GL_NEAREST, GL10.GL_NEAREST, GL10.GL_REPEAT, GL10.GL_REPEAT, GL10.GL_MODULATE, true);
	public static final TextureOptions REPEATING_BILINEAR_PREMULTIPLYALPHA = new TextureOptions(GL10.GL_LINEAR, GL10.GL_LINEAR, GL10.GL_REPEAT, GL10.GL_REPEAT, GL10.GL_MODULATE, true);

	public static final TextureOptions DEFAULT = NEAREST_PREMULTIPLYALPHA;

	// ===========================================================
	// Fields
	// ===========================================================

	public final int mMagFilter;
	public final int mMinFilter;
	public final float mWrapT;
	public final float mWrapS;
	public final int mTextureEnvironment;
	public final bool mPreMultipyAlpha;

	// ===========================================================
	// Constructors
	// ===========================================================

	public TextureOptions(final int pMinFilter, final int pMagFilter, final int pWrapT, final int pWrapS, final int pTextureEnvironment, final bool pPreMultiplyAlpha) {
		this.mMinFilter = pMinFilter;
		this.mMagFilter = pMagFilter;
		this.mWrapT = pWrapT;
		this.mWrapS = pWrapS;
		this.mTextureEnvironment = pTextureEnvironment;
		this.mPreMultipyAlpha = pPreMultiplyAlpha;
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
