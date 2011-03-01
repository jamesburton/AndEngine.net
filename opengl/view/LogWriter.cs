/**
 * 
 */
namespace andengine.opengl.view
{

    using Writer = Java.IO.Writer;

    using Log = Android.Util.Log;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 20:42:02 - 28.06.2010
     */
    class LogWriter : Writer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly StringBuilder mBuilder = new StringBuilder();

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        public override void close()
        {
            this.flushBuilder();
        }

        public override void flush()
        {
            this.flushBuilder();
        }

        public override void write(char[] buf, int offset, int count)
        {
            for (int i = 0; i < count; i++)
            {
                char c = buf[offset + i];
                if (c == '\n')
                {
                    this.flushBuilder();
                }
                else
                {
                    this.mBuilder.Append(c);
                }
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        private void flushBuilder()
        {
            if (this.mBuilder.Length() > 0)
            {
                Log.Verbose("GLSurfaceView", this.mBuilder.ToString());
                this.mBuilder.Delete(0, this.mBuilder.Length());
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}