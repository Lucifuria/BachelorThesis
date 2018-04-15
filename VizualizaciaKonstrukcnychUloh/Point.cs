using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for representation of point.
    /// </summary>
    class Point :GeometricObject
    {
        /// <summary>
        /// Little constatnt for ideal distance for objects.
        /// </summary>
        const int distance = 20;

        /// <summary>
        /// Little mistake during rounding.
        /// </summary>
        const double epsilon = 0.0000000001;

        /// <summary>
        /// Point which is up left in the picture.
        /// </summary>
        static public Point upLeft = new Point("upLeft", 0, 0);
        /// <summary>
        /// Point which is up right in the picture.
        /// </summary>
        static public Point upRight = new Point("upRight", 0, 0);
        /// <summary>
        /// Point which is down left in the picture.
        /// </summary>
        static public Point downLeft = new Point("downLeft", 0, 0);
        /// <summary>
        /// Point which is down right in the picture.
        /// </summary>
        static public Point downRight = new Point("downRight", 0, 0);

        /// <summary>
        /// X ordinate of point.
        /// </summary>
        public double x = 0;
        /// <summary>
        /// Y ordinate of point, saved reversed (point (0;0) is left up, not left down).
        /// </summary>
        public double y = 0;        

        /// <summary>
        /// Name(s) of point.
        /// </summary>
        public string firstName = "";
        public string secondName = "";      

        /// <summary>
        /// Random.
        /// </summary>
        Random r = new Random();

        /// <summary>
        /// Creates a new point in particular place.
        /// </summary>
        /// <param name="X">Name of new point.</param>
        /// <param name="x">X ordinate of new point.</param>
        /// <param name="y">Y ordinate of new point.</param>
        public Point(string X, double x, double y)
        {
            firstName = X;
            this.x = x;
            this.y = y;
        }

        /// <summary>
        /// Creates a new point.
        /// </summary>
        /// <param name="X">Name of new point.</param>
        public Point(string X)
        {
            firstName = X;
            x = r.Next((int)upLeft.x + 5*distance, (int)upRight.x - 5*distance);         
            y = r.Next((int)upLeft.y + 5*distance, (int)downLeft.y - 5*distance);

            // Changes ordinates to not have same or close ordinates as another object.
            bool notend = true;
            Point temp;
            while (notend)
            {
                notend = false;
                temp = new Point("temp", x, y);
                foreach (GeometricObject o in Reader.allObjects)
                {
                    if (!temp.AllRightToObject(o))
                    {
                        x = r.Next((int)upLeft.x + 5*distance, (int)upRight.x - 5*distance);
                        y = r.Next((int)upLeft.y + 5*distance, (int)downLeft.y - 5*distance);
                        notend = true;
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Creates a point in the proper distance from another point.
        /// </summary>
        /// <param name="X">Name of new point.</param>
        /// <param name="A">Point from which creates new point in distance.</param>
        /// <param name="n">Distance between new and old point.</param>
        public Point(string X, Point A, double n)
        {
            if (A.firstName == A.secondName)
            {
                firstName = X;
                secondName = X;
                x = r.Next((int)Math.Truncate(A.x - n) + 1, (int)Math.Truncate(A.x + n));
                double discriminant = Sqr(-2 * A.y) - 4 * (Sqr(A.y) - Sqr(n) + Sqr(x - A.x));
                if (r.Next(100) < 50)
                    y = (2 * A.y + Math.Sqrt(discriminant)) / 2.0;
                else
                    y = (2 * A.y - Math.Sqrt(discriminant)) / 2.0;

                var temp = Reader.FoundObject(A.firstName);
                Point tempp;
                foreach (GeometricObject o in temp)
                {
                    if (o.GetFirstName() != o.GetSecondName())
                    {
                        tempp = new Point(X);
                        tempp.secondName = X + o.GetSecondName().Substring(o.GetFirstName().Length);
                        tempp.x = x + ((Point)o).x - A.x;
                        tempp.y = y + ((Point)o).y - A.y;
                        Reader.allObjects.Add(tempp);
                    }
                }
            }
            else
            {
                firstName = X;
                if (((int)Math.Truncate(A.x - n) + 1 < (int)Math.Truncate(A.x + n)))
                    x = r.Next((int)Math.Truncate(A.x - n) + 1, (int)Math.Truncate(A.x + n));
                else
                    x = r.Next((int)downLeft.x, (int)downRight.x);
                double discriminant = Sqr(-2 * A.y) - 4 * (Sqr(A.y) - Sqr(n) + Sqr(x - A.x));
                if (r.Next(100) < 50)
                    y = (2 * A.y + Math.Sqrt(discriminant)) / 2.0;
                else
                    y = (2 * A.y - Math.Sqrt(discriminant)) / 2.0;
            }
        }

        /// <summary>
        /// Creates a point which is or is not in object.
        /// </summary>
        /// <param name="X">Name of new point.</param>
        /// <param name="o">Object where new point lie or do not lie on.</param> 
        /// <param name="lie">Boolean which tells if it should be on object or not (true - lie, false - do not lie).</param>
        public Point(string X, GeometricObject o, bool lie)
        {
            firstName = X;
            if (lie)
            {
                if (o is Line)
                {
                    Line templ = (Line)o;
                    Point tempp;
                    bool notAllRight = true;
                    while (!this.OnObject(templ) || this.y < upLeft.y + distance || this.y > downLeft.y - distance || notAllRight)
                    {
                        tempp = new Point("temp");
                        this.x = tempp.x;
                        if (templ.b != 0)
                        {
                            this.y = (-templ.a * this.x - templ.c) / templ.b;
                            tempp.y = this.y;
                        }
                        else
                            this.y = tempp.y;
                        notAllRight = false;
                        foreach (var ob in Reader.allObjects)
                        {
                            if (!tempp.AllRightToObject(ob))
                            {
                                notAllRight = true;
                                break;
                            }
                        }
                    }
                }
                else if (o is LineSegment)
                {    
                    LineSegment templ = (LineSegment)o;
                    Point tempp = new Point("temp");
                    bool notAllRight = true;
                    while (!this.OnObject(templ) || this.y < upLeft.y + distance || this.y > downLeft.y - distance || !tempp.AllRightToObject(o) || notAllRight)
                    {
                        tempp = new Point("temp");
                        this.x = tempp.x;
                        if (templ.b != 0)
                        {
                            this.y = (-templ.a * this.x - templ.c) / templ.b;
                            tempp.y = this.y;
                        }
                        else
                            this.y = tempp.y;
                        notAllRight = false;
                        foreach (var ob in Reader.allObjects)
                        {
                            if (!tempp.AllRightToObject(ob))
                            {
                                notAllRight = true;
                                break;
                            }
                        }
                    }
                }
                else if (o is Circle)
                {
                    Circle tempc = (Circle)o;
                    Point tempp;
                    if (tempc.end1 == null)
                    {
                        tempp = new Point("temp", tempc.center, tempc.radius);
                        bool temp = true;
                        while (temp)
                        {
                            temp = false;
                            foreach(GeometricObject ob in Reader.allObjects)
                            {
                                while (ob is Point && !tempp.AllRightToObject(ob))
                                {
                                    tempp = new Point("temp", tempc.center, tempc.radius);
                                    temp = true;
                                }
                                if (temp)
                                    break;
                            }
                        }                     
                    }
                    else
                    {
                        tempp = new Point("temp", tempc.center, tempc.radius);
                        Angle compareTo = new Angle(tempc.end1, tempc.center, tempc.end2);
                        Angle tempa = new Angle(tempc.end1, tempc.center, tempp);
                        int tempdistance;
                        if (compareTo.size < 2 * distance)
                            tempdistance = (int)Math.Truncate(compareTo.size / 4.0);
                        else
                            tempdistance = distance;
                        bool temp = true;
                        int max = 50;
                        int current = 0;
                        while (temp)
                        {
                            current++;
                            while (tempa.size <= compareTo.size + tempdistance || tempa.size >= 360 - tempdistance)
                            {
                                tempp = new Point("temp", tempc.center, tempc.radius);
                                tempa = new Angle(tempc.end1, tempc.center, tempp);
                            }
                            temp = false;
                            foreach (GeometricObject ob in Reader.allObjects)
                            {
                                if (ob is Point && ((Point)ob).OnObject(tempc))
                                {
                                    while (!tempp.AllRightToObject(ob))
                                    {
                                        tempp = new Point("temp", tempc.center, tempc.radius);
                                        tempa = new Angle(tempc.end1, tempc.center, tempp);
                                        temp = true;
                                    }
                                    if (temp)
                                        break;
                                }
                            }
                            if (max < current)
                                temp = false;
                        }
                    }
                    this.x = tempp.x;
                    this.y = tempp.y;
                }
            }
            else
            {
                Point tempp = new Point("temp");
                while (tempp.OnObject(o))
                {
                    tempp = new Point("temp");
                }
                this.x = tempp.x;
                this.y = tempp.y;
            }
        }

        /// <summary>
        /// Termines if the point is able to see in picture.
        /// </summary>
        /// <returns>True if it is able to see in picture, false if it is not.</returns>
        public bool CanSee()
        {
            if (x >= Point.downLeft.x - epsilon && x <= Point.downRight.x + epsilon && y >= Point.upLeft.y - epsilon && y <= Point.downLeft.y + epsilon)
                return true;
            return false;
        }        

        /// <summary>
        /// Creates intersection of two lines.
        /// </summary>
        /// <param name="name">Name of intersection.</param>
        /// <param name="l1">First line.</param>
        /// <param name="l2">Second line.</param>
        static public void MakeIntersection(string name, Line l1, Line l2)
        {
            string name1;
            string name2;
            if (l1.firstName != l1.secondName && l1.secondName != "")
                name1 = l1.secondName;
            else
                name1 = l1.firstName;
            if (l2.firstName != l2.secondName && l2.secondName != "")
                name2 = l2.secondName;
            else
                name2 = l2.firstName;

            Point temp = (l1.Intersection(l2));
            // If there is not one intersection, finds out how many and shows a message.
            if (temp == null)
            {
                if (l1.ZeroIntersections(l2))
                {
                    Reader.noError = false;
                    MessageBox.Show("Priamky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                }
                else
                {
                    Reader.noError = false;
                    MessageBox.Show("Priamky " + name1 + " a " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                }
            }
            // If there is object with same name, shows a message.
            else if (Reader.FoundObject(name) != null)
            {
                Reader.noError = false;
                MessageBox.Show("Objekt s menom " + name + " už existuje.");
            }
            // If everything is ok, add a intersection and draws it.
            else
            {
                temp.firstName = name;
                Reader.allObjects.Add(temp);
                Drawing.DrawPoint(temp);
            }
        }

        /// <summary>
        /// Creates intersection of line and line segment.
        /// </summary>
        /// <param name="name">Name of intersection.</param>
        /// <param name="l">Line.</param>
        /// <param name="ls">Line segment.</param>
        static public void MakeIntersection(string name, Line l, LineSegment ls)
        {
            string name1;
            string name2;
            if (l.firstName != l.secondName && l.secondName != "")
                name1 = l.secondName;
            else
                name1 = l.firstName;
            if (ls.firstName != ls.secondName && ls.secondName != "")
                name2 = ls.secondName;
            else
                name2 = ls.firstName;

            Point temp = (l.Intersection(ls));
            // If there is not one intersection, finds out how many and shows a message.
            if (temp == null)
            {
                if (l.a == 0 && ls.a == 0)
                {
                    if ((double)l.c / l.b == (double)ls.c / ls.b)
                    {
                        if (ls.half)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " +name1 + " a polpriamka " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a úsečka " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                        }
                    }
                    else
                    {
                        if (ls.half)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a polpriamka " + name2 + " nemajú spoločný priesečník.");
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a úsečka " + name2 + " nemajú spoločný priesečník.");
                        }
                    }
                }
                else if (l.a != 0 && ls.a != 0)
                {
                    if ((double)l.b / l.a == (double)ls.b / ls.a)
                    {
                        if (ls.half)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a polpriamka " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a úsečka " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                        }
                    }
                    else
                    {
                        if (ls.half)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a polpriamka " + name2 + " nemajú spoločný priesečník.");
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Priamka " + name1 + " a úsečka " + name2 + " nemajú spoločný priesečník.");
                        }
                    }
                } 
                else
                {
                    if (ls.half)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Priamka " + name1 + " a polpriamka " + name2 + " nemajú spoločný priesečník.");
                    }
                    else
                    {
                        Reader.noError = false;
                        MessageBox.Show("Priamka " + name1 + " a úsečka " + name2 + " nemajú spoločný priesečník.");
                    }
                }
            }
            // If there is object with same name, shows a message.
            else if (Reader.FoundObject(name) != null)
            {
                Reader.noError = false;
                MessageBox.Show("Objekt s menom " + name + " už existuje.");
            }
            // If everything is ok, add a intersection and draws it.
            else
            {
                temp.firstName = name;
                Reader.allObjects.Add(temp);
                Drawing.DrawPoint(temp);
            }
        }

        /// <summary>
        /// Creates intersection of line and circle.
        /// </summary>
        /// <param name="name">Name of intersection.</param>
        /// <param name="l">Line.</param>
        /// <param name="c">Circle.</param>
        static public void MakeIntersection(string name, Line l, Circle c)
        {
            string name1;
            string name2;
            if (l.firstName != l.secondName && l.secondName != "")
                name1 = l.secondName;
            else
                name1 = l.firstName;
            if (c.firstName != c.secondName && c.secondName != "")
                name2 = c.secondName;
            else
                name2 = c.firstName;

            List<Point> temp = (l.Intersection(c));
            // If there is not one intersection, shows a message.
            if (temp == null || temp.Count ==  0)
            {
                Reader.noError = false;
                MessageBox.Show("Priamka " + name1 + " a kružnica " + name2 + " nemajú spoločný priesečník.");
            }
            // If there is object with same name, shows a message.
            else if (Reader.FoundObject(name) != null)
            {
                Reader.noError = false;
                MessageBox.Show("Objekt s menom " + name + " už existuje.");
            }
            // If there is one intersection, adds its.
            else if (temp.Count == 1)
            {
                temp[0].firstName = name;
                Reader.allObjects.Add(temp[0]);
                Drawing.DrawPoint(temp[0]);
                MessageBox.Show("Priamka " + name1 + " sa dotýka kružnice " + name2 + " v jednom bode.");
            }
            // If there is two intersections, adds them.
            else
            {
                temp[0].firstName = name;
                temp[1].firstName = name;
                temp[0].secondName = name;
                Reader.allObjects.Add(temp[0]);
                temp[0].secondName = name + "1";
                temp[1].secondName = name + "2";
                Reader.allObjects.Add(temp[0]);
                Drawing.DrawPoint(temp[0]);
                Reader.allObjects.Add(temp[1]);
                Drawing.DrawPoint(temp[1]);
                var tempp = new Point(temp[0].firstName, temp[0].x, temp[0].y);
                tempp.secondName = tempp.firstName;
                Reader.allObjects.Add(tempp);
                Drawing.DrawPoint(tempp);
                MessageBox.Show("Priamka " + name1 + " pretína kružnicu " + name2 + " v dvoch bodoch.");
            }
        }

        /// <summary>
        /// Creates intersection of two line segments.
        /// </summary>
        /// <param name="name">Name of intersection.</param>
        /// <param name="ls1">First line segment.</param>
        /// <param name="ls2">Second line segment.</param>
        static public void MakeIntersection(string name, LineSegment ls1, LineSegment ls2)
        {
            string name1;
            string name2;
            if (ls1.firstName != ls1.secondName && ls1.secondName != "")
                name1 = ls1.secondName;
            else
                name1 = ls1.firstName;
            if (ls2.firstName != ls2.secondName && ls2.secondName != "")
                name2 = ls2.secondName;
            else
                name2 = ls2.firstName;

            Point temp = (ls1.Intersection(ls2));
            // If there is not one intersection, finds out how many and shows a message.
            if (temp == null)
            {
                if (ls1.a == 0 && ls2.a == 0)
                {
                    if ((double)ls1.c / ls1.b == (double)ls2.c / ls2.b)
                    {
                        if (Math.Max(ls1.point1.x, ls1.point2.x) < Math.Min(ls2.point1.x, ls2.point2.x) || Math.Min(ls1.point1.x, ls1.point2.x) > Math.Max(ls2.point1.x, ls2.point2.x))
                        {
                            Reader.noError = false;
                            MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                        }
                        else if (Math.Max(ls1.point1.y, ls1.point2.y) < Math.Min(ls2.point1.y, ls2.point2.y) || Math.Min(ls1.point1.y, ls1.point2.y) > Math.Max(ls2.point1.y, ls2.point2.y))
                        {
                            Reader.noError = false;
                            MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Úsečky " + name1 + " a " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                        }
                    }
                    else
                    {
                        Reader.noError = false;
                        MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                    }
                }
                else if (ls1.a != 0 && ls2.a != 0)
                {
                    if ((double)ls1.b / ls1.a == (double)ls2.b / ls2.a)
                    {
                        if (Math.Max(ls1.point1.x, ls1.point2.x) < Math.Min(ls2.point1.x, ls2.point2.x) || Math.Min(ls1.point1.x, ls1.point2.x) > Math.Max(ls2.point1.x, ls2.point2.x))
                        {
                            Reader.noError = false;
                            MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                        }
                        else if (Math.Max(ls1.point1.y, ls1.point2.y) < Math.Min(ls2.point1.y, ls2.point2.y) || Math.Min(ls1.point1.y, ls1.point2.y) > Math.Max(ls2.point1.y, ls2.point2.y))
                        {
                            Reader.noError = false;
                            MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Úsečky " + name1 + " a " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
                        }
                    }
                    else
                    {
                        Reader.noError = false;
                        MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                    }
                }
                else
                {
                    Reader.noError = false;
                    MessageBox.Show("Úsečky " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
                }
            }
            // If there is object with same name, shows a message.
            else if (Reader.FoundObject(name) != null)
            {
                Reader.noError = false;
                MessageBox.Show("Objekt s menom " + name + " už existuje.");
            }
            // If everything is ok, add a intersection and draws it.
            else
            {
                temp.firstName = name;
                Reader.allObjects.Add(temp);
                Drawing.DrawPoint(temp);
            }
        }

        /// <summary>
        /// Creates intersection of line segment and circle.
        /// </summary>
        /// <param name="name">Name of intersection.</param>
        /// <param name="ls">Line segment.</param>
        /// <param name="c">Circle.</param>
        static public void MakeIntersection(string name, LineSegment ls, Circle c)
        {
            string name1;
            string name2;
            if (ls.firstName != ls.secondName && ls.secondName != "")
                name1 = ls.secondName;
            else
                name1 = ls.firstName;
            if (c.firstName != c.secondName && c.secondName != "")
                name2 = c.secondName;
            else
                name2 = c.firstName;

            List<Point> temp = (ls.Intersection(c));
            // If there is not one intersection, shows a message.
            if (temp == null)
            {
                if (ls.half)
                {
                    Reader.noError = false;
                    MessageBox.Show("Pplpriamka " + name1 + " a kružnica " + name2 + " nemajú spoločný priesečník.");
                }
                else
                {
                    Reader.noError = false;
                    MessageBox.Show("Úsečka " + name1 + " a kružnica " + name2 + " nemajú spoločný priesečník.");
                }                
            }
            // If there is object with same name, shows a message.
            else if (Reader.FoundObject(name) != null)
            {
                Reader.noError = false;
                MessageBox.Show("Objekt s menom " + name + " už existuje.");
            }
            // If there is one intersection, adds its.
            else if (temp.Count == 1)
            {
                temp[0].firstName = name;
                Reader.allObjects.Add(temp[0]);
                Drawing.DrawPoint(temp[0]);
                if (ls.half)
                {
                    MessageBox.Show("Polpriamka " + name1 + " sa dotýka kružnice alebo pretína kružnicu " + name2 + " v jednom bode.");
                }
                else
                {
                    MessageBox.Show("Úsečka " + name1 + " sa dotýka kružnice alebo pretína kružnicu " + name2 + " v jednom bode.");
                }
            }
            // If there is two intersections, adds them.
            else
            {
                temp[0].firstName = name;
                temp[1].firstName = name;
                temp[0].secondName = name;
                Reader.allObjects.Add(temp[0]);
                temp[0].secondName = name + "1";
                temp[1].secondName = name + "2";
                Reader.allObjects.Add(temp[0]);
                Drawing.DrawPoint(temp[0]);
                Reader.allObjects.Add(temp[1]);
                Drawing.DrawPoint(temp[1]);
                var tempp = new Point(temp[0].firstName, temp[0].x, temp[0].y);
                tempp.secondName = tempp.firstName;
                Reader.allObjects.Add(tempp);
                Drawing.DrawPoint(tempp);
                if (ls.half)
                {
                    MessageBox.Show("Polpriamka " + name1 + " pretína kružnicu " + name2 + " v dvoch bodoch.");
                }
                else
                {
                    MessageBox.Show("Úsečka " + name1 + " pretína kružnicu " + name2 + " v dvoch bodoch.");
                }                
            }
        }

        /// <summary>
        /// Decides if the point is on object (point, circle, line, line segment).
        /// </summary>
        /// <param name="o">Object.</param>
        /// <returns> True if the point is on object, otherwise false.</returns>
        public bool OnObject(GeometricObject o)
        {
            // If object is point, compares their coordinates.
            if (o is Point)
            {
                return (this.x == ((Point)o).x && this.y == ((Point)o).y);
            }
            // If object is line, compares their parameters.
            else if (o is Line)
            {
                return (((Line)o).a * this.x + ((Line)o).b * this.y + ((Line)o).c == 0);
            }
            // If object is circle, compares their centers and diameters.
            else if (o is Circle)
            {
                if (((Circle)o).end1 == null)
                    return Math.Abs(Sqr(this.x - ((Circle)o).center.x) + Sqr(this.y - ((Circle)o).center.y) - Sqr(((Circle)o).radius)) <= epsilon;
                else
                {
                    if (Math.Abs(Sqr(this.x - ((Circle)o).center.x) + Sqr(this.y - ((Circle)o).center.y) - Sqr(((Circle)o).radius)) <= epsilon)
                    {
                        Angle compareTo = new Angle(((Circle)o).end1, ((Circle)o).center, ((Circle)o).end2);
                        Angle comparingAngle = new Angle(((Circle)o).end1, ((Circle)o).center, new Point("temp", this.x, this.y));
                        if (comparingAngle.size <= compareTo.size)
                            return false;
                        else
                            return true;
                    }
                    else
                        return false;
                }
            }
            // If object is line segment, compares their parameters and end points.
            else if (o is LineSegment)
            {
                // Says if the point could be on line segment according to border points.
                bool temp = true;
                // If it is half line.
                if (((LineSegment)o).half)
                {
                    if (((LineSegment)o).point1.x < ((LineSegment)o).point2.x && ((LineSegment)o).point1.x > this.x)
                        temp = false;
                    if (((LineSegment)o).point1.y < ((LineSegment)o).point2.y && ((LineSegment)o).point1.y > this.y)
                        temp = false;

                    if (((LineSegment)o).point1.x > ((LineSegment)o).point2.x && ((LineSegment)o).point1.x < this.x)
                        temp = false;
                    if (((LineSegment)o).point1.y > ((LineSegment)o).point2.y && ((LineSegment)o).point1.y < this.y)
                        temp = false;
                }
                // If it is line segment.
                else
                {
                    if ((Math.Min(((LineSegment)o).point1.x, ((LineSegment)o).point2.x) > this.x) || (Math.Max(((LineSegment)o).point1.x, ((LineSegment)o).point2.x) < this.x))
                        temp = false;
                    if ((Math.Min(((LineSegment)o).point1.y, ((LineSegment)o).point2.y) > this.y) || (Math.Max(((LineSegment)o).point1.y, ((LineSegment)o).point2.y) < this.y))
                        temp = false;
                }
                return ((((LineSegment)o).a * this.x + ((LineSegment)o).b * this.y + ((LineSegment)o).c == 0) && temp);
            }
            return false;
        }


        /// <summary>
        /// Decides if the point is not near to object (point, line, line segment, circle).
        /// </summary>
        /// <param name="o">Object.</param>
        /// <returns> True if the point is not near  to object, otherwise false.</returns>
        public bool AllRightToObject(GeometricObject o)
        {
            // If object is point, compares their coordinates.
            if (o is Point)
            {
                return !(Math.Abs(this.x - ((Point)o).x) <= distance && Math.Abs(this.y - ((Point)o).y) <= distance);
            }
            // If object is line, compares their parameters.
            else if (o is Line)
            {
                return !(Math.Abs(((Line)o).a * this.x + ((Line)o).b + this.y + ((Line)o).c) <= distance);
            }
            // If object is circle, compares their centers and diameters.
            else if (o is Circle)
            {
                Circle tempc = (Circle)o;
                if (tempc.end1 == null)
                {
                    if (Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) <= Sqr(tempc.radius))
                        return !(Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) >= Sqr(tempc.radius) - distance);
                    return !(Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) <= Sqr(tempc.radius) + distance);
                }
                else
                {
                    Angle compareTo = new Angle(tempc.end1, tempc.center, tempc.end2);
                    Angle comparing = new Angle(tempc.end1, tempc.center, new Point("temp", this.x, this.y));
                    int tempdistance;
                    if (compareTo.size < 2 * distance)
                        tempdistance = (int)Math.Truncate(compareTo.size / 4.0);
                    else
                        tempdistance = distance;
                    if ((Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) <= Sqr(tempc.radius)) && (Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) >= Sqr(tempc.radius) - distance))
                    {                        
                        if (comparing.size <= compareTo.size - tempdistance || comparing.size >= 360 + tempdistance)
                            return false;
                        else
                            return true;
                    }
                    else if ((Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) >= Sqr(tempc.radius)) && (Sqr(this.x - tempc.center.x) + Sqr(this.y - tempc.center.y) <= Sqr(tempc.radius) + distance))
                    {
                        if (comparing.size <= compareTo.size - tempdistance || comparing.size >= 360 + tempdistance)
                            return false;
                        else
                            return true;
                    }
                }
            }
            // If object is line segment, compares their parameters and end points.
            else if (o is LineSegment)
            {
                // Says if the point could be on line segment according to border points.
                bool temp = true;
                // If it is half line.
                if (((LineSegment)o).half)
                {
                    if (((LineSegment)o).point1.x < ((LineSegment)o).point2.x && ((LineSegment)o).point1.x > this.x)
                        temp = false;
                    if (((LineSegment)o).point1.y < ((LineSegment)o).point2.y && ((LineSegment)o).point1.y > this.x)
                        temp = false;

                    if (((LineSegment)o).point1.x > ((LineSegment)o).point2.x && ((LineSegment)o).point1.x < this.x)
                        temp = false;
                    if (((LineSegment)o).point1.y > ((LineSegment)o).point2.y && ((LineSegment)o).point1.y < this.x)
                        temp = false;
                }
                // If it is line segment.
                else
                {
                    if ((Math.Min(((LineSegment)o).point1.x, ((LineSegment)o).point2.x) > this.x) || (Math.Max(((LineSegment)o).point1.x, ((LineSegment)o).point2.x) < this.x))
                        temp = false;
                    if ((Math.Min(((LineSegment)o).point1.y, ((LineSegment)o).point2.y) > this.y) || (Math.Max(((LineSegment)o).point1.y, ((LineSegment)o).point2.y) < this.y))
                        temp = false;
                }

                if (this.x - ((LineSegment)o).point1.x > distance || this.y - ((LineSegment)o).point1.y > distance)
                    temp = false;
                if (this.x - ((LineSegment)o).point2.x > distance || this.y - ((LineSegment)o).point2.y > distance)
                    temp = false;

                return !((Math.Abs(((LineSegment)o).a * this.x + ((LineSegment)o).b + this.y + ((LineSegment)o).c) <= distance) && temp);
            }
            return true;
        }

        /// <summary>
        /// Returns square of number.
        /// </summary>
        /// <param name="a">Number.</param>
        /// <returns>Square of number a.</returns>
        double Sqr(double a)
        {
            return a * a;
        }


        /// <summary>
        /// Creates intersection of two circles.
        /// </summary>
        /// <param name="name">Name of intersection.</param>
        /// <param name="c1">First circle.</param>
        /// <param name="c2">Second circle.</param>
        static public void MakeIntersection(string name, Circle c1, Circle c2)
        {
            var temp = (c1.Intersection(c2));
            string name1;
            string name2;
            if (c1.firstName != c1.secondName && c1.secondName != "")
                name1 = c1.secondName;
            else
                name1 = c1.firstName;
            if (c2.firstName != c2.secondName && c2.secondName != "")
                name2 = c2.secondName;
            else
                name2 = c2.firstName;

            // If there is not one intersection, finds out how many and shows a message.
            if (temp == null)
            {
                Reader.noError = false;
                MessageBox.Show("Kružnice " + name1 + " a " + name2 + " nemajú spoločný priesečník.");
            }
            else if (temp.Count == 0 && Math.Abs(c1.center.x - c2.center.x) <= epsilon && Math.Abs(c1.center.y - c2.center.y) <= epsilon)
            {
                Reader.noError = false;
                MessageBox.Show("Kružnice " + name1 + " a " + name2 + " majú nekonečne veľa spoločných priesečníkov.");
            }
            // If there is object with same name, shows a message.
            else if (Reader.FoundObject(name) != null && (c1.secondName == "" && c2.secondName == ""))
            {
                Reader.noError = false;
                MessageBox.Show("Objekt s menom " + name + " už existuje.");
            }
            // If everything is ok, add a intersection(s) and draws it.
            else if (temp.Count > 0)
            {
                var output = new Point(name, temp.ElementAt(0).x, temp.ElementAt(0).y);
                output.firstName = name;
                if (temp.Count == 1)
                {
                    if (Reader.FoundObject(name) == null)
                        Reader.allObjects.Add(output);
                }
                if (temp.Count == 2)
                {
                    temp = DeleteExistingPoints(temp);
                    var allPoints = Reader.FoundObject(output.firstName);

                    if (temp.Count > 0)
                    {
                        output.secondName = name;
                        if (Reader.FoundObject(name) == null)
                            Reader.allObjects.Add(output);                        

                        var copyoutput = new Point(name, temp.ElementAt(0).x, temp.ElementAt(0).y);
                        if (allPoints != null)
                            copyoutput.secondName = name + Convert.ToString(allPoints.Count);
                        else
                            copyoutput.secondName = name + "1";
                        Reader.allObjects.Add(copyoutput);
                    }
                    if (temp.Count > 1)
                    {
                        var copyoutput = new Point(name, temp.ElementAt(1).x, temp.ElementAt(1).y);
                        if (allPoints != null)
                            copyoutput.secondName = name + Convert.ToString(allPoints.Count + 1);
                        else
                            copyoutput.secondName = name + "2";
                        Reader.allObjects.Add(copyoutput);
                    }
                }
                if (temp.Count > 0)
                    Drawing.DrawPoint(output);
            }
        }

        /// <summary>
        /// Deletes existing points from the list.
        /// </summary>
        /// <param name="listOfPoints">List of points which will be confirmated.</param>
        /// <returns>List of points which don't exist yet.</returns>
        static List<Point> DeleteExistingPoints(List<Point> listOfPoints)
        {
            Point op;
            var output = new List<Point>();
            bool addp = true;
            foreach (Point p in listOfPoints)
            {
                addp = true;
                foreach (Object o in Reader.allObjects)
                {
                    if (o is Point)
                    {
                        op = (Point)o;
                        if (Math.Abs(op.x - p.x) <= 0.00001 && Math.Abs(op.y - p.y) <= 0.00001 && (op.firstName != p.firstName || (op.firstName == p.firstName && op.secondName == p.secondName)))
                        {
                            addp = false;
                            break;
                        }
                    }
                }
                if (addp)
                    output.Add(p);
            }
            return output;
        }

        /// <summary>
        /// Creates a new point according input. If the point (or object) already exists, shows a message.
        /// </summary>
        /// <param name="input">Input line with command.</param>
        public static void WorkWithPoint(string[] input)
        {
            Point temp;
            switch (input.Length)
            {
                // Creates just a point. Also according coordinates.
                case 2:
                    char[] sep = { '(', ')', ';' };
                    string[] temps = input[1].Split(sep, StringSplitOptions.RemoveEmptyEntries);
                    double x, y;
                    if (temps.Length > 1)
                    {
                        if (double.TryParse(temps[1], out x) && double.TryParse(temps[2], out y))
                        {
                            temp = new Point(temps[0], x, y);
                            if (Reader.FoundObject(temp.firstName) != null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + temp.firstName + " už existuje.");
                            }
                            else
                            {
                                Reader.allObjects.Add(temp);
                                Drawing.DrawPoint(temp);
                            }
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Neplatný zápis, chýbajú dve celé čísla.");
                        }
                    }
                    else
                    {
                        temp = new Point(input[1]);
                        if (Reader.FoundObject(temp.firstName) != null)
                        {
                            Reader.noError = false;
                            MessageBox.Show("Objekt s menom " + temp.firstName + " už existuje.");
                        }
                        else
                        {
                            Reader.allObjects.Add(temp);
                            Drawing.DrawPoint(temp);
                        }
                    }
                    break;
                // Creates a point in distance from another point.
                case 3:
                    char[] separator = { '|', ',', '=' };
                    input[1] = input[1].Split(separator, StringSplitOptions.RemoveEmptyEntries)[0];
                    string[] partOfInput = input[2].Split(separator, StringSplitOptions.RemoveEmptyEntries);

                    // Shows a message if there is no point for distance.
                    if (Reader.FoundObject(partOfInput[0]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Bod " + partOfInput[0] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(partOfInput[0])[0] is Point))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Bod " + partOfInput[0] + " neexistuje.");
                    }
                    // If there is point for distance, creates a new point.
                    else
                    {
                        int distancei;
                        // Reads a integer distance.
                        if (Int32.TryParse(partOfInput[2], out distancei))
                        {
                            var tempp = Reader.FoundObject(partOfInput[0]);
                            Point p = (Point)tempp[0];
                            foreach (Point t in tempp)
                            {
                                if (t.firstName == t.secondName)
                                    p = t;
                            }
                            temp = new Point(input[1], p, distancei);
                            // If object with name exists, shows a message.
                            if (Reader.FoundObject(temp.firstName) != null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + temp.firstName + " už existuje.");
                            }
                            else
                            {
                                Reader.allObjects.Add(temp);
                                Drawing.DrawPoint(temp);
                            }
                        }
                        // Reads a line segment distance.
                        else if (Reader.FoundObject('|' + partOfInput[2] + ',' + partOfInput[3] + '|') != null && (Reader.FoundObject('|' + partOfInput[2] + ',' + partOfInput[3] + '|')[0] is LineSegment))
                        {
                            double distanced;
                            distanced = ((LineSegment)(Reader.FoundObject('|' + partOfInput[2] + ',' + partOfInput[3] + '|'))[0]).length;
                            var tempp = Reader.FoundObject(partOfInput[0]);
                            Point p = (Point)tempp[0];
                            foreach (Point t in tempp)
                            {
                                if (t.firstName == t.secondName)
                                    p = t;
                            }
                            temp = new Point(input[1], p, distanced);
                            // If object with name exists, shows a message.
                            if (Reader.FoundObject(temp.firstName) != null)
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + temp.firstName + " už existuje.");
                            }
                            else
                            {
                                Reader.allObjects.Add(temp);
                                Drawing.DrawPoint(temp);
                            }
                        }
                        // Shows a message about wrong input.
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Ako dĺžka nie je uvedené ani číslo ani existujúca úsečka.");
                        }
                    }
                    break;
                // Creates point on object.
                case 4:
                    if (Reader.FoundObject(input[1]) != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[1] + " už existuje.");
                    }
                    else if (Reader.FoundObject(input[3]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[3] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(input[3])[0] is Line || Reader.FoundObject(input[3])[0] is LineSegment || Reader.FoundObject(input[3])[0] is Circle))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[3] + " nie je priamka, polpriamka, úsečka a ani kružnica.");
                    }
                    else
                    {
                        var tempo = Reader.FoundObject(input[3]);
                        GeometricObject o = tempo[0];
                        foreach (GeometricObject ob in tempo)
                        {
                            if (ob.GetFirstName() == ob.GetSecondName())
                                o = ob;
                        }
                        Point tempp = new Point(input[1], o, true);
                        Reader.allObjects.Add(tempp);
                        Drawing.DrawPoint(tempp);
                    }
                    break;
                // Creates point not on object.
                case 5:
                    if (Reader.FoundObject(input[1]) != null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[1] + " už existuje.");
                    }
                    else if (Reader.FoundObject(input[4]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[4] + " neexistuje.");
                    }
                    else if (!(Reader.FoundObject(input[4])[0] is Line || Reader.FoundObject(input[4])[0] is LineSegment || Reader.FoundObject(input[4])[0] is Circle))
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[4] + " nie je priamka, polpriamka, úsečka a ani kružnica.");
                    }
                    else
                    {
                        var tempo = Reader.FoundObject(input[4]);
                        GeometricObject o = tempo[0];
                        foreach (GeometricObject ob in tempo)
                        {
                            if (ob.GetFirstName() == ob.GetSecondName())
                                o = ob;
                        }
                        Point tempp = new Point(input[1], o, false);
                        Reader.allObjects.Add(tempp);
                        Drawing.DrawPoint(tempp);
                    }
                    break;
                // Creates intersection of two objects.
                case 6:
                    // Test if there is particular objects.
                    if (Reader.FoundObject(input[3]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[3] + " neexistuje.");
                    }
                    else if (Reader.FoundObject(input[5]) == null)
                    {
                        Reader.noError = false;
                        MessageBox.Show("Objekt s menom " + input[5] + " neexistuje.");
                    }
                    else
                    {
                        // Test if the first object is line, line segment or circle.
                        string num = "";
                        if (Reader.FoundObject(input[3])[0] is Line)
                        {
                            // Test if the second object is line, line segment or circle.
                            if (Reader.FoundObject(input[5])[0] is Line)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (Line o1 in temp1)
                                {
                                    foreach (Line o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length) + o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o1, o2);
                                    }
                                }
                            }
                            else if (Reader.FoundObject(input[5])[0] is LineSegment)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (Line o1 in temp1)
                                {
                                    foreach (LineSegment o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length) + o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o1, o2);
                                    }
                                }
                            }
                            else if (Reader.FoundObject(input[5])[0] is Circle)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (Line o1 in temp1)
                                {
                                    foreach (Circle o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length) + o2.GetSecondName().Substring(o2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.GetSecondName().Substring(o2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o1, o2);
                                    }
                                }
                            }
                            else
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + input[5] + " nie je priamka, úsečka ani kružnica.");
                            }
                        }
                        else if (Reader.FoundObject(input[3])[0] is LineSegment)
                        {
                            // Test if the second object is line, line segment or circle.
                            if (Reader.FoundObject(input[5])[0] is Line)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (LineSegment o1 in temp1)
                                {
                                    foreach (Line o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length) + o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o2, o1);
                                    }
                                }
                            }
                            else if (Reader.FoundObject(input[5])[0] is LineSegment)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (LineSegment o1 in temp1)
                                {
                                    foreach (LineSegment o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length) + o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o1, o2);
                                    }
                                }
                            }
                            else if (Reader.FoundObject(input[5])[0] is Circle)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (LineSegment o1 in temp1)
                                {
                                    foreach (Circle o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length) + o2.GetSecondName().Substring(o2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.point1.GetSecondName().Substring(o1.point1.GetFirstName().Length) + o1.point2.GetSecondName().Substring(o1.point2.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.GetSecondName().Substring(o2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o1, o2);
                                    }
                                }
                            }
                            else
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + input[5] + " nie je priamka, úsečka ani kružnica.");
                            }
                        }
                        else if (Reader.FoundObject(input[3])[0] is Circle)
                        {
                            // Test if the second object is line, line segment or circle.
                            if (Reader.FoundObject(input[5])[0] is Line)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (Circle o1 in temp1)
                                {
                                    foreach (Line o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.GetSecondName().Substring(o1.GetFirstName().Length) + o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.GetSecondName().Substring(o1.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o2, o1);
                                    }
                                }
                            }
                            else if (Reader.FoundObject(input[5])[0] is LineSegment)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (Circle o1 in temp1)
                                {
                                    foreach (LineSegment o2 in temp2)
                                    {
                                        if (o1.GetSecondName() != "" && o2.GetSecondName() != "")
                                            num = o1.GetSecondName().Substring(o1.GetFirstName().Length) + o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);
                                        else if (o1.GetSecondName() != "")
                                            num = o1.GetSecondName().Substring(o1.GetFirstName().Length);
                                        else if (o2.GetSecondName() != "")
                                            num = o2.point1.GetSecondName().Substring(o2.point1.GetFirstName().Length) + o2.point2.GetSecondName().Substring(o2.point2.GetFirstName().Length);

                                        MakeIntersection(input[1] + num, o2, o1);
                                    }
                                }
                            }
                            else if (Reader.FoundObject(input[5])[0] is Circle)
                            {
                                var temp1 = Reader.FoundObject(input[3]);
                                var temp2 = Reader.FoundObject(input[5]);
                                GeometricObject t1 = temp1[0];
                                GeometricObject t2 = temp2[0];
                                foreach (Circle o1 in temp1)
                                {
                                    foreach (Circle o2 in temp2)
                                    {
                                        MakeIntersection(input[1], o1, o2);
                                    }
                                }
                            }
                            else
                            {
                                Reader.noError = false;
                                MessageBox.Show("Objekt s menom " + input[5] + " nie je priamka, úsečka ani kružnica.");
                            }
                        }
                        else
                        {
                            Reader.noError = false;
                            MessageBox.Show("Objekt s menom " + input[3] + " nie je priamka, úsečka ani kružnica.");
                        }
                    }
                    break;
                // Command for point is not correct, shows a message.
                default:
                    Reader.noError = false;
                    MessageBox.Show("Neplatný zápis pre konštrukciu bodu.");
                    break;
            }
        }

        /// <summary>
        /// Returns the first name of point.
        /// </summary>
        /// <returns>The first name of point.</returns>
        public string GetFirstName()
        {
            return firstName;
        }
        /// <summary>
        /// Returns the second name of point.
        /// </summary>
        /// <returns>The second name of point.</returns>
        public string GetSecondName()
        {
            return secondName;
        }
    }
}
