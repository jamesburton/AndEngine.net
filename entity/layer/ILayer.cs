namespace andengine.entity.layer
{

    //import java.util.ArrayList;
    //import java.util.Comparator;
    //using Comparator = Java.Util.IComparator;
    using Java.Util;

    using Engine = andengine.engine.Engine;
    using RunnableHandler = andengine.engine.handler.runnable.RunnableHandler;
    using IEntity = andengine.entity.IEntity;
    using Scene = andengine.entity.scene.Scene;
    using ITouchArea = andengine.entity.scene.Scene.ITouchArea;
    using IEntityMatcher = andengine.util.IEntityMatcher;
    using System.Collections.Generic;

    /**
     * @author Nicolas Gramlich
     * @since 12:09:22 - 09.07.2010
     */
    public interface ILayer : IEntity
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ void SetEntity(/* final */ int pEntityIndex, /* final */ IEntity pEntity);

        /* public */ void SwapEntities(/* final */ int pEntityIndexA, /* final */ int pEntityIndexB);

        /**
         * Similar to {@link ILayer#setEntity(int, ILayer)} but returns the {@link IEntity} that would be overwritten.
         * 
         * @param pEntityIndex
         * @param pEntity
         * @return the layer that has been replaced.
         */
        /* public */ IEntity ReplaceEntity(/* final */ int pEntityIndex, /* final */ IEntity pEntity);

        /**
         * Sorts the {@link IEntity}s based on their ZIndex. Sort is stable. 
         */
        /* public */ void SortEntities();

        /**
         * Sorts the {@link IEntity}s based on the {@link Comparator} supplied. Sort is stable.
         * @param pEntityComparator
         */
        // public void sortEntities(/* final */ Comparator<IEntity> pEntityComparator);
        void SortEntities(/* final */ IComparator pEntityComparator);

        /* public */ IEntity GetEntity(/* final */ int pIndex);

        /* public */ void AddEntity(/* final */ IEntity pEntity);

        /* public */ IEntity FindEntity(/* final */ IEntityMatcher pEntityMatcher);

        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        /* public */ IEntity RemoveEntity(/* final */ int pIndex);
        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        /* public */ bool RemoveEntity(/* final */ IEntity pEntity);
        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        /* public */ bool RemoveEntity(/* final */ IEntityMatcher pEntityMatcher);

        //public ArrayList<ITouchArea> getTouchAreas();
        /* public */ IList<ITouchArea> GetTouchAreas();

        /* public */ void RegisterTouchArea(/* final */ ITouchArea pTouchArea);

        /* public */ void UnregisterTouchArea(/* final */ ITouchArea pTouchArea);

        /* public */ int GetEntityCount();

        /* public */ /* abstract */ void Clear();
    }
}