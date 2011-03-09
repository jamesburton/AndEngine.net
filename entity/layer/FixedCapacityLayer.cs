namespace andengine.entity.layer
{

    //import java.util.Comparator;

    //import javax.microedition.khronos.opengles.GL10;
    using GL10 = Javax.Microedition.Khronos.Opengles.IGL10;
    using GL10Consts = Javax.Microedition.Khronos.Opengles.GL10Consts;

    using Camera = andengine.engine.camera.Camera;
    using IEntity = andengine.entity.IEntity;
    using IEntityMatcher = andengine.util.IEntityMatcher;
    using System.Collections.Generic;
    using Java.Lang;


    /**
     * @author Nicolas Gramlich
     * @since 12:47:43 - 08.03.2010
     */
    public class FixedCapacityLayer : BaseLayer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly IEntity[] mEntities;
        private readonly int mCapacity;
        private int mEntityCount;

        // ===========================================================
        // Constructors
        // ===========================================================

        public FixedCapacityLayer(int pCapacity)
        {
            this.mCapacity = pCapacity;
            this.mEntities = new IEntity[pCapacity];
            this.mEntityCount = 0;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override int GetEntityCount()
        {
            return this.mEntityCount;
        }
        public int EntityCount { get { return GetEntityCount(); } }

        protected override void OnManagedDraw(GL10 pGL, Camera pCamera)
        {
            IEntity[] entities = this.mEntities;
            int entityCount = this.mEntityCount;
            for (int i = 0; i < entityCount; i++)
            {
                entities[i].OnDraw(pGL, pCamera);
            }
        }

        protected override void OnManagedUpdate(float pSecondsElapsed)
        {
            IEntity[] entities = this.mEntities;
            int entityCount = this.mEntityCount;
            for (int i = 0; i < entityCount; i++)
            {
                entities[i].OnUpdate(pSecondsElapsed);
            }
        }

        public override void Reset()
        {
            base.Reset();

            IEntity[] entities = this.mEntities;
            for (int i = this.mEntityCount - 1; i >= 0; i--)
            {
                entities[i].Reset();
            }
        }

        public override IEntity GetEntity(int pIndex)
        {
            this.CheckIndex(pIndex);

            return this.mEntities[pIndex];
        }

        public override void Clear() {
		    IEntity[] entities = this.mEntities;
		    for(int i = this.mEntityCount - 1; i >= 0; i--) {
			    entities[i] = null;
		    }
		    this.mEntityCount = 0;
	    }

        public override void AddEntity(IEntity pEntity)
        {
            if (this.mEntityCount < this.mCapacity)
            {
                this.mEntities[this.mEntityCount] = pEntity;
                this.mEntityCount++;
            }
        }

        public override bool RemoveEntity(IEntity pEntity)
        {
            return this.RemoveEntity(this.indexOfEntity(pEntity)) != null;
        }

        public override IEntity RemoveEntity(int pIndex)
        {
            this.CheckIndex(pIndex);

            IEntity[] entities = this.mEntities;
            IEntity retVal = entities[pIndex];

            int lastIndex = this.mEntityCount - 1;
            if (pIndex == lastIndex)
            {
                this.mEntities[lastIndex] = null;
            }
            else
            {
                entities[pIndex] = entities[lastIndex];
                entities[this.mEntityCount] = null;
            }
            this.mEntityCount = lastIndex;

            return retVal;
        }

        public override bool RemoveEntity(IEntityMatcher pEntityMatcher)
        {
            IEntity[] entities = this.mEntities;
            for (int i = entities.Length - 1; i >= 0; i--)
            {
                if (pEntityMatcher.Matches(entities[i]))
                {
                    this.RemoveEntity(i);
                    return true;
                }
            }
            return false;
        }

        public override IEntity FindEntity(IEntityMatcher pEntityMatcher)
        {
            IEntity[] entities = this.mEntities;
            for (int i = entities.Length - 1; i >= 0; i--)
            {
                IEntity entity = entities[i];
                if (pEntityMatcher.Matches(entity))
                {
                    return entity;
                }
            }
            return null;
        }

        private int IndexOfEntity(IEntity pEntity)
        {
            IEntity[] entities = this.mEntities;
            for (int i = entities.Length - 1; i >= 0; i--)
            {
                IEntity entity = entities[i];
                if (entity == pEntity)
                {
                    return i;
                }
            }
            return -1;
        }

        public override void SortEntities()
        {
            //ZIndexSorter.getInstance().sort(this.mEntities, 0, this.mEntityCount);
            ZIndexSorter.Instance.Sort(this.mEntities, 0, this.mEntityCount);
        }

        public override void SortEntities(/*final Comparator<IEntity>*/ Comparer<IEntity> pEntityComparator)
        {
            //ZIndexSorter.getInstance().sort(this.mEntities, 0, this.mEntityCount, pEntityComparator);
            ZIndexSorter.Instance.Sort(this.mEntities, 0, this.mEntityCount, pEntityComparator);
        }

        public override IEntity ReplaceEntity(int pIndex, IEntity pEntity)
        {
            this.CheckIndex(pIndex);

            IEntity[] entities = this.mEntities;
            IEntity oldEntity = entities[pIndex];
            entities[pIndex] = pEntity;
            return oldEntity;
        }

        public override void SetEntity(int pIndex, IEntity pEntity)
        {
            this.checkIndex(pIndex);

            if (pIndex == this.mEntityCount)
            {
                this.AddEntity(pEntity);
            }
            else if (pIndex < this.mEntityCount)
            {
                this.mEntities[pIndex] = pEntity;
            }
        }

        public override void SwapEntities(int pEntityIndexA, int pEntityIndexB)
        {
            if (pEntityIndexA > this.mEntityCount)
            {
                throw new IndexOutOfBoundsException("pEntityIndexA was bigger than the EntityCount.");
            }
            if (pEntityIndexA > this.mEntityCount)
            {
                throw new IndexOutOfBoundsException("pEntityIndexB was bigger than the EntityCount.");
            }
            IEntity[] entities = this.mEntities;
            IEntity tmp = entities[pEntityIndexA];
            entities[pEntityIndexA] = entities[pEntityIndexB];
            entities[pEntityIndexB] = tmp;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        private void CheckIndex(int pIndex)
        {
            if (pIndex < 0 || pIndex >= this.mEntityCount)
            {
                throw new IndexOutOfBoundsException("Invalid index: " + pIndex + " (Size: " + this.mEntityCount + " | Capacity: " + this.mCapacity + ")");
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}