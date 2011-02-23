package org.anddev.andengine.util.path.astar;

using andengine.util.path.ITiledMap;

/**
 * @author Nicolas Gramlich
 * @since 22:59:20 - 16.08.2010
 */
public interface IAStarHeuristic<T> {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	public float getExpectedRestCost(final ITiledMap<T> pTiledMap, final T pEntity, final int pFromTileColumn, final int pFromTileRow, final int pToTileColumn, final int pToTileRow);
}
