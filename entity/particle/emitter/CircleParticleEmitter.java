package org.anddev.andengine.entity.particle.emitter;

import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;

using andengine.util.MathUtils;
using andengine.util.constants.MathConstants;

import android.util.FloatMath;

/**
 * @author Nicolas Gramlich
 * @since 20:18:41 - 01.10.2010
 */
public class CircleParticleEmitter extends BaseCircleParticleEmitter {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public CircleParticleEmitter(final float pCenterX, final float pCenterY, final float pRadius) {
		super(pCenterX, pCenterY, pRadius);
	}

	public CircleParticleEmitter(final float pCenterX, final float pCenterY, final float pRadiusX, final float pRadiusY) {
		super(pCenterX, pCenterY, pRadiusX, pRadiusY);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void getPositionOffset(final float[] pOffset) {
		final float random = MathUtils.RANDOM.nextFloat() * MathConstants.PI * 2;
		pOffset[VERTEX_INDEX_X] = this.mCenterX + FloatMath.cos(random) * this.mRadiusX * MathUtils.RANDOM.nextFloat();
		pOffset[VERTEX_INDEX_Y] = this.mCenterY + FloatMath.sin(random) * this.mRadiusY * MathUtils.RANDOM.nextFloat();
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
