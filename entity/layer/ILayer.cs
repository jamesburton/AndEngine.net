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

        /* public */ void setEntity(/* final */ int pEntityIndex, /* final */ IEntity pEntity);

        /* public */ void swapEntities(/* final */ int pEntityIndexA, /* final */ int pEntityIndexB);

        /**
         * Similar to {@link ILayer#setEntity(int, ILayer)} but returns the {@link IEntity} that would be overwritten.
         * 
         * @param pEntityIndex
         * @param pEntity
         * @return the layer that has been replaced.
         */
        /* public */ IEntity replaceEntity(/* final */ int pEntityIndex, /* final */ IEntity pEntity);

        /**
         * Sorts the {@link IEntity}s based on their ZIndex. Sort is stable. 
         */
        /* public */ void sortEntities();

        /**
         * Sorts the {@link IEntity}s based on the {@link Comparator} supplied. Sort is stable.
         * @param pEntityComparator
         */
        // public void sortEntities(/* final */ Comparator<IEntity> pEntityComparator);
        void sortEntities(/* final */ IComparator pEntityComparator);

        /* public */ IEntity getEntity(/* final */ int pIndex);

        /* public */ void addEntity(/* final */ IEntity pEntity);

        /* public */ IEntity findEntity(/* final */ IEntityMatcher pEntityMatcher);

        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        /* public */ IEntity removeEntity(/* final */ int pIndex);
        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        /* public */ bool removeEntity(/* final */ IEntity pEntity);
        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        /* public */ bool removeEntity(/* final */ IEntityMatcher pEntityMatcher);

        //public ArrayList<ITouchArea> getTouchAreas();
        /* public */ IList<ITouchArea> getTouchAreas();

        /* public */ void registerTouchArea(/* final */ ITouchArea pTouchArea);

        /* public */ void unregisterTouchArea(/* final */ ITouchArea pTouchArea);

        /* public */ int getEntityCount();

        /* public */ /* abstract */ void clear();
    }
}