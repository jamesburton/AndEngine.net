namespace andengine.collision
{

    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_X;
    //import static org.anddev.andengine.util.constants.Constants.VERTEX_INDEX_Y;
    using Constants = andengine.util.constants.Constants;

    using Shape = andengine.entity.shape.Shape;
    using MathUtils = andengine.util.MathUtils;


    /**
     * @author Nicolas Gramlich
     * @since 11:50:19 - 11.03.2010
     */
    public class ShapeCollisionChecker : BaseCollisionChecker
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private static /* final */ readonly float[] VERTICES_SCENE_TO_LOCAL_TMP = new float[2];
        private static /* final */ readonly float[] VERTICES_LOCAL_TO_SCENE_TMP = new float[2];

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

        public static float[] ConvertSceneToLocalCoordinates(/* final */ Shape pShape, /* final */ float pX, /* final */ float pY)
        {
            VERTICES_SCENE_TO_LOCAL_TMP[Constants.VERTEX_INDEX_X] = pX;
            VERTICES_SCENE_TO_LOCAL_TMP[Constants.VERTEX_INDEX_Y] = pY;

            /* final */
            float left = pShape.GetX();
            /* final */
            float top = pShape.GetY();

            MathUtils.RevertRotateAndScaleAroundCenter(VERTICES_SCENE_TO_LOCAL_TMP,
                    pShape.GetRotation(), left + pShape.GetRotationCenterX(), top + pShape.GetRotationCenterY(),
                    pShape.GetScaleX(), pShape.GetScaleY(), left + pShape.GetScaleCenterX(), top + pShape.GetScaleCenterY());

            return VERTICES_SCENE_TO_LOCAL_TMP;
        }


        public static float[] ConvertLocalToSceneCoordinates(/* final */ Shape pShape, /* final */ float pX, /* final */ float pY)
        {
            VERTICES_LOCAL_TO_SCENE_TMP[Constants.VERTEX_INDEX_X] = pX;
            VERTICES_LOCAL_TO_SCENE_TMP[Constants.VERTEX_INDEX_Y] = pY;

            MathUtils.RotateAndScaleAroundCenter(VERTICES_LOCAL_TO_SCENE_TMP,
                    pShape.GetRotation(), pShape.GetRotationCenterX(), pShape.GetRotationCenterY(),
                    pShape.GetScaleX(), pShape.GetScaleY(), pShape.GetScaleCenterX(), pShape.GetScaleCenterY());

            return VERTICES_LOCAL_TO_SCENE_TMP;
        }

        public static bool CheckCollision(/* final */ int pVerticesALength, /* final */ int pVerticesBLength, /* final */ float[] pVerticesA, /* final */ float[] pVerticesB)
        {
            /* Check all the lines of A ... */
            for (int a = pVerticesALength - 4; a >= 0; a -= 2)
            {
                /* ... against all lines in B. */
                if (CheckCollisionSub(a, a + 2, pVerticesA, pVerticesB, pVerticesBLength))
                {
                    return true;
                }
            }
            /* Also check the 'around the corner of the array' line of A against all lines in B. */
            if (CheckCollisionSub(pVerticesALength - 2, 0, pVerticesA, pVerticesB, pVerticesBLength))
            {
                return true;
            }
            else
            {
                /* At last check if one polygon 'contains' the other one by checking 
                 * if one vertex of the one vertices is contained by all of the other vertices. */
                if (ShapeCollisionChecker.CheckContains(pVerticesA, pVerticesALength, pVerticesB[Constants.VERTEX_INDEX_X], pVerticesB[VERTEX_INDEX_Y]))
                {
                    return true;
                }
                else if (ShapeCollisionChecker.CheckContains(pVerticesB, pVerticesBLength, pVerticesA[Constants.VERTEX_INDEX_X], pVerticesA[VERTEX_INDEX_Y]))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        /**
         * Checks line specified by pVerticesA[pVertexIndexA1] and pVerticesA[pVertexIndexA2] against all lines in pVerticesB.
         */
        private static bool CheckCollisionSub(/* final */ int pVertexIndexA1, /* final */ int pVertexIndexA2, /* final */ float[] pVerticesA, /* final */ float[] pVerticesB, /* final */ int pVerticesBLength)
        {
            /* Check against all the lines of B. */
            /* final */
            float vertexA1X = pVerticesA[pVertexIndexA1 + Constants.VERTEX_INDEX_X];
            /* final */
            float vertexA1Y = pVerticesA[pVertexIndexA1 + Constants.VERTEX_INDEX_Y];
            /* final */
            float vertexA2X = pVerticesA[pVertexIndexA2 + Constants.VERTEX_INDEX_X];
            /* final */
            float vertexA2Y = pVerticesA[pVertexIndexA2 + Constants.VERTEX_INDEX_Y];

            for (int b = pVerticesBLength - 4; b >= 0; b -= 2)
            {
                if (LineCollisionChecker.CheckLineCollision(vertexA1X, vertexA1Y, vertexA2X, vertexA2Y, pVerticesB[b + Constants.VERTEX_INDEX_X], pVerticesB[b + Constants.VERTEX_INDEX_Y], pVerticesB[b + 2 + Constants.VERTEX_INDEX_X], pVerticesB[b + 2 + Constants.VERTEX_INDEX_Y]))
                {
                    return true;
                }
            }
            /* Also check the 'around the corner of the array' line of B. */
            if (LineCollisionChecker.CheckLineCollision(vertexA1X, vertexA1Y, vertexA2X, vertexA2Y, pVerticesB[pVerticesBLength - 2], pVerticesB[pVerticesBLength - 1], pVerticesB[Constants.VERTEX_INDEX_X], pVerticesB[Constants.VERTEX_INDEX_Y]))
            {
                return true;
            }
            return false;
        }

        public static bool CheckContains(/* final */ float[] pVertices, /* final */ int pVerticesLength, /* final */ float pX, /* final */ float pY)
        {
            int edgeResult;
            int edgeResultSum = 0;

            for (int i = pVerticesLength - 4; i >= 0; i -= 2)
            {
                /* final */
                edgeResult = RelativeCCW(pVertices[i], pVertices[i + 1], pVertices[i + 2], pVertices[i + 3], pX, pY);
                if (edgeResult == 0)
                {
                    return true;
                }
                else
                {
                    edgeResultSum += edgeResult;
                }
            }
            /* Also check the 'around the corner of the array' line. */
            /* final */
            edgeResult = RelativeCCW(pVertices[pVerticesLength - 2], pVertices[pVerticesLength - 1], pVertices[Constants.VERTEX_INDEX_X], pVertices[Constants.VERTEX_INDEX_Y], pX, pY);
            if (edgeResult == 0)
            {
                return true;
            }
            else
            {
                edgeResultSum += edgeResult;
            }

            /* final */
            int vertexCount = pVerticesLength / 2;
            /* Point is not on the edge, so check if the edge is on the same side(left or right) of all edges. */
            return edgeResultSum == vertexCount || edgeResultSum == -vertexCount;
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}