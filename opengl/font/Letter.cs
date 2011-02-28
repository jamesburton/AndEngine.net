namespace andengine.opengl.font
{
    /**
     * @author Nicolas Gramlich
     * @since 10:30:22 - 03.04.2010
     */
    public class Letter
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        public readonly int mAdvance;
        public readonly int mWidth;
        public readonly int mHeight;
        public readonly float mTextureX;
        public readonly float mTextureY;
        public readonly float mTextureWidth;
        public readonly float mTextureHeight;
        public readonly char mCharacter;

        // ===========================================================
        // Constructors
        // ===========================================================

        Letter(char pCharacter, int pAdvance, int pWidth, int pHeight, float pTextureX, float pTextureY, float pTextureWidth, float pTextureHeight)
        {
            this.mCharacter = pCharacter;
            this.mAdvance = pAdvance;
            this.mWidth = pWidth;
            this.mHeight = pHeight;
            this.mTextureX = pTextureX;
            this.mTextureY = pTextureY;
            this.mTextureWidth = pTextureWidth;
            this.mTextureHeight = pTextureHeight;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override int hashCode()
        {
            int prime = 31;
            int result = 1;
            result = prime * result + this.mCharacter;
            return result;
        }

        //public override bool equals(Object obj)
        public override bool equals(object obj)
        {
            if (this == obj)
            {
                return true;
            }
            if (obj == null)
            {
                return false;
            }
            //if(this.getClass() != obj.getClass()) {
            if (this.GetType() != obj.GetType())
            {
                return false;
            }
            Letter other = (Letter)obj;
            if (this.mCharacter != other.mCharacter)
            {
                return false;
            }
            return true;
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}