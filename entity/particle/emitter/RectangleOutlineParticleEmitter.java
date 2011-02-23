package org.anddev.andengine.entity.particle.emitter;

import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;

using andengine.util.MathUtils;

/**
 * @author Nicolas Gramlich
 * @since 15:48:00 - 01.10.2010
 */
public class RectangleOutlineParticleEmitter extends BaseRectangleParticleEmitter {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public RectangleOutlineParticleEmitter(final float pCenterX, final float pCenterY, final float pWidth, final float pHeight) {
		super(pCenterX, pCenterY, pWidth, pHeight);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void getPositionOffset(final float[] pOffset) {
		pOffset[VERTEX_INDEX_X] = this.mCenterX + MathUtils.randomSign() * this.mWidthHalf;
		pOffset[VERTEX_INDEX_Y] = this.mCenterY + MathUtils.randomSign() * this.mHeightHalf;
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
