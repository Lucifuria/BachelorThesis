using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for representation of circle or part of circle.
    /// </summary>
    class Circle : GeometricObject
    {
        /// <summary>
        /// The first name of circle.
        /// </summary>
        public string firstName="";
        /// <summary>
        /// The second name of circle.
        /// </summary>
        public string secondName="";

        /// <summary>
        /// Center of circle.
        /// </summary>
        public Point center;
        /// <summary>
        /// Radius of circle.
        /// </summary>
        public double radius;

        /// <summary>
        /// The first point of part of circle, if exists, else null.
        /// </summary>
        public Point end1 = null;
        /// <summary>
        /// The second point of part of circle, if exists, else null.
        /// </summary>
        public Point end2 = null;

        /// <summary>
        /// Creates new circle with given center and radius.
        /// </summary>
        /// <param name="k">Name of new circle.</param>
        /// <param name="S">Center of new circle.</param>
        /// <param name="r">Radius of new circle.</param>
        public Circle(string k, Point S, double r)
        {
            // If input point is not unique, works with all copies of it.
            if (S.firstName == S.secondName)
            {
                firstName = k;
                secondName = k;

                var tempp = Reader.FoundObject(S.firstName);
                Circle tempc;
                Point temp;
                foreach (Point p in tempp)
                {
                    if (p.firstName != p.secondName)
                    {
                        tempc = new Circle(k, p, r);
                        tempc.secondName = tempc.firstName;
                        tempc.firstName = k;
                        Reader.allObjects.Add(tempc);
                    }
                }

                center = S;
                radius = r;

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (S.secondName != "")
                {
                    firstName = k + S.secondName.Substring(S.firstName.Length);
                }
                else
                {
                    firstName = k;                    
                }

                var temp = new Point("*" + k, S, r);
                if (S.secondName != "")
                    temp.secondName = "*" + k + S.secondName.Substring(S.firstName.Length);
                Reader.allObjects.Add(temp);

                center = S;
                radius = r;                
            }            
        }

        /// <summary>
        /// Creates new circle with given center and point on circle.
        /// </summary>
        /// <param name="k">Name of new circle.</param>
        /// <param name="S">Center of new circle.</param>
        /// <param name="X">Point on circle.</param>
        public Circle(string k, Point S, Point X)
        {
            // If all input points are not unique, works with all copies of them.
            if (S.firstName == S.secondName && X.firstName == X.secondName)
            {
                firstName = k;
                secondName = k;

                var temps = Reader.FoundObject(S.firstName);
                var tempx = Reader.FoundObject(X.firstName);
                Circle tempc;
                foreach (Point ps in temps)
                {
                    foreach (Point px in tempx)
                    {
                        if (ps.firstName != ps.secondName && px.firstName != px.secondName)
                        {
                            tempc = new Circle(k, ps, px);
                            tempc.secondName = tempc.firstName;
                            tempc.firstName = k;
                            Reader.allObjects.Add(tempc);
                        }
                    }
                }

                center = S;
                radius = Math.Sqrt((S.x - X.x) * (S.x - X.x) + (S.y - X.y) * (S.y - X.y));

                Point temp;
                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (S.firstName == S.secondName)
            {
                firstName = k;
                secondName = k;

                var temps = Reader.FoundObject(S.firstName);
                Circle tempc;
                foreach (Point ps in temps)
                {
                    if (ps.firstName != ps.secondName)
                    {
                        tempc = new Circle(k, ps, X);
                        tempc.secondName = tempc.firstName;
                        tempc.firstName = k;
                        Reader.allObjects.Add(tempc);
                    }
                }

                center = S;
                radius =  Math.Sqrt((S.x - X.x) * (S.x - X.x) + (S.y - X.y) * (S.y - X.y));

                Point temp;
                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (X.firstName == X.secondName)
            {
                firstName = k;
                secondName = k;

                var tempx = Reader.FoundObject(X.firstName);
                Circle tempc;
                foreach (Point px in tempx)
                {
                    if (px.firstName != px.secondName)
                    {
                        tempc = new Circle(k, S, px);
                        tempc.secondName = tempc.firstName;
                        tempc.firstName = k;
                        Reader.allObjects.Add(tempc);
                    }
                }

                center = S;
                radius = Math.Sqrt((S.x - X.x) * (S.x - X.x) + (S.y - X.y) * (S.y - X.y));

                Point temp;
                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                center = S;
                radius = Math.Sqrt((S.x - X.x) * (S.x - X.x) + (S.y - X.y) * (S.y - X.y));

                if (S.secondName != "" && X.secondName != "")
                {
                    firstName = k + S.secondName.Substring(S.firstName.Length) + X.secondName.Substring(X.firstName.Length);
                }
                else if (S.secondName != "")
                {
                    firstName = k + S.secondName.Substring(S.firstName.Length);
                }
                else if (X.secondName != "")
                {
                    firstName = k + X.secondName.Substring(X.firstName.Length);
                }
                else
                {
                    firstName = k;
                }

                var temp = new Point("*" + k, S, radius);
                if (S.secondName != "" && X.secondName != "")
                    temp.secondName = "*" + k + S.secondName.Substring(S.firstName.Length) + X.secondName.Substring(X.firstName.Length);
                else if (S.secondName != "")
                    temp.secondName = "*" + k + S.secondName.Substring(S.firstName.Length);
                else if (X.secondName != "")
                    temp.secondName = "*" + k + X.secondName.Substring(X.firstName.Length);
                Reader.allObjects.Add(temp);
            }            
        }

        /// <summary>
        /// Creates new part of circle with given center and two points on the end of part of circle.
        /// </summary>
        /// <param name="k">Name of new part of circle.</param>
        /// <param name="S">Center of new part of circle.</param>
        /// <param name="A">The first point on part of circle.</param>
        /// <param name="B">The last point on part of circle.</param>
        public Circle(string k, Point S, Point A, Point B)
        {
            Point temp;
            // If all input points are not unique, works with all copies of them.
            if (S.firstName == S.secondName && A.firstName == A.secondName && B.firstName == B.secondName)
            {
                secondName = k;
                var temps = Reader.FoundObject(S.firstName);
                var tempa = Reader.FoundObject(A.firstName);
                var tempb = Reader.FoundObject(B.firstName);
                Circle tempcir;
                foreach (Point ts in temps)
                {
                    foreach (Point ta in tempa)
                    {
                        foreach (Point tb in tempb)
                        {
                            if (ts.firstName != ts.secondName && ta.firstName != ta.secondName && tb.firstName != tb.secondName)
                            {
                                tempcir = new Circle(k, ts, ta, tb);
                                Reader.allObjects.Add(tempcir);
                            }
                        }
                    }
                }

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If two of all input points are not unique, works with all copies of them.
            else if (A.firstName == A.secondName && B.firstName == B.secondName)
            {
                secondName = k;
                var tempa = Reader.FoundObject(A.firstName);
                var tempb = Reader.FoundObject(B.firstName);
                Circle tempcir;
                foreach (Point ta in tempa)
                {
                    foreach (Point tb in tempb)
                    {
                        if (ta.firstName != ta.secondName && tb.firstName != tb.secondName)
                        {
                            tempcir = new Circle(k, S, ta, tb);
                            Reader.allObjects.Add(tempcir);
                        }
                    }
                }

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If two of all input points are not unique, works with all copies of them.
            else if (S.firstName == S.secondName && B.firstName == B.secondName)
            {
                secondName = k;
                var temps = Reader.FoundObject(S.firstName);
                var tempb = Reader.FoundObject(B.firstName);
                Circle tempcir;
                foreach (Point ts in temps)
                {
                    foreach (Point tb in tempb)
                    {
                        if (ts.firstName != ts.secondName && tb.firstName != tb.secondName)
                        {
                            tempcir = new Circle(k, ts, A, tb);
                            Reader.allObjects.Add(tempcir);
                        }
                    }
                }

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If two of all input points are not unique, works with all copies of them.
            else if (S.firstName == S.secondName && A.firstName == A.secondName)
            {
                secondName = k;
                var temps = Reader.FoundObject(S.firstName);
                var tempa = Reader.FoundObject(A.firstName);
                Circle tempcir;
                foreach (Point ts in temps)
                {
                    foreach (Point ta in tempa)
                    {
                        if (ts.firstName != ts.secondName && ta.firstName != ta.secondName)
                        {
                            tempcir = new Circle(k, ts, ta, B);
                            Reader.allObjects.Add(tempcir);
                        }
                    }
                }

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (S.firstName == S.secondName)
            {
                secondName = k;
                var temps = Reader.FoundObject(S.firstName);
                Circle tempcir;
                foreach (Point ts in temps)
                {
                    if (ts.firstName != ts.secondName)
                    {
                        tempcir = new Circle(k, ts, A, B);
                        Reader.allObjects.Add(tempcir);
                    }
                }
                
                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (A.firstName == A.secondName)
            {
                secondName = k;
                var tempa = Reader.FoundObject(A.firstName);
                Circle tempcir;
                foreach (Point ta in tempa)
                {
                    if (ta.firstName != ta.secondName)
                    {
                        tempcir = new Circle(k, S, ta, B);
                        Reader.allObjects.Add(tempcir);
                    }
                }

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If one of all input points is not unique, works with all copies of it.
            else if (B.firstName == B.secondName)
            {
                secondName = k;
                var tempb = Reader.FoundObject(B.firstName);
                Circle tempcir;
                foreach (Point tb in tempb)
                {
                    if (tb.firstName != tb.secondName)
                    {
                        tempcir = new Circle(k, S, A, tb);
                        Reader.allObjects.Add(tempcir);
                    }
                }

                var allPoints = Reader.FoundObject("*" + k);
                temp = new Point("*" + k, ((Point)allPoints[0]).x, ((Point)allPoints[0]).y);
                temp.secondName = temp.firstName;
                Reader.allObjects.Add(temp);
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (S.secondName != "" && A.secondName != "" && B.secondName != "")
                {
                    secondName = k + S.secondName.Substring(S.firstName.Length) + A.secondName.Substring(A.firstName.Length) + B.secondName.Substring(B.firstName.Length);
                }
                else if (A.secondName != "" && B.secondName != "")
                {
                    secondName = k  + A.secondName.Substring(A.firstName.Length) + B.secondName.Substring(B.firstName.Length);
                }
                else if (S.secondName != ""  && B.secondName != "")
                {
                    secondName = k + S.secondName.Substring(S.firstName.Length) + B.secondName.Substring(B.firstName.Length);
                }
                else if (S.secondName != "" && A.secondName != "")
                {
                    secondName = k + S.secondName.Substring(S.firstName.Length) + A.secondName.Substring(A.firstName.Length);
                }
                else if (S.secondName != "")
                {
                    secondName = k + S.secondName.Substring(S.firstName.Length);
                }
                else if (A.secondName != "")
                {
                    secondName = k + A.secondName.Substring(A.firstName.Length);
                }
                else if (B.secondName != "")
                {
                    secondName = k + B.secondName.Substring(B.firstName.Length);
                }

                temp = new Point("*" + k, S, radius);
                if (secondName != "")
                    temp.secondName = "*" + secondName;
                Reader.allObjects.Add(temp);
            }

            firstName = k;
            center = S;
            radius = Math.Sqrt((A.x - S.x) * (A.x - S.x) + (A.y - S.y) * (A.y - S.y));
            end1 = A;

            bool isOnCircle = ((Math.Abs((B.x - S.x) * (B.x - S.x) + (B.y - S.y) * (B.y - S.y) - (radius*radius))) <= 0.0000001);

            if (isOnCircle)
            {
                end2 = B;
            }
            else
            {
                end1 = null;
                radius = 0;
            }
        }

        /// <summary>
        /// Returns intersection of two cirlces.
        /// </summary>
        /// <param name="circle">Circle which intersects.</param>
        /// <returns>Intersection(s) of two circles, if there is not one, returns null.</returns>
        public List<Point> Intersection(Circle circle)
        {
            // The distance between the centers.
            double dx = this.center.x - circle.center.x;
            double dy = this.center.y - circle.center.y;
            double dist = Math.Sqrt(dx * dx + dy * dy);

            var output = new List<Point>();

            // Find out how many solutions there are.
            if (dist > this.radius + circle.radius)
            {
                // The circles aren't close enough, no solution.
                return null;
            }
            else if (dist < Math.Abs(this.radius - circle.radius))
            {
                // One circle is in the other, no solution.
                return null;
            }
            else if ((dist == 0) && (this.radius == circle.radius))
            {
                // The circles are the same, infinite number of solutions.                
                return output;
            }  
            else
            {
                double tempa = (this.radius * this.radius - circle.radius * circle.radius + dist * dist) / (2 * dist);
                double temph = Math.Sqrt(this.radius * this.radius - tempa * tempa);

                double centerx = this.center.x + tempa * (circle.center.x - this.center.x) / dist;
                double centery = this.center.y + tempa * (circle.center.y - this.center.y) / dist;

                Point intersect1 = new Point("#" + "P" + Convert.ToString(Reader.counter++), (centerx + temph * (circle.center.y - this.center.y) / dist), (centery - temph * (circle.center.x - this.center.x) / dist));
                Point intersect2 = new Point("#" + "P" + Convert.ToString(Reader.counter++), (centerx - temph * (circle.center.y - this.center.y) / dist), (centery + temph * (circle.center.x - this.center.x) / dist));

                // Find out if we have 1 or 2 solutions, which don't exist yet (maybe we will have zero).
                output.Add(intersect1);
                if (dist == this.radius + circle.radius)
                {
                    return output;
                }
                else
                    output.Add(intersect2);
                if (this.end1 == null && circle.end1 == null)
                {
                    return output;
                }
                else if (this.end1 != null)
                {
                    // Only this circle is part of circle.
                    if (circle.end1 == null)
                    {                        
                        var tempoutput = new List<Point>();
                        foreach (Point p in output)
                        {
                            if (p.OnObject(this))
                            {
                                tempoutput.Add(p);
                            }
                        }
                        return tempoutput;
                    }
                    // This circle and also circle are part of circle.
                    else
                    {
                        var tempoutput = new List<Point>();
                        foreach (Point p in output)
                        {
                            if (p.OnObject(this) && p.OnObject(circle))
                            {
                                tempoutput.Add(p);
                            }
                        }
                        return tempoutput;
                    }
                }
                // Only circle is part of circle.
                else
                {
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
        }        

        /// <summary>
        /// Creates a new circle according input. If the circle (or object) already exists, shows a message.
        /// </summary>
        /// <param name="input">Input line with command.</param>
        public static void WorkWithCircle(string[] input)
        {
            Circle temp;
            char[] separator = { '(', ')', ',' };
            string[] partsOfCircle = input[1].Split(separator, StringSplitOptions.RemoveEmptyEntries);
            int r = 0;
            if (Reader.FoundObject(partsOfCircle[0]) != null)
            {
                Reader.noError = false;
                MessageBox.Show("Objekt " + partsOfCircle[0] + " už existuje.");
            }
            // Command for circle.
            else if (partsOfCircle.Length == 3)
            {
                if (Reader.FoundObject(partsOfCircle[1]) == null)
                {
                    Reader.noError = false;
                    MessageBox.Show("Bod " + partsOfCircle[1] + " neexistuje.");
                }
                else if (!(Reader.FoundObject(partsOfCircle[1])[0] is Point))
                {
                    Reader.noError = false;
                    MessageBox.Show("Bod " + partsOfCircle[1] + " neexistuje.");
                }
                else
                {
                    if (Int32.TryParse(partsOfCircle[2], out r))
                    {
                        var tempp = Reader.FoundObject(partsOfCircle[1]);
                        Point p = (Point)tempp[0];
                        foreach (Point tp in tempp)
                        {
                            if (tp.firstName == tp.secondName)
                                p = tp;
                        }
                        temp = new Circle(partsOfCircle[0], p, r);
                        if (r != 0)
                        {
                            Reader.allObjects.Add(temp);
                            Drawing.DrawCircle(temp);
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Polomer kružnice " + partsOfCircle[0] + " nemôže byť 0.");
                        }
                    }
                    else
                    {
                        if (Reader.FoundObject(partsOfCircle[2]) != null && Reader.FoundObject(partsOfCircle[2])[0] is Point)
                        {
                            var tempp1 = Reader.FoundObject(partsOfCircle[1]);
                            var tempp2 = Reader.FoundObject(partsOfCircle[2]);
                            Point p1 = (Point)tempp1[0];
                            Point p2 = (Point)tempp2[0];
                            foreach (Point tp in tempp1)
                            {
                                if (tp.firstName == tp.secondName)
                                    p1 = tp;
                            }
                            foreach (Point tp in tempp2)
                            {
                                if (tp.firstName == tp.secondName)
                                    p2 = tp;
                            }
                            temp = new Circle(partsOfCircle[0], p1, p2);
                            if (temp.radius != 0)
                            {
                                Reader.allObjects.Add(temp);
                                Drawing.DrawCircle(temp);
                            }
                        }
                        else if (Reader.FoundObject(partsOfCircle[2]) != null && Reader.FoundObject(partsOfCircle[2])[0] is LineSegment && !((LineSegment)(Reader.FoundObject(partsOfCircle[2])[0])).half)
                        {
                            var tempp = Reader.FoundObject(partsOfCircle[1]);
                            Point p = (Point)tempp[0];
                            foreach (Point tp in tempp)
                            {
                                if (tp.firstName == tp.secondName)
                                    p = tp;
                            }
                            temp = new Circle(partsOfCircle[0], p, ((LineSegment)(Reader.FoundObject(partsOfCircle[2])[0])).length);
                            Reader.allObjects.Add(temp);
                            Drawing.DrawCircle(temp);
                        }
                        else if (Reader.FoundObject(partsOfCircle[2]) == null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Bod " + partsOfCircle[2] + " neexistuje.");
                        }
                        else if (!(Reader.FoundObject(partsOfCircle[2])[0] is Point))
                        {
                            Reader.noError = false;
                            MessageBox.Show("Bod " + partsOfCircle[2] + " neexistuje.");
                        }
                    }
                }
            }
            // Command for part of circle or circle with radius as line segment.
            else if (partsOfCircle.Length == 4)
            {
                if (Reader.FoundObject(partsOfCircle[2]+','+partsOfCircle[3]) != null && Reader.FoundObject(partsOfCircle[2] + ',' + partsOfCircle[3])[0] is LineSegment && ((LineSegment)Reader.FoundObject(partsOfCircle[2] + ',' + partsOfCircle[3])[0]).half == false)
                {
                    // Command "kruznica k(S,|A,B|)".
                    partsOfCircle[2] = partsOfCircle[2] + ',' + partsOfCircle[3];

                    var temp1 = Reader.FoundObject(partsOfCircle[1]);
                    var temp2 = Reader.FoundObject(partsOfCircle[2]);
                    Point p = (Point)temp1[0];
                    LineSegment ls = (LineSegment)temp2[0];
                    foreach (Point t in temp1)
                    {
                        if (t.firstName == t.secondName)
                            p = t;
                    }
                    foreach (LineSegment t in temp2)
                    {
                        if (t.firstName == t.secondName)
                            ls = t;
                    }
                    temp = new Circle(partsOfCircle[0], p, ls.length);
                    if (temp.radius != 0)
                    {
                        Reader.allObjects.Add(temp);
                        Drawing.DrawCircle(temp);
                    }
                    
                }
                // Checks if input is correct.
                else if (Reader.FoundObject(partsOfCircle[1]) == null)
                {
                    Reader.noError = false;
                    MessageBox.Show("Bod " + partsOfCircle[1] + " neexistuje.");
                }
                else if (!(Reader.FoundObject(partsOfCircle[1])[0] is Point))
                {
                    Reader.noError = false;
                    MessageBox.Show("Bod " + partsOfCircle[1] + " neexistuje.");
                }
                else
                {                  
                    if (Reader.FoundObject(partsOfCircle[2]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Bod " + partsOfCircle[2] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(partsOfCircle[2])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Bod " + partsOfCircle[2] + " neexistuje.");
                    }
                    else if (Reader.FoundObject(partsOfCircle[3]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Bod " + partsOfCircle[3] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(partsOfCircle[3])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Bod " + partsOfCircle[3] + " neexistuje.");
                    }
                    // If input is corect, creates a new circle.
                    else
                    {
                        var tempp1 = Reader.FoundObject(partsOfCircle[1]);
                        var tempp2 = Reader.FoundObject(partsOfCircle[2]);
                        var tempp3 = Reader.FoundObject(partsOfCircle[3]);
                        Point p1 = (Point)tempp1[0];
                        Point p2 = (Point)tempp2[0];
                        Point p3 = (Point)tempp3[0];
                        foreach (Point tp in tempp1)
                        {
                            if (tp.firstName == tp.secondName)
                                p1 = tp;
                        }
                        foreach (Point tp in tempp2)
                        {
                            if (tp.firstName == tp.secondName)
                                p2 = tp;
                        }
                        foreach (Point tp in tempp3)
                        {
                            if (tp.firstName == tp.secondName)
                                p3 = tp;
                        }
                        temp = new Circle(partsOfCircle[0], p1, p2, p3);
                        if (temp.radius != 0)
                        {
                            Reader.allObjects.Add(temp);
                            Drawing.DrawCircle(temp);
                        }
                        // If command for a circle isn't correct, shows a message.
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Oblúk " + partsOfCircle[0] + " nemôže byť takto zadaný.");
                        }
                    }
                }
            }
            // If command for a circle isn't correct, shows a message.
            else
            {
                Reader.noError = false;
                MessageBox.Show("Neplatný zápis pre konštrukciu kružnice alebo kružnicového oblúka.");
            }
        }

        /// <summary>
        /// Returns the first name of circle.
        /// </summary>
        /// <returns>The first name of circle.</returns>
        public string GetFirstName()
        {
            return firstName;
        }
        /// <summary>
        /// Returns the second name of circle.
        /// </summary>
        /// <returns>The second name of circle.</returns>
        public string GetSecondName()
        {
            return secondName;
        }
    }
}
