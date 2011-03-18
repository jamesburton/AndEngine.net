namespace andengine.ui.dialog;

using andengine.util.Callback;

using Context = android.content.Context;
import android.content.DialogInterface.OnCancelListener;

/**
 * @author Nicolas Gramlich
 * @since 09:46:00 - 14.12.2009
 */
public class StringInputDialogBuilder extends GenericInputDialogBuilder<String> {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	// ===========================================================
	// Constructors
	// ===========================================================

	public StringInputDialogBuilder(final Context pContext, final int pTitleResID, final int pMessageResID, final int pErrorResID, final int pIconResID, final Callback<String> pSuccessCallback, final OnCancelListener pOnCancelListener) {
		super(pContext, pTitleResID, pMessageResID, pErrorResID, pIconResID, pSuccessCallback, pOnCancelListener);
	}

	public StringInputDialogBuilder(final Context pContext, final int pTitleResID, final int pMessageResID, final int pErrorResID, final int pIconResID, final String pDefaultText, final Callback<String> pSuccessCallback, final OnCancelListener pOnCancelListener) {
		super(pContext, pTitleResID, pMessageResID, pErrorResID, pIconResID, pDefaultText, pSuccessCallback, pOnCancelListener);
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	protected String generateResult(final String pInput) {
		return pInput;
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
