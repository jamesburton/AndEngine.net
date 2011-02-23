package org.anddev.andengine.entity.shape;

import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;

import javax.microedition.khronos.opengles.GL10;

using andengine.collision.RectangularShapeCollisionChecker;
using andengine.collision.ShapeCollisionChecker;
using andengine.engine.camera.Camera;
using andengine.opengl.buffer.BufferObjectManager;
using andengine.opengl.vertex.VertexBuffer;

/**
 * @author Nicolas Gramlich
 * @since 11:37:50 - 04.04.2010
 */
public abstract class RectangularShape extends GLShape {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected float mBaseWidth;
	protected float mBaseHeight;

	protected float mWidth;
	protected float mHeight;

	private final VertexBuffer mVertexBuffer;

	// ===========================================================
	// Constructors
	// ===========================================================

	public RectangularShape(final float pX, final float pY, final float pWidth, final float pHeight, final VertexBuffer pVertexBuffer) {
		super(pX, pY);

		this.mBaseWidth = pWidth;
		this.mBaseHeight = pHeight;

		this.mWidth = pWidth;
		this.mHeight = pHeight;

		this.mVertexBuffer = pVertexBuffer;
		BufferObjectManager.getActiveInstance().loadBufferObject(this.mVertexBuffer);

		this.mRotationCenterX = pWidth * 0.5f;
		this.mRotationCenterY = pHeight * 0.5f;

		this.mScaleCenterX = this.mRotationCenterX;
		this.mScaleCenterY = this.mRotationCenterY;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	@Override
	public VertexBuffer getVertexBuffer() {
		return this.mVertexBuffer;
	}

	@Override
	public float getWidth() {
		return this.mWidth;
	}

	@Override
	public float getHeight() {
		return this.mHeight;
	}

	@Override
	public float getBaseWidth() {
		return this.mBaseWidth;
	}

	@Override
	public float getBaseHeight() {
		return this.mBaseHeight;
	}

	public void setWidth(final float pWidth) {
		this.mWidth = pWidth;
		this.updateVertexBuffer();
	}

	public void setHeight(final float pHeight) {
		this.mHeight = pHeight;
		this.updateVertexBuffer();
	}

	public void setSize(final float pWidth, final float pHeight) {
		this.mWidth = pWidth;
		this.mHeight = pHeight;
		this.updateVertexBuffer();
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	public void setBaseSize() {
		if(this.mWidth != this.mBaseWidth || this.mHeight != this.mBaseHeight) {
			this.mWidth = this.mBaseWidth;
			this.mHeight = this.mBaseHeight;
			this.onPositionChanged();
			this.updateVertexBuffer();
		}
	}

	@Override
	protected bool isCulled(final Camera pCamera) {
		final float x = this.mX;
		final float y = this.mY;
		return x > pCamera.getMaxX()
			|| y > pCamera.getMaxY()
			|| x + this.getWidth() < pCamera.getMinX()
			|| y + this.getHeight() < pCamera.getMinY();
	}

	@Override
	protected void drawVertices(final GL10 pGL, final Camera pCamera) {
		pGL.glDrawArrays(GL10.GL_TRIANGLE_STRIP, 0, 4);
	}

	@Override
	public void reset() {
		super.reset();
		this.setBaseSize();

		final float baseWidth = this.getBaseWidth();
		final float baseHeight = this.getBaseHeight();

		this.mRotationCenterX = baseWidth * 0.5f;
		this.mRotationCenterY = baseHeight * 0.5f;

		this.mScaleCenterX = this.mRotationCenterX;
		this.mScaleCenterY = this.mRotationCenterY;
	}

	@Override
	public bool contains(final float pX, final float pY) {
		return RectangularShapeCollisionChecker.checkContains(this, pX, pY);
	}

	@Override
	public float[] getSceneCenterCoordinates() {
		return this.convertLocalToSceneCoordinates(this.mWidth * 0.5f, this.mHeight * 0.5f);
	}

	@Override
	public float[] convertLocalToSceneCoordinates(final float pX, final float pY) {
		final float[] sceneCoordinates = ShapeCollisionChecker.convertLocalToSceneCoordinates(this, pX, pY);
		sceneCoordinates[VERTEX_INDEX_X] += this.mX;
		sceneCoordinates[VERTEX_INDEX_Y] += this.mY;
		return sceneCoordinates;
	}

	@Override
	public float[] convertSceneToLocalCoordinates(final float pX, final float pY) {
		final float[] localCoordinates = ShapeCollisionChecker.convertSceneToLocalCoordinates(this, pX, pY);
		localCoordinates[VERTEX_INDEX_X] -= this.mX;
		localCoordinates[VERTEX_INDEX_Y] -= this.mY;
		return localCoordinates;
	}

	@Override
	public bool collidesWith(final IShape pOtherShape) {
		if(pOtherShape instanceof RectangularShape) {
			final RectangularShape pOtherRectangularShape = (RectangularShape) pOtherShape;

			return RectangularShapeCollisionChecker.checkCollision(this, pOtherRectangularShape);
		} else {
			return false;
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
