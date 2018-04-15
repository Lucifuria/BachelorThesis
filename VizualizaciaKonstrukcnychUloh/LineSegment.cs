using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for representation of line segment.
    /// </summary>
    class LineSegment :GeometricObject
    {
        /// <summary>
        /// The first name of line.
        /// </summary>
        public string firstName="";
        /// <summary>
        /// The second name of line.
        /// </summary>
        public string secondName="";

        /// <summary>
        /// First point which determines a line.
        /// </summary>
        public Point point1;
        /// <summary>
        /// Second point which determines a line.
        /// </summary>
        public Point point2;
        /// <summary>
        /// Information about it is half line segment or not.
        /// </summary>
        public bool half = false;

        /// <summary>
        /// Constant a for general equation.
        /// </summary>
        public double a;
        /// <summary>
        /// Constant b for general equation.
        /// </summary>
        public double b;
        /// <summary>
        /// Constant c for general equation.
        /// </summary>
        public double c;

        /// <summary>
        /// Length of line segment (distance between point1 and point2).
        /// </summary>
        public double length = 0;

        /// <summary>
        /// Creates a new segment line from point A to point B and name it as |A,B| or half line from point A through point B and name it as |A,B).
        /// </summary>
        /// <param name="A">First point determinating a line.</param>
        /// <param name="B">Second point determinating a line.</param>
        /// <param name="half">Information if it is a half line or not - true is half line, false is line segment.</param>
        public LineSegment(Point A, Point B, bool half)
        {
            LineSegment templ;
            // If all input points are not unique, works with all copies of them.           
            if (A.firstName == A.secondName && B.firstName == B.secondName)
            {
                firstName = '|' + A.firstName + ',' + B.firstName + '|';
                secondName = firstName;

                var tempA = Reader.FoundObject(A.firstName);
                var tempB = Reader.FoundObject(B.firstName);
                foreach (GeometricObject oA in tempA)
                {
                    foreach (GeometricObject oB in tempB)
                    {
                        if (!(oA.GetFirstName() == oA.GetSecondName() || oB.GetFirstName() == oB.GetSecondName()))
                        {
                            templ = new LineSegment((Point)oA, (Point)oB, half);
                            templ.secondName = templ.firstName;
                            templ.firstName = '|' + oA.GetFirstName() + ',' + oB.GetFirstName() + templ.secondName[templ.secondName.Length - 1];
                            Reader.allObjects.Add(templ);
                        }
                    }
                }
            }
            // If one of all input points is not unique, works with all copies of it.   
            else if (A.firstName == A.secondName)
            {
                var tempA = Reader.FoundObject(A.firstName);
                firstName = '|' + A.firstName + ',' + B.firstName + '|';
                secondName = firstName;
                foreach (GeometricObject o in tempA)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        templ = new LineSegment((Point)o, B, half);
                        templ.secondName = templ.firstName;
                        templ.firstName = '|' + o.GetFirstName() + ',' + B.firstName + templ.secondName[templ.secondName.Length - 1];
                        Reader.allObjects.Add(templ);
                    }
                }
            }
            // If one of all input points is not unique, works with all copies of it. 
            else if (B.firstName == B.secondName)
            {
                var tempB = Reader.FoundObject(B.firstName);
                firstName = '|' + A.firstName + ',' + B.firstName + '|';
                secondName = firstName;
                foreach (GeometricObject o in tempB)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        templ = new LineSegment(A, (Point)o, half);
                        templ.secondName = templ.firstName;
                        templ.firstName = '|' + A.firstName + ',' + o.GetFirstName() + templ.secondName[templ.secondName.Length-1];
                        Reader.allObjects.Add(templ);
                    }
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (A.secondName != "" && B.secondName != "")
                {
                    firstName = '|' + A.secondName + ',' + B.secondName + '|';
                }
                else if (A.secondName != "")
                {
                    firstName = '|' + A.secondName + ',' + B.firstName + '|';
                }
                else if (B.secondName != "")
                {
                    firstName = '|' + A.firstName + ',' + B.secondName + '|';
                }   
                else
                {
                    firstName = '|' + A.firstName + ',' + B.firstName + '|';
                }            
            }

            // Sets all variables.
            if (half)
            {
                firstName = this.firstName.Substring(0,this.firstName.Length-1)+')';
                if (secondName != "")
                    secondName = this.secondName.Substring(0,this.secondName.Length - 1) + ')';
            }
            this.half = half;
            point1 = A;
            point2 = B;

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;

            length = Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y));
        }

        /// <summary>
        /// Creates a new segment line from point A to point B and name it.
        /// </summary>
        /// <param name="name">Name of the new line segment.</param>
        /// <param name="A">First point determinating a line.</param>
        /// <param name="B">Second point determinating a line.</param>
        /// <param name="half">Information if it is a half line or not - true is half line, false is line segment.</param>
        public LineSegment(string name, Point A, Point B, bool half)
        {
            this.firstName = name;
            this.half = half;
            point1 = A;
            point2 = B;

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;

            length = Math.Sqrt((A.x - B.x) * (A.x - B.x) + (A.y - B.y) * (A.y - B.y));
        }

        /// <summary>
        /// Creates a new line segment with particular distance.
        /// </summary>
        /// <param name="u">Name of new line segment.</param>
        /// <param name="n">Distance of new line segment.</param>
        public LineSegment(string u, double n)
        {
            firstName = u;
            point1 = new Point("#P" + Reader.counter++);
            point2 = new Point("#P" + Reader.counter++, point1, n);

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;

            length = n;
        }

        /// <summary>
        /// Creates a new line segment in the proper distance from another point.
        /// </summary>
        /// <param name="u">Name of new line segment.</param>
        /// <param name="A">Point from which creates new point in distance.</param>
        /// <param name="n">Distance between new and old point.</param>
        public LineSegment(string u, Point A, double n)
        {
            char[] separator = { '|', ',' };
            string[] points = u.Split(separator, StringSplitOptions.RemoveEmptyEntries);

            LineSegment templs;

            // If input point is not unique, works with all copies of it. 
            if (A.firstName == A.secondName)
            {
                point1 = A;
                firstName = u;
                secondName = u;

                if (points.Length ==2)
                {
                    var tempp = new Point(points[1], A, n);
                    Reader.allObjects.Add(tempp);
                    var temppoints = Reader.FoundObject(points[1]);
                    foreach (Point tp in temppoints)
                    {
                        if (tp.firstName != tp.secondName)
                        {
                            templs = new LineSegment(A, tp, false);
                            Reader.allObjects.Add(templs);

                            var templsn = Reader.FoundObject(u);
                            foreach (LineSegment tlsn in templsn)
                            {
                                if (tlsn.length != n)
                                    Reader.allObjects.Remove(tlsn);
                            }
                        }
                        else
                        {
                            point2 = tp;
                        }
                    }
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                point1 = A;
                if (points.Length == 1)
                    point2 = new Point("#P"+Reader.counter++, A, n);
                else if (points.Length == 2)
                    point2 = new Point(points[1], A, n);
                firstName = u;
            }           

            // Sets all variables.
            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;

            length = n;
        }    

        /// <summary>
        /// Returns intersection of two line segments.
        /// </summary>
        /// <param name="lineSegment">Line segment which intersects.</param>
        /// <returns>Intersection of two line segments, if there is not one, returns null.</returns>
        public Point Intersection(LineSegment lineSegment)
        {
            // Determinates if it is in same direction or same line.
            if (a == 0 && lineSegment.a == 0)
            {
                if (point1.x == lineSegment.point1.x && point1.y == lineSegment.point1.y)
                {
                    return new Point("#" + "P" + Convert.ToString(Reader.counter++), point1.x, point1.y);
                }
                if (point1.x == lineSegment.point2.x && point1.y == lineSegment.point2.y)
                {
                    return new Point("#" + "P" + Convert.ToString(Reader.counter++), point1.x, point1.y);
                }
                if (point2.x == lineSegment.point1.x && point2.y == lineSegment.point1.y)
                {
                    return new Point("#" + "P" + Convert.ToString(Reader.counter++), point2.x, point2.y);
                }
                if (point2.x == lineSegment.point2.x && point2.y == lineSegment.point2.y)
                {
                    return new Point("#" + "P" + Convert.ToString(Reader.counter++), point2.x, point2.y);
                }
                return null;
            }
            else if (a != 0 && lineSegment.a != 0)
            {
                if ((double)b / a == (double)lineSegment.b / lineSegment.a)
                {
                    if (point1.x == lineSegment.point1.x && point1.y == lineSegment.point1.y)
                    {
                        return new Point("#" + "P" + Convert.ToString(Reader.counter++), point1.x, point1.y);
                    }
                    if (point1.x == lineSegment.point2.x && point1.y == lineSegment.point2.y)
                    {
                        return new Point("#" + "P" + Convert.ToString(Reader.counter++), point1.x, point1.y);
                    }
                    if (point2.x == lineSegment.point1.x && point2.y == lineSegment.point1.y)
                    {
                        return new Point("#" + "P" + Convert.ToString(Reader.counter++), point2.x, point2.y);
                    }
                    if (point2.x == lineSegment.point2.x && point2.y == lineSegment.point2.y)
                    {
                        return new Point("#" + "P" + Convert.ToString(Reader.counter++), point2.x, point2.y);
                    }
                    return null;
                }
            }

            double x;
            double y;
            y = (double)(lineSegment.a * c - a * lineSegment.c) / (a * lineSegment.b - lineSegment.a * b);
            x = (double)(-b * y - c) / a;

            // It works diferently with half line segment and line segment because of the half part. It works with this line segment.
            if (this.half)
            {
                // If there is no intersection because of this half line segment, return null.
                if (this.point1.x < this.point2.x && x < this.point1.x)
                    return null;
                if (this.point1.x > this.point2.x && x > this.point1.x)
                    return null;

                if (this.point1.y < this.point2.y && y < this.point1.y)
                    return null;
                if (this.point1.y > this.point2.y && y > this.point1.y)
                    return null;
            }
            else
            {
                // If there is no intersection because of line segment, return null.
                if (!(x >= Math.Min(this.point1.x, this.point2.x) && x <= Math.Max(this.point1.x, this.point2.x) && y >= Math.Min(this.point1.y, this.point2.y) && y <= Math.Max(this.point1.y, this.point2.y)))
                    return null;
            }

            // It works diferently with half line segment and line segment because of the half part.
            if (lineSegment.half)
            {
                // If there is no intersection because of half line segment, return null.
                if (lineSegment.point1.x < lineSegment.point2.x && x < lineSegment.point1.x)
                    return null;
                if (lineSegment.point1.x > lineSegment.point2.x && x > lineSegment.point1.x)
                    return null;

                if (lineSegment.point1.y < lineSegment.point2.y && y < lineSegment.point1.y)
                    return null;
                if (lineSegment.point1.y > lineSegment.point2.y && y > lineSegment.point1.y)
                    return null;
            }
            else
            {
                // If there is no intersection because of line segment, return null.
                if (!(x >= Math.Min(lineSegment.point1.x, lineSegment.point2.x) && x <= Math.Max(lineSegment.point1.x, lineSegment.point2.x) && y >= Math.Min(lineSegment.point1.y, lineSegment.point2.y) && y <= Math.Max(lineSegment.point1.y, lineSegment.point2.y)))
                    return null;
            }
            
            return new Point("#" + "P" + Convert.ToString(Reader.counter++), x, y);
        }

        /// <summary>
        /// Returns intersection of line segment and cirlce.
        /// </summary>
        /// <param name="circle">Circle which intersects.</param>
        /// <returns>Intersection(s) of line segment and circle, if there is not one, returns null.</returns>
        public List<Point> Intersection(Circle circle)
        {
            Line tempLine = new Line(this.point1, this.point2);
            var tempIntersections = tempLine.Intersection(circle);
            if (tempIntersections == null)
                return null;
            var output = new List<Point>();
            foreach(var intersect in tempIntersections)
            {
                if (this.Belongs(intersect))
                    output.Add(intersect);
            }
            if (circle.end1 == null)
            {
                return output;
            }
            else
            {
                // It is part of circle.
                var tempoutput = new List<Point>();
                foreach (Point p in output)
                {
                    if (p.OnObject(circle))
                    {
                        tempoutput.Add(p);
                    }
                }
                return tempoutput;
            }
        }

        /// <summary>
        /// Decide if point p belongs line.
        /// </summary>
        /// <param name="p">Point on or not on line.</param>
        /// <returns>True if point p belongs line, otherwise false.</returns>
        public bool Belongs(Point p)
        {
            // If works diferently with half line segment and line segment because of the half part.
            if (this.half)
            {
                // If there is no intersection because of half line segment, return false.
                if (this.point1.x < this.point2.x && p.x < this.point1.x)
                    return false;
                if (this.point1.x > this.point2.x && p.x > this.point1.x)
                    return false;

                if (this.point1.y < this.point2.y && p.y < this.point1.y)
                    return false;
                if (this.point1.y > this.point2.y && p.y > this.point1.y)
                    return false;
                return true;
            }
            else
            {
                // If there is intersection with line segment, return true.
                if (this.a * p.x + this.b * p.y + c == 0)
                {
                    double minx = Math.Min(this.point1.x, this.point2.x);
                    double miny = Math.Min(this.point1.y, this.point2.y);
                    double maxx = Math.Max(this.point1.x, this.point2.x);
                    double maxy = Math.Max(this.point1.y, this.point2.y);
                    if (p.x >= minx && p.x <= maxx && p.y >= miny && p.y <= maxy)
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Creates a new line segment according input. If the line segment (or object) already exists, shows a message.
        /// </summary>
        /// <param name="input">Input line with command.</param>
        /// <param name="halfLS">If it should be a half line, it is true, else it is false.</param>
        public static void WorkWithLineSegment(string[] input, bool halfLS)
        {
            LineSegment temp;
            char[] separator = { ',', '|', '(', ')' };
            char[] sepeq = { '=' };
            switch (input.Length)
            {
                // Command for line segment or half line segment "usecka |A,B|", "polpriamka |A,B)", "usecka u=|A,B|", "polpriamka p=|A,B)".
                case 2:
                    string[] lineEqEnds = input[1].Split(sepeq, StringSplitOptions.RemoveEmptyEntries);
                    string[] lineEnds = null;
                    if (lineEqEnds.Length == 1)
                        lineEnds = lineEqEnds[0].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    else if (lineEqEnds.Length == 2)
                        lineEnds = lineEqEnds[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (lineEnds.Length != 2)
                    {
                        Reader.noError = false;
                        if (input[0] == "usecka")
                            MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                        else
                            MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                    }
                    else
                    {
                        if (Reader.FoundObject(lineEnds[0]) != null && Reader.FoundObject(lineEnds[1]) != null)
                        {
                            if (Reader.FoundObject(lineEnds[0])[0] is Point && Reader.FoundObject(lineEnds[1])[0] is Point)
                            {
                                if (halfLS)
                                {
                                    int bracketOrder = 1 + lineEnds[0].Length + 1 + lineEnds[1].Length;
                                    if ((lineEqEnds.Length == 1 && (input[1][0] != '|' || input[1][bracketOrder] != ')')) || (lineEqEnds.Length == 2 && (lineEqEnds[1][0] != '|' || lineEqEnds[1][bracketOrder] != ')')))
                                    {
                                        Reader.noError = false;
                                        if (input[0] == "usecka")
                                            MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                                        else
                                            MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                                    }
                                    else
                                    {
                                        var temp1 = Reader.FoundObject(lineEnds[0]);
                                        var temp2 = Reader.FoundObject(lineEnds[1]);
                                        Point p1 = (Point)temp1[0];
                                        Point p2 = (Point)temp2[0];
                                        foreach (Point t1 in temp1)
                                        {
                                            if (t1.firstName == t1.secondName)
                                                p1 = t1;
                                        }
                                        foreach (Point t2 in temp2)
                                        {
                                            if (t2.firstName == t2.secondName)
                                                p2 = t2;
                                        }
                                        temp = new LineSegment(p1, p2, halfLS);
                                        Reader.allObjects.Add(temp);
                                        Drawing.DrawLineSegment(temp);
                                        if (lineEqEnds.Length == 2)
                                        {
                                            temp = new LineSegment(lineEqEnds[0], p1, p2, halfLS);
                                            Reader.allObjects.Add(temp);
                                        }
                                    }
                                }
                                else
                                {
                                    int bracketOrder = 1 + lineEnds[0].Length + 1 + lineEnds[1].Length;
                                    if ((lineEqEnds.Length == 1 && (input[1][0] != '|' || input[1][bracketOrder] != '|')) || (lineEqEnds.Length == 2 && (lineEqEnds[1][0] != '|' || lineEqEnds[1][bracketOrder] != '|')))
                                    {
                                        Reader.noError = false;
                                        if (input[0] == "usecka")
                                            MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                                        else
                                            MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                                    }
                                    else
                                    {
                                        var temp1 = Reader.FoundObject(lineEnds[0]);
                                        var temp2 = Reader.FoundObject(lineEnds[1]);
                                        Point p1 = (Point)temp1[0];
                                        Point p2 = (Point)temp2[0];
                                        foreach (Point t1 in temp1)
                                        {
                                            if (t1.firstName == t1.secondName)
                                                p1 = t1;
                                        }
                                        foreach (Point t2 in temp2)
                                        {
                                            if (t2.firstName == t2.secondName)
                                                p2 = t2;
                                        }
                                        temp = new LineSegment(p1, p2, halfLS);
                                        Reader.allObjects.Add(temp);
                                        Drawing.DrawLineSegment(temp);
                                        if (lineEqEnds.Length == 2)
                                        {
                                            temp = new LineSegment(lineEqEnds[0], p1, p2, halfLS);
                                            Reader.allObjects.Add(temp);
                                        }
                                    }
                                }
                            }
                            else if (lineEqEnds.Length == 2 && Reader.FoundObject(lineEqEnds[0]) != null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + lineEqEnds[0] + " už existuje.");
                            }
                            else if (!(Reader.FoundObject(lineEnds[0])[0] is Point) && !(Reader.FoundObject(lineEnds[1])[0] is Point))
                            {
                                Reader.noError = false;
                                MessageBox.Show("Bod " + lineEnds[0] + " ani bod " + lineEnds[1] + " neexistuje.");
                            }
                            else if (!(Reader.FoundObject(lineEnds[0])[0] is Point))
                            {
                                Reader.noError = false;
                                MessageBox.Show("Bod " + lineEnds[0] + " neexistuje.");
                            }
                            else if (!(Reader.FoundObject(lineEnds[1])[0] is Point))
                            {
                                Reader.noError = false;
                                MessageBox.Show("Bod " + lineEnds[1] + " neexistuje.");
                            }
                        }
                        else if (Reader.FoundObject(lineEnds[0]) == null && Reader.FoundObject(lineEnds[1]) == null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Bod " + lineEnds[0] + " ani bod " + lineEnds[1] + " neexistuje.");
                        }
                        else if (Reader.FoundObject(lineEnds[1]) == null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Bod " + lineEnds[1] + " neexistuje.");
                        }
                        else if (Reader.FoundObject(lineEnds[0]) == null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Bod " + lineEnds[0] + " neexistuje.");
                        }
                    }
                    break;
                // Command for line segment "usecka u, u=n" or "usecka |A,X|, |A,X|=n".
                case 3:
                    char[] tempsep = { '=' };
                    string[] partOfInput = input[2].Split(tempsep, StringSplitOptions.RemoveEmptyEntries);
                    
                    // Shows a message about wrong input.
                    if (partOfInput.Length < 2)
                    {
                        Reader.noError = false;
                        if (input[0] == "usecka")
                            MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                        else
                            MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                    }
                    // Command for line segment "usecka u, u=n" (u=|A,B| / u=v / u=100) or " usecka |A,X|, |A,X|=n" or "usecka u=|A,X|, u=n" (|A,X|=|C,D| / |A,X|=u / |A,X|=100).
                    else if (partOfInput.Length == 2)
                    {
                        char[] sepOfName = { '|', ',' };
                        lineEqEnds = input[1].Split(tempsep, StringSplitOptions.RemoveEmptyEntries);

                        string[] newLineSegment = null;
                        if (lineEqEnds.Length == 1)
                            newLineSegment = lineEqEnds[0].Split(sepOfName, StringSplitOptions.RemoveEmptyEntries);
                        else if (lineEqEnds.Length == 2)
                            newLineSegment = lineEqEnds[1].Split(sepOfName, StringSplitOptions.RemoveEmptyEntries);

                        // If object with name exists, shows a message.
                        if (Reader.FoundObject(partOfInput[0]) != null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Objekt s menom " + partOfInput[0] + " už existuje.");
                        }
                        // Command u=n; u is line segment, n is length - number/line segment (u=|A,B| / u=v / u=100).
                        else if (newLineSegment.Length == 1)
                        {
                            int distancei;
                            // Reads a integer distance, draws a line segment.
                            if (Int32.TryParse(partOfInput[1], out distancei))
                            {
                                temp = new LineSegment(partOfInput[0], new Point("#P"+Reader.counter++), distancei);
                                Reader.allObjects.Add(temp);
                                Drawing.DrawLineSegment(temp);
                            }
                            // Reads a line segment distance, draws a line segment.
                            else if (Reader.FoundObject(partOfInput[1]) != null)
                            {
                                double distanced = ((LineSegment)(Reader.FoundObject(partOfInput[1])[0])).length;
                                temp = new LineSegment(partOfInput[0], new Point("#P" + Reader.counter++), distanced);
                                Reader.allObjects.Add(temp);
                                Drawing.DrawLineSegment(temp);
                            }
                            // Shows a message about wrong input.
                            else
                            {
                                Reader.noError = false;
                                MessageBox.Show("Ako dĺžka nie je uvedené ani číslo ani existujúca úsečka.");
                            }
                        }
                        // Command |A,X|=n; A is given point, X is new created point, n is length = number/line segment (|A,X|=|C,D| / |A,X|=u / |A,X|=100).
                        else if (newLineSegment.Length == 2)
                        {
                            if (lineEqEnds.Length == 2 && Reader.FoundObject(lineEqEnds[0]) != null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + lineEqEnds[0] + " už existuje.");
                            }
                            else if (Reader.FoundObject(newLineSegment[0]) == null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom "+newLineSegment[0]+" neexistuje.");
                            }
                            else if (!(Reader.FoundObject(newLineSegment[0])[0] is Point))
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt " + newLineSegment[0] + " nie je bod.");
                            }
                            else if (Reader.FoundObject(newLineSegment[1]) != null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + newLineSegment[0] + " už existuje.");
                            }
                            else if (lineEqEnds.Length == 2 && (lineEqEnds[1][0] != '|' || lineEqEnds[1][lineEqEnds[1].Length-1] != ',' || lineEqEnds[1][lineEqEnds[1].Length - 2] != '|'))
                            {
                                Reader.noError = false;
                                if (input[0] == "usecka")
                                    MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                                else
                                    MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                            }
                            else
                            {
                                int distancei;
                                // Reads a integer distance, draws a line segment.
                                if (Int32.TryParse(partOfInput[1], out distancei))
                                {
                                    var tempp = Reader.FoundObject(newLineSegment[0]);
                                    Point tp = (Point)tempp[0];
                                    foreach (Point p in tempp)
                                    {
                                        if (p.firstName == p.secondName)
                                            tp = p;
                                    }
                                    if (lineEqEnds.Length == 2)
                                        temp = new LineSegment(lineEqEnds[1].Substring(0, lineEqEnds[1].Length - 1), tp, distancei);
                                    else
                                        temp = new LineSegment(lineEqEnds[0], tp, distancei);
                                    Reader.allObjects.Add(temp);
                                    Drawing.DrawLineSegment(temp);
                                    Reader.allObjects.Add(temp.point2);
                                    Drawing.DrawPoint(temp.point2);
                                    if (lineEqEnds.Length == 2)
                                    {
                                        temp = new LineSegment(lineEqEnds[0], temp.point1, temp.point2, false);
                                        Reader.allObjects.Add(temp);
                                    }

                                }
                                // Reads a line segment distance, draws a line segment.
                                else if (Reader.FoundObject(partOfInput[1]) != null)
                                {
                                    double distanced = ((LineSegment)(Reader.FoundObject(partOfInput[1])[0])).length;
                                    var tempp = Reader.FoundObject(newLineSegment[0]);
                                    Point tp = (Point)tempp[0];
                                    foreach (Point p in tempp)
                                    {
                                        if (p.firstName == p.secondName)
                                            tp = p;
                                    }
                                    if (lineEqEnds.Length == 2)
                                        temp = new LineSegment(lineEqEnds[1].Substring(0, lineEqEnds[1].Length - 1), tp, distanced);
                                    else
                                        temp = new LineSegment(lineEqEnds[0], tp, distanced);
                                    Reader.allObjects.Add(temp);
                                    Drawing.DrawLineSegment(temp);
                                    Reader.allObjects.Add(temp.point2);
                                    Drawing.DrawPoint(temp.point2);
                                    if (lineEqEnds.Length == 2)
                                    {
                                        temp = new LineSegment(lineEqEnds[0], temp.point1, temp.point2, false);
                                        Reader.allObjects.Add(temp);
                                    }
                                }
                                // Shows a message about wrong input.
                                else
                                {
                                    Reader.noError = false;
                                    MessageBox.Show("Ako dĺžka nie je uvedené ani číslo ani existujúca úsečka.");
                                }
                            }
                        }
                        else
                        {
                            Reader.noError = false;
                            if (input[0] == "usecka")
                                MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                            else
                                MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                        }
                    }
                    else
                    {
                        Reader.noError = false;
                        if (input[0] == "usecka")
                            MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                        else
                            MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                    }
                    break;
                // Command for line segment is not correct, shows a message.
                default:
                    Reader.noError = false;
                    if (input[0] == "usecka")
                        MessageBox.Show("Neplatný zápis pre konštrukciu úsečky.");
                    else
                        MessageBox.Show("Neplatný zápis pre konštrukciu polpriamky.");
                    break;
            }
        }

        /// <summary>
        /// Returns the first name of line segment.
        /// </summary>
        /// <returns>The first name of line segment.</returns>
        public string GetFirstName()
        {
            return firstName;
        }
        /// <summary>
        /// Returns the second name of line segment.
        /// </summary>
        /// <returns>The second name of line segment.</returns>
        public string GetSecondName()
        {
            return secondName;
        }
    }
}
