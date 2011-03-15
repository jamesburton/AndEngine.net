package org.anddev.andengine.entity.layer.tiled.tmx;

import java.util.ArrayList;

using andengine.entity.layer.tiled.tmx.util.constants.TMXConstants;

/**
 * @author Nicolas Gramlich
 * @since 10:14:06 - 27.07.2010
 */
public class TMXProperties<T extends TMXProperty> extends ArrayList<T> : TMXConstants {
	// ===========================================================
	// Constants
	// ===========================================================

	private static final long serialVersionUID = 8912773556975105201L;

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public bool containsTMXProperty(final String pName, final String pValue) {
		for(int i = this.size() - 1; i >= 0; i--) {
			final T tmxProperty = this.get(i);
			if(tmxProperty.getName().equals(pName) && tmxProperty.getValue().equals(pValue)) {
				return true;
			}
		}
		return false;
	}

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