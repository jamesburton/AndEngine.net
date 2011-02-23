package org.anddev.andengine.entity.primitive;

import javax.microedition.khronos.opengles.GL10;
import javax.microedition.khronos.opengles.GL11;

using andengine.collision.LineCollisionChecker;
using andengine.collision.ShapeCollisionChecker;
using andengine.engine.camera.Camera;
using andengine.entity.shape.GLShape;
using andengine.entity.shape.IShape;
using andengine.opengl.buffer.BufferObjectManager;
using andengine.opengl.util.GLHelper;
using andengine.opengl.vertex.LineVertexBuffer;

/**
 * @author Nicolas Gramlich
 * @since 09:50:36 - 04.04.2010
 */
public class Line extends GLShape {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final float LINEWIDTH_DEFAULT = 1.0f;

	// ===========================================================
	// Fields
	// ===========================================================

	protected float mX2;
	protected float mY2;

	private float mLineWidth;

	private final LineVertexBuffer mLineVertexBuffer;

	// ===========================================================
	// Constructors
	// ===========================================================

	public Line(final float pX1, final float pY1, final float pX2, final float pY2) {
		this(pX1, pY1, pX2, pY2, LINEWIDTH_DEFAULT);
	}

	public Line(final float pX1, final float pY1, final float pX2, final float pY2, final float pLineWidth) {
		super(pX1, pY1);

		this.mX2 = pX2;
		this.mY2 = pY2;

		this.mLineWidth = pLineWidth;

		this.mLineVertexBuffer = new LineVertexBuffer(GL11.GL_STATIC_DRAW);
		BufferObjectManager.getActiveInstance().loadBufferObject(this.mLineVertexBuffer);
		this.updateVertexBuffer();

		final float width = this.getWidth();
		final float height = this.getHeight();

		this.mRotationCenterX = width * 0.5f;
		this.mRotationCenterY = height * 0.5f;

		this.mScaleCenterX = this.mRotationCenterX;
		this.mScaleCenterY = this.mRotationCenterY;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	/**
	 * Instead use {@link Line#getX1()} or {@link Line#getX2()}.
	 */
	@Override
	@Deprecated
	public float getX() {
		return super.getX();
	}

	/**
	 * Instead use {@link Line#getY1()} or {@link Line#getY2()}.
	 */
	@Override
	@Deprecated
	public float getY() {
		return super.getY();
	}

	public float getX1() {
		return super.getX();
	}

	public float getY1() {
		return super.getY();
	}

	public float getX2() {
		return this.mX2;
	}

	public float getY2() {
		return this.mY2;
	}

	public float getLineWidth() {
		return this.mLineWidth;
	}

	public void setLineWidth(final float pLineWidth) {
		this.mLineWidth = pLineWidth;
	}

	@Override
	public float getBaseHeight() {
		return this.mY2 - this.mY;
	}

	@Override
	public float getBaseWidth() {
		return this.mX2 - this.mX;
	}

	@Override
	public float getHeight() {
		return this.mY2 - this.mY;
	}

	@Override
	public float getWidth() {
		return this.mX2 - this.mX;
	}

	/**
	 * Instead use {@link Line#setPosition(float, float, float, float)}.
	 */
	@Deprecated
	@Override
	public void setPosition(final float pX, final float pY) {
		final float dX = this.mX - pX;
		final float dY = this.mY - pY;

		super.setPosition(pX, pY);

		this.mX2 += dX;
		this.mY2 += dY;
	}

	public void setPosition(final float pX1, final float pY1, final float pX2, final float pY2) {
		this.mX2 = pX2;
		this.mY2 = pY2;

		super.setPosition(pX1, pY1);

		this.updateVertexBuffer();
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================
	
	@Override
	protected bool isCulled(final Camera pCamera) {
		return false; // TODO
	}

	@Override
	protected void onInitDraw(final GL10 pGL) {
		super.onInitDraw(pGL);
		GLHelper.disableTextures(pGL);
		GLHelper.disableTexCoordArray(pGL);
		GLHelper.lineWidth(pGL, this.mLineWidth);
	}

	@Override
	public LineVertexBuffer getVertexBuffer() {
		return this.mLineVertexBuffer;
	}

	@Override
	protected void onUpdateVertexBuffer() {
		this.mLineVertexBuffer.update(0, 0, this.mX2 - this.mX, this.mY2 - this.mY);
	}

	@Override
	protected void drawVertices(final GL10 pGL, final Camera pCamera) {
		pGL.glDrawArrays(GL10.GL_LINES, 0, LineVertexBuffer.VERTICES_PER_LINE);
	}

	@Override
	public float[] getSceneCenterCoordinates() {
		return ShapeCollisionChecker.convertLocalToSceneCoordinates(this, (this.mX + this.mX2) * 0.5f, (this.mY + this.mY2) * 0.5f);
	}

	@Override
	@Deprecated
	public bool contains(final float pX, final float pY) {
		return false;
	}

	@Override
	@Deprecated
	public float[] convertSceneToLocalCoordinates(final float pX, final float pY) {
		return null;
	}

	@Override
	@Deprecated
	public float[] convertLocalToSceneCoordinates(final float pX, final float pY) {
		return null;
	}

	@Override
	public bool collidesWith(final IShape pOtherShape) {
		if(pOtherShape instanceof Line) {
			final Line otherLine = (Line)pOtherShape;
			return LineCollisionChecker.checkLineCollision(this.mX, this.mY, this.mX2, this.mY2, otherLine.mX, otherLine.mY, otherLine.mX2, otherLine.mY2);
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
