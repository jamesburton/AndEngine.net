package org.anddev.andengine.util.path.astar;

using andengine.util.path.ITiledMap;

import android.util.FloatMath;

/**
 * @author Nicolas Gramlich
 * @since 22:58:01 - 16.08.2010
 */
public class EuclideanHeuristic<T> : IAStarHeuristic<T> {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public float getExpectedRestCost(final ITiledMap<T> pTileMap, final T pEntity, final int pTileFromX, final int pTileFromY, final int pTileToX, final int pTileToY) {
		final float dX = pTileToX - pTileFromX;
		final float dY = pTileToY - pTileFromY;

		return FloatMath.sqrt(dX * dX + dY * dY);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
