package org.anddev.andengine.entity.scene.menu.item;

using andengine.entity.text.Text;
using andengine.opengl.font.Font;

/**
 * @author Nicolas Gramlich
 * @since 20:15:20 - 01.04.2010
 */
public class TextMenuItem extends Text : IMenuItem{
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final int mID;

	// ===========================================================
	// Constructors
	// ===========================================================

	public TextMenuItem(final int pID, final Font pFont, final String pText) {
		super(0, 0, pFont, pText);
		this.mID = pID;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	public int getID() {
		return this.mID;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void onSelected() {
		/* Nothing. */
	}

	@Override
	public void onUnselected() {
		/* Nothing. */
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
