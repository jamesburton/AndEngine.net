using andengine.util;
using andengine.util.modifier;

namespace andengine.entity.modifier
{

using IEntity = andengine.entity.IEntity;

/**
 * @author Nicolas Gramlich
 * @since 11:17:50 - 19.03.2010
 */
public interface IEntityModifier : IModifier<IEntity> {
	// ===========================================================
	// Final Fields
	// ===========================================================

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================

}

    //NOTE: Was previously an inner class to IEntityModifier
	public /*static*/ interface IEntityModifierListener : IModifierListener<IEntity>{
		// ===========================================================
		// Final Fields
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================
	}
	
    //NOTE: Was previously an inner class to IEntityModifier
	public interface IEntityModifierMatcher : IMatcher<IModifier<IEntity>> {
		// ===========================================================
		// Constants
		// ===========================================================

		// ===========================================================
		// Methods
		// ===========================================================
	}
}