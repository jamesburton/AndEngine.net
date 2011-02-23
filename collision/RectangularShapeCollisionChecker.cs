namespace andengine.collision
{

    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;
    // TODO: Check this conversion
    using andegine.util.constants;

    //using andengine.entity.shape.RectangularShape;
    using andengine.util.MathUtils;

    /**
     * @author Nicolas Gramlich
     * @since 11:50:19 - 11.03.2010
     */
    public class RectangularShapeCollisionChecker : ShapeCollisionChecker
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ sealed int RECTANGULARSHAPE_VERTEX_COUNT = 4;

        private static /* final */ sealed float[] VERTICES_CONTAINS_TMP = new float[2 * RECTANGULARSHAPE_VERTEX_COUNT];
        private static /* final */ sealed float[] VERTICES_COLLISION_TMP_A = new float[2 * RECTANGULARSHAPE_VERTEX_COUNT];
        private static /* final */ sealed float[] VERTICES_COLLISION_TMP_B = new float[2 * RECTANGULARSHAPE_VERTEX_COUNT];

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        public static bool checkContains(/* final */ RectangularShape pRectangularShape, /* final */ float pX, /* final */ float pY)
        {
            RectangularShapeCollisionChecker.fillVertices(pRectangularShape, VERTICES_CONTAINS_TMP);
            return ShapeCollisionChecker.checkContains(VERTICES_CONTAINS_TMP, 2 * RECTANGULARSHAPE_VERTEX_COUNT, pX, pY);
        }

        public static bool checkCollision(/* final */ RectangularShape pRectangularShapeA, /* final */ RectangularShape pRectangularShapeB) {
		if(pRectangularShapeA.getRotation() == 0 && pRectangularShapeB.getRotation() == 0 && pRectangularShapeA.isScaled() == false && pRectangularShapeB.isScaled() == false) {
			/* final */ float aLeft = pRectangularShapeA.getX();
			/* final */ float aTop = pRectangularShapeA.getY();
			/* final */ float bLeft = pRectangularShapeB.getX();
			/* final */ float bTop = pRectangularShapeB.getY();
			return BaseCollisionChecker.checkAxisAlignedRectangleCollision(aLeft, aTop, aLeft + pRectangularShapeA.getWidth(), aTop + pRectangularShapeA.getHeight(),
																			bLeft, bTop, bLeft + pRectangularShapeB.getWidth(), bTop + pRectangularShapeB.getHeight());
		} else {
			RectangularShapeCollisionChecker.fillVertices(pRectangularShapeA, VERTICES_COLLISION_TMP_A);
			RectangularShapeCollisionChecker.fillVertices(pRectangularShapeB, VERTICES_COLLISION_TMP_B);

			return ShapeCollisionChecker.checkCollision(2 * RECTANGULARSHAPE_VERTEX_COUNT, 2 * RECTANGULARSHAPE_VERTEX_COUNT, VERTICES_COLLISION_TMP_A, VERTICES_COLLISION_TMP_B);
		}
	}

        public static void fillVertices(/* final */ RectangularShape pRectangularShape, /* final */ float[] pVertices)
        {
            /* final */
            float left = pRectangularShape.getX();
            /* final */
            float top = pRectangularShape.getY();
            /* final */
            float right = pRectangularShape.getWidth() + left;
            /* final */
            float bottom = pRectangularShape.getHeight() + top;

            pVertices[0 + Constants.VERTEX_INDEX_X] = left;
            pVertices[0 + Constants.VERTEX_INDEX_Y] = top;

            pVertices[2 + Constants.VERTEX_INDEX_X] = right;
            pVertices[2 + Constants.VERTEX_INDEX_Y] = top;

            pVertices[4 + Constants.VERTEX_INDEX_X] = right;
            pVertices[4 + Constants.VERTEX_INDEX_Y] = bottom;

            pVertices[6 + Constants.VERTEX_INDEX_X] = left;
            pVertices[6 + Constants.VERTEX_INDEX_Y] = bottom;

            MathUtils.rotateAndScaleAroundCenter(pVertices,
                    pRectangularShape.getRotation(), left + pRectangularShape.getRotationCenterX(), top + pRectangularShape.getRotationCenterY(),
                    pRectangularShape.getScaleX(), pRectangularShape.getScaleY(), left + pRectangularShape.getScaleCenterX(), top + pRectangularShape.getScaleCenterY());
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}