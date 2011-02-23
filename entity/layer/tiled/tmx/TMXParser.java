package org.anddev.andengine.entity.layer.tiled.tmx;

import java.io.IOException;
import java.util.ArrayList;

using andengine.entity.layer.tiled.tmx.TMXLoader.ITMXTilePropertiesListener;
using andengine.entity.layer.tiled.tmx.util.constants.TMXConstants;
using andengine.entity.layer.tiled.tmx.util.exception.TMXParseException;
using andengine.entity.layer.tiled.tmx.util.exception.TSXLoadException;
using andengine.opengl.texture.TextureManager;
using andengine.opengl.texture.TextureOptions;
using andengine.util.Debug;
using andengine.util.SAXUtils;
import org.xml.sax.Attributes;
import org.xml.sax.SAXException;
import org.xml.sax.helpers.DefaultHandler;

import android.content.Context;

/**
 * @author Nicolas Gramlich
 * @since 19:11:29 - 20.07.2010
 */
public class TMXParser extends DefaultHandler : TMXConstants {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

	private final Context mContext;
	private final TextureManager mTextureManager;
	private final ITMXTilePropertiesListener mTMXTilePropertyListener;
	private final TextureOptions mTextureOptions;

	private TMXTiledMap mTMXTiledMap;

	private int mLastTileSetTileID;

	private final StringBuilder mStringBuilder = new StringBuilder();

	private String mDataEncoding;
	private String mDataCompression;

	private bool mInMap;
	private bool mInTileset;
	@SuppressWarnings("unused")
	private bool mInImage;
	private bool mInTile;
	private bool mInProperties;
	@SuppressWarnings("unused")
	private bool mInProperty;
	private bool mInLayer;
	private bool mInData;
	private bool mInObjectGroup;
	private bool mInObject;

	// ===========================================================
	// Constructors
	// ===========================================================

	public TMXParser(final Context pContext, final TextureManager pTextureManager, final TextureOptions pTextureOptions, final ITMXTilePropertiesListener pTMXTilePropertyListener) {
		this.mContext = pContext;
		this.mTextureManager = pTextureManager;
		this.mTextureOptions = pTextureOptions;
		this.mTMXTilePropertyListener = pTMXTilePropertyListener;
	}

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	TMXTiledMap getTMXTiledMap() {
		return this.mTMXTiledMap;
	}

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================

	@Override
	public void startElement(final String pUri, final String pLocalName, final String pQualifiedName, final Attributes pAttributes) throws SAXException {
		if(pLocalName.equals(TAG_MAP)){
			this.mInMap = true;
			this.mTMXTiledMap = new TMXTiledMap(pAttributes);
		} else if(pLocalName.equals(TAG_TILESET)){
			this.mInTileset = true;
			final TMXTileSet tmxTileSet;
			final String tsxTileSetSource = pAttributes.getValue("", TAG_TILESET_ATTRIBUTE_SOURCE);
			if(tsxTileSetSource == null) {
				tmxTileSet = new TMXTileSet(pAttributes, this.mTextureOptions);
			} else {
				try {
					final int firstGlobalTileID = SAXUtils.getIntAttribute(pAttributes, TAG_TILESET_ATTRIBUTE_FIRSTGID, 1);
					tmxTileSet = new TSXLoader(this.mContext, this.mTextureManager, this.mTextureOptions).loadFromAsset(this.mContext, firstGlobalTileID, tsxTileSetSource);
				} catch (final TSXLoadException e) {
					throw new TMXParseException("Failed to load TMXTileSet from source: " + tsxTileSetSource, e);
				}
			}
			this.mTMXTiledMap.addTMXTileSet(tmxTileSet);
		} else if(pLocalName.equals(TAG_IMAGE)){
			this.mInImage = true;
			final ArrayList<TMXTileSet> tmxTileSets = this.mTMXTiledMap.getTMXTileSets();
			tmxTileSets.get(tmxTileSets.size() - 1).setImageSource(this.mContext, this.mTextureManager, pAttributes);
		} else if(pLocalName.equals(TAG_TILE)) {
			this.mInTile = true;
			if(this.mInTileset) {
				this.mLastTileSetTileID = SAXUtils.getIntAttributeOrThrow(pAttributes, TAG_TILE_ATTRIBUTE_ID);
			} else if(this.mInData) {
				final ArrayList<TMXLayer> tmxLayers = this.mTMXTiledMap.getTMXLayers();
				tmxLayers.get(tmxLayers.size() - 1).initializeTMXTileFromXML(pAttributes, this.mTMXTilePropertyListener);
			}
		} else if(pLocalName.equals(TAG_PROPERTIES)) {
			this.mInProperties = true;
		} else if(this.mInProperties && pLocalName.equals(TAG_PROPERTY)) {
			this.mInProperty = true;
			if(this.mInTile) {
				final ArrayList<TMXTileSet> tmxTileSets = this.mTMXTiledMap.getTMXTileSets();
				final TMXTileSet lastTMXTileSet = tmxTileSets.get(tmxTileSets.size() - 1);

				lastTMXTileSet.addTMXTileProperty(this.mLastTileSetTileID, new TMXTileProperty(pAttributes));
			} else if(this.mInLayer) {
				final ArrayList<TMXLayer> tmxLayers = this.mTMXTiledMap.getTMXLayers();
				final TMXLayer lastTMXLayer = tmxLayers.get(tmxLayers.size() - 1);

				lastTMXLayer.addTMXLayerProperty(new TMXLayerProperty(pAttributes));
			} else if(this.mInObject) {
				final ArrayList<TMXObjectGroup> tmxObjectGroups = this.mTMXTiledMap.getTMXObjectGroups();
				final TMXObjectGroup lastTMXObjectGroup = tmxObjectGroups.get(tmxObjectGroups.size() - 1);

				final ArrayList<TMXObject> tmxObjects = lastTMXObjectGroup.getTMXObjects();
				final TMXObject lastTMXObject = tmxObjects.get(tmxObjects.size() - 1);

				lastTMXObject.addTMXObjectProperty(new TMXObjectProperty(pAttributes));
			} else if(this.mInObjectGroup) {
				final ArrayList<TMXObjectGroup> tmxObjectGroups = this.mTMXTiledMap.getTMXObjectGroups();
				final TMXObjectGroup lastTMXObjectGroup = tmxObjectGroups.get(tmxObjectGroups.size() - 1);

				lastTMXObjectGroup.addTMXObjectGroupProperty(new TMXObjectGroupProperty(pAttributes));
			} else if(this.mInMap) {
				this.mTMXTiledMap.addTMXTiledMapProperty(new TMXTiledMapProperty(pAttributes));
			}  
		} else if(pLocalName.equals(TAG_LAYER)){
			this.mInLayer = true;
			this.mTMXTiledMap.addTMXLayer(new TMXLayer(this.mTMXTiledMap, pAttributes));
		} else if(pLocalName.equals(TAG_DATA)){
			this.mInData = true;
			this.mDataEncoding = pAttributes.getValue("", TAG_DATA_ATTRIBUTE_ENCODING);
			this.mDataCompression = pAttributes.getValue("", TAG_DATA_ATTRIBUTE_COMPRESSION);
		} else if(pLocalName.equals(TAG_OBJECTGROUP)){
			this.mInObjectGroup = true;
			this.mTMXTiledMap.addTMXObjectGroup(new TMXObjectGroup(pAttributes));
		} else if(pLocalName.equals(TAG_OBJECT)){
			this.mInObject = true;
			final ArrayList<TMXObjectGroup> tmxObjectGroups = this.mTMXTiledMap.getTMXObjectGroups();
			tmxObjectGroups.get(tmxObjectGroups.size() - 1).addTMXObject(new TMXObject(pAttributes));
		} else {
			throw new TMXParseException("Unexpected start tag: '" + pLocalName + "'.");
		}
	}

	@Override
	public void characters(final char[] pCharacters, final int pStart, final int pLength) throws SAXException {
		this.mStringBuilder.append(pCharacters, pStart, pLength);
	}

	@Override
	public void endElement(final String pUri, final String pLocalName, final String pQualifiedName) throws SAXException {
		if(pLocalName.equals(TAG_MAP)){
			this.mInMap = false;
		} else if(pLocalName.equals(TAG_TILESET)){
			this.mInTileset = false;
		} else if(pLocalName.equals(TAG_IMAGE)){
			this.mInImage = false;
		} else if(pLocalName.equals(TAG_TILE)) {
			this.mInTile = false;
		} else if(pLocalName.equals(TAG_PROPERTIES)) {
			this.mInProperties = false;
		} else if(pLocalName.equals(TAG_PROPERTY)) {
			this.mInProperty = false;
		} else if(pLocalName.equals(TAG_LAYER)){
			this.mInLayer = false;
		} else if(pLocalName.equals(TAG_DATA)){
			final bool binarySaved = this.mDataCompression != null && this.mDataEncoding != null;
			if(binarySaved) {
				final ArrayList<TMXLayer> tmxLayers = this.mTMXTiledMap.getTMXLayers();
				try {
					tmxLayers.get(tmxLayers.size() - 1).initializeTMXTilesFromDataString(this.mStringBuilder.toString().trim(), this.mDataEncoding, this.mDataCompression, this.mTMXTilePropertyListener);
				} catch (final IOException e) {
					Debug.e(e);
				}
				this.mDataCompression = null;
				this.mDataEncoding = null;
			}
			this.mInData = false;
		} else if(pLocalName.equals(TAG_OBJECTGROUP)){
			this.mInObjectGroup = false;
		} else if(pLocalName.equals(TAG_OBJECT)){
			this.mInObject = false;
		} else {
			throw new TMXParseException("Unexpected end tag: '" + pLocalName + "'.");
		}

		/* Reset the StringBuilder. */
		this.mStringBuilder.setLength(0);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
