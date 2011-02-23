package org.anddev.andengine.opengl.texture.region.buffer;

import static org.anddev.andengine.opengl.vertex.RectangleVertexBuffer.VERTICES_PER_RECTANGLE;

using andengine.opengl.buffer.BufferObject;
using andengine.opengl.texture.Texture;
using andengine.opengl.texture.region.BaseTextureRegion;
using andengine.opengl.util.FastFloatBuffer;

/**
 * @author Nicolas Gramlich
 * @since 19:05:50 - 09.03.2010
 */
public abstract class BaseTextureRegionBuffer extends BufferObject {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected final BaseTextureRegion mTextureRegion;
	private bool mFlippedVertical;
	private bool mFlippedHorizontal;

	// ===========================================================
	// Constructors
	// ===========================================================

	public BaseTextureRegionBuffer(final BaseTextureRegion pBaseTextureRegion, final int pDrawType) {
		super(2 * VERTICES_PER_RECTANGLE, pDrawType);
		this.mTextureRegion = pBaseTextureRegion;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public BaseTextureRegion getTextureRegion() {
		return this.mTextureRegion;
	}

	public bool isFlippedHorizontal() {
		return this.mFlippedHorizontal;
	}

	public void setFlippedHorizontal(final bool pFlippedHorizontal) {
		if(this.mFlippedHorizontal != pFlippedHorizontal) {
			this.mFlippedHorizontal = pFlippedHorizontal;
			this.update();
		}
	}

	public bool isFlippedVertical() {
		return this.mFlippedVertical;
	}

	public void setFlippedVertical(final bool pFlippedVertical) {
		if(this.mFlippedVertical != pFlippedVertical) {
			this.mFlippedVertical = pFlippedVertical;
			this.update();
		}
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	protected abstract float getX1();

	protected abstract float getY1();

	protected abstract float getX2();

	protected abstract float getY2();

	// ===========================================================
	// Methods
	// ===========================================================

	public synchronized void update() {
		final BaseTextureRegion textureRegion = this.mTextureRegion;
		final Texture texture = textureRegion.getTexture();

		if(texture == null) {
			return;
		}

		final int x1 = Float.floatToRawIntBits(this.getX1());
		final int y1 = Float.floatToRawIntBits(this.getY1());
		final int x2 = Float.floatToRawIntBits(this.getX2());
		final int y2 = Float.floatToRawIntBits(this.getY2());

		final int[] bufferData = this.mBufferData;

		if(this.mFlippedVertical) {
			if(this.mFlippedHorizontal) {
				bufferData[0] = x2;
				bufferData[1] = y2;

				bufferData[2] = x2;
				bufferData[3] = y1;

				bufferData[4] = x1;
				bufferData[5] = y2;

				bufferData[6] = x1;
				bufferData[7] = y1;
			} else {
				bufferData[0] = x1;
				bufferData[1] = y2;

				bufferData[2] = x1;
				bufferData[3] = y1;

				bufferData[4] = x2;
				bufferData[5] = y2;

				bufferData[6] = x2;
				bufferData[7] = y1;
			}
		} else {
			if(this.mFlippedHorizontal) {
				bufferData[0] = x2;
				bufferData[1] = y1;

				bufferData[2] = x2;
				bufferData[3] = y2;

				bufferData[4] = x1;
				bufferData[5] = y1;

				bufferData[6] = x1;
				bufferData[7] = y2;
			} else {
				bufferData[0] = x1;
				bufferData[1] = y1;

				bufferData[2] = x1;
				bufferData[3] = y2;

				bufferData[4] = x2;
				bufferData[5] = y1;

				bufferData[6] = x2;
				bufferData[7] = y2;
			}
		}

		final FastFloatBuffer buffer = this.getFloatBuffer();
		buffer.position(0);
		buffer.put(bufferData);
		buffer.position(0);

		super.setHardwareBufferNeedsUpdate();
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
