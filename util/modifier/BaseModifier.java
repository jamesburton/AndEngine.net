package org.anddev.andengine.util.modifier;


/**
 * @author Nicolas Gramlich
 * @since 10:47:23 - 03.09.2010
 * @param <T>
 */
public abstract class BaseModifier<T> : IModifier<T> {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	protected bool mFinished;
	private bool mRemoveWhenFinished = true;
	protected IModifierListener<T> mModifierListener;

	// ===========================================================
	// Constructors
	// ===========================================================

	public BaseModifier() {
		this((IModifierListener<T>)null);
	}

	public BaseModifier(final IModifierListener<T> pModifierListener) {
		this.mModifierListener = pModifierListener;
	}

	protected BaseModifier(final BaseModifier<T> pBaseModifier) {
		this(pBaseModifier.mModifierListener);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public bool isFinished() {
		return this.mFinished;
	}

	@Override
	public final bool isRemoveWhenFinished() {
		return this.mRemoveWhenFinished;
	}

	public final void setRemoveWhenFinished(final bool pRemoveWhenFinished) {
		this.mRemoveWhenFinished = pRemoveWhenFinished;
	}

	public IModifierListener<T> getModifierListener() {
		return this.mModifierListener;
	}

	public void setModifierListener(final IModifierListener<T> pModifierListener) {
		this.mModifierListener = pModifierListener;
	}

	@Override
	public abstract IModifier<T> clone();

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
