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

        public override IEntity getEntity(int pIndex)
        {
            return this.mEntities[pIndex];
        }

        public override int getEntityCount()
        {
            return this.mEntities.Count;
        }

        protected override void onManagedDraw(GL10 pGL, Camera pCamera)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = mEntities;
            int entityCount = entities.Count;
            for (int i = 0; i < entityCount; i++)
            {
                entities[i].onDraw(pGL, pCamera);
            }
        }

        protected override void onManagedUpdate(float pSecondsElapsed)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = mEntities;
            int entityCount = entities.Count;
            for (int i = 0; i < entityCount; i++)
            {
                entities[i].onUpdate(pSecondsElapsed);
            }
        }

        public override void reset()
        {
            base.reset();

            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = this.mEntities;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                entities[i].reset();
            }
        }

        public override void clear()
        {
            this.mEntities.Clear();
        }

        public override void addEntity(IEntity pEntity)
        {
            this.mEntities.Add(pEntity);
        }

        public override bool removeEntity(IEntity pEntity)
        {
            return this.mEntities.Remove(pEntity);
        }

        public override IEntity removeEntity(int pIndex)
        {
            return this.mEntities.RemoveAt(pIndex);
        }

        public override bool removeEntity(IEntityMatcher pEntityMatcher)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = this.mEntities;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                if (pEntityMatcher.matches(entities[i]))
                {
                    entities.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }

        public override IEntity findEntity(IEntityMatcher pEntityMatcher)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = this.mEntities;
            for (int i = entities.Count - 1; i >= 0; i--)
            {
                IEntity entity = entities[i];
                if (pEntityMatcher.matches(entity))
                {
                    return entity;
                }
            }
            return null;
        }

        public override void sortEntities()
        {
            ZIndexSorter.getInstance().sort(this.mEntities);
        }

        public override void sortEntities(/*Comparator<IEntity>*/ Comparer<IEntity> pEntityComparator)
        {
            ZIndexSorter.getInstance().sort(this.mEntities, pEntityComparator);
        }

        public override IEntity replaceEntity(int pEntityIndex, IEntity pEntity)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = this.mEntities;
            IEntity oldEntity = entities[pEntityIndex] = pEntity;
            return oldEntity;
        }

        public override void setEntity(int pEntityIndex, IEntity pEntity)
        {
            if (pEntityIndex == this.mEntities.Count)
            {
                this.addEntity(pEntity);
            }
            else
            {
                this.mEntities[pEntityIndex] = pEntity;
            }
        }

        public override void swapEntities(int pEntityIndexA, int pEntityIndexB)
        {
            //final ArrayList<IEntity> entities = this.mEntities;
            List<IEntity> entities = this.mEntities;
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