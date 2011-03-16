namespace andengine.opengl.texture.builder
{

    //import java.util.ArrayList;
    using System.Collections.Generic;
    //import java.util.Collections;
    //import java.util.Comparator;
    //using Comparator = Java.Util.IComparator;

    using BuildableTexture = andengine.opengl.texture.BuildableTexture;
    using TextureSourceWithLocationCallback = andengine.opengl.texture.BuildableTexture.TextureSourceWithWithLocationCallback;
    using TextureSourceWithLocation = andengine.opengl.texture.Texture.TextureSourceWithLocation;
    using ITextureSource = andengine.opengl.texture.source.ITextureSource;
    using Java.Lang;

    /**
     * @author Nicolas Gramlich
     * @since 16:03:01 - 12.08.2010
     * @see http://www.blackpawn.com/texts/lightmaps/default.html
     */
    public class BlackPawnTextureBuilder : ITextureBuilder
    {
        // ===========================================================
        // Constants
        // ===========================================================

        /*
        private static final Comparator<ITextureSource> TEXTURESOURCE_COMPARATOR = new Comparator<ITextureSource>() {
            @Override
            public int compare(final ITextureSource pTextureSourceA, final ITextureSource pTextureSourceB) {
                final int deltaWidth = pTextureSourceB.getWidth() - pTextureSourceA.getWidth();
                if(deltaWidth != 0) {
                    return deltaWidth;
                } else {
                    return pTextureSourceB.getHeight() - pTextureSourceA.getHeight();
                }
            }
        };
        */

        private readonly Comparer<ITextureSource> TEXTURESOURCE_COMPARER;
        public class TextureSourceComparer : Comparer<ITextureSource>
        {
            protected readonly BlackPawnTextureBuilder Parent;
            public TextureSourceComparer(BlackPawnTextureBuilder parent)
            {
                Parent = parent;
            }

            public override int Compare(ITextureSource x, ITextureSource y)
            {
                int deltaWidth = y.GetWidth() - x.GetWidth();
                if (deltaWidth != 0)
                {
                    return deltaWidth;
                }
                else
                {
                    return y.GetHeight() - x.GetHeight();
                }
            }
        }

        // ===========================================================
        // Fields
        // ===========================================================

        private readonly int mTextureSourceSpacing;

        // ===========================================================
        // Constructors
        // ===========================================================

        public BlackPawnTextureBuilder(int pTextureSourceSpacing)
        {
            TEXTURESOURCE_COMPARER = new TextureSourceComparer(this);
            this.mTextureSourceSpacing = pTextureSourceSpacing;
        }

        // ===========================================================
        // Getter & Setter
        // ===========================================================

        // ===========================================================
        // Methods for/from SuperClass/Interfaces
        // ===========================================================

        //@SuppressWarnings("deprecation")
        //@Override
        //public override void pack(final BuildableTexture pBuildableTexture, final ArrayList<TextureSourceWithWithLocationCallback> pTextureSourcesWithLocationCallback) throws IllegalArgumentException {
        public /* override */ void Pack(BuildableTexture pBuildableTexture, List<TextureSourceWithLocationCallback> pTextureSourcesWithLocationCallback)
        {
            //Collections.sort(pTextureSourcesWithLocationCallback, TEXTURESOURCE_COMPARATOR);
            pTextureSourcesWithLocationCallback.Sort((IComparer<TextureSourceWithLocationCallback>)TEXTURESOURCE_COMPARER);

            NodeClass root = new NodeClass(new RectClass(0, 0, pBuildableTexture.GetWidth(), pBuildableTexture.GetHeight()));

            int textureSourceCount = pTextureSourcesWithLocationCallback.Count;

            for (int i = 0; i < textureSourceCount; i++)
            {
                TextureSourceWithLocationCallback textureSourceWithLocationCallback = pTextureSourcesWithLocationCallback[i];
                ITextureSource textureSource = textureSourceWithLocationCallback.GetTextureSource();

                NodeClass inserted = root.Insert(textureSource, pBuildableTexture.GetWidth(), pBuildableTexture.GetHeight(), this.mTextureSourceSpacing);

                if (inserted == null)
                {
                    throw new IllegalArgumentException("Could not pack: " + textureSource.ToString());
                }
                TextureSourceWithLocation textureSourceWithLocation = pBuildableTexture.AddTextureSource(textureSource, inserted.mRect.mLeft, inserted.mRect.mTop);
                textureSourceWithLocationCallback.GetCallback().OnCallback(textureSourceWithLocation);
            }
        }

        // ===========================================================
        // Methods
        // ===========================================================

        // ===========================================================
        // Inner and Anonymous Classes
        // ===========================================================

        protected static RectClass Rect;
        //protected static class Rect {
        public class RectClass
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            internal readonly int mLeft;
            internal readonly int mTop;
            internal readonly int mWidth;
            internal readonly int mHeight;

            // ===========================================================
            // Constructors
            // ===========================================================

            //public Rect(int pLeft, int pTop, int pWidth, int pHeight) {
            public RectClass(int pLeft, int pTop, int pWidth, int pHeight)
            {
                this.mLeft = pLeft;
                this.mTop = pTop;
                this.mWidth = pWidth;
                this.mHeight = pHeight;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            public int Width { get { return GetWidth(); } }
            public int Height { get { return GetHeight(); } }
            public int Top { get { return GetTop(); } }
            public int Bottom { get { return GetBottom(); } }
            public int Left { get { return GetLeft(); } }
            public int Right { get { return GetRight(); } }

            public int GetWidth()
            {
                return this.mWidth;
            }

            public int GetHeight()
            {
                return this.mHeight;
            }

            public int GetLeft()
            {
                return this.mLeft;
            }

            public int GetTop()
            {
                return this.mTop;
            }

            public int GetRight()
            {
                return this.mLeft + this.mWidth;
            }

            public int GetBottom()
            {
                return this.mTop + this.mHeight;
            }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            /*
            public override String toString() {
                return "@: " + this.mLeft + "/" + this.mTop + " * " + this.mWidth + "x" + this.mHeight;
            }
            */
            public override System.String ToString()
            {
                return System.String.Format("@: {0}/{1} * {2}x{3}", this.mLeft, this.mTop, this.mWidth, this.mHeight);
            }
            /*
            public override Java.Lang.String toString()
            {
                return new Java.Lang.String(ToString());
            } //*/

            // ===========================================================
            // Methods
            // ===========================================================

            // ===========================================================
            // Inner and Anonymous Classes
            // ===========================================================
        }

        //protected static class Node {
        protected static NodeClass Node;
        public class NodeClass
        {
            // ===========================================================
            // Constants
            // ===========================================================

            // ===========================================================
            // Fields
            // ===========================================================

            //private Node mChildA;
            private NodeClass mChildA;
            //private Node mChildB;
            private NodeClass mChildB;
            //private final Rect mRect;
            internal readonly RectClass mRect;
            private ITextureSource mTextureSource;

            // ===========================================================
            // Constructors
            // ===========================================================

            //public Node(final int pLeft, final int pTop, final int pWidth, final int pHeight)
            public NodeClass(int pLeft, int pTop, int pWidth, int pHeight)
                : this(new RectClass(pLeft, pTop, pWidth, pHeight))
            {
            }

            //public Node(final Rect pRect) {
            public NodeClass(RectClass pRect)
            {
                this.mRect = pRect;
            }

            // ===========================================================
            // Getter & Setter
            // ===========================================================

            //public Rect getRect() {
            public RectClass GetRect()
            {
                return this.mRect;
            }
            public RectClass Rect { get { return GetRect(); } }

            //public Node getChildA() {
            public NodeClass GetChildA()
            {
                return this.mChildA;
            }
            public NodeClass ChildA { get { return GetChildA(); } }

            //public Node getChildB() {
            public NodeClass GetChildB()
            {
                return this.mChildB;
            }
            public NodeClass ChildB { get { return GetChildB(); } }

            // ===========================================================
            // Methods for/from SuperClass/Interfaces
            // ===========================================================

            // ===========================================================
            // Methods
            // ===========================================================

            //	public Node insert(final ITextureSource pTextureSource, final int pTextureWidth, final int pTextureHeight, final int pTextureSpacing) throws IllegalArgumentException {
            public NodeClass Insert(ITextureSource pTextureSource, int pTextureWidth, int pTextureHeight, int pTextureSpacing) /* throws IllegalArgumentException */ {
                if (this.mChildA != null && this.mChildB != null)
                {
                    NodeClass newNode = this.mChildA.Insert(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing);
                    if (newNode != null)
                    {
                        return newNode;
                    }
                    else
                    {
                        return this.mChildB.Insert(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing);
                    }
                }
                else
                {
                    if (this.mTextureSource != null)
                    {
                        return null;
                    }

                    int textureSourceWidth = pTextureSource.GetWidth();
                    int textureSourceHeight = pTextureSource.GetHeight();

                    int rectWidth = this.mRect.GetWidth();
                    int rectHeight = this.mRect.GetHeight();

                    if (textureSourceWidth > rectWidth || textureSourceHeight > rectHeight)
                    {
                        return null;
                    }

                    int textureSourceWidthWithSpacing = textureSourceWidth + pTextureSpacing;
                    int textureSourceHeightWithSpacing = textureSourceHeight + pTextureSpacing;

                    int rectLeft = this.mRect.GetLeft();
                    int rectTop = this.mRect.GetTop();

                    bool fitToBottomWithoutSpacing = textureSourceHeight == rectHeight && rectTop + textureSourceHeight == pTextureHeight;
                    bool fitToRightWithoutSpacing = textureSourceWidth == rectWidth && rectLeft + textureSourceWidth == pTextureWidth;

                    if (textureSourceWidthWithSpacing == rectWidth)
                    {
                        if (textureSourceHeightWithSpacing == rectHeight)
                        { /* Normal case with padding. */
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (fitToBottomWithoutSpacing)
                        { /* Bottom edge of the Texture. */
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                    }

                    if (fitToRightWithoutSpacing)
                    { /* Right edge of the Texture. */
                        if (textureSourceHeightWithSpacing == rectHeight)
                        {
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (fitToBottomWithoutSpacing)
                        { /* Bottom edge of the Texture. */
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (textureSourceHeightWithSpacing > rectHeight)
                        {
                            return null;
                        }
                        else
                        {

                            return CreateChildren(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing, rectWidth - textureSourceWidth, rectHeight - textureSourceHeightWithSpacing);
                        }
                    }

                    if (fitToBottomWithoutSpacing)
                    {
                        if (textureSourceWidthWithSpacing == rectWidth)
                        {
                            this.mTextureSource = pTextureSource;
                            return this;
                        }
                        else if (textureSourceWidthWithSpacing > rectWidth)
                        {
                            return null;
                        }
                        else
                        {
                            return CreateChildren(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing, rectWidth - textureSourceWidthWithSpacing, rectHeight - textureSourceHeight);
                        }
                    }
                    else if (textureSourceWidthWithSpacing > rectWidth || textureSourceHeightWithSpacing > rectHeight)
                    {
                        return null;
                    }
                    else
                    {
                        return CreateChildren(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing, rectWidth - textureSourceWidthWithSpacing, rectHeight - textureSourceHeightWithSpacing);
                    }
                }
            }

            //private Node createChildren(final ITextureSource pTextureSource, final int pTextureWidth, final int pTextureHeight, final int pTextureSpacing, final int pDeltaWidth, final int pDeltaHeight) {
            private NodeClass CreateChildren(ITextureSource pTextureSource, int pTextureWidth, int pTextureHeight, int pTextureSpacing, int pDeltaWidth, int pDeltaHeight)
            {
                RectClass rect = this.mRect;

                if (pDeltaWidth >= pDeltaHeight)
                {
                    /* Split using a vertical axis. */
                    this.mChildA = new NodeClass(
                            rect.GetLeft(),
                            rect.GetTop(),
                            pTextureSource.GetWidth() + pTextureSpacing,
                            rect.GetHeight()
                    );

                    this.mChildB = new NodeClass(
                            rect.GetLeft() + (pTextureSource.GetWidth() + pTextureSpacing),
                            rect.GetTop(),
                            rect.GetWidth() - (pTextureSource.GetWidth() + pTextureSpacing),
                            rect.GetHeight()
                    );
                }
                else
                {
                    /* Split using a horizontal axis. */
                    this.mChildA = new NodeClass(
                            rect.GetLeft(),
                            rect.GetTop(),
                            rect.GetWidth(),
                            pTextureSource.GetHeight() + pTextureSpacing
                    );

                    this.mChildB = new NodeClass(
                            rect.GetLeft(),
                            rect.GetTop() + (pTextureSource.GetHeight() + pTextureSpacing),
                            rect.GetWidth(),
                            rect.GetHeight() - (pTextureSource.GetHeight() + pTextureSpacing)
                    );
                }

                return this.mChildA.Insert(pTextureSource, pTextureWidth, pTextureHeight, pTextureSpacing);
            }

            // ===========================================================
            // Inner and Anonymous Classes
            // ===========================================================
        }
    }
}