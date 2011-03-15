namespace andengine.entity.layer
{

    //import java.util.ArrayList;
    using System.Collections.Generic;

    using Entity = andengine.entity.Entity;
    using ITouchArea = andengine.entity.scene.Scene.ITouchArea;
    using IComparator = Java.Util.IComparator;
    using IEntityMatcher = andengine.util.IEntityMatcher;

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
        private readonly IList<ITouchArea> mTouchAreas = new List<ITouchArea>();

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

        public /* override */ virtual void RegisterTouchArea(ITouchArea pTouchArea)
        {
            this.mTouchAreas.Add(pTouchArea);
        }

        public /* override */ virtual void UnregisterTouchArea(ITouchArea pTouchArea)
        {
            this.mTouchAreas.Remove(pTouchArea);
        }

        //public ArrayList<ITouchArea> getTouchAreas() {
        public IList<ITouchArea> GetTouchAreas()
        {
            return this.mTouchAreas;
        }

        public abstract void SetEntity(int pEntityIndex, IEntity pEntity);
        public abstract void SwapEntities(int pEntityIndexA, int pEntityIndexB);
        public abstract IEntity ReplaceEntity(int pEntityIndex, IEntity pEntity);
        public abstract void SortEntities();
        public abstract void SortEntities(IComparer<IEntity> entityComparator);
        public abstract IEntity GetEntity(/* final */ int pIndex);
        public abstract void AddEntity(/* final */ IEntity pEntity);
        public abstract IEntity FindEntity(/* final */ IEntityMatcher pEntityMatcher);
        public abstract IEntity RemoveEntity(/* final */ int pIndex);
        public abstract bool RemoveEntity(/* final */ IEntity pEntity);
        public abstract bool RemoveEntity(/* final */ IEntityMatcher pEntityMatcher);
        public abstract int GetEntityCount();
        public abstract void Clear();


        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}