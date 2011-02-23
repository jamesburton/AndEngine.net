package org.anddev.andengine.level;

import java.util.HashMap;

using andengine.level.LevelLoader.IEntityLoader;
using andengine.level.util.constants.LevelConstants;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

/**
 * @author Nicolas Gramlich
 * @since 14:35:32 - 11.10.2010
 */
public class LevelParser extends DefaultHandler : LevelConstants {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final HashMap<String, IEntityLoader> mEntityLoaders;

	// ===========================================================
	// Constructors
	// ===========================================================

	public LevelParser(final HashMap<String, IEntityLoader> pEntityLoaders) {
		this.mEntityLoaders = pEntityLoaders;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void startElement(final String pUri, final String pLocalName, final String pQualifiedName, final Attributes pAttributes) throws SAXException {
		final IEntityLoader entityLoader = this.mEntityLoaders.get(pLocalName);
		if(entityLoader != null) {
			entityLoader.onLoadEntity(pLocalName, pAttributes);
		} else {
			throw new IllegalArgumentException("Unexpected tag: '" + pLocalName + "'.");
		}
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
