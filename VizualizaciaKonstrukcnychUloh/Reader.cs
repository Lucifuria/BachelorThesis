using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for reading lines with command in construction.
    /// </summary>
    class Reader
    {
        /// <summary>
        /// List with all geometric objects used in construction, can be used for control.
        /// </summary>
        public static List<GeometricObject> allObjects = new List<GeometricObject>();

        /// <summary>
        /// Boolean for stoping calling commands of macro, false, if there is some error.
        /// </summary>
        public static bool noError = true;

        /// <summary>
        /// Order of temporary object for naming.
        /// </summary>
        public static int counter = 0;

        /// <summary>
        /// Reads line in process of construction and works with it.
        /// </summary>
        /// <param name="line">Read line.</param>
        public static void ReadLine(string line)
        {
            char[] separator = { ' ' };
            string[] parsed = line.Split(separator, StringSplitOptions.RemoveEmptyEntries);
            // If there is a empty line, shows a message.
            if (parsed.Length != 0)
            {
                // Works with geometric object(or macro) according its name.
                switch (parsed[0])
                {
                    case "bod":
                        Point.WorkWithPoint(parsed);
                        break;
                    case "priamka":
                        Line.WorkWithLine(parsed);
                        break;
                    case "usecka":
                        LineSegment.WorkWithLineSegment(parsed, false);
                        break;
                    case "polpriamka":
                        LineSegment.WorkWithLineSegment(parsed, true);
                        break;
                    case "kruznica":
                        Circle.WorkWithCircle(parsed);
                        break;
                    case "obluk":
                        Circle.WorkWithCircle(parsed);
                        break;
                    case "uhol":
                        Angle.WorkWithAngle(parsed);
                        break;
                    default:
                        if (line.Contains("(") && line.Contains(")") && line.Contains("-"))
                            Macros.FindMacro(line);
                        else
                        {
                            MessageBox.Show("Riadok " + line + " nie je v správnom tvare.");
                            Reader.noError = false;
                        }
                        break;
                }
            }
        }

        /// <summary>
        /// Returns a object in list of all objects with the particular name or null if there is no one.
        /// </summary>
        /// <param name="nameOfObject">Name of wanted object.</param>
        /// <returns>Returns a wanted object according its name or null if it does not exist.</returns>
        public static List<GeometricObject> FoundObject(string nameOfObject)
        {
            // Searchs whole list of geometric objects.
            var output = new List<GeometricObject>();

            foreach (GeometricObject o in allObjects)
            {
                if (o.GetSecondName() == "" && o.GetFirstName() == nameOfObject)
                {
                    output.Add(o);
                    return output;
                }
                if (o.GetSecondName() == nameOfObject && o.GetSecondName() != o.GetFirstName())
                {
                    output.Add(o);
                    return output;
                }
                if (o.GetFirstName() == nameOfObject)
                {
                    output.Add(o);
                }
            }
            if (output.Count == 0)
                return null;
            int firstIndex = 0;
            if (output.Count != 1)
            {
                foreach (GeometricObject o in output)
                {
                    if (o.GetFirstName() == o.GetSecondName())
                        break;
                    firstIndex++;
                }
                if (firstIndex < output.Count)
                {
                    var temp = output[0];
                    output[0] = output[firstIndex];
                    output[firstIndex] = temp;
                }
            }
            return output;
        }
    }
}
