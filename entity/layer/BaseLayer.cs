namespace andengine.entity.layer
{

    //import java.util.ArrayList;
    using System.Collections.Generic;

    using Entity = andengine.entity.Entity;
    using ITouchArea = andengine.entity.scene.Scene.ITouchArea;

    /**
     * @author Nicolas Gramlich
     * @since 00:13:59 - 23.07.2010
     */
    public abstract class BaseLayer : Entity, ILayer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        //private final ArrayList<ITouchArea> mTouchAreas = new ArrayList<ITouchArea>();
        private readonly List<ITouchArea> mTouchAreas = new List<ITouchArea>();

        // ===========================================================
        // Constructors
        // ===========================================================

        public BaseLayer()
        {

        }

        public BaseLayer(int pZIndex)
            : base(pZIndex)
        {
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void registerTouchArea(ITouchArea pTouchArea)
        {
            this.mTouchAreas.Add(pTouchArea);
        }

        public override void unregisterTouchArea(ITouchArea pTouchArea)
        {
            this.mTouchAreas.Remove(pTouchArea);
        }

        //public ArrayList<ITouchArea> getTouchAreas() {
        public List<ITouchArea> getTouchAreas()
        {
            return this.mTouchAreas;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}