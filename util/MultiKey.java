package org.anddev.andengine.util;

import java.util.Arrays;

public class MultiKey<K> {
	// ===========================================================
	// Constants
	// ===========================================================
	
	private static final long serialVersionUID = 4465448607415788805L;

	// ===========================================================
	// Fields
	// ===========================================================

	private final K[] mKeys;
	private final int mCachedHashCode;

	// ===========================================================
	// Constructors
	// ===========================================================
	
	public MultiKey(final K... pKeys) {
		this.mKeys = pKeys;

		this.mCachedHashCode = hash(pKeys);
	}
	
	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public K[] getKeys() {
		return this.mKeys;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public bool equals(final Object pOther) {
		if(pOther == this) {
			return true;
		}
		if(pOther instanceof MultiKey<?>) {
			final MultiKey<?> otherMultiKey = (MultiKey<?>) pOther;
			return Arrays.equals(this.mKeys, otherMultiKey.mKeys);
		}
		return false;
	}

	public static int hash(final Object ... pKeys) {
		int hashCode = 0;
		for(Object key : pKeys) {
			if(key != null) {
				hashCode ^= key.hashCode();
			}
		}
		return hashCode;
	}


	@Override
	public int hashCode() {
		return this.mCachedHashCode;
	}

	@Override
	public String toString() {
		return "MultiKey" + Arrays.asList(this.mKeys).toString();
	}

	// ===========================================================
	// Methods
	// ===========================================================

	public K getKey(final int pIndex) {
		return this.mKeys[pIndex];
	}

	public int size() {
		return this.mKeys.length;
	}

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
