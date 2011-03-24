using System.Collections.Generic;
using andengine.entity.modifier;
using andengine.util;

namespace andengine.entity
{

    using andengine.engine.handler/*.IUpdateHandler*/;
    using andengine.opengl/*.IDrawable*/;


    /**
     * @author Nicolas Gramlich
     * @since 11:20:25 - 08.03.2010
     */

    public interface IEntity : IDrawable, IUpdateHandler
    {
        int GetZIndex();
        void SetZIndex(int pZIndex);


        bool IsVisible();
        void SetVisible(bool pVisible);

        bool IsIgnoreUpdate();
        void SetIgnoreUpdate(bool pIgnoreUpdate);

        IEntity getParent();
        void setParent(IEntity pEntity);

        float getX();
        float getY();

        float getInitialX();
        float getInitialY();

        void setInitialPosition();
        void setPosition(IEntity pOtherEntity);
        void setPosition(float pX, float pY);

        float getRotation();
        void setRotation(float pRotation);

        float getRotationCenterX();
        float getRotationCenterY();
        void setRotationCenterX(float pRotationCenterX);
        void setRotationCenterY(float pRotationCenterY);
        void setRotationCenter(float pRotationCenterX, float pRotationCenterY);

        bool isScaled();
        float getScaleX();
        float getScaleY();
        void setScaleX(float pScaleX);
        void setScaleY(float pScaleY);
        void setScale(float pScale);
        void setScale(float pScaleX, float pScaleY);

        float getScaleCenterX();
        float getScaleCenterY();
        void setScaleCenterX(float pScaleCenterX);
        void setScaleCenterY(float pScaleCenterY);
        void setScaleCenter(float pScaleCenterX, float pScaleCenterY);

        float getRed();
        float getGreen();
        float getBlue();
        float getAlpha();
        void setAlpha(float pAlpha);

        void setColor(float pRed, float pGreen, float pBlue);
        void setColor(float pRed, float pGreen, float pBlue, float pAlpha);

        float[] getSceneCenterCoordinates();

        float[] convertLocalToSceneCoordinates(float pX, float pY);
        float[] convertSceneToLocalCoordinates(float pX, float pY);

        Transformation getLocalToSceneTransformation();
        Transformation getSceneToLocalTransformation();

        int getChildCount();

        void onAttached();
        void onDetached();

        void attachChild(IEntity pEntity);

        IEntity getChild(int pIndex);
        IEntity getFirstChild();
        IEntity getLastChild();

        IEntity findChild(IEntityMatcher pEntityMatcher);


        /// <summary>
        /// Sorts the <see cref="IEntity"/>s based on their ZIndex. Sort is stable.
        /// </summary>
        void sortChildren();

        /// <summary>
        /// Sorts the <see cref="IEntity"/>s based on the <see cref="IComparer"> supplied. Sort is stable.
        /// </summary>
        /// <param name="pEntityComparator">The entity comparer.</param>
        void sortChildren(IComparer<IEntity> pEntityComparator);

        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        bool detachChild(IEntity pEntity);
        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        IEntity detachChild(IEntityMatcher pEntityMatcher);
        /**
         * <b><i>WARNING:</i> This function should be called from within
         * {@link RunnableHandler#postRunnable(Runnable)} which is registered
         * to a {@link Scene} or the {@link Engine} itself, because otherwise
         * it may throw an {@link ArrayIndexOutOfBoundsException} in the
         * Update-Thread or the GL-Thread!</b>
         */
        bool detachChildren(IEntityMatcher pEntityMatcher);

        void detachChildren();

        void registerUpdateHandler(IUpdateHandler pUpdateHandler);
        bool unregisterUpdateHandler(IUpdateHandler pUpdateHandler);
        bool unregisterUpdateHandlers(IUpdateHandlerMatcher pUpdateHandlerMatcher);
        void clearUpdateHandlers();

        void registerEntityModifier(IEntityModifier pEntityModifier);
        bool unregisterEntityModifier(IEntityModifier pEntityModifier);
        bool unregisterEntityModifiers(IEntityModifierMatcher pEntityModifierMatcher);
        void clearEntityModifiers();

        void setUserData(object pUserData);
        object getUserData();
    }
}
