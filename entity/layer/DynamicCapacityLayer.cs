namespace andengine.entity.layer
{

    //import java.util.ArrayList;
    //import java.util.Comparator;
    using System.Collections.Generic;
    using Comparator = Java.Util.IComparator;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using Camera = andengine.engine.camera.Camera;
    using IEntity = andengine.entity.IEntity;
    using IEntityMatcher = andengine.util.IEntityMatcher;


    /**
     * @author Nicolas Gramlich
     * @since 12:47:43 - 08.03.2010
     */
    public class DynamicCapacityLayer : BaseLayer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        private /* static final */ const int CAPACITY_DEFAULT = 10;

        // ===========================================================
        // Fields
        // ===========================================================

        //private final ArrayList<IEntity> mEntities;
        private readonly List<IEntity> mEntities;

        // ===========================================================
        // Constructors
        // ===========================================================

        public DynamicCapacityLayer()
        {
            mEntities = new List<IEntity>(CAPACITY_DEFAULT);
        }

        public DynamicCapacityLayer(int pExpectedCapacity)
        {
            this.mEntities = new List<IEntity>(pExpectedCapacity);
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override IEntity GetEntity(int pIndex)
        {
            return this.mEntities[pIndex];
        }

        public override int GetEntityCount()
        {
            return this.mEntities.Count;
        }
        public int EntityCount { get { return GetEntityCount(); } }

        protected override void OnManagedDraw(GL10 pGL, Camera pCamera)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = mEntities;
            int entityCount = entities.Count;
            for (int i = 0; i < entityCount; i++)
            {
                entities[i].OnDraw(pGL, pCamera);
            }
        }

        protected override void OnManagedUpdate(float pSecondsElapsed)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = mEntities;
            int entityCount = entities.Count;
            for (int i = 0; i < entityCount; i++)
            {
                entities[i].OnUpdate(pSecondsElapsed);
            }
        }

        public override void Reset()
        {
            base.Reset();

            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = this.mEntities;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                entities[i].Reset();
            }
        }

        public override void Clear()
        {
            this.mEntities.Clear();
        }

        public override void AddEntity(IEntity pEntity)
        {
            this.mEntities.Add(pEntity);
        }

        public override bool RemoveEntity(IEntity pEntity)
        {
            return this.mEntities.Remove(pEntity);
        }

        public override IEntity RemoveEntity(int pIndex)
        {
            IEntity entity = this.mEntities[pIndex];
            this.mEntities.RemoveAt(pIndex);
            return entity;
        }

        public override bool RemoveEntity(IEntityMatcher pEntityMatcher)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = this.mEntities;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                if (pEntityMatcher.Matches(entities[i]))
                {
                    entities.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public override IEntity FindEntity(IEntityMatcher pEntityMatcher)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = this.mEntities;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                IEntity entity = entities[i];
                if (pEntityMatcher.Matches(entity))
                {
                    return entity;
                }
            }
            return null;
        }

        public override void SortEntities()
        {
            //ZIndexSorter.GetInstance().sort(this.mEntities);
            ZIndexSorter.Instance.Sort(this.mEntities);
        }

        public override void SortEntities(/*Comparator<IEntity>*/ IComparer<IEntity> pEntityComparator)
        {
            //ZIndexSorter.getInstance().sort(this.mEntities, pEntityComparator);
            ZIndexSorter.Instance.Sort(this.mEntities, pEntityComparator);
        }

        public override IEntity ReplaceEntity(int pEntityIndex, IEntity pEntity)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = this.mEntities;
            IEntity oldEntity = entities[pEntityIndex] = pEntity;
            return oldEntity;
        }

        public override void SetEntity(int pEntityIndex, IEntity pEntity)
        {
            if (pEntityIndex == this.mEntities.Count)
            {
                this.AddEntity(pEntity);
            }
            else
            {
                this.mEntities[pEntityIndex] = pEntity;
            }
        }

        public override void SwapEntities(int pEntityIndexA, int pEntityIndexB)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            IList<IEntity> entities = this.mEntities;
            IEntity entityA = entities[pEntityIndexA];
            IEntity entityB = entities[pEntityIndexB] = entityA;
            entities[pEntityIndexA] = entityB;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}