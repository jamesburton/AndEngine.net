namespace andengine.util
{

    using IEntity = andengine.entity.IEntity;

    /**
     * @author Nicolas Gramlich
     * @since 15:45:58 - 21.06.2010
     */
    public interface IEntityMatcher
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /* public */ bool matches(/* final */ IEntity pEntity);
    }
}

