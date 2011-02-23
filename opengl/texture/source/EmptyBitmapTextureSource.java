package org.anddev.andengine.opengl.texture.source;

import android.graphics.Bitmap;
import android.graphics.Bitmap.Config;

/**
 * @author Nicolas Gramlich
 * @since 20:20:36 - 08.08.2010
 */
public class EmptyBitmapTextureSource : ITextureSource {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final int mWidth;
	private final int mHeight;

	// ===========================================================
	// Constructors
	// ===========================================================

	public EmptyBitmapTextureSource(final int pWidth, final int pHeight) {
		this.mWidth = pWidth;
		this.mHeight = pHeight;
	}

	@Override
	public EmptyBitmapTextureSource clone() {
		return new EmptyBitmapTextureSource(this.mWidth, this.mHeight);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public int getHeight() {
		return this.mHeight;
	}

	@Override
	public int getWidth() {
		return this.mWidth;
	}

	@Override
	public Bitmap onLoadBitmap() {
		return Bitmap.createBitmap(this.mWidth, this.mHeight, Config.ARGB_8888);
	}

	@Override
	public String toString() {
		return this.getClass().getSimpleName() + "(" + this.mWidth + " x " + this.mHeight + ")";
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}