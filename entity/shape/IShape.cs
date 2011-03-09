namespace andengine.entity.shape
{

    using andengine.entity/*.IEntity*/;
    using andengine.entity.scene/*.Scene.ITouchArea*/;
    using andengine.util.modifier/*.IModifier*/;

    /**
     * @author Nicolas Gramlich
     * @since 13:32:52 - 07.07.2010
     */
    public interface IShape : IEntity, Scene.ITouchArea
    {
        // ===========================================================
        // Final Fields
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ float GetRed();
        /* public */ float GetGreen();
        /* public */ float GetBlue();
        /* public */ float GetAlpha();
        /* public */ void SetAlpha(/* final */ float pAlpha);

        /* public */ void SetColor(/* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue);
        /* public */ void SetColor(/* final */ float pRed, /* final */ float pGreen, /* final */ float pBlue, /* final */ float pAlpha);

        /* public */ float GetX();
        /* public */ float GetY();

        /* public */ float GetBaseX();
        /* public */ float GetBaseY();

        /* public */ float[] GetSceneCenterCoordinates();

        /* public */ void SetBasePosition();
        /* public */ void SetPosition(/* final */ IShape pOtherShape);
        /* public */ void SetPosition(/* final */ float pX, /* final */ float pY);

        /* public */ float GetVelocityX();
        /* public */ float GetVelocityY();
        /* public */ void SetVelocityX(/* final */ float pVelocityX);
        /* public */ void SetVelocityY(/* final */ float pVelocityY);
        /* public */ void SetVelocity(/* final */ float pVelocity);
        /* public */ void SetVelocity(/* final */ float pVelocityX, /* final */ float pVelocityY);

        /* public */ float GetAccelerationX();
        /* public */ float GetAccelerationY();
        /* public */ void SetAccelerationX(/* final */ float pAccelerationX);
        /* public */ void SetAccelerationY(/* final */ float pAccelerationY);
        /* public */ void SetAcceleration(/* final */ float pAcceleration);
        /* public */ void SetAcceleration(/* final */ float pAccelerationX, /* final */ float pAccelerationY);
        /* public */ void Accelerate(/* final */ float pAccelerationX, /* final */ float pAccelerationY);

        /* public */ float GetRotation();
        /* public */ void SetRotation(/* final */ float pRotation);

        /* public */ float GetAngularVelocity();
        /* public */ void SetAngularVelocity(/* final */ float pAngularVelocity);

        /* public */ float GetRotationCenterX();
        /* public */ float GetRotationCenterY();
        /* public */ void SetRotationCenterX(/* final */ float pRotationCenterX);
        /* public */ void SetRotationCenterY(/* final */ float pRotationCenterY);
        /* public */ void SetRotationCenter(/* final */ float pRotationCenterX, /* final */ float pRotationCenterY);

        /* public */ bool IsScaled();
        /* public */ float GetScaleX();
        /* public */ float GetScaleY();
        /* public */ void SetScaleX(/* final */ float pScaleX);
        /* public */ void SetScaleY(/* final */ float pScaleY);
        /* public */ void SetScale(/* final */ float pScale);
        /* public */ void SetScale(/* final */ float pScaleX, /* final */ float pScaleY);

        /* public */ float GetScaleCenterX();
        /* public */ float GetScaleCenterY();
        /* public */ void SetScaleCenterX(/* final */ float pScaleCenterX);
        /* public */ void SetScaleCenterY(/* final */ float pScaleCenterY);
        /* public */ void SetScaleCenter(/* final */ float pScaleCenterX, /* final */ float pScaleCenterY);

        /* public */ bool IsUpdatePhysics();
        /* public */ void SetUpdatePhysics(/* final */ bool pUpdatePhysics);

        /* public */ bool IsCullingEnabled();
        /* public */ void SetCullingEnabled(/* final */ bool pCullingEnabled);

        /* public */ float GetWidth();
        /* public */ float GetHeight();

        /* public */ float GetBaseWidth();
        /* public */ float GetBaseHeight();

        /* public */ float GetWidthScaled();
        /* public */ float GetHeightScaled();

        /* public */ void AddShapeModifier(/* final */ IModifier<IShape> pShapeModifier);
        /* public */ bool RemoveShapeModifier(/* final */ IModifier<IShape> pShapeModifier);
        /* public */ void ClearShapeModifiers();

        /* public */ bool CollidesWith(/* final */ IShape pOtherShape);

        /* public */ void SetBlendFunction(/* final */ int pSourceBlendFunction, /* final */ int pDestinationBlendFunction);
    }
}