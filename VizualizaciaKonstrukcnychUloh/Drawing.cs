using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

namespace Visualization
{  
    /// <summary>
    /// Class for drawing of all objects.
    /// </summary>
    class Drawing
    {
        /// <summary>
        /// Graphics for drawing.
        /// </summary>
        public static Graphics graphics;

        /// <summary>
        /// Size of drawn point.
        /// </summary>
        const int sizeOfPoint = 1;
        /// <summary>
        /// Size of font for names of objects.
        /// </summary>
        const int sizeOfFont = 8;

        /// <summary>
        /// Value of the zoom in the pictureBox.
        /// </summary>
        static public double zoomForModifying = 1;
        /// <summary>
        /// X coordinate of origin of zoom in the pictureBox.
        /// </summary>
        static public double xForModifying = 0;
        /// <summary>
        /// Y coordinate of origin of zoom in the pictureBox.
        /// </summary>
        static public double yForModyfying = 0;

        /// <summary>
        /// Font for names of objects.
        /// </summary>
        static Font f = new Font(FontFamily.GenericSansSerif, sizeOfFont);

        /// <summary>
        /// Visualizer where it is drawn.
        /// </summary>
        static public Visualizer visualizer;

        /// <summary>
        /// Procedure for drawing any object.
        /// </summary>
        /// <param name="o">Object which should be drawn.</param>
        public static void DrawObject(GeometricObject o)
        {
            if (o is Point)
            {
                DrawPoint((Point)o);
            }
            else if (o is Line)
            {
                DrawLine((Line)o);
            }
            else if (o is LineSegment)
            {
                DrawLineSegment((LineSegment)o);
            }
            else if (o is Circle)
            {
                DrawCircle((Circle)o);
            }
            else if (o is Angle)
            {
                DrawAngle((Angle)o);
            }
        }

        /// <summary>
        /// Procedure for clearing the canvas.
        /// </summary>
        /// <param name="width">Width of canvas.</param>
        /// <param name="height">Height of canvas.</param>
        public static void ClearCanvas(int width, int height)
        {
            graphics = visualizer.GetPictureBox().CreateGraphics();
            graphics.FillRectangle(Brushes.Snow, 0, 0, width, height);
        }

        /// <summary>
        /// Procedure for drawing just particular point with particular color.
        /// </summary>
        /// <param name="name">Name of drawn point.</param>
        /// <param name="brush">Color of name of point.</param>
        /// <param name="pen">Color of point, colors are same.</param>
        /// <param name="p">Point.</param>
        public static void DrawP(string name, Brush brush, Pen pen, Point p)
        {
            graphics = visualizer.GetPictureBox().CreateGraphics();

            double tempx = p.x;
            double tempy = visualizer.GetPictureBox().Height - p.y;
            tempx = xForModifying + tempx * zoomForModifying;
            tempy = (visualizer.GetPictureBox().Height - yForModyfying) + (tempy - visualizer.GetPictureBox().Height) * zoomForModifying;

            // It is name of circle.
            if (p.firstName[0] == '*')
            {
                if (name.Length > 1)
                {
                    name = name.Substring(1);
                    graphics.DrawString(name, f, brush, (int)Math.Round(tempx), (int)Math.Round(tempy));
                }
            }
            // It is point.
            else
            {
                graphics.DrawRectangle(pen, (int)Math.Round(tempx), (int)Math.Round(tempy), sizeOfPoint, sizeOfPoint);
                graphics.DrawString(name, f, brush, (int)Math.Round(tempx), (int)Math.Round(tempy));
            }
        }

        /// <summary>
        /// Procedure for drawing point.
        /// </summary>
        /// <param name="p">Point which sholud be drawn.</param>
        public static void DrawPoint(Point p)
        {
            if (p.firstName == p.secondName)
            {
                var points = Reader.FoundObject(p.firstName);
                foreach (Point pi in points)
                {
                    // Temporary point from macro.
                    if (pi.firstName.Contains('#') && pi.firstName.Contains('_'))
                    {
                        if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                        {
                            DrawP(pi.firstName, Brushes.Black, Pens.Black, pi);
                        }
                        else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                        {
                            DrawP(pi.firstName, Brushes.Silver, Pens.Silver, pi);
                        }
                    }
                    // Just point.
                    else if (!pi.firstName.Contains('#'))
                    {
                       if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                        {
                            DrawP(pi.secondName, Brushes.Black, Pens.Black, pi);
                        }
                        else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey && (pi.x != p.x || pi.y != p.y))
                        {
                            DrawP(pi.secondName, Brushes.Silver, Pens.Silver, pi);
                        }
                    }
                }                

                // Temporary point from macro.
                if (p.firstName.Contains('#') && p.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawP(p.firstName, Brushes.Black, Pens.Black, p);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawP(p.firstName, Brushes.Silver, Pens.Silver, p);
                    }
                }
                // Just point.
                else if (!p.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey || Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.NotDraw)
                    {
                        DrawP(p.secondName, Brushes.Black, Pens.Black, p);
                    }
                }
            }
            else if (p.secondName != "")
            {
                // Temporary point from macro.
                if (p.firstName.Contains('#') && p.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawP(p.secondName, Brushes.Black, Pens.Black, p);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawP(p.secondName, Brushes.Silver, Pens.Silver, p);
                    }
                }
                // Just point.
                else if (!p.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                    {
                        DrawP(p.secondName, Brushes.Black, Pens.Black, p);
                    }
                    else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey && (((Point)Reader.FoundObject(p.firstName)[0]) != p))
                    {
                        DrawP(p.secondName, Brushes.Silver, Pens.Silver, p);
                    }
                }
            }
            else
            { 
                // Temporary point from macro.
                if (p.firstName.Contains('#') && p.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawP(p.firstName, Brushes.Silver, Pens.Silver, p);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawP(p.firstName, Brushes.Silver, Pens.Silver, p);
                    }
                }
                // Just point.
                else if (!p.firstName.Contains('#'))
                {
                    DrawP(p.firstName, Brushes.Black, Pens.Black, p);
                }
            }
        }

        /// <summary>
        /// Procedure for drawing just particular line with particular color.
        /// </summary>
        /// <param name="name">Name of drawn line.</param>
        /// <param name="brush">Color of name of line.</param>
        /// <param name="pen">Color of line, colors are same.</param>
        /// <param name="l">Line.</param>
        public static void DrawL(string name, Brush brush, Pen pen, Line l)
        {
            graphics = visualizer.GetPictureBox().CreateGraphics();
            
            // Last points of line in picture.
            List<Point> lastPoint = new List<Point>();

            // Intersections with border of picture, for drawing line through picture.          

            double tempp1x = l.point1.x;
            tempp1x = xForModifying + tempp1x * zoomForModifying;
            double tempp1y = visualizer.GetPictureBox().Height - l.point1.y;
            tempp1y = (visualizer.GetPictureBox().Height - yForModyfying) + (tempp1y - visualizer.GetPictureBox().Height) * zoomForModifying;
            double tempp2x = l.point2.x;
            tempp2x = xForModifying + tempp2x * zoomForModifying;
            double tempp2y = visualizer.GetPictureBox().Height - l.point2.y;
            tempp2y = (visualizer.GetPictureBox().Height - yForModyfying) + (tempp2y - visualizer.GetPictureBox().Height) * zoomForModifying;
            
            Point tempp1 = new Point("#P" + Reader.counter++, tempp1x, tempp1y);
            Point tempp2 = new Point("#P" + Reader.counter++, tempp2x, tempp2y);
            Line temp = new Line(tempp1, tempp2);
            Point tempUp = temp.Intersection(Line.up);
            Point tempDown = temp.Intersection(Line.down);
            Point tempLeft = temp.Intersection(Line.left);
            Point tempRight = temp.Intersection(Line.right);

            //Determines as first and last point of line if it is able to see them in picture.
            if (tempLeft != null)
            {
                if (tempLeft.CanSee())
                {
                    lastPoint.Add(tempLeft);
                }
                if (tempRight.CanSee())
                {
                    lastPoint.Add(tempRight);
                }
            }
            if (lastPoint.Count != 2 && tempUp != null)
            {
                if (tempUp.CanSee())
                {
                    lastPoint.Add(tempUp);
                }
                if (tempDown.CanSee())
                {
                    lastPoint.Add(tempDown);
                }
            }

            if (lastPoint.Count == 2)
            {
                // Drawing of particular line.
                graphics.DrawLine(pen, (int)Math.Round(lastPoint[0].x), (int)Math.Round(lastPoint[0].y), (int)Math.Round(lastPoint[1].x), (int)Math.Round(lastPoint[1].y));
                if (l.firstName.First() != '(' && (lastPoint[0].OnObject(Line.left) || lastPoint[0].OnObject(Line.up)))
                    graphics.DrawString(name, f, brush, (int)Math.Round(lastPoint[0].x), (int)Math.Round(lastPoint[0].y));
                else if (l.firstName.First() != '(' && lastPoint[0].OnObject(Line.down))
                    graphics.DrawString(name, f, brush, (int)Math.Round(lastPoint[0].x), (int)Math.Round(lastPoint[0].y) - 20);
                else if (l.firstName.First() != '(' && lastPoint[0].OnObject(Line.right))
                    graphics.DrawString(name, f, brush, (int)Math.Round(lastPoint[0].x) - 20, (int)Math.Round(lastPoint[0].y));
            }
        }

        /// <summary>
        /// Procedure for drawing line.
        /// </summary>
        /// <param name="l">Line which should be drawn.</param>
        public static void DrawLine(Line l)
        {
            if (l.firstName == l.secondName)
            {
                var lines = Reader.FoundObject(l.firstName);

                foreach (Line li in lines)
                {              

                    // Temporary line from macro.
                    if (li.firstName.Contains('#') && li.firstName.Contains('_'))
                    {
                        if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                        {
                            DrawL(li.firstName, Brushes.Black, Pens.Black, li);
                        }
                        else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                        {
                            DrawL(li.firstName, Brushes.Silver, Pens.Silver, li);
                        }
                    }
                    // Just line.
                    else if (!li.firstName.Contains('#'))
                    {
                        if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                        {
                            DrawL(li.firstName, Brushes.Black, Pens.Black, li);
                        }
                        else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey && (li.a != l.a || li.b != l.b || li.c != l.c))
                        {
                            DrawL(li.firstName, Brushes.Silver, Pens.Silver, li);
                        }
                    }
                }                

                // Temporary line from macro.
                if (l.firstName.Contains('#') && l.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawL(l.firstName, Brushes.Black, Pens.Black, l);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawL(l.firstName, Brushes.Silver, Pens.Silver, l);
                    }
                }
                // Just line.
                else if (!l.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawL(l.secondName, Brushes.Black, Pens.Black, l);
                    }
                    else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.NotDraw)
                    {
                        DrawL(l.secondName, Brushes.Black, Pens.Black, l);
                    }
                }
            }
            else if (l.secondName != "")
            {          

                // Temporary line from macro.
                if (l.firstName.Contains('#') && l.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawL(l.secondName, Brushes.Black, Pens.Black, l);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawL(l.secondName, Brushes.Silver, Pens.Silver, l);
                    }
                }
                // Just line.
                else if (!l.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                    {
                        DrawL(l.secondName, Brushes.Black, Pens.Black, l);
                    }
                    else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawL(l.secondName, Brushes.Silver, Pens.Silver, l);
                    }                  
                }
            }
            else
            {  
                // Temporary line from macro.
                if (l.firstName.Contains('#') && l.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawL(l.firstName, Brushes.Silver, Pens.Silver, l);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawL(l.firstName, Brushes.Silver, Pens.Silver, l);
                    }
                }
                // Just line.
                else if (!l.firstName.Contains('#'))
                {
                    DrawL(l.firstName, Brushes.Black, Pens.Black, l);
                }
            }
        }

        /// <summary>
        /// Procedure for drawing just particular line segment with particular color.
        /// </summary>
        /// <param name="name">Name of drawn line segment.</param>
        /// <param name="brush">Color of name of line segment.</param>
        /// <param name="pen">Color of line segment, colors are same.</param>
        /// <param name="l">Line segment.</param>
        public static void DrawLS(string name, Brush brush, Pen pen, LineSegment ls)
        {
            graphics = visualizer.GetPictureBox().CreateGraphics();

            var lastPoint = new List<Point>();

            double tempp1x = ls.point1.x;
            tempp1x = xForModifying + tempp1x * zoomForModifying;
            double tempp1y = visualizer.GetPictureBox().Height - ls.point1.y;
            tempp1y = (visualizer.GetPictureBox().Height - yForModyfying) + (tempp1y - visualizer.GetPictureBox().Height) * zoomForModifying;
            double tempp2x = ls.point2.x;
            tempp2x = xForModifying + tempp2x * zoomForModifying;
            double tempp2y = visualizer.GetPictureBox().Height - ls.point2.y;
            tempp2y = (visualizer.GetPictureBox().Height - yForModyfying) + (tempp2y - visualizer.GetPictureBox().Height) * zoomForModifying;


            if (ls.half)
            {
                // Intersections with border of picture, for drawing line through picture.           
                Point tempp1 = new Point("#P" + Reader.counter++, tempp1x, tempp1y);
                Point tempp2 = new Point("#P" + Reader.counter++, tempp2x, tempp2y);
                Line temp = new Line(tempp1, tempp2);
                Point tempUp = temp.Intersection(Line.up);
                Point tempDown = temp.Intersection(Line.down);
                Point tempLeft = temp.Intersection(Line.left);
                Point tempRight = temp.Intersection(Line.right);

                //Determines as first and last point of line if it is able to see them in picture.
                if (tempLeft != null)
                {
                    if (tempLeft.CanSee())
                    {
                        lastPoint.Add(tempLeft);
                    }
                    if (tempRight.CanSee())
                    {
                        lastPoint.Add(tempRight);
                    }
                }
                if (lastPoint.Count != 2 && tempUp != null)
                {
                    if (tempUp.CanSee())
                    {
                        lastPoint.Add(tempUp);
                    }
                    if (tempDown.CanSee())
                    {
                        lastPoint.Add(tempDown);
                    }
                }

                if (lastPoint.Count == 2)
                {
                    Point old0 = new Point("old0", lastPoint[0].x, lastPoint[0].y);
                    Point old1 = new Point("old1", lastPoint[1].x, lastPoint[1].y);
                    if (ls.point1.x < ls.point2.x)
                    {
                        if (lastPoint[0].x < tempp1x)
                            lastPoint[0].x = tempp1x;
                        else
                            lastPoint[1].x = tempp1x;
                    }
                    else
                    {
                        if (lastPoint[0].x > tempp1x)
                            lastPoint[0].x = tempp1x;
                        else
                            lastPoint[1].x = tempp1x;
                    }

                    if (ls.point1.y > ls.point2.y)
                    {
                        if (lastPoint[0].y < tempp1y)
                            lastPoint[0].y = tempp1y;
                        else
                            lastPoint[1].y = tempp1y;
                    }
                    else
                    {
                        if (lastPoint[0].y > tempp1y)
                            lastPoint[0].y = tempp1y;
                        else
                            lastPoint[1].y = tempp1y;
                    }

                    LineSegment templs = new LineSegment(temp.point1, temp.point2, true);
                    if (!lastPoint[0].CanSee())
                    {
                        if (old0.OnObject(templs))
                            lastPoint[0] = old0;
                        else
                            lastPoint.RemoveAt(0);
                    }
                    if (lastPoint.Count == 2 && !lastPoint[1].CanSee())
                    {
                        if (old1.OnObject(templs))
                            lastPoint[1] = old1;
                        else
                            lastPoint.RemoveAt(0);
                    }
                }
                if (lastPoint.Count == 2)
                {
                    graphics.DrawLine(pen, (int)Math.Round(lastPoint[0].x), (int)Math.Round(lastPoint[0].y), (int)Math.Round(lastPoint[1].x), (int)Math.Round(lastPoint[1].y));
                }
            }            
            else
            {
                graphics.DrawLine(pen, (int)Math.Round(tempp1x), (int)Math.Round(tempp1y), (int)Math.Round(tempp2x), (int)Math.Round(tempp2y));
                if (ls.firstName.First() != '|' && ls.firstName.First() != '(')
                    graphics.DrawString(name, f, brush, (int)Math.Round(tempp1x), (int)Math.Round(tempp1y));
            }
        }

        /// <summary>
        /// Procedure for drawing line segment.
        /// </summary>
        /// <param name="ls">Line segment which should be drawn.</param>
        public static void DrawLineSegment(LineSegment ls)
        {
            if (ls.firstName == ls.secondName)
            {
                var linesegments = Reader.FoundObject(ls.firstName);
                foreach (LineSegment lls in linesegments)
                {                  
                    // Temporary line segment from macro.
                    if (lls.firstName.Contains('#') && lls.firstName.Contains('_'))
                    {
                        if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                        {
                            DrawLS(lls.secondName, Brushes.Black, Pens.Black, lls);
                        }
                        else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                        {
                            DrawLS(lls.secondName, Brushes.Silver, Pens.Silver, lls);
                        }
                    }                                        
                    // Just line segment.
                    else if (!lls.firstName.Contains('#'))
                    {
                        if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                        {
                            DrawLS(lls.secondName, Brushes.Black, Pens.Black, lls);
                        }
                        else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey && (lls.a != ls.a || lls.b != ls.b || lls.c != ls.c))
                        {
                            DrawLS(lls.secondName, Brushes.Silver, Pens.Silver, lls);
                        }                          
                    }
                }                
                // Temporary line segment from macro.
                if (ls.firstName.Contains('#') && ls.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawLS(ls.firstName, Brushes.Black, Pens.Black, ls);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawLS(ls.firstName, Brushes.Silver, Pens.Silver, ls);
                    }
                }
                // Just line segment.
                else if (!ls.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey || Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.NotDraw)
                    {
                        DrawLS(ls.firstName, Brushes.Black, Pens.Black, ls);
                    }
                }
            }
            else if (ls.secondName != "")
            {                
                // Temporary line segment from macro.
                if (ls.firstName.Contains('#') && ls.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawLS(ls.secondName, Brushes.Black, Pens.Black, ls);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawLS(ls.secondName, Brushes.Silver, Pens.Silver, ls);
                    }
                }
                // Just line segment.
                else if (!ls.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                    {
                        DrawLS(ls.secondName, Brushes.Black, Pens.Black, ls);
                    }
                    else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawLS(ls.secondName, Brushes.Silver, Pens.Silver, ls);
                    }
                }
            }
            else
            {                
                // Temporary line segment from macro.
                if (ls.firstName.Contains('#') && ls.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawLS(ls.firstName, Brushes.Black, Pens.Black, ls);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawLS(ls.firstName, Brushes.Silver, Pens.Silver, ls);
                    }
                }
                // Just line segment.
                else if (!ls.firstName.Contains('#'))
                {
                    DrawLS(ls.firstName, Brushes.Black, Pens.Black, ls);
                }
            }
        }
         
        /// <summary>
        /// Procedure for drawing just particular circle with particular color.
        /// </summary>
        /// <param name="name">Name of drawn circle.</param>
        /// <param name="p">Color of circle.</param>
        /// <param name="c">Circle.</param>
        public static void DrawC(string name, Pen p, Circle c)
        {
            graphics = visualizer.GetPictureBox().CreateGraphics();       

            double temppx = c.center.x;
            temppx = xForModifying + temppx * zoomForModifying;
            temppx = temppx - c.radius * zoomForModifying;                       
            double temppy = visualizer.GetPictureBox().Height - c.center.y;
            temppy = (visualizer.GetPictureBox().Height - yForModyfying) + (temppy - visualizer.GetPictureBox().Height) * zoomForModifying;
            temppy = temppy - c.radius * zoomForModifying;
            double templ = 2 * c.radius * zoomForModifying;

            if (c.end1 == null)
            {
                graphics = visualizer.GetPictureBox().CreateGraphics();
                Rectangle rectangle = new Rectangle((int)Math.Round(temppx), (int)Math.Round(temppy), (int)Math.Round(templ), (int)Math.Round(templ));
                graphics.DrawEllipse(p, rectangle);
                DrawObject(Reader.FoundObject("*" + name)[0]);
            }
            else
            {
                graphics = visualizer.GetPictureBox().CreateGraphics();
                Rectangle rectangle = new Rectangle((int)Math.Round(temppx), (int)Math.Round(temppy), (int)Math.Round(templ), (int)Math.Round(templ));

                float size1 = (float)(Math.Atan2(c.end1.y - c.center.y, c.end1.x - c.center.x) * (180 / Math.PI));
                float size2 = (float)(Math.Atan2(c.end2.y - c.center.y, c.end2.x - c.center.x) * (180 / Math.PI)); 
                if (size1 < 0)
                    size1 = 360 + size1;
                if (size2 < 0)
                    size2 = 360 + size2;
                float size = (size2 - size1) % 360;
                if (size < 0)
                    size = 360 + size;

                graphics.DrawArc(p, rectangle, 360 - size1, - size);
                DrawObject(Reader.FoundObject("*" + name)[0]);
            }
        }

        /// <summary>
        /// Procedure for drawing circle.
        /// </summary>
        /// <param name="c">Circle which should be drawn.</param>
        public static void DrawCircle(Circle c)
        {
            if (c.firstName == c.secondName)
            {
                var circles = Reader.FoundObject(c.firstName);
                
                foreach (Circle ci in circles)
                {
                    // Temporary circle from macro.
                    if (ci.firstName.Contains('#') && ci.firstName.Contains('_'))
                    {
                        if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                        {
                            DrawC(ci.firstName, Pens.Black, ci);
                        }
                        else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                        {
                            DrawC(ci.firstName, Pens.Silver, ci);
                        }
                    }
                    // Just circle.
                    else if (!ci.firstName.Contains('#'))
                    {
                        if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                        {
                            DrawC(ci.secondName, Pens.Black, ci);
                        }
                        else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey && (ci.center.x != c.center.x || ci.center.y != c.center.y || ci.radius != c.radius))
                        {
                            DrawC(ci.secondName, Pens.Silver, ci);
                        }
                    }
                                        
                }

                // Temporary circle from macro.
                if (c.firstName.Contains('#') && c.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawC(c.firstName, Pens.Black, c);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawC(c.firstName, Pens.Silver, c);
                    }
                }
                // Just circle.
                else if (!c.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey || Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.NotDraw)
                    {
                        DrawC(c.secondName, Pens.Black, c);
                    }
                }
            }
            else if (c.secondName != "")
            {
                // Temporary circle from macro.
                if (c.firstName.Contains('#') && c.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawC(c.secondName, Pens.Black, c);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawC(c.secondName, Pens.Silver, c);
                    }
                }
                // Just circle.
                else if (!c.firstName.Contains('#'))
                {
                    if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.Draw)
                    {
                        DrawC(c.secondName, Pens.Black, c); 
                    }
                    else if (Visualizer.secondObjectsInConstruction == Visualizer.DrawingObjects.DrawGrey && (((Circle)Reader.FoundObject(c.firstName)[0]).center.x  != c.center.x || ((Circle)Reader.FoundObject(c.firstName)[0]).center.y != c.center.y || ((Circle)Reader.FoundObject(c.firstName)[0]).radius != c.radius))
                    {
                        DrawC(c.secondName, Pens.Silver, c);
                    }
                    else if (Reader.FoundObject(c.firstName).Count == 1)
                    {
                        DrawC(c.firstName, Pens.Black, c);
                    }
                }
                                
            }
            else
            {
                // Temporary circle from macro.
                if (c.firstName.Contains('#') && c.firstName.Contains('_'))
                {
                    if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.Draw)
                    {
                        DrawC(c.firstName, Pens.Black, c);
                    }
                    else if (Visualizer.tempObjectsInMacro == Visualizer.DrawingObjects.DrawGrey)
                    {
                        DrawC(c.firstName, Pens.Silver, c);
                    }
                }
                // Just circle.
                else if (!c.firstName.Contains('#'))
                {
                    DrawC(c.firstName, Pens.Black, c);
                }
            }
        }

        /// <summary>
        /// Procedure for drawing angle.
        /// </summary>
        /// <param name="a">The angle which should be drawn.</param>
        public static void DrawAngle(Angle a)
        {
            DrawPoint(a.point1);
            DrawPoint(a.point2);
            DrawPoint(a.vertex);
            DrawLineSegment(a.line1);
            DrawLineSegment(a.line2);
        }
    }
}
