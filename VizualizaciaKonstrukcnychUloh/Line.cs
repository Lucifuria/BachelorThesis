using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for representation of line.
    /// </summary>
    class Line :GeometricObject
    {
        /// <summary>
        /// Line which is up of the picture.
        /// </summary>
        public static Line up = new Line(Point.upLeft, Point.upRight);
        /// <summary>
        /// Line which is down of the picture.
        /// </summary>
        public static Line down = new Line(Point.downLeft, Point.downRight);
        /// <summary>
        /// Line which is on right of the picture.
        /// </summary>
        public static Line right = new Line(Point.upRight, Point.downRight);
        /// <summary>
        /// Line which is on left of the picture.
        /// </summary>
        public static Line left = new Line(Point.upLeft, Point.downLeft);

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
        /// Constant a for general equation.
        /// ax + by + c = 0
        /// </summary>
        public double a;
        /// <summary>
        /// Constant b for general equation.
        /// ax + by + c = 0
        /// </summary>
        public double b;
        /// <summary>
        /// Constant c for general equation.
        /// ax + by + c = 0
        /// </summary>
        public double c;

        /// <summary>
        /// Creates a new line through two points.
        /// </summary>
        /// <param name="p">Name of the new line.</param>
        public Line(string p)
        {
            firstName = p;

            point1 = new Point("#P"+Convert.ToString(Reader.counter++));            
            Reader.allObjects.Add(point1);

            point2 = new Point("#P" + Convert.ToString(Reader.counter++));
            Reader.allObjects.Add(point2);

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;
        }

        /// <summary>
        /// Creates a new line through two points A and B and name it as (A,B).
        /// </summary>
        /// <param name="A">First point determinating a line.</param>
        /// <param name="B">Second point determinating a line.</param>
        public Line(Point A, Point B)
        {
            Line templ;
            // If all input points are not unique, works with all copies of them.       
            if (A.firstName == A.secondName && B.firstName == B.secondName)
            {
                firstName = '(' + A.firstName + ',' + B.firstName + ')';
                secondName = firstName;
                point1 = A;
                point2 = B;

                var tempA = Reader.FoundObject(A.firstName);
                var tempB = Reader.FoundObject(B.firstName);
                foreach(GeometricObject oA in tempA)
                {
                    foreach(GeometricObject oB in tempB)
                    {
                        if (!(oA.GetFirstName() == oA.GetSecondName() || oB.GetFirstName() == oB.GetSecondName()))
                        {
                            templ = new Line((Point)oA, (Point)oB);
                            templ.secondName = templ.firstName;
                            templ.firstName = '(' + oA.GetFirstName() + ',' + oB.GetFirstName() + ')';
                            Reader.allObjects.Add(templ);
                        }
                    }
                }
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (A.firstName == A.secondName)
            {
                var tempA = Reader.FoundObject(A.firstName);
                firstName = '(' + A.firstName + ',' + B.firstName + ')';
                secondName = firstName;
                foreach (GeometricObject o in tempA)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        templ = new Line((Point)o, B);
                        templ.secondName = templ.firstName;
                        templ.firstName = '(' + o.GetFirstName() + ',' + B.firstName + ')';
                        Reader.allObjects.Add(templ);
                    }
                }
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (B.firstName == B.secondName)
            {
                var tempB = Reader.FoundObject(B.firstName);
                firstName = '(' + A.firstName + ',' + B.firstName + ')';
                secondName = firstName;
                foreach (GeometricObject o in tempB)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        templ = new Line(A, (Point)o);
                        templ.secondName = templ.firstName;
                        templ.firstName = '(' + A.firstName + ',' + o.GetFirstName() + ')';
                        Reader.allObjects.Add(templ);
                    }
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (A.secondName != "" && B.secondName != "")
                {
                    firstName = '(' + A.secondName + ',' + B.secondName + ')';
                }
                else if (A.secondName != "")
                {
                    firstName = '(' + A.secondName + ',' + B.firstName + ')';
                }
                else if (B.secondName != "")
                {
                    firstName = '(' + A.firstName + ',' + B.secondName + ')';
                }
                else
                {
                    firstName = '(' + A.firstName + ',' + B.firstName + ')';
                }          
            }

            // Sets points and constants.
            point1 = A;
            point2 = B;

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;
        }

        /// <summary>
        /// Creates a new line through two points A and B and name it as (A,B).
        /// </summary>
        /// <param name="A">First point determinating a line.</param>
        /// <param name="B">Second point determinating a line.</param>
        /// <param name="otherName">The other name of line, if it is written as "priamka p=(A,B)", then p is the other name of the line (A,B).</param>
        public Line(Point A, Point B, string otherName)
        {
            Line templ;
            // If all input points are not unique, works with all copies of them.       
            if (A.firstName == A.secondName && B.firstName == B.secondName)
            {
                firstName = otherName;
                secondName = firstName;
                point1 = A;
                point2 = B;

                var tempA = Reader.FoundObject(A.firstName);
                var tempB = Reader.FoundObject(B.firstName);
                foreach (GeometricObject oA in tempA)
                {
                    foreach (GeometricObject oB in tempB)
                    {
                        if (!(oA.GetFirstName() == oA.GetSecondName() || oB.GetFirstName() == oB.GetSecondName()))
                        {
                            templ = new Line((Point)oA, (Point)oB, otherName);
                            templ.secondName = templ.firstName;
                            templ.firstName = otherName;
                            Reader.allObjects.Add(templ);
                        }
                    }
                }
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (A.firstName == A.secondName)
            {
                var tempA = Reader.FoundObject(A.firstName);
                firstName = otherName;
                secondName = firstName;
                foreach (GeometricObject o in tempA)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        templ = new Line((Point)o, B, otherName);
                        templ.secondName = templ.firstName;
                        templ.firstName = otherName;
                        Reader.allObjects.Add(templ);
                    }
                }
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (B.firstName == B.secondName)
            {
                var tempB = Reader.FoundObject(B.firstName);
                firstName = otherName;
                secondName = firstName;
                foreach (GeometricObject o in tempB)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        templ = new Line(A, (Point)o, otherName);
                        templ.secondName = templ.firstName;
                        templ.firstName = otherName;
                        Reader.allObjects.Add(templ);
                    }
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (A.secondName != "" && B.secondName != "")
                {
                    firstName = otherName + A.secondName.Substring(A.firstName.Length) + B.secondName.Substring(B.firstName.Length);
                }
                else if (A.secondName != "")
                {
                    firstName = otherName + A.secondName.Substring(A.firstName.Length);
                }
                else if (B.secondName != "")
                {
                    firstName = otherName + B.secondName.Substring(B.firstName.Length);
                }
                else
                {
                    firstName = otherName;
                }
            }

            // Sets points and constants.
            point1 = A;
            point2 = B;

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;
        }

        /// <summary>
        /// Creates a new line through two points A and B and name it.
        /// </summary>
        /// <param name="name">Name of the new line.</param>
        /// <param name="A">First point determinating a line.</param>
        /// <param name="B">Second point determinating a line.</param>
        public Line(string name, Point A, Point B)
        {
            this.firstName = name;
            point1 = A;
            point2 = B;

            a = (point1.y - point2.y);
            b = (point2.x - point1.x);
            c = -point1.x * a - point1.y * b;
        }

        /// <summary>
        /// Determines if it has at least one intersection.
        /// </summary>
        /// <param name="line">Line which intersects.</param>
        /// <returns>False if there is at least one intersection, in other way true.</returns>
        public bool ZeroIntersections(Line line)
        {
            // Determinates if it is in same direction or same line.
            if (a == 0 && line.a == 0)
            {
                if ((double)c / b == (double)line.c / line.b)
                    return false;
                else
                    return true;
            }
            else if (a != 0 && line.a != 0)
            {
                if ((double)b / a == (double)line.b / line.a)
                {
                    if ((double)c / a == (double)line.c / line.a)
                        return false;
                    else
                        return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Returns intersection of two lines.
        /// </summary>
        /// <param name="line">Line which intersects.</param>
        /// <returns>Intersection of two lines, if there is not one, returns null.</returns>
        public Point Intersection(Line line)
        {
            // Determinates if it is in same direction or same line.
            if (a==0 && line.a == 0)
            {
                return null;
            }
            else if (a != 0 && line.a != 0 )
            {
                if ((double)b / a == (double)line.b / line.a)
                {
                    return null;
                }
            }

            double x;
            double y;

            if (b == 0)
            {
                if (line.b == 0)
                {
                    return null;
                }
                else
                {
                    y = -line.c / line.b;
                }
            }
            else
                y = (line.a * c - a * line.c) / (a * line.b - line.a * b);
            if (a == 0)
            {
                x = -line.c / line.a;
            }
            else
                x = (-b * y - c) / a;
            
            return new Point("#" + "P" + Convert.ToString(Reader.counter++), x, y);
        }

        /// <summary>
        /// Returns intersection of line and line segment.
        /// </summary>
        /// <param name="lineSegment">Line segment which intersects.</param>
        /// <returns>Intersection of line and line segment, if there is not one, returns null.</returns>
        public Point Intersection(LineSegment lineSegment)
        {
            // Determinates if it is in same direction or same line.
            if (a == 0 && lineSegment.a == 0)
            {
                return null;
            }
            else if (a != 0 && lineSegment.a != 0)
            {
                if ((double)b / a == (double)lineSegment.b / lineSegment.a)
                {
                    return null;
                }
            }

            double x;
            double y;
            y = (double)(lineSegment.a * c - a * lineSegment.c) / (a * lineSegment.b - lineSegment.a * b);
            x = (double)(-b * y - c) / a;

            // If works diferently with half line segment and line segment because of the half part.
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
        /// Returns intersection or intersections of line and cirlce.
        /// </summary>
        /// <param name="circle">Circle which intersects.</param>
        /// <returns>Intersection(s) of line and circle, if there is not one, returns null.</returns>
        public List<Point> Intersection(Circle circle)
        {
            double discriminant;
            List<Point> output = new List<Point>();
            List<double> outputcoordinates = new List<double>();
            if (b == 0)
            {
                discriminant = Sqr(-2 * circle.center.y) - 4 * (Sqr(-(c+0.0)/a)+Sqr(circle.center.x)+Sqr(circle.center.y)-Sqr(circle.radius)-(2*circle.center.x*(-(c+0.0)/a)));
                if (discriminant < 0)
                {
                    return null;
                }
                else if (discriminant == 0)
                {
                    outputcoordinates.Add(-c / a + 0.0);
                    outputcoordinates.Add((2*circle.center.y)/(2));
                }
                else
                {
                    outputcoordinates.Add((-c / a) + 0.0);
                    outputcoordinates.Add((-c / a) + 0.0);
                    outputcoordinates.Add(((2 * circle.center.y) + Math.Sqrt(discriminant))/ (2.0));
                    outputcoordinates.Add(((2 * circle.center.y) - Math.Sqrt(discriminant)) / (2.0));
                }
            }
            else
            {
                discriminant = Sqr(-2 * circle.center.x + ((2 * a * c + 0.0) / Sqr(b)) + ((2 * circle.center.y * a + 0.0) / b)) - 4 * (1 + Sqr((a + 0.0) / b)) * (Sqr((c + 0.0) / b) + ((2 * circle.center.y * c + 0.0) / b) + Sqr(circle.center.x) + Sqr(circle.center.y) - Sqr(circle.radius));
                if (discriminant < 0)
                {
                    return null;
                }
                else if (discriminant == 0)
                {
                    outputcoordinates.Add(Math.Round((-(-2 * circle.center.x + ((2 * a * c + 0.0) / Sqr(b)) + ((2 * circle.center.y * a + 0.0) / b))) / (2 * (1 + Sqr((a + 0.0) / b)))));
                    outputcoordinates.Add(Math.Round((0.0 + (-a * outputcoordinates[0] - c)) / b));
                }
                else
                {
                    outputcoordinates.Add(Math.Round((-(-2 * circle.center.x + ((2 * a * c + 0.0) / Sqr(b)) + ((2 * circle.center.y * a + 0.0) / b)) + Math.Sqrt(discriminant)) / (2 * (1 + Sqr((a + 0.0) / b)))));
                    outputcoordinates.Add(Math.Round((-(-2 * circle.center.x + ((2 * a * c + 0.0) / Sqr(b)) + ((2 * circle.center.y * a + 0.0) / b)) - Math.Sqrt(discriminant)) / (2 * (1 + Sqr((a + 0.0) / b)))));
                    outputcoordinates.Add(Math.Round((0.0 + (-a * outputcoordinates[0] - c)) / b));
                    outputcoordinates.Add(Math.Round((0.0 + (-a * outputcoordinates[1] - c)) / b));
                }
            }

            if (outputcoordinates.Count == 2)
            {
                output.Add(new Point("#P" + Reader.counter++, outputcoordinates[0], outputcoordinates[1]));               
            }
            else
            {
                output.Add(new Point("#P" + Reader.counter++, outputcoordinates[0], outputcoordinates[2]));
                output.Add(new Point("#P" + Reader.counter++, outputcoordinates[1], outputcoordinates[3]));
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
        /// Counting of square of integer.
        /// </summary>
        /// <param name="number">Integer number.</param>
        /// <returns></returns>
        static int Sqr(int number)
        {
            return number * number;
        }

        /// <summary>
        /// Counting of square of real number.
        /// </summary>
        /// <param name="number">Real number.</param>
        /// <returns></returns>
        static double Sqr(double number)
        {
            return number * number;
        }

        /// <summary>
        /// Decide if point p belongs line.
        /// </summary>
        /// <param name="p">Point on or not on line.</param>
        /// <returns>True if point p belongs line, otherwise false.</returns>
        public bool Belongs(Point p)
        {
            if (this.a * p.x + this.b * p.y + c == 0)
                return true;
            return false;
        }

        /// <summary>
        /// Creates a new line according input. If the line (or object) already exists, shows a message.
        /// </summary>
        /// <param name="input">Input line with command.</param>
        public static void WorkWithLine(string[] input)
        {
            Line temp;
            char[] separator = { ',', '(', ')', '=' };
            switch (input.Length)
            {
                // Command for line "priamka p" or "priamka (A,B)" or "priamka p=(A,B)".
                case 2:
                    string[] lineEnds = input[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);
                    if (lineEnds.Length == 1)
                    {
                        temp = new Line(input[1]);
                        if (Reader.FoundObject(temp.firstName) != null && Reader.FoundObject(temp.secondName) != null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Objekt s menom " + input[1] + " už existuje.");
                        }
                        else
                        {
                            Reader.allObjects.Add(temp);
                            Drawing.DrawLine(temp);
                        }
                    }
                    else if (lineEnds.Length == 2 || lineEnds.Length == 3)
                    {  
                        if (lineEnds.Length == 3)
                        {
                            string tempFirst = lineEnds[0];
                            lineEnds[0] = lineEnds[1];
                            lineEnds[1] = lineEnds[2];
                            lineEnds[2] = tempFirst;
                        }
                        if (Reader.FoundObject(lineEnds[0]) != null && Reader.FoundObject(lineEnds[1]) != null)
                        {
                            if (Reader.FoundObject(lineEnds[0])[0] is Point && Reader.FoundObject(lineEnds[1])[0] is Point)
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

                                temp = new Line(p1, p2);
                                Reader.allObjects.Add(temp);
                                if (lineEnds.Length == 3)
                                {
                                    temp = new Line(p1, p2, lineEnds[2]);
                                    Reader.allObjects.Add(temp);
                                }                                
                                
                                Drawing.DrawLine(temp);
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
                                MessageBox.Show("Bod "+ lineEnds[1] + " neexistuje.");
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
                            MessageBox.Show("Bod "+ lineEnds[1] + " neexistuje.");
                        }
                        else if (Reader.FoundObject(lineEnds[0]) == null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Bod " + lineEnds[0] + " neexistuje.");
                        }
                    }                    
                    break;
                // If the command is not correct, show the message.
                default:
                    Reader.noError = false;
                    MessageBox.Show("Príkaz je v nesprávnom tvare.");
                    break;
            }
        }

        /// <summary>
        /// Returns the first name of line.
        /// </summary>
        /// <returns>The first name of line.</returns>
        public string GetFirstName()
        {
            return firstName;
        }
        /// <summary>
        /// Returns the second name of line.
        /// </summary>
        /// <returns>The first name of line.</returns>
        public string GetSecondName()
        {
            return secondName;
        }
    }
}
