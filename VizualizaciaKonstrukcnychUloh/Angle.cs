using System;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for representation of angle.
    /// </summary>
    class Angle : GeometricObject
    {
        /// <summary>
        /// The first name of angle.
        /// </summary>
        public string firstName="";
        /// <summary>
        /// The second name of angle.
        /// </summary>
        public string secondName="";

        /// <summary>
        /// Vertex of angle.
        /// </summary>
        public Point vertex;
        /// <summary>
        /// The first point determinating the angle.
        /// </summary>
        public Point point1;
        /// <summary>
        /// The second point determinating the angle.
        /// </summary>
        public Point point2;

        /// <summary>
        /// The first half line segment from the center through the point1.
        /// </summary>
        public LineSegment line1;
        /// <summary>
        /// The second half line segment from the center through the point2.
        /// </summary>
        public LineSegment line2;

        /// <summary>
        /// Size of angle, in degrees, in interval (-360,360), depends on direction, represented in clokwise direction.
        /// </summary>
        public float size;
        /// <summary>
        /// Size of angle of line1 and x axis, represented in clokwise direction.
        /// </summary>
        public float size1;
        /// <summary>
        /// Size of angle of line2 and x axis, represented in clokwise direction.
        /// </summary>
        public float size2;

        /// <summary>
        /// Little constant.
        /// </summary>
        const int epsilon = 30;

        /// <summary>
        /// Creates a new angle given by center and two points.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="vertex">The vertex of the angle.</param>
        /// <param name="point2">The second point.</param>
        public Angle(Point point1, Point vertex, Point point2)
        {
            this.firstName = "|" + point1.firstName + "," + vertex.firstName + "," + point2.firstName + "|";
            Angle tempa;
            
            // If all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && vertex.firstName == vertex.secondName && point2.firstName==point2.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var temp2 = Reader.FoundObject(point2.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point tv in tempv)
                    {
                        foreach (Point t2 in temp2)
                        {
                            if (t1.firstName != t1.secondName && t2.firstName != t2.secondName && tv.firstName != tv.secondName)
                            {
                                tempa = new Angle(t1, tv, t2);
                                Reader.allObjects.Add(tempa);
                            }
                        }
                    }
                }
            }
            // If two of all input points are not unique, works with all copies of them.
            if (vertex.firstName == vertex.secondName && point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp2 = Reader.FoundObject(point2.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point tv in tempv)
                {
                    foreach (Point t2 in temp2)
                    {
                        if (t2.firstName != t2.secondName && tv.firstName != tv.secondName)
                        {
                            tempa = new Angle(point1, tv, t2);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If two of all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var temp2 = Reader.FoundObject(point2.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point t2 in temp2)
                    {
                        if (t1.firstName != t1.secondName && t2.firstName != t2.secondName)
                        {
                            tempa = new Angle(t1, vertex, t2);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If two of all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && vertex.firstName == vertex.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point tv in tempv)
                    {
                        if (t1.firstName != t1.secondName && tv.firstName != tv.secondName)
                        {
                            tempa = new Angle(t1, tv, point2);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            if (point1.firstName == point1.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                foreach (Point t1 in temp1)
                {
                    if (t1.firstName != t1.secondName)
                    {
                        tempa = new Angle(t1, vertex, point2);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            if (vertex.firstName == vertex.secondName)
            {
                secondName = firstName;
                var tempv = Reader.FoundObject(vertex.firstName);foreach (Point tv in tempv)
                {
                    if (tv.firstName != tv.secondName)
                    {
                        tempa = new Angle(point1, tv, point2);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            if (point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp2 = Reader.FoundObject(point2.firstName);
                foreach (Point t2 in temp2)
                {
                    if (t2.firstName != t2.secondName)
                    {
                        tempa = new Angle(point1, vertex, t2);
                        Reader.allObjects.Add(tempa);
                    }                        
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (point1.secondName != "" && vertex.secondName != "" && point2.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.secondName + ',' + point2.secondName + '|';
                }
                else if (point1.secondName != "" && vertex.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.secondName + ',' + point2.firstName + '|';
                }
                else if (point1.secondName != "" && point2.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.firstName + ',' + point2.secondName + '|';
                }
                else if (vertex.secondName != "" && point2.secondName != "")
                {
                    secondName = '|' + point1.firstName + ',' + vertex.secondName + ',' + point2.secondName + '|';
                }
                else if (point1.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.firstName + ',' + point2.firstName + '|';
                }
                else if (vertex.secondName != "")
                {
                    secondName = '|' + point1.firstName + ',' + vertex.secondName + ',' + point2.firstName + '|';
                }
                else if (point2.secondName != "")
                {
                    secondName = '|' + point1.firstName + ',' + vertex.firstName + ',' + point2.secondName + '|';
                }
            }

            // Sets points, lines and sizes.
            this.point1 = point1;
            this.point2 = point2;
            this.vertex = vertex;

            line1 = new LineSegment(vertex, point1, true);
            line2 = new LineSegment(vertex, point2, true);
            if (Reader.FoundObject(line1.firstName) == null)
                Reader.allObjects.Add(line1);
            if (Reader.FoundObject(line2.firstName) == null)
                Reader.allObjects.Add(line2);

            size1 = (float)(Math.Atan2(point1.y - vertex.y, point1.x - vertex.x) * (180 / Math.PI));
            size2 = (float)(Math.Atan2(point2.y - vertex.y, point2.x - vertex.x) * (180 / Math.PI));
            if (size1 < 0)
                size1 = 360 + size1;
            if (size2 < 0)
                size2 = 360 + size2;
            size = (size2 - size1) % 360;
            if (size < 0)
                size = 360 + size;
        }

        /// <summary>
        /// Creates a new angle given by center and two points.
        /// </summary>
        /// <param name="name">Name of the angle.</param>
        /// <param name="point1">The first point.</param>
        /// <param name="vertex">The vertex of the angle.</param>
        /// <param name="point2">The second point.</param>
        public Angle(string name, Point point1, Point vertex, Point point2)
        {
            this.firstName = name;

            Angle tempa;

            // If all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && vertex.firstName == vertex.secondName && point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var temp2 = Reader.FoundObject(point2.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point tv in tempv)
                    {
                        foreach (Point t2 in temp2)
                        {
                            if (t1.firstName != t1.secondName && t2.firstName != t2.secondName && tv.firstName != tv.secondName)
                            {
                                tempa = new Angle(name, t1, tv, t2);
                                Reader.allObjects.Add(tempa);
                            }
                        }
                    }
                }
            }
            // If two of all input points are not unique, works with all copies of them.
            if (vertex.firstName == vertex.secondName && point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp2 = Reader.FoundObject(point2.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point tv in tempv)
                {
                    foreach (Point t2 in temp2)
                    {
                        if (t2.firstName != t2.secondName && tv.firstName != tv.secondName)
                        {
                            tempa = new Angle(name, point1, tv, t2);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If two of all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var temp2 = Reader.FoundObject(point2.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point t2 in temp2)
                    {
                        if (t1.firstName != t1.secondName && t2.firstName != t2.secondName)
                        {
                            tempa = new Angle(name, t1, vertex, t2);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If two of all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && vertex.firstName == vertex.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point tv in tempv)
                    {
                        if (t1.firstName != t1.secondName && tv.firstName != tv.secondName)
                        {
                            tempa = new Angle(name, t1, tv, point2);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            if (point1.firstName == point1.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                foreach (Point t1 in temp1)
                {
                    if (t1.firstName != t1.secondName)
                    {
                        tempa = new Angle(name, t1, vertex, point2);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            if (vertex.firstName == vertex.secondName)
            {
                secondName = firstName;
                var tempv = Reader.FoundObject(vertex.firstName); foreach (Point tv in tempv)
                {
                    if (tv.firstName != tv.secondName)
                    {
                        tempa = new Angle(name, point1, tv, point2);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            if (point2.firstName == point2.secondName)
            {
                secondName = firstName;
                var temp2 = Reader.FoundObject(point2.firstName);
                foreach (Point t2 in temp2)
                {
                    if (t2.firstName != t2.secondName)
                    {
                        tempa = new Angle(name, point1, vertex, t2);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (point1.secondName != "" && vertex.secondName != "" && point2.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.secondName + ',' + point2.secondName + '|';
                }
                else if (point1.secondName != "" && vertex.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.secondName + ',' + point2.firstName + '|';
                }
                else if (point1.secondName != "" && point2.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.firstName + ',' + point2.secondName + '|';
                }
                else if (vertex.secondName != "" && point2.secondName != "")
                {
                    secondName = '|' + point1.firstName + ',' + vertex.secondName + ',' + point2.secondName + '|';
                }
                else if (point1.secondName != "")
                {
                    secondName = '|' + point1.secondName + ',' + vertex.firstName + ',' + point2.firstName + '|';
                }
                else if (vertex.secondName != "")
                {
                    secondName = '|' + point1.firstName + ',' + vertex.secondName + ',' + point2.firstName + '|';
                }
                else if (point2.secondName != "")
                {
                    secondName = '|' + point1.firstName + ',' + vertex.firstName + ',' + point2.secondName + '|';
                }
            }

            // Sets points, lines and sizes.
            this.point1 = point1;
            this.point2 = point2;
            this.vertex = vertex;

            line1 = new LineSegment(vertex, point1, true);
            line2 = new LineSegment(vertex, point2, true);
            if (Reader.FoundObject(line1.firstName) == null)
                Reader.allObjects.Add(line1);
            if (Reader.FoundObject(line2.firstName) == null)
                Reader.allObjects.Add(line2);

            size1 = (float)(Math.Atan2(point1.y - vertex.y, point1.x - vertex.x) * (180 / Math.PI));
            size2 = (float)(Math.Atan2(point2.y - vertex.y, point2.x - vertex.x) * (180 / Math.PI));
            if (size1 < 0)
                size1 = 360 + size1;
            if (size2 < 0)
                size2 = 360 + size2;
            size = (size2 - size1) % 360;
            if (size < 0)
                size = 360 + size;
        }

        /// <summary>
        /// Creates a new angle given by point, center point and size.
        /// </summary>
        /// <param name="point1">The first point.</param>
        /// <param name="vertex">The vertex of the angle.</param>
        /// <param name="point2">Name of the second point.</param>
        /// <param name="size">Size of the angle.</param>
        public Angle(Point point1, Point vertex, string spoint2, float size)
        {
            firstName = "|" + point1.firstName + "," + vertex.firstName + "," + spoint2 + "|";

            Angle tempa;

            // If all input points are not unique, works with all copies of them.
            if (point1.firstName == point1.secondName && vertex.firstName == vertex.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point t1 in temp1)
                {
                    foreach (Point tv in tempv)
                    {
                        if (t1.firstName != t1.secondName && tv.firstName != tv.secondName)
                        {
                            tempa = new Angle(t1, tv, spoint2, size);
                            Reader.allObjects.Add(tempa);
                        }
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            else if (point1.firstName == point1.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                foreach (Point t1 in temp1)
                {
                    if (t1.firstName != t1.secondName)
                    {
                        tempa = new Angle(t1, vertex, spoint2, size);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If one of all input points are not unique, works with all copies of it.
            else if (vertex.firstName == vertex.secondName)
            {
                secondName = firstName;
                var temp1 = Reader.FoundObject(point1.firstName);
                var tempv = Reader.FoundObject(vertex.firstName);
                foreach (Point tv in tempv)
                {
                    if (tv.firstName != tv.secondName)
                    {
                        tempa = new Angle(point1, tv, spoint2, size);
                        Reader.allObjects.Add(tempa);
                    }
                }
            }
            // If none of all input points is not unique, works only with this point.
            else
            {
                if (point1.secondName != "" && vertex.secondName != "" )
                {
                    spoint2 = spoint2 + point1.secondName.Substring(point1.firstName.Length) + vertex.secondName.Substring(vertex.firstName.Length);
                    secondName = '|' + point1.secondName + ',' + vertex.secondName + ',' + point2;
                }
                else if (point1.secondName != "")
                {
                    spoint2 = spoint2 + point1.secondName.Substring(point1.firstName.Length);
                    secondName = '|' + point1.secondName + ',' + vertex.firstName + ',' + point2;
                }
                else if (vertex.secondName != "")
                {
                    spoint2 = spoint2 + vertex.secondName.Substring(vertex.firstName.Length);
                    secondName = '|' + point1.firstName + ',' + vertex.secondName + ',' + point2;
                }
            }

            // Sets points, lines and sizes.
            this.point1 = point1;
            this.vertex = vertex;

            this.size = size;
            size1 = (float)(Math.Atan2(point1.y - vertex.y, point1.x - vertex.x) * (180 / Math.PI));            
            size2 = size + size1;

            point2 = new Point(spoint2);

            // Sets x axis.
            float tempsize = size2;
            if (tempsize > 0)
                tempsize -= 360;
            if ((tempsize > -90 || tempsize < -270))
            {
                point2.x = vertex.x + epsilon;
            }
            else if ((tempsize < -90 && tempsize > -270))
            {
                point2.x = vertex.x - epsilon;
            }
            else
            {
                point2.x = vertex.x;
            }

            point2.y = Math.Tan(size2 * Math.PI / 180) * (point2.x-vertex.x) + vertex.y;
            Point temppoint2 = new Point("t", point2.x, point2.y);
            LineSegment templine2 = new LineSegment(vertex, temppoint2, true);

            // Puts point on line2 in the same distance from vertex as point1.
            point2 = templine2.Intersection(new Circle("temp", vertex, point1))[0];
            point2.firstName = spoint2;

            if (Reader.FoundObject(spoint2) == null)
                Reader.allObjects.Add(point2);


            line1 = new LineSegment(vertex, point1, true);
            line2 = new LineSegment(vertex, this.point2, true);
            if (Reader.FoundObject(line1.firstName) == null)
                Reader.allObjects.Add(line1);
            if (Reader.FoundObject(line2.firstName) == null)
                Reader.allObjects.Add(line2);
        }


        /// <summary>
        /// Creates a new angle according input. If the angle (or object) already exists, shows a message.
        /// </summary>
        /// <param name="input">Input line with command.</param>
        public static void WorkWithAngle(string[] input)
        {
            // Command for angle as "uhol A,V,B", given by three points.
            if (input.Length == 2)
            {
                char[] sepeq = { '=' };
                string[] names = input[1].Split(sepeq, StringSplitOptions.RemoveEmptyEntries);
                char[] sepcomma = { ',' };
                string[] points = null;
                if (names.Length == 1)
                     points = input[1].Split(sepcomma, StringSplitOptions.RemoveEmptyEntries);
                else if (names.Length == 2)
                    points = names[1].Split(sepcomma, StringSplitOptions.RemoveEmptyEntries);
                // Checks if input is correct.
                if (points.Length == 3)
                {
                    if (names.Length == 2 && Reader.FoundObject(names[0]) != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + names[0] + " už existuje.");
                    }
                    else if (Reader.FoundObject(input[1]) != null || Reader.FoundObject('|' + input[1] + '|') != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[1] + " už existuje.");
                    }
                    else if (Reader.FoundObject(points[0]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[0] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(points[0])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[0] + " nie je bod.");
                    }
                    else if (Reader.FoundObject(points[1]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[1] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(points[1])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[1] + " nie je bod.");
                    }
                    if (Reader.FoundObject(points[2]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[2] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(points[2])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[2] + " nie je bod.");
                    }
                    // If input is correct, creates a new angle according it.
                    else
                    {
                        var temp0 = Reader.FoundObject(points[0]);
                        var temp1 = Reader.FoundObject(points[1]);
                        var temp2 = Reader.FoundObject(points[2]);
                        Point p0 = (Point)temp0[0];
                        Point p1 = (Point)temp1[0];
                        Point p2 = (Point)temp2[0];
                        foreach (Point t in temp0)
                        {
                            if (t.firstName == t.secondName)
                                p0 = t;
                        }
                        foreach (Point t in temp1)
                        {
                            if (t.firstName == t.secondName)
                                p1 = t;
                        }
                        foreach (Point t in temp2)
                        {
                            if (t.firstName == t.secondName)
                                p2 = t;
                        }
                        var temp = new Angle(p0, p1, p2);
                        Reader.allObjects.Add(temp);
                        Drawing.DrawAngle(temp);
                        if (names.Length == 2)
                        {
                            temp = new Angle(names[0], p0, p1, p2);
                            Reader.allObjects.Add(temp);
                        }
                    }
                }
                // If command for new angle is not correct, writes a message about it.
                else
                {
                    Reader.noError = false;
                    MessageBox.Show("Neplatný zápis pre konštrukciu uhla.");
                }
            }
            // Command for angle as "uhol A,V,X, |A,V,X|=100" or "uhol A,V,X, |A,V,X|=|L,M,O|"
            else if (input.Length == 3)
            {
                char[] sepequals = { '=' };
                string[] nameAndSize = input[2].Split(sepequals, StringSplitOptions.RemoveEmptyEntries);

                string[] names = input[1].Split(sepequals, StringSplitOptions.RemoveEmptyEntries);
                char[] sepcomma = { ',' };
                string[] points = null;
                if (names.Length == 1)
                    points = input[1].Split(sepcomma, StringSplitOptions.RemoveEmptyEntries);
                else if (names.Length == 2)
                    points = names[1].Split(sepcomma, StringSplitOptions.RemoveEmptyEntries);

                // Checks if input is correct.
                if (points.Length == 3)
                {
                    if (names.Length == 2 && Reader.FoundObject(names[0]) != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + names[0] + " už existuje.");
                    }
                    else if (nameAndSize.Length != 2)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Neplatný zápis pre konštrukciu uhla.");
                    }
                    else if (Reader.FoundObject(input[1].Substring(0,input[1].Length-1)) != null || Reader.FoundObject('|' + input[1].Substring(0, input[1].Length - 1) + '|') != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[1] + " už existuje.");
                    }
                    else if (Reader.FoundObject(points[0]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[0] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(points[0])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[0] + " nie je bod.");
                    }
                    else if (Reader.FoundObject(points[1]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[1] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(points[1])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[1] + " nie je bod.");
                    }
                    if (Reader.FoundObject(points[2]) != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + points[2] + " už existuje.");
                    }
                    // If input is corect, creates a new angle.
                    else
                    {
                        double size;
                        if (Double.TryParse(nameAndSize[1], out size))
                        {
                            var temp0 = Reader.FoundObject(points[0]);
                            var temp1 = Reader.FoundObject(points[1]);
                            Point p0 = (Point)temp0[0];
                            Point p1 = (Point)temp1[0];
                            foreach (Point t in temp0)
                            {
                                if (t.firstName == t.secondName)
                                    p0 = t;
                            }
                            foreach (Point t in temp1)
                            {
                                if (t.firstName == t.secondName)
                                    p1 = t;
                            }
                            var temp = new Angle(p0, p1, points[2], (float)(size%360));
                            Reader.allObjects.Add(temp);
                            Drawing.DrawAngle(temp);
                            if (names.Length == 2)
                            {
                                temp = new Angle(names[0], temp.point1, temp.vertex, temp.point2);
                                Reader.allObjects.Add(temp);
                            }
                        }
                        else if (Reader.FoundObject(nameAndSize[1]) != null && Reader.FoundObject(nameAndSize[1])[0] is Angle)
                        {
                            var temp0 = Reader.FoundObject(points[0]);
                            var temp1 = Reader.FoundObject(points[1]);
                            Point p0 = (Point)temp0[0];
                            Point p1 = (Point)temp1[0];
                            foreach (Point t in temp0)
                            {
                                if (t.firstName == t.secondName)
                                    p0 = t;
                            }
                            foreach (Point t in temp1)
                            {
                                if (t.firstName == t.secondName)
                                    p1 = t;
                            }
                            var temp = new Angle(p0, p1, points[2], ((Angle)Reader.FoundObject(nameAndSize[1])[0]).size);
                            Reader.allObjects.Add(temp);
                            Drawing.DrawAngle(temp);
                            if (names.Length == 2)
                            {
                                temp = new Angle(names[0], temp.point1, temp.vertex, temp.point2);
                                Reader.allObjects.Add(temp);
                            }
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Veľkosť uhla nie je udaná číslom ani veľkosťou iného uhla.");
                        }
                    }

                }
                // If command for an angle isn't correct, shows a message.
                else
                {
                    Reader.noError = false;
                    MessageBox.Show("Neplatný zápis pre konštrukciu uhla.");
                }
            }
            // If command for an angle isn't correct, shows a message.
            else
            {
                Reader.noError = false;
                MessageBox.Show("Neplatný zápis pre konštrukciu uhla.");
            }
        }

        /// <summary>
        /// Returns the first name of the angle.
        /// </summary>
        /// <returns>The first name of the angle.</returns>
        public string GetFirstName()
        {
            return firstName;
        }
        /// <summary>
        /// Returns the second name of the angle.
        /// </summary>
        /// <returns>The second name of the angle.</returns>
        public string GetSecondName()
        {
            return secondName;
        }
    }
}
