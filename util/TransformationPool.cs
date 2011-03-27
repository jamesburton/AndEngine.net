using andengine.util;
using andengine.util.pool;

namespace andengine.util
{

/**
 * @author Nicolas Gramlich
 * @since 23:07:53 - 23.02.2011
 */
public class TransformationPool {
	// ===========================================================
	// Constants
	// ===========================================================

	// ===========================================================
	// Fields
	// ===========================================================

    private static GenericPool<Transformation> POOL = new GenericTransformationPool();
    private class GenericTransformationPool : GenericPool<Transformation> {
		protected override Transformation OnAllocatePoolItem() {
			return new Transformation();
		}
	};

	// ===========================================================
	// Constructors
	// ===========================================================

	// ===========================================================
	// Getter & Setter
	// ===========================================================

	// ===========================================================
	// Methods for/from SuperClass/Interfaces
	// ===========================================================
	
	public static Transformation obtain() {
		return POOL.ObtainPoolItem();
	}
	
	public static void recycle(Transformation pTransformation) {
		pTransformation.setToIdentity();
		POOL.RecyclePoolItem(pTransformation);
	}

	// ===========================================================
	// Methods
	// ===========================================================

	// ===========================================================
	// Inner and Anonymous Classes
	// ===========================================================
}
}