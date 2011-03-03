namespace andengine.opengl.util
{

    //import static org.anddev.andengine.opengl.util.GLHelper.BYTES_PER_FLOAT;
    //using GLHelper = andengine.opengl.util.GLHelper;

    //import java.lang.ref.SoftReference;
    //import java.nio.ByteBuffer;
    using ByteBuffer = Java.Nio.ByteBuffer;
    //import java.nio.ByteOrder;
    using ByteOrder = Java.Nio.ByteOrder;
    //import java.nio.FloatBuffer;
    using FloatBuffer = Java.Nio.FloatBuffer;
    //import java.nio.IntBuffer;
    using IntBuffer = Java.Nio.IntBuffer;
    using Java.Lang;

    /**
     * Convenient work-around for poor {@link FloatBuffer#put(float[])} performance.
     * This should become unnecessary in gingerbread, 
     * @see <a href="http://code.google.com/p/android/issues/detail?id=11078">Issue 11078</a>
     * 
     * @author ryanm
     */
    public class FastFloatBuffer
    {
        // ===========================================================
        // Constants
        // ===========================================================

        // ===========================================================
        // Fields
        // ===========================================================

        /**
         * Use a {@link SoftReference} so that the array can be collected if
         * necessary
         */
        private static SoftReference<int[]> sWeakIntArray = new SoftReference<int[]>(new int[0]);

        /**
         * Underlying data - give this to OpenGL
         */
        public readonly ByteBuffer mByteBuffer;
        private readonly FloatBuffer mFloatBuffer;
        private readonly IntBuffer mIntBuffer;

        // ===========================================================
        // Constructors
        // ===========================================================

        /**
         * Constructs a new direct native-ordered buffer
         */
        public FastFloatBuffer(int pCapacity)
        {
            this.mByteBuffer = ByteBuffer.AllocateDirect((pCapacity * GLHelper.BYTES_PER_FLOAT)).Order(ByteOrder.NativeOrder());
            this.mFloatBuffer = this.mByteBuffer.AsFloatBuffer();
            this.mIntBuffer = this.mByteBuffer.AsIntBuffer();
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /**
         * See {@link FloatBuffer#flip()}
         */
        public void Flip()
        {
            this.mByteBuffer.Flip();
            this.mFloatBuffer.Flip();
            this.mIntBuffer.Flip();
        }

        /**
         * See {@link FloatBuffer#put(float)}
         */
        public void pPut(float f)
        {
            ByteBuffer byteBuffer = this.mByteBuffer;
            IntBuffer intBuffer = this.mIntBuffer;

            byteBuffer.Position(byteBuffer.Position() + GLHelper.BYTES_PER_FLOAT);
            this.mFloatBuffer.Put(f);
            intBuffer.Position(intBuffer.Position() + 1);
        }

        /**
         * It's like {@link FloatBuffer#put(float[])}, but about 10 times faster
         */
        public void Put(float[] data)
        {
            int length = data.Length;

            int[] ia = sWeakIntArray.Get();
            if (ia == null || ia.Length < length)
            {
                ia = new int[length];
                sWeakIntArray = new SoftReference<int[]>(ia);
            }

            for (int i = 0; i < length; i++)
            {
                ia[i] = Float.FloatToRawIntBits(data[i]);
            }

            ByteBuffer byteBuffer = this.mByteBuffer;
            byteBuffer.Position(byteBuffer.Position() + GLHelper.BYTES_PER_FLOAT * length);
            FloatBuffer floatBuffer = this.mFloatBuffer;
            floatBuffer.Position(floatBuffer.Position() + length);
            this.mIntBuffer.Put(ia, 0, length);
        }
        public void Put(float data) { float[] dataArray = new float[1]; dataArray[0] = data; Put(dataArray); }

        /**
         * For use with pre-converted data. This is 50x faster than
         * {@link #put(float[])}, and 500x faster than
         * {@link FloatBuffer#put(float[])}, so if you've got float[] data that
         * won't change, {@link #convert(float...)} it to an int[] once and use this
         * method to put it in the buffer
         * 
         * @param data floats that have been converted with {@link Float#floatToIntBits(float)}
         */
        public void Put(int[] data)
        {
            ByteBuffer byteBuffer = this.mByteBuffer;
            byteBuffer.Position(byteBuffer.Position() + GLHelper.BYTES_PER_FLOAT * data.Length);
            FloatBuffer floatBuffer = this.mFloatBuffer;
            floatBuffer.Position(floatBuffer.Position() + data.Length);
            this.mIntBuffer.Put(data, 0, data.Length);
        }
        public void Put(int data) { int[] dataArray = new int[1]; dataArray[0] = data; Put(dataArray); }

        /**
         * Converts float data to a format that can be quickly added to the buffer
         * with {@link #put(int[])}
         * 
         * @param data
         * @return the int-formatted data
         */
        //public static int[] convert(final float ... data) {
        public static int[] Convert(params float[] data)
        {
            int length = data.Length;
            int[] id = new int[length];
            for (int i = 0; i < length; i++)
            {
                id[i] = Float.FloatToRawIntBits(data[i]);
            }

            return id;
        }

        /**
         * See {@link FloatBuffer#put(FloatBuffer)}
         */
        public void Put(FastFloatBuffer b)
        {
            ByteBuffer byteBuffer = this.mByteBuffer;
            byteBuffer.Put(b.mByteBuffer);
            this.mFloatBuffer.Position(byteBuffer.Position() >> 2);
            this.mIntBuffer.Position(byteBuffer.Position() >> 2);
        }

        /**
         * @return See {@link FloatBuffer#capacity()}
         */
        public int Capacity()
        {
            return this.mFloatBuffer.Capacity();
        }

        /**
         * @return See {@link FloatBuffer#position()}
         */
        public int Position()
        {
            return this.mFloatBuffer.Position();
        }

        /**
         * See {@link FloatBuffer#position(int)}
         */
        public void Position(int p)
        {
            this.mByteBuffer.Position(p * GLHelper.BYTES_PER_FLOAT);
            this.mFloatBuffer.Position(p);
            this.mIntBuffer.Position(p);
        }

        /**
         * @return See {@link FloatBuffer#slice()}
         */
        public FloatBuffer Slice()
        {
            return this.mFloatBuffer.Slice();
        }

        /**
         * @return See {@link FloatBuffer#remaining()}
         */
        public int Remaining()
        {
            return this.mFloatBuffer.Remaining();
        }

        /**
         * @return See {@link FloatBuffer#limit()}
         */
        public int Limit()
        {
            return this.mFloatBuffer.Limit();
        }

        /**
         * See {@link FloatBuffer#clear()}
         */
        public void Clear()
        {
            this.mByteBuffer.Clear();
            this.mFloatBuffer.Clear();
            this.mIntBuffer.Clear();
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}