namespace andengine.collision
{

    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;
    // TODO: Check this conversion
    using Constants = andengine.util.constants.Constants;

    //using andengine.entity.shape.RectangularShape;
    using RectangularShape = andengine.entity.shape.RectangularShape;
    using MathUtil = andengine.util.MathUtils;

    /**
     * @author Nicolas Gramlich
     * @since 11:50:19 - 11.03.2010
     */
    public class RectangularShapeCollisionChecker : ShapeCollisionChecker
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly int RECTANGULARSHAPE_VERTEX_COUNT = 4;

        private static /* final */ readonly float[] VERTICES_CONTAINS_TMP = new float[2 * RECTANGULARSHAPE_VERTEX_COUNT];
        private static /* final */ readonly float[] VERTICES_COLLISION_TMP_A = new float[2 * RECTANGULARSHAPE_VERTEX_COUNT];
        private static /* final */ readonly float[] VERTICES_COLLISION_TMP_B = new float[2 * RECTANGULARSHAPE_VERTEX_COUNT];

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

        public static bool CheckContains(/* final */ RectangularShape pRectangularShape, /* final */ float pX, /* final */ float pY)
        {
            RectangularShapeCollisionChecker.FillVertices(pRectangularShape, VERTICES_CONTAINS_TMP);
            return ShapeCollisionChecker.CheckContains(VERTICES_CONTAINS_TMP, 2 * RECTANGULARSHAPE_VERTEX_COUNT, pX, pY);
        }

        public static bool CheckCollision(/* final */ RectangularShape pRectangularShapeA, /* final */ RectangularShape pRectangularShapeB) {
		if(pRectangularShapeA.GetRotation() == 0 && pRectangularShapeB.GetRotation() == 0 && pRectangularShapeA.IsScaled() == false && pRectangularShapeB.IsScaled() == false) {
			/* final */ float aLeft = pRectangularShapeA.GetX();
			/* final */ float aTop = pRectangularShapeA.GetY();
			/* final */ float bLeft = pRectangularShapeB.GetX();
			/* final */ float bTop = pRectangularShapeB.GetY();
			return BaseCollisionChecker.CheckAxisAlignedRectangleCollision(aLeft, aTop, aLeft + pRectangularShapeA.GetWidth(), aTop + pRectangularShapeA.GetHeight(),
																			bLeft, bTop, bLeft + pRectangularShapeB.GetWidth(), bTop + pRectangularShapeB.GetHeight());
		} else {
			RectangularShapeCollisionChecker.FillVertices(pRectangularShapeA, VERTICES_COLLISION_TMP_A);
			RectangularShapeCollisionChecker.FillVertices(pRectangularShapeB, VERTICES_COLLISION_TMP_B);

			return ShapeCollisionChecker.CheckCollision(2 * RECTANGULARSHAPE_VERTEX_COUNT, 2 * RECTANGULARSHAPE_VERTEX_COUNT, VERTICES_COLLISION_TMP_A, VERTICES_COLLISION_TMP_B);
		}
	}

        public static void FillVertices(/* final */ RectangularShape pRectangularShape, /* final */ float[] pVertices)
        {
            /* final */
            float left = pRectangularShape.GetX();
            /* final */
            float top = pRectangularShape.GetY();
            /* final */
            float right = pRectangularShape.GetWidth() + left;
            /* final */
            float bottom = pRectangularShape.GetHeight() + top;

            pVertices[0 + Constants.VERTEX_INDEX_X] = left;
            pVertices[0 + Constants.VERTEX_INDEX_Y] = top;

            pVertices[2 + Constants.VERTEX_INDEX_X] = right;
            pVertices[2 + Constants.VERTEX_INDEX_Y] = top;

            pVertices[4 + Constants.VERTEX_INDEX_X] = right;
            pVertices[4 + Constants.VERTEX_INDEX_Y] = bottom;

            pVertices[6 + Constants.VERTEX_INDEX_X] = left;
            pVertices[6 + Constants.VERTEX_INDEX_Y] = bottom;

            MathUtil.RotateAndScaleAroundCenter(pVertices,
                    pRectangularShape.GetRotation(), left + pRectangularShape.GetRotationCenterX(), top + pRectangularShape.GetRotationCenterY(),
                    pRectangularShape.GetScaleX(), pRectangularShape.GetScaleY(), left + pRectangularShape.GetScaleCenterX(), top + pRectangularShape.GetScaleCenterY());
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}