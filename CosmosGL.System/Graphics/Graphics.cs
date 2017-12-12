using System;
using System.Collections.Generic;
using PolyPartition;

namespace CosmosGL.System.Graphics
{
    public class Graphics
    {
        private Canvas _canvas;

        public Graphics(Canvas canvas)
        {
            _canvas = canvas;
        }

        #region Internals

        public void SetPixel(int x, int y, Color c)
        {
            if (c.A != 255)
            {
                //transparency needed
                var p = 1 - c.A;
                var C2 = _canvas.GetPixel(x, y);

                float alpha = c.A / 255f;
                float oneminusalpha = 1 - alpha;

                var newR = ((c.R * alpha) + (oneminusalpha * C2.R));
                var newG = ((c.G * alpha) + (oneminusalpha * C2.G));
                var newB = ((c.B * alpha) + (oneminusalpha * C2.B));

                _canvas.SetPixel(x, y, new Color((byte) newR, (byte) newG, (byte) newB));
            }
            else
            {
                _canvas.SetPixel(x, y, c);
            }
        }

        #endregion

        #region Containers

        public void SetContainer()
        {
            /* TODO */
        }

        #endregion

        #region Misc

        public void Clear(Color c)
        {
            _canvas.Clear((uint) c.ToHex());
        }

        public void Flush()
        {
            _canvas.WriteToScreen();
        }

        #endregion

        #region Draw

        public void DrawImage()
        {
            /* TODO */
        }

        #endregion

        #region Strings

        public void DrawString()
        {
            /* TODO */
        }

        public void MeasureString()
        {
            /* TODO */
        }

        #endregion

        #region DrawShapes

        public void DrawEllipse(int xc, int yc, int width, int height, Color c)
        {
            int a2 = width * width;
            int b2 = height * height;
            int fa2 = 4 * a2, fb2 = 4 * b2;
            int x, y, sigma;

            /* first half */
            for (x = 0, y = height, sigma = 2 * b2 + a2 * (1 - 2 * height); b2 * x <= a2 * y; x++)
            {
                SetPixel(xc + x, yc + y, c);
                SetPixel(xc - x, yc + y, c);
                SetPixel(xc + x, yc - y, c);
                SetPixel(xc - x, yc - y, c);
                if (sigma >= 0)
                {
                    sigma += fa2 * (1 - y);
                    y--;
                }
                sigma += b2 * ((4 * x) + 6);
            }

            /* second half */
            for (x = width, y = 0, sigma = 2 * a2 + b2 * (1 - 2 * width); a2 * y <= b2 * x; y++)
            {
                SetPixel(xc + x, yc + y, c);
                SetPixel(xc - x, yc + y, c);
                SetPixel(xc + x, yc - y, c);
                SetPixel(xc - x, yc - y, c);
                if (sigma >= 0)
                {
                    sigma += fb2 * (1 - x);
                    x--;
                }
                sigma += a2 * ((4 * y) + 6);
            }
        }

        public void DrawLine(int x, int y, int x2, int y2, Color c)
        {
            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1;
            else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1;
            else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1;
            else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1;
                else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                SetPixel(x, y, c);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public void DrawPath(Point[] points, Color c)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                var a = points[i];
                var b = points[i + 1];

                DrawLine(a.X, a.Y, b.X, b.Y, c);
            }
        }

        public void DrawPolygon(Point[] points, Color c)
        {
            for (int i = 0; i < points.Length - 1; i++)
            {
                var a = points[i];
                var b = points[i + 1];

                DrawLine(a.X, a.Y, b.X, b.Y, c);
            }

            var f = points[0];
            var t = points[points.Length - 1];

            DrawLine(f.X, f.Y, t.X, t.Y, c);
        }

        public void DrawRectangle(int x, int y, int w, int h, Color c)
        {
            DrawLine(x, y, x + w, y, c);
            DrawLine(x, y + h, x + w, y + h, c);

            DrawLine(x, y, x, y + h, c);
            DrawLine(x + w, y, x + w, y + h, c);
        }

        #endregion

        #region Fill

        public void FillEllipse(int xc, int yc, int width, int height, Color c)
        {
            int a2 = width * width;
            int b2 = height * height;
            int fa2 = 4 * a2, fb2 = 4 * b2;
            int x, y, sigma;

            /* first half */
            for (x = 0, y = height, sigma = 2 * b2 + a2 * (1 - 2 * height); b2 * x <= a2 * y; x++)
            {
                DrawLine(xc + x, yc + y, xc - x, yc + y, c);
                DrawLine(xc + x, yc - y, xc - x, yc - y, c);

                if (sigma >= 0)
                {
                    sigma += fa2 * (1 - y);
                    y--;
                }
                sigma += b2 * ((4 * x) + 6);
            }

            /* second half */
            for (x = width, y = 0, sigma = 2 * a2 + b2 * (1 - 2 * width); a2 * y <= b2 * x; y++)
            {
                DrawLine(xc + x, yc + y, xc - x, yc + y, c);
                DrawLine(xc + x, yc - y, xc - x, yc - y, c);
                if (sigma >= 0)
                {
                    sigma += fb2 * (1 - x);
                    x--;
                }
                sigma += a2 * ((4 * y) + 6);
            }
        }

        public void FillPath()
        {
            /* TODO */
        }

        public void FillPolygon(Point[] points, Color c)
        {
            var polly = new TPPLPoly(points.Length);

            for (int i = 0; i < points.Length; i++)
            {
                polly[i].X = points[i].X;
                polly[i].Y = points[i].Y;
            }

            var outlist = new List<TPPLPoly>();

            var p = new TPPLPartition();
            p.Triangulate_EC(polly, outlist);

            foreach (var tpplPoly in outlist)
            {
                for (int i = 0; i < tpplPoly.Points.Count - 1; i++)
                {
                    var a = tpplPoly.Points[i];
                    var b = tpplPoly.Points[i + 1];

                    DrawLine((int) a.X, (int) a.Y, (int) b.X, (int) b.Y, c);
                }
            }
        }

        public void FillRectangle(int x, int y, int w, int h, Color c)
        {
            for (int width = x; width < w; width++)
            {
                for (int height = y; height < h; height++)
                {
                    SetPixel(width, height, c);
                }
            }
        }

        #endregion

        #region Clipping

        public void ResetClip()
        {
            /* TODO */
        }

        public void ExcludeClip()
        {
            /* TODO */
        }

        public void SetClip()
        {
            /* TODO */
        }

        #endregion

        #region Translation

        public void ResetTransform()
        {
            /* TODO */
        }

        public void ScaleTransform()
        {
            /* TODO */
        }

        public void TransformPoints()
        {
            /* TODO */
        }

        public void TranslateClip()
        {
            /* TODO */
        }

        #endregion
    }
}