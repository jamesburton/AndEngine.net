namespace andengine.util
{

    //using ByteArrayOutputStream = Java.IO.ByteArrayOutputStream;
    using Closeable = Java.IO.ICloseable;
    using IOException = Java.IO.IOException;
    //using InputStream = Java.IO.InputStream;
    using OutputStream = Java.IO.OutputStream;
    using Writer = Java.IO.Writer;
    using Scanner = Java.Util.Scanner;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 15:48:56 - 03.09.2009
     */
    public class StreamUtils
    {
        // ===========================================================
        // Constants
        // ===========================================================

        public static readonly int IO_BUFFER_SIZE = 8 * 1024;

        // ===========================================================
        // Fields
        // ===========================================================

        // ===========================================================
        // Constructors
        // ===========================================================

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods from SuperClass/Interfaces
        // ===========================================================

        // ===========================================================
        // Methods
        // ===========================================================

        /*
        public static String readFully(InputStream input)
            // throws IOException
        {
            StringBuilder sb = new StringBuilder();
            Scanner sc = new Scanner(input);
            while (sc.HasNextLine)
            {
                sb.Append(sc.NextLine());
            }
            return new Java.Lang.String(sb.ToString());
        }
        */

        public static String readFully(System.IO.Stream stream) {
            System.IO.StreamReader sr = new System.IO.StreamReader(stream);
            return new Java.Lang.String(sr.ReadToEnd());
        }

        /*
        public static byte[] streamToBytes(InputStream input) 
            // throws IOException
        {
            return StreamUtils.streamToBytes(input, -1);
        }
        */
        public static byte[] streamToBytes(System.IO.Stream input)
        {
            return StreamUtils.streamToBytes(input, -1);
        }

        /*
        public static byte[] streamToBytes(InputStream input, int pReadLimit) 
            // throws IOException
        {
            ByteArrayOutputStream os = new ByteArrayOutputStream(Math.min(pReadLimit, IO_BUFFER_SIZE));
            StreamUtils.copy(input, os, pReadLimit);
            return os.toByteArray();
        }
        */
        public static byte[] streamToBytes(System.IO.Stream input, int pReadLimit)
        {
            System.IO.MemoryStream mem = new System.IO.MemoryStream();
            StreamUtils.copy(input, mem, pReadLimit);
            return mem.ToArray();
        }

        /*
        public static void copy(InputStream input, OutputStream output) 
            // throws IOException 
        {
            StreamUtils.copy(input, output, -1);
        }
        */
        public static void copy(System.IO.Stream inputStream, System.IO.Stream outputStream)
        {
            StreamUtils.copy(inputStream, outputStream, -1);
        }

        //public static bool copyAndClose(InputStream input, OutputStream output)
        public static bool copyAndClose(System.IO.Stream input, System.IO.Stream output)
        {
            try
            {
                StreamUtils.copy(input, output, -1);
                return true;
            }
            catch (IOException e)
            {
                return false;
            }
            finally
            {
                StreamUtils.closeStream(input);
                StreamUtils.closeStream(output);
            }
        }

        /**
         * Copy the content of the input stream into the output stream, using a temporary
         * byte array buffer whose size is defined by {@link #IO_BUFFER_SIZE}.
         *
         * @param in The input stream to copy from.
         * @param out The output stream to copy to.
         * @param pByteLimit not more than so much bytes to read, or unlimited if smaller than 0.
         *
         * @throws IOException If any error occurs during the copy.
         */
        /* public static void copy(InputStream input, OutputStream output, long pByteLimit) 
            // throws IOException
        {
            byte[] b = new byte[IO_BUFFER_SIZE];
            long pBytesLeftToRead = pByteLimit;
            int read;
            if (pByteLimit < 0)
            {
                while ((read = input.read(b)) != -1)
                {
                    output.write(b, 0, read);
                }
            }
            else
            {
                while ((read = input.read(b)) != -1)
                {
                    if (pBytesLeftToRead > read)
                    {
                        output.write(b, 0, read);
                        pBytesLeftToRead -= read;
                    }
                    else
                    {
                        output.write(b, 0, (int)pBytesLeftToRead);
                        break;
                    }
                }
            }
            output.flush();
        }
        */
        public static void copy(System.IO.Stream input, System.IO.Stream output, int pByteLimit)
        {
            int bufferSize = pByteLimit < 0 ? IO_BUFFER_SIZE : (pByteLimit < IO_BUFFER_SIZE ? pByteLimit : IO_BUFFER_SIZE);
            byte[] b = new byte[bufferSize];
            long pBytesLeftToRead = pByteLimit;
            int read;
            if (pByteLimit < 0)
            {
                while ((read = input.Read(b, 0, bufferSize)) != -1)
                {
                    output.Write(b, 0, read);
                }
            }
            else
            {
                while ((read = input.Read(b, 0, bufferSize)) != -1)
                {
                    if (pBytesLeftToRead > read)
                    {
                        output.write(b, 0, read);
                        pBytesLeftToRead -= read;
                    }
                    else
                    {
                        output.Write(b, 0, (int)pBytesLeftToRead);
                        break;
                    }
                }
            }
            output.Flush();
        }

        /**
         * Closes the specified stream.
         *
         * @param pStream The stream to close.
         */
        public static void closeStream(Closeable pStream)
        {
            if (pStream != null)
            {
                try
                {
                    pStream.Close();
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        /**
         * Flushes and closes the specified stream.
         *
         * @param pStream The stream to close.
         */
        public static void flushCloseStream(OutputStream pStream)
        {
            if (pStream != null)
            {
                try
                {
                    pStream.Flush();
                    StreamUtils.closeStream(pStream);
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        /**
         * Flushes and closes the specified stream.
         *
         * @param pWriter The Writer to close.
         */
        public static void flushCloseWriter(Writer pWriter)
        {
            if (pWriter != null)
            {
                try
                {
                    pWriter.Flush();
                    StreamUtils.closeStream(pWriter);
                }
                catch (IOException e)
                {
                    e.PrintStackTrace();
                }
            }
        }

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================
    }
}