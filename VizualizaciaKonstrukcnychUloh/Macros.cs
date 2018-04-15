using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Visualization
{
    /// <summary>
    /// Class for working with macros.
    /// </summary>
    class Macros
    {
        /// <summary>
        /// Folder which is the macros saved in.
        /// </summary>
        const string pathOfFile = "macros/";

        /// <summary>
        /// Visualizer where it is drawn.
        /// </summary>
        static public Visualizer visualizer;

        /// <summary>
        /// Enumeration of parts of macro.
        /// </summary>
        public enum PartsOfMacro
        {
            /// <summary>
            /// Name of macro.
            /// </summary>
            Name = 0,

            /// <summary>
            /// Input objects of macro.
            /// </summary>
            Inputs = 1,

            /// <summary>
            /// Commands of macro.
            /// </summary>
            Commands = 2,

            /// <summary>
            /// Output objects of macro.
            /// </summary>
            Outputs = 3,

            /// <summary>
            /// Description of macro.
            /// </summary>
            Description = 4
        };

        /// <summary>
        /// Rewrites macro for easy calling and saves it to the file.
        /// </summary>
        /// <param name="name">Name of the macro.</param>
        /// <param name="lines">Lines of macro.</param>
        /// <param name="outlines">Changed lines.</param>
        /// <param name="currentPart">Current part of macro which is saving.</param>
        /// <param name="allVariables"></param>
        /// <returns>All variables which are created now.</returns>
        public static Dictionary<string,string> SaveMacro(string name, string[] lines, out string[] outlines, PartsOfMacro currentPart, Dictionary<string,string> allVariables)
        {
            // All variables used in the macro, where key is the original name and value is the new name.

            char[] sepspace = { ' ' };
            string[] partsOfLine;
            
            int currentLine = 0;

            if (lines.Length == 0)
            {
                outlines = lines;
                return allVariables;
            }

            if (String.IsNullOrWhiteSpace(lines[lines.Length-1]))
            {
                Array.Resize(ref lines, lines.Length - 1);
            }
            foreach (string line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    MessageBox.Show("V zápise sa nachádza prázdny riadok.");
                    outlines = null;
                    return null;
                }
                #region Input
                else if (currentPart == PartsOfMacro.Inputs)
                {
                    partsOfLine = line.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);

                    if (partsOfLine.Length != 2)
                    {
                        MessageBox.Show("Nesprávna forma vstupného parametru: " + line);
                        outlines = null;
                        return null;
                    }
                    else
                    {
                        if (allVariables.ContainsKey(partsOfLine[1]))
                        {
                            MessageBox.Show("Objekt s názov " + partsOfLine[1] + " už je v zozname vstupných parametrov.");
                            outlines = null;
                            return null;
                        }
                        else if (!IsTypeOfObject(partsOfLine[0]))
                        {
                            MessageBox.Show("Typ objektu " + partsOfLine[0] + " neexistuje a nemôže byť zadaný ako vstupný parameter.");
                            outlines = null;
                            return null;
                        }
                        else
                        {
                            allVariables.Add(partsOfLine[1], "#" + name + "_" + partsOfLine[1]);
                            lines[currentLine] = partsOfLine[0]+ " "+ "#" + name + "_" + partsOfLine[1];
                        }
                    }
                }
                #endregion
                #region Commands
                else if (currentPart == PartsOfMacro.Commands)
                {
                    lines[currentLine] = ChangeCommand(line, ref allVariables, name);
                    if (lines[currentLine] == null)
                    {
                        outlines = null;
                        return null;
                    }
                }
                #endregion
                #region Outputs
                else if (currentPart == PartsOfMacro.Outputs)
                {
                    partsOfLine = line.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);

                    if (partsOfLine.Length != 2)
                    {
                        MessageBox.Show("Nesprávna forma výstupného parametru: " + line);
                        outlines = null;
                        return null;
                    }
                    else
                    {
                        if (!allVariables.ContainsKey(partsOfLine[1]))
                        {
                            MessageBox.Show("Objekt s názov " + partsOfLine[1] + " nevznikne počas konštrukcie makra, nie je možné ho pridať ako výstupný parameter.");
                            outlines = null;
                            return null;
                        }
                        else if (!IsTypeOfObject(partsOfLine[0]))
                        {
                            MessageBox.Show("Typ objektu " + partsOfLine[0] + " neexistuje a nemôže byť zadaný ako výstupný parameter.");
                            outlines = null;
                            return null;
                        }
                        else
                        {
                            lines[currentLine] = partsOfLine[0] + " " + allVariables[partsOfLine[1]];
                        }
                    }
                }
                #endregion
                currentLine++;
            }
            outlines = lines;
            return allVariables;
        }

        /// <summary>
        /// Changes command for creating a new object to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of original macro for some commands to change.</param>
        /// <returns>New command.</returns>
        static string ChangeCommand(string command, ref Dictionary<string,string> variables, string nameOfMacro)
        {
            char[] sep = { ' ' };
            string[] partsOfCommands = command.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommands.Length == 1)
            {
                return ChangeCommandForMacro(command, ref variables);
            }
            else
            {
                switch (partsOfCommands[0])
                {
                    case "bod":
                        return ChangeCommandForPoint(command, ref variables, nameOfMacro);
                    case "priamka":
                        return ChangeCommandForLine(command, ref variables, nameOfMacro);
                    case "usecka":
                        return ChangeCommandForLineSegment(command, ref variables,nameOfMacro);
                    case "polpriamka":
                        return ChangeCommandForHalfLineSegment(command, ref variables, nameOfMacro);
                    case "kruznica":
                        return ChangeCommandForCircle(command, ref variables, nameOfMacro);
                    case "obluk":
                        return ChangeCommandForPartCircle(command, ref variables, nameOfMacro);
                    case "uhol":
                        return ChangeCommandForAngle(command, ref variables, nameOfMacro);
                    default:
                        MessageBox.Show("Príkaz " + command + "nie je v správnom tvare.");
                        return null;
                }
            }
        }


        /// <summary>
        /// Changes command for creating a new point to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForPoint(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sepspace = { ' ' };
            string[] partsOfCommand = command.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);

            switch (partsOfCommand.Length)
            {
                case 2:
                    if (partsOfCommand[1][partsOfCommand[1].Length-1] == ')')
                    {
                        char[] septemp = { '(' };
                        string[] temp = partsOfCommand[1].Split(septemp, StringSplitOptions.RemoveEmptyEntries);
                        if (variables.ContainsKey(temp[0]))
                        {
                            MessageBox.Show("Objekt s menom " + temp[0] + " už existuje a nemožno ho použiť v príkaze " + command + ".");
                            return null;
                        }
                        else
                        {
                            variables.Add(temp[0], "#" + nameOfMacro + "_" + temp[0]);
                            return partsOfCommand[0] + " " + variables[temp[0]] + "(" + temp[1];
                        }
                    }
                    else
                    {
                        if (variables.ContainsKey(partsOfCommand[1]))
                        {
                            MessageBox.Show("Objekt s menom " + partsOfCommand[1] + " už existuje a nemožno ho použiť v príkaze " + command + ".");
                            return null;
                        }
                        else
                        {
                            variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                            return partsOfCommand[0] + " " + variables[partsOfCommand[1]];
                        }
                    }
                case 3:
                    partsOfCommand[1] = partsOfCommand[1].Substring(0, partsOfCommand[1].Length - 1);
                    if (variables.ContainsKey(partsOfCommand[1]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[1] + " už existuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else
                    {
                        char[] sepequals = { '=' };
                        string[] partsOfDef = partsOfCommand[2].Split(sepequals, StringSplitOptions.RemoveEmptyEntries);

                        int number;
                        if (partsOfDef.Length != 2)
                        {
                            MessageBox.Show("Príkaz " + command + " je v nesprávnom tvare a nemožno ho použiť.");
                            return null;
                        }
                        else if (!Int32.TryParse(partsOfDef[1], out number) && !variables.ContainsKey(partsOfDef[1]))
                        {
                            MessageBox.Show("V príkaze " + command + " nie je ako veľkosť uvedené prirodzené číslo ani iná úsečka.");
                            return null;
                        }
                        else
                        {
                            char[] sepline = { '|', ',' };
                            string[] pointsOnLine = partsOfDef[0].Split(sepline, StringSplitOptions.RemoveEmptyEntries);
                            if (pointsOnLine.Length != 2)
                            {
                                MessageBox.Show("Príkaz " + command + " je v nesprávnom tvare a nemožno ho použiť.");
                                return null;
                            }
                            else if (pointsOnLine[1] != partsOfCommand[1])
                            {
                                MessageBox.Show("Príkaz " + command + " je v nesprávnom tvare a nemožno ho použiť.");
                                return null;
                            }
                            else if (!variables.ContainsKey(pointsOnLine[0]) && !ContainsSimilarToKey(ref variables, pointsOnLine[0]))
                            {
                                MessageBox.Show("Bod " + pointsOnLine[0] + " v príkaze " + command + " neexistuje a nemožno ho použiť.");
                                return null;
                            }
                            else if (variables.ContainsKey(partsOfDef[1]))
                            {
                                variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                                return partsOfCommand[0] + " " + variables[partsOfCommand[1]] + ", |" + variables[pointsOnLine[0]] + "," + variables[partsOfCommand[1]] + "|=" + variables[partsOfDef[1]];
                            }        
                            else
                            {
                                variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                                return partsOfCommand[0] + " " + variables[partsOfCommand[1]] + ", |" + variables[pointsOnLine[0]] + "," + variables[partsOfCommand[1]] + "|=" + partsOfDef[1];

                            }
                        }
                    }
                case 4:
                    if (variables.ContainsKey(partsOfCommand[1]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[1] + " už existuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else if (!variables.ContainsKey(partsOfCommand[3]) && !ContainsSimilarToKey(ref variables, partsOfCommand[3]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[3] + " neexistuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else
                    {
                        variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                        return partsOfCommand[0] + " " + variables[partsOfCommand[1]] + " " + partsOfCommand[2] + " " + variables[partsOfCommand[3]];
                    }
                case 5:
                    if (variables.ContainsKey(partsOfCommand[1]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[1] + " už existuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else if (!variables.ContainsKey(partsOfCommand[4]) && !ContainsSimilarToKey(ref variables, partsOfCommand[4]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[4] + " neexistuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else
                    {
                        variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                        return partsOfCommand[0] + " " + variables[partsOfCommand[1]] + " " + partsOfCommand[2] + " " + partsOfCommand[3]+ " " + variables[partsOfCommand[4]];
                    }
                case 6:
                    if (variables.ContainsKey(partsOfCommand[1]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[1] + " už existuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else if (!variables.ContainsKey(partsOfCommand[3]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[3] + " neexistuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else if (!variables.ContainsKey(partsOfCommand[5]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[5] + " neexistuje a nemožno ho použiť v príkaze " + command + ".");
                        return null;
                    }
                    else
                    {
                        variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                        return partsOfCommand[0] + " " + variables[partsOfCommand[1]] + " " + partsOfCommand[2] + " " + variables[partsOfCommand[3]] + " " + partsOfCommand[4] + " " + variables[partsOfCommand[5]];
                    }
                default:
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                    return null;
            }
        }

        /// <summary>
        /// Changes command for creating a new line to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForLine(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sepspace = { ' ' };
            string[] partsOfCommand = command.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommand.Length != 2)
            {
                MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                return null;
            }
            else
            {
                char[] sepname = { '(', ')', ',' };
                string[] name = partsOfCommand[1].Split(sepname, StringSplitOptions.RemoveEmptyEntries);
                if (name.Length == 1)
                {
                    if (variables.ContainsKey(name[0]))
                    {
                        MessageBox.Show("Objekt s menom " + name[0] + " v príkaze " + command + " už existuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        variables.Add(name[0], "#" + nameOfMacro + "_" + name[0]);
                        return partsOfCommand[0] + " " + variables[name[0]];
                    }
                }
                else if (name.Length == 2)
                {
                    if (!variables.ContainsKey(name[0]) && !ContainsSimilarToKey(ref variables, name[0]))
                    {
                        MessageBox.Show("Objekt s menom " + name[0] + " v príkaze " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(name[1]) && !ContainsSimilarToKey(ref variables, name[1]))
                    {
                        MessageBox.Show("Objekt s menom " + name[1] + " v príkaze " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        variables.Add('(' + name[0] + ',' + name[1] + ')', '(' + variables[name[0]] + ',' + variables[name[1]] + ')');
                        return partsOfCommand[0] + " " + variables[partsOfCommand[1]];
                    }
                }
                else if (name.Length == 3)
                {
                    if (!variables.ContainsKey(name[1]) && !ContainsSimilarToKey(ref variables, name[1]))
                    {
                        MessageBox.Show("Objekt s menom " + name[1] + " v príkaze " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(name[2]) && !ContainsSimilarToKey(ref variables, name[2]))
                    {
                        MessageBox.Show("Objekt s menom " + name[2] + " v príkaze " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        variables.Add('(' + name[1] + ',' + name[2] + ')', '(' + variables[name[1]] + ',' + variables[name[2]] + ')');
                        variables.Add(name[0].Substring(name[0].Length-1), "#" + nameOfMacro + "_" + name[0].Substring(name[0].Length - 1));
                        return partsOfCommand[0] + " " + variables[name[0].Substring(name[0].Length - 1)]+"="+variables['(' + name[1] + ',' + name[2] + ')'];
                    }
                }
                else
                {
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                    return null;
                }
            }
        }

        /// <summary>
        /// Changes command for creating a new line segment to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForLineSegment(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sepspace = { ' ' };
            string[] partsOfCommand = command.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommand.Length == 2)
            {
                char[] seppoints = { '|', ',' };
                string[] points = partsOfCommand[1].Split(seppoints, StringSplitOptions.RemoveEmptyEntries);
                if (points.Length != 2)
                {
                    if (points.Length == 3)
                    {
                        if (!variables.ContainsKey(points[1]) && !ContainsSimilarToKey(ref variables, points[1]))
                        {
                            MessageBox.Show("Bod " + points[1] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                            return null;
                        }
                        else if (!variables.ContainsKey(points[2]) && !ContainsSimilarToKey(ref variables, points[2]))
                        {
                            MessageBox.Show("Bod " + points[2] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                            return null;
                        }
                        else
                        {
                            variables.Add('|' + points[1] + ',' + points[2] + '|', '|' + variables[points[1]] + ',' + variables[points[2]] + '|');
                            variables.Add(points[0].Substring(0,points[0].Length - 1), "#" + nameOfMacro + "_" + points[0].Substring(0,points[0].Length - 1));
                            return partsOfCommand[0] + " " + variables[points[0].Substring(0, points[0].Length - 1)] + "=" + variables['|' + points[1] + ',' + points[2] + '|'];
                            //usecka u=|A,B|
                        }
                    }
                    else
                    {
                        MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                        return null;
                    }
                }
                else
                {
                    if (!variables.ContainsKey(points[0]) && !ContainsSimilarToKey(ref variables, points[0]))
                    {
                        MessageBox.Show("Bod " + points[0] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(points[1]) && !ContainsSimilarToKey(ref variables, points[1]))
                    {
                        MessageBox.Show("Bod " + points[1] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        variables.Add(partsOfCommand[1], '|' + variables[points[0]] + ',' + variables[points[1]] + '|');
                        return partsOfCommand[0] + " " + variables[partsOfCommand[1]];
                        //usecka |A,B|
                    }
                }
            }
            else if (partsOfCommand.Length == 3)
            {
                char[] sepnames = { '|', ',' };
                string[] partsOfName = partsOfCommand[1].Split(sepnames, StringSplitOptions.RemoveEmptyEntries);
                if (partsOfName.Length == 1)
                {
                    if (variables.ContainsKey(partsOfName[0]))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfName[0] + " v príkaze " + command + " už existuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        char[] sepnamesize = { '=' };
                        string[] nameAndSize = partsOfCommand[2].Split(sepnamesize, StringSplitOptions.RemoveEmptyEntries);
                        if (nameAndSize.Length != 2)
                        {
                            MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                            return null;
                        }
                        else
                        {
                            int number;
                            if (Int32.TryParse(nameAndSize[1], out number))
                            {
                                // usecka u, u=100
                                variables.Add(partsOfName[0], "#" + nameOfMacro + "_" + partsOfName[0]);
                                return partsOfCommand[0] + " " + variables[partsOfName[0]] + ", " + variables[partsOfName[0]] + "=" + number;                                
                            }
                            else if (variables.ContainsKey(nameAndSize[1]))
                            {
                                // usecka u, u=|A,B|
                                variables.Add(partsOfName[0], "#" + nameOfMacro + "_" + partsOfName[0]);
                                return partsOfCommand[0] + " " + variables[partsOfName[0]] + ", " + variables[partsOfName[0]] + "=" + variables[nameAndSize[1]];                                
                            }
                            else
                            {
                                MessageBox.Show("Objekt " + nameAndSize[1] + " v príkaze " + command + " nie je existujúca úsečka ani číslo a nemôže byť použitý.");
                                return null;
                            }
                        }
                    }
                }
                else if (partsOfName.Length == 2)
                {
                    if (!variables.ContainsKey(partsOfName[0]) && !ContainsSimilarToKey(ref variables, partsOfName[0]))
                    {
                        MessageBox.Show("Bod " + partsOfName[0] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (variables.ContainsKey(partsOfName[1]))
                    {
                        MessageBox.Show("Objekt " + partsOfName[1] + " z príkazu " + command + " už existuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        char[] sepnamesize = { '=' };
                        string[] nameAndSize = partsOfCommand[2].Split(sepnamesize, StringSplitOptions.RemoveEmptyEntries);
                        if (nameAndSize.Length != 2)
                        {
                            MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                            return null;
                        }
                        else
                        {
                            int number;
                            if (Int32.TryParse(nameAndSize[1], out number))
                            {
                                // usecka |A,X|, |A,X|=100
                                variables.Add('|' + partsOfName[0]+','+partsOfName[1]+'|', '|' + variables[partsOfName[0]] + ',' + variables[partsOfName[1]] + '|');
                                return partsOfCommand[0] + " " + '|' + variables[partsOfName[0]] + ',' + variables[partsOfName[1]] + '|' + ", " + '|' + variables[partsOfName[0]] + ',' + variables[partsOfName[1]] + '|' + "=" + number;
                            }
                            else if (variables.ContainsKey(nameAndSize[1]) || ContainsSimilarToKey(ref variables, nameAndSize[1]))
                            {
                                // usecka |A,X|, |A,X|=|L,M|
                                variables.Add('|' + partsOfName[0] + ',' + partsOfName[1] + '|', '|' + variables[partsOfName[0]] + ',' + variables[partsOfName[1]] + '|');
                                return partsOfCommand[0] + " " + '|' + variables[partsOfName[0]] + ',' + variables[partsOfName[1]] + '|' + ", " + '|' + variables[partsOfName[0]] + ',' + variables[partsOfName[1]] + '|' + "=" + variables[nameAndSize[1]];
                            }
                            else
                            {
                                MessageBox.Show("Objekt " + nameAndSize[1] + " v príkaze " + command + " nie je existujúca úsečka ani číslo a nemôže byť použitý.");
                                return null;
                            }
                        }
                    }
                }
                else if (partsOfName.Length == 3)
                {
                    if (!variables.ContainsKey(partsOfName[1]) && !ContainsSimilarToKey(ref variables, partsOfName[1]))
                    {
                        MessageBox.Show("Bod " + partsOfName[1] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (variables.ContainsKey(partsOfName[2]))
                    {
                        MessageBox.Show("Objekt " + partsOfName[2] + " z príkazu " + command + " už existuje a nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        char[] sepnamesize = { '=' };
                        string[] nameAndSize = partsOfCommand[2].Split(sepnamesize, StringSplitOptions.RemoveEmptyEntries);
                        if (nameAndSize.Length != 2)
                        {
                            MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                            return null;
                        }
                        else
                        {
                            int number;
                            if (Int32.TryParse(nameAndSize[1], out number))
                            {
                                // usecka u=|A,X|, u=100
                                variables.Add(partsOfName[2], '#' + nameOfMacro + '_' + partsOfName[2]);
                                variables.Add('|' + partsOfName[1] + ',' + partsOfName[2] + '|', '|' + variables[partsOfName[1]] + ',' + variables[partsOfName[2]] + '|');
                                variables.Add(partsOfName[0].Substring(0, partsOfName[0].Length - 1), "#" + nameOfMacro + "_" + partsOfName[0].Substring(0, partsOfName[0].Length - 1));
                                return partsOfCommand[0] + " " + variables[partsOfName[0].Substring(0, partsOfName[0].Length - 1)] + "=" + variables['|' + partsOfName[1] + ',' + partsOfName[2] + '|'] +", " + variables[partsOfName[0].Substring(0, partsOfName[0].Length - 1)] + '=' + number;
                            }
                            else if (variables.ContainsKey(nameAndSize[1]) || ContainsSimilarToKey(ref variables, nameAndSize[1]))
                            {
                                // usecka u=|A,X|, u=|L,M|
                                variables.Add(partsOfName[2], '#' + nameOfMacro + '_' + partsOfName[2]);
                                variables.Add('|' + partsOfName[1] + ',' + partsOfName[2] + '|', '|' + variables[partsOfName[1]] + ',' + variables[partsOfName[2]] + '|');
                                variables.Add(partsOfName[0].Substring(0, partsOfName[0].Length - 1), "#" + nameOfMacro + "_" + partsOfName[0].Substring(0, partsOfName[0].Length - 1));
                                return partsOfCommand[0] + " " + variables[partsOfName[0].Substring(0, partsOfName[0].Length - 1)] + "=" + '|' + variables[partsOfName[1]] + ',' + variables[partsOfName[2]] + '|' + ", " + variables[partsOfName[0].Substring(0, partsOfName[0].Length - 1)] + "=" + variables[nameAndSize[1]];
                            }
                            else
                            {
                                MessageBox.Show("Objekt " + nameAndSize[1] + " v príkaze " + command + " nie je existujúca úsečka ani číslo a nemôže byť použitý.");
                                return null;
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                    return null;
                }
            }
            else
            {
                MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                return null;
            }
        }

        /// <summary>
        /// Changes command for creating a new half line segment to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param> 
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForHalfLineSegment(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sep = { ' ', '|', ')', ',' };
            string[] partsOfCommand = command.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommand.Length != 3)
            {
                if (partsOfCommand.Length == 4)
                {
                    if (!variables.ContainsKey(partsOfCommand[2]) && !ContainsSimilarToKey(ref variables, partsOfCommand[2]))
                    {
                        MessageBox.Show("Bod " + partsOfCommand[2] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(partsOfCommand[3]) && !ContainsSimilarToKey(ref variables, partsOfCommand[3]))
                    {
                        MessageBox.Show("Bod " + partsOfCommand[3] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                        return null;
                    }
                    else if (variables.ContainsKey('|' + partsOfCommand[2] + ',' + partsOfCommand[3] + ')'))
                    {
                        MessageBox.Show("Objekt s menom " + '|' + partsOfCommand[2] + ',' + partsOfCommand[3] + ')' + " z príkazu" + command + " už existuje a nie je možné vytvoriť nový s rovnakým menom.");
                        return null;
                    }
                    else if (variables.ContainsKey(partsOfCommand[1].Substring(0, partsOfCommand[1].Length-1)))
                    {
                        MessageBox.Show("Objekt s menom " + partsOfCommand[1].Substring(0, partsOfCommand[1].Length - 1) + " z príkazu" + command + " už existuje a nie je možné vytvoriť nový s rovnakým menom.");
                        return null;
                    }
                    else
                    {
                        variables.Add('|' + partsOfCommand[2] + ',' + partsOfCommand[3] + ')', '|' + variables[partsOfCommand[2]] + ',' + variables[partsOfCommand[3]] + ')');
                        variables.Add(partsOfCommand[1].Substring(0, partsOfCommand[1].Length - 1), '#' +  nameOfMacro + '_' + partsOfCommand[1].Substring(0, partsOfCommand[1].Length - 1));
                        return partsOfCommand[0] + " " + variables[partsOfCommand[1].Substring(0, partsOfCommand[1].Length - 1)] + "=" + '|' + variables[partsOfCommand[2]] + ',' + variables[partsOfCommand[3]] + ')';
                    }
                }
                else
                {
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemožno ho použiť.");
                    return null;
                }
            }
            else
            {
                if (!variables.ContainsKey(partsOfCommand[1]) && !ContainsSimilarToKey(ref variables, partsOfCommand[1]))
                {
                    MessageBox.Show("Bod " + partsOfCommand[1] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                    return null;
                }
                else if (!variables.ContainsKey(partsOfCommand[2]) && !ContainsSimilarToKey(ref variables, partsOfCommand[2]))
                {
                    MessageBox.Show("Bod " + partsOfCommand[2] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                    return null;
                }
                else if (variables.ContainsKey('|'+partsOfCommand[1]+','+partsOfCommand[2]+')'))
                {
                    MessageBox.Show("Objekt s menom " + '|' + partsOfCommand[1] + ',' + partsOfCommand[2] + ')' + " z príkazu" + command + " už existuje a nie je možné vytvoriť nový s rovnakým menom.");
                    return null;
                }
                else
                {
                    variables.Add('|' + partsOfCommand[1] + ',' + partsOfCommand[2] + ')', '|' + variables[partsOfCommand[1]] + ',' + variables[partsOfCommand[2]] + ')');
                    return partsOfCommand[0] + " " + '|' + variables[partsOfCommand[1]] + ',' + variables[partsOfCommand[2]] + ')';
                }
            }
        }

        /// <summary>
        /// Changes command for creating a new circle to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForCircle(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sepspace = { ' ' };
            string[] partsOfCommand = command.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommand.Length != 2)
            {
                MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                return null;
            }
            else
            {
                char[] sepname = { '(', ')', ',' };
                string[] names = partsOfCommand[1].Split(sepname, StringSplitOptions.RemoveEmptyEntries);
                if (names.Length == 3)
                {
                    int number;
                    if (variables.ContainsKey(names[0]))
                    {
                        MessageBox.Show("Objekt s menom " + names[0] + " z príkazu " + command + " už existuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(names[1]) && !ContainsSimilarToKey(ref variables, names[1]))
                    {
                        MessageBox.Show("Bod " + names[1] + " v príkaze " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (Int32.TryParse(names[2],out number))
                    {
                        variables.Add(names[0], "#" + nameOfMacro + "_" + names[0]);
                        return partsOfCommand[0] + " " + variables[names[0]] + "(" + variables[names[1]] + "," + names[2] + ")";
                    }
                    else if (variables.ContainsKey(names[2]) || ContainsSimilarToKey(ref variables, names[2]))
                    {
                        variables.Add(names[0], "#" + nameOfMacro + "_" + names[0]);
                        return partsOfCommand[0] + " " + variables[names[0]] + "(" + variables[names[1]] + "," + variables[names[2]] + ")";
                    }
                    else
                    {
                        MessageBox.Show("Objekt "+names[2] +" v príkaze " + command + " nie je existujúca úsečka ani číslo a nemôže byť použitý.");
                        return null;
                    }
                }
                else if (names.Length == 4)
                {
                    string lastNameFromNames = names[2] + "," + names[3];
                    if (variables.ContainsKey(names[0]))
                    {
                        MessageBox.Show("Objekt s menom " + names[0] + " z príkazu " + command + " už existuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(names[1]) && !ContainsSimilarToKey(ref variables, names[1]))
                    {
                        MessageBox.Show("Bod " + names[1] + " v príkaze " + command + " neexistuje a nemôže byť použitý.");
                        return null;
                    }
                    else if (!variables.ContainsKey(lastNameFromNames) && !ContainsSimilarToKey(ref variables, lastNameFromNames))
                    {
                        MessageBox.Show("Úsečka " + lastNameFromNames + " v príkaze " + command + " neexistuje a nemôže byť použitá.");
                        return null;
                    }
                    else
                    {
                        variables.Add(names[0], "#" + nameOfMacro + "_" + names[0]);
                        return partsOfCommand[0] + " " + variables[names[0]] + "(" + variables[names[1]] + "," + variables[lastNameFromNames] + ")";
                    }
                }
                else
                {
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemôže byť použitý.");
                    return null;
                }
            }               
        }

        /// <summary>
        /// Changes command for creating a new part of circle to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForPartCircle(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sep = { ' ', '(', ')', ',' };
            string[] partsOfCommand = command.Split(sep, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommand.Length != 5)
            {
                MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemožno ho použiť.");
                return null;
            }
            else
            {
                if (!variables.ContainsKey(partsOfCommand[2]) && !ContainsSimilarToKey(ref variables, partsOfCommand[2]))
                {
                    MessageBox.Show("Bod " + partsOfCommand[2] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                    return null;
                }
                else if (!variables.ContainsKey(partsOfCommand[3]) && !ContainsSimilarToKey(ref variables, partsOfCommand[3]))
                {
                    MessageBox.Show("Bod " + partsOfCommand[3] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                    return null;
                }
                else if (!variables.ContainsKey(partsOfCommand[4]) && !ContainsSimilarToKey(ref variables, partsOfCommand[4]))
                {
                    MessageBox.Show("Bod " + partsOfCommand[4] + " požívaný v príkaze " + command + " ešte nebol vytvorený a nemôže byť použitý.");
                    return null;
                }
                else if (variables.ContainsKey(partsOfCommand[1]))
                {
                    MessageBox.Show("Objekt s menom " + partsOfCommand[1] + " z príkazu" + command + " už existuje a nie je možné vytvoriť nový s rovnakým menom.");
                    return null;
                }
                else
                {
                    variables.Add(partsOfCommand[1], "#" + nameOfMacro + "_" + partsOfCommand[1]);
                    return partsOfCommand[0] + " " + variables[partsOfCommand[1]] + "(" + partsOfCommand[2] + "," + partsOfCommand[3] + "," + partsOfCommand[4] + ")";
                }
            }
        }

        /// <summary>
        /// Changes command for creating a new angle to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="nameOfMacro">Name of macro for saving of the name of object.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForAngle(string command, ref Dictionary<string, string> variables, string nameOfMacro)
        {
            char[] sepspace = { ' ' };
            string[] partsOfCommands = command.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
            if (partsOfCommands.Length == 2)
            {
                char[] sepcomma = { ',' };
                string[] points = partsOfCommands[1].Split(sepcomma, StringSplitOptions.RemoveEmptyEntries);
                if (points.Length != 3)
                {
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemožno ho použiť.");
                    return null;
                }
                else if (!variables.ContainsKey(points[0]) && !ContainsSimilarToKey(ref variables, points[0]))
                {
                    MessageBox.Show("Bod " + points[0] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                    return null;
                }
                else if (!variables.ContainsKey(points[1]) && !ContainsSimilarToKey(ref variables, points[1]))
                {
                    MessageBox.Show("Bod " + points[1] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                    return null;
                }
                else if (!variables.ContainsKey(points[2]) && !ContainsSimilarToKey(ref variables, points[2]))
                {
                    MessageBox.Show("Bod " + points[2] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                    return null;
                }
                else
                {
                    variables.Add('|'+partsOfCommands[1]+'|', '|' + variables[points[0]] + ',' + variables[points[1]] + ',' + variables[points[2]] + '|');
                    return partsOfCommands[0] + " " + variables[points[0]] + ',' + variables[points[1]] + ',' + variables[points[2]];
                }	
            }
            else if (partsOfCommands.Length == 3)
            {
                char[] sepcomma = { ',' };
                string[] points = partsOfCommands[1].Split(sepcomma, StringSplitOptions.RemoveEmptyEntries);
                if (points.Length != 3)
                {
                    MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemožno ho použiť.");
                    return null;
                }
                else if (!variables.ContainsKey(points[0]) && !ContainsSimilarToKey(ref variables, points[0]))
                {
                    MessageBox.Show("Bod " + points[0] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                    return null;
                }
                else if (!variables.ContainsKey(points[1]) && !ContainsSimilarToKey(ref variables, points[1]))
                {
                    MessageBox.Show("Bod " + points[1] + " z príkazu " + command + " neexistuje a nemôže byť použitý.");
                    return null;
                }
                else if (variables.ContainsKey(points[2]) && !ContainsSimilarToKey(ref variables, points[2]))
                {
                    MessageBox.Show("Objekt " + points[2] + " z príkazu " + command + " už existuje a nemôže byť použitý.");
                    return null;
                }
                else
                {
                    char[] sepequals = { '=' };
                    string[] nameAndSize = partsOfCommands[2].Split(sepequals, StringSplitOptions.RemoveEmptyEntries);

                    int number;
                    if (nameAndSize.Length != 2)
                    {
                        MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemožno ho použiť.");
                        return null;
                    }
                    else if (Int32.TryParse(nameAndSize[1],out number))
                    {
                        // uhol A,V,X, |A,V,X|=90
                        variables.Add(points[2], "#" + nameOfMacro + "_" + points[2]);
                        variables.Add(nameAndSize[0], '|' + variables[points[0]] + ',' + variables[points[1]] + ',' + variables[points[2]] + '|');
                        return partsOfCommands[0] + " " + variables[points[0]] + ',' + variables[points[1]] + ',' + variables[points[2]] + ", " + variables[nameAndSize[0]] + '=' + nameAndSize[1];
                    }
                    else if (variables.ContainsKey(nameAndSize[1]) || ContainsSimilarToKey(ref variables, nameAndSize[1]))
                    {
                        // uhol A,V,X, |A,V,X|=|L,M,N|
                        variables.Add(points[2], "#" + nameOfMacro + "_" + points[2]);
                        variables.Add(nameAndSize[0], '|' + variables[points[0]] + ',' + variables[points[1]] + ',' + variables[points[2]] + '|');
                        return partsOfCommands[0] + " " + variables[points[0]] + ',' + variables[points[1]] + ',' + variables[points[2]] + ", " + variables[nameAndSize[0]] + '=' + variables[nameAndSize[1]];
                    }
                    else
                    {
                        MessageBox.Show("Objekt " + nameAndSize[1] + " v príkaze " + command + " nie je existujúci uhol ani číslo a nemôže byť použitý.");
                        return null;
                    }
                }
            }
            else
            {
                MessageBox.Show("Príkaz " + command + " nie je v správnom tvare a nemožno ho použiť.");
                return null;
            }           
        }

        /// <summary>
        /// Changes command for calling the macro to be suitable for saving as macro.
        /// </summary>
        /// <param name="command">Old command.</param>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <returns>New command.</returns>
        static string ChangeCommandForMacro(string command, ref Dictionary<string, string> variables)
        {
            char[] sepnameparam = { '(', ')', ';' };
            string[] nameInputsOutputs = command.Split(sepnameparam, StringSplitOptions.RemoveEmptyEntries);
            if (nameInputsOutputs.Length != 3)
            {
                MessageBox.Show("Volanie makra v príkaze " + command + " nie je v správnom tvare a nemôže byť použité.");
                return null;
            }
            else
            {
                char[] sepparam = { ',' };
                string[] inputs = nameInputsOutputs[1].Split(sepparam, StringSplitOptions.RemoveEmptyEntries);
                string[] outputs = nameInputsOutputs[2].Split(sepparam, StringSplitOptions.RemoveEmptyEntries);

                char[] sepspace = { ' ' };
                string[] inp;
                string[] outp;

                string newinputs = "";
                string newoutputs = "";

                foreach (string input in inputs)
                {
                    inp = input.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                    if (inp.Length != 2)
                    {
                        MessageBox.Show("Volanie makra v príkaze " + command + " nie je v správnom tvare a nemôže byť použité.");
                        return null;
                    }
                    else if (!variables.ContainsKey(inp[1]) && !ContainsSimilarToKey(ref variables, inp[1]))
                    {
                        MessageBox.Show("V príkaze " + command + " neexistuje " + input + " a príkaz nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        newinputs += ',' + inp[0] + " " + variables[inp[1]];
                    }
                }

                foreach (string output in outputs)
                {
                    outp = output.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                    if (outp.Length != 2)
                    {
                        MessageBox.Show("Volanie makra v príkaze " + command + " nie je v správnom tvare a nemôže byť použité.");
                        return null;
                    }
                    else if (variables.ContainsKey(outp[1]))
                    {
                        MessageBox.Show("V príkaze " + command + " už objekt " + outp[1] + " existuje a príkaz nemôže byť použitý.");
                        return null;
                    }
                    else
                    {
                        newoutputs += ',' + outp[0] + " " + variables[outp[1]];
                    }
                }

                if (newinputs.Length < 2 && newoutputs.Length < 2)
                    return nameInputsOutputs[0] + "(;)";
                else if (newinputs.Length < 2)
                    return nameInputsOutputs[0] + "(;" + newoutputs.Substring(1) + ")";
                else if (newoutputs.Length < 2)
                    return nameInputsOutputs[0] + "(" + newinputs.Substring(1) + ";)";
                else
                    return nameInputsOutputs[0] + "(" + newinputs.Substring(1) + ";" + newoutputs.Substring(1) + ")";
            }
        }

        /// <summary>
        /// Finds if there is key similar to string (if there could be copy of object, for example point C, C1 and C2).
        /// </summary>
        /// <param name="variables">List with old and new name of all variables.</param>
        /// <param name="key">Key which is looking for.</param>
        /// <returns>True, if in variables is simiar key, otherwise false.</returns>
        static bool ContainsSimilarToKey(ref Dictionary<string,string> variables, string key)
        {
            var dictionary = new Dictionary<string, string>();
            foreach (var item in variables)
            {
                dictionary.Add(item.Key,item.Value);
            }

            foreach (var item in dictionary)
            {
                if (key.Length > item.Key.Length && key.Substring(0,item.Key.Length) == item.Key)
                {
                    variables.Add(key, item.Value + key.Substring(item.Key.Length));
                    return true;
                }
            }

            return false;

        }

        /// <summary>
        /// Finds out if type of object exists.
        /// </summary>
        /// <param name="name">Type of object.</param>
        /// <returns>True if type of object exists, otherwise false.</returns>
        static bool IsTypeOfObject(string name)
        {
            return (name == "bod" || name == "priamka" || name == "usecka" || name == "polpriamka" || name == "kruznica" || name == "obluk" || name == "uhol");
        }

        /// <summary>
        /// Finds macro, if exists, and starts to work with it, otherwise write a message.
        /// </summary>
        /// <param name="input">Name of macro (file), names of inputs and outputs.</param>
        public static void FindMacro(string input)
        {
            //Splits to name of macro and its input and output parametres
            int firstLeftBracket = input.IndexOf('(');
            string[] nameAndParameters = new string[2];
            nameAndParameters[0] = input.Substring(0, firstLeftBracket);
            nameAndParameters[1] = input.Substring(firstLeftBracket + 1, input.Length - nameAndParameters[0].Length - 2);

            // Macros (files) are saved in folder with name saved is pathOfFile.
            string[] files = Directory.GetFiles(pathOfFile);

            // Used for finding out if there is such macro.
            bool ok = false;

            // Searchs for file.
            foreach (string f in files)
            {
                // If it is founded, checks it and reads from it.
                if (nameAndParameters[0] == f.Substring(pathOfFile.Length))
                {
                    ok = true;
                    WorkWithMacro(nameAndParameters);
                    System.Drawing.Graphics graphics = visualizer.GetPictureBox().CreateGraphics();
                    visualizer.GetPictureBox().Enabled = false;
                    visualizer.GetPictureBox().Enabled = true;
                    foreach (GeometricObject o in Reader.allObjects)
                        Drawing.DrawObject(o);
                }
            }
            // If there was not found macro, shows a message.
            if (!ok)
            {
                MessageBox.Show("Makro s názvom " + input + " neexistuje.");
            }
        }

        /// <summary>
        /// Checks input and output parametres, if it is ok, reads macro line by line and works with its commands.
        /// </summary>
        /// <param name="nameAndParameters">Name and input/output parametres.</param>
        public static void WorkWithMacro(string[] nameAndParameters)
        {
            // Macro is in the form macro(type in1,type in2,type in3;type out1,type out2,type out3); name-macro, parameters-in1,in2,in3;out1,out2,out3
            // All objects in input/output parameters in calling of macro.
            string[] inputs = null;
            string[] outputs = null;

            // All lines in macro.
            string[] linesinMacro = null;

            // Part of macro which program is in.
            PartsOfMacro partNow;

            #region Checking count of parameters.
            // Checks number of input and output parameters. If it is wrong, writes a message.
            if (nameAndParameters.Length != 2)
            {
                MessageBox.Show("Makro " + nameAndParameters[0] + " nie je v správnom tvare.");
            }
            else
            {
                char[] sepparameters = { '-' };
                string[] parameters = nameAndParameters[1].Split(sepparameters);
                if (parameters.Length != 2)
                {
                    MessageBox.Show("Makro " + nameAndParameters[0] + "nemá správny tvar vstupných a výstupných parametrov.");
                }
                else
                {
                    char[] sepinputsoutputs = { ';' };
                    inputs = parameters[0].Split(sepinputsoutputs);
                    outputs = parameters[1].Split(sepinputsoutputs);

                    linesinMacro = File.ReadAllLines(pathOfFile+nameAndParameters[0]);
                    int countOfInputs = 0;
                    int countOfOutputs = 0;
                    partNow = PartsOfMacro.Inputs;

                    foreach (string line in linesinMacro)
                    {
                        if (string.IsNullOrWhiteSpace(line))
                        {
                            partNow++;
                        }
                        else if (partNow == PartsOfMacro.Inputs)
                        {
                            countOfInputs++;
                        }
                        else if (partNow == PartsOfMacro.Outputs)
                        {
                            countOfOutputs++;
                        }
                    }
                    
                    if ((inputs.Length != countOfInputs) && !(countOfInputs == 0 && inputs.Length == 1 && inputs[0] == ""))
                    {
                        MessageBox.Show("Makro " + nameAndParameters[0] + " nemá správny počet vstupných parametrov.");
                    }
                    if (outputs.Length != countOfOutputs && !(countOfOutputs == 0 && outputs.Length == 1 && outputs[0] == ""))
                    {
                        MessageBox.Show("Makro " + nameAndParameters[0] + " nemá správny počet výstupných parametrov.");
                    }
                }                
            }
            #endregion

            #region Checking parameters.
            // Checks type, order and (non)existence of inputs and outputs.
            partNow = PartsOfMacro.Inputs;
            int counterInputs = 0;
            int counterOutputs = 0;
            string[] input;
            string[] output;

            string[] stringOnLine;

            char[] sepspace = { ' ' };
            bool error = false;
            foreach (string line in linesinMacro)
            {
                if (error)
                    break;
                if (line == "")
                {
                    partNow++;
                }
                #region Checking input parameters.
                else if (partNow == PartsOfMacro.Inputs)
                {
                    input = inputs[counterInputs].Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                    if (input.Length != 2)
                    {
                        MessageBox.Show("Vstupný parameter " + inputs[counterInputs] + " je v nesprávnom tvare.");
                        error = true;
                    }
                    else
                    {
                        if (Reader.FoundObject(input[1]) == null)
                        {
                            MessageBox.Show("Objekt s menom " + input[1] + " neexistuje.");
                            error = true;
                        }
                        else
                        {
                            stringOnLine = line.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                            switch (input[0])
                            {
                                case "bod":
                                    if (!(Reader.FoundObject(input[1])[0] is Point))
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je bod.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "bod")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "priamka":
                                    if (!(Reader.FoundObject(input[1])[0] is Line))
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je priamka.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "priamka")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "usecka":
                                    if (!(Reader.FoundObject(input[1])[0] is LineSegment) || ((LineSegment)Reader.FoundObject(input[1])[0]).half)
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je úsečka.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "usecka")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "polpriamka":
                                    if (!(Reader.FoundObject(input[1])[0] is LineSegment) || !((LineSegment)Reader.FoundObject(input[1])[0]).half)
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je polpriamka.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "polrpiamka")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "kruznica":
                                    if (!(Reader.FoundObject(input[1])[0] is Circle) || ((Circle)Reader.FoundObject(input[1])[0]).end1 != null)
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je kružnica.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "kruznica")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "obluk":
                                    if (!(Reader.FoundObject(input[1])[0] is Circle) || ((Circle)Reader.FoundObject(input[1])[0]).end1 == null)
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je kružnicový oblúk.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "obluk")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "uhol":
                                    if (!(Reader.FoundObject(input[1])[0] is Angle))
                                    {
                                        MessageBox.Show("Objekt s menom " + input[1] + " nie je uhol.");
                                        error = true;
                                    }
                                    else if (stringOnLine[0] != "uhol")
                                    {
                                        MessageBox.Show(inputs[counterInputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi vstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                            }
                        }
                    }
                    counterInputs++;
                }
                #endregion
                #region Checking output parameters.
                else if (partNow == PartsOfMacro.Outputs)
                {
                    output = outputs[counterOutputs].Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                    if (output.Length != 2)
                    {
                        MessageBox.Show("Vstupný parameter " + outputs[counterOutputs] + " je v nesprávnom tvare.");
                        error = true;
                    }
                    else
                    {
                        if (Reader.FoundObject(output[1]) != null)
                        {
                            MessageBox.Show("Objekt s menom " + output[1] + " už existuje.");
                            error = true;
                        }
                        else
                        {
                            stringOnLine = line.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                            switch (output[0])
                            {
                                case "bod":
                                    if (stringOnLine[0] != "bod")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "priamka":
                                    if (stringOnLine[0] != "priamka")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "usecka":
                                    if (stringOnLine[0] != "usecka")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "polpriamka":
                                    if (stringOnLine[0] != "polpriamka")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "kruznica":
                                    if (stringOnLine[0] != "kruznica")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "obluk":
                                    if (stringOnLine[0] != "obluk")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                                case "uhol":
                                    if (stringOnLine[0] != "uhol")
                                    {
                                        MessageBox.Show(outputs[counterOutputs] + " sa typovo nezhoduje s objektom v makre " + nameAndParameters[0] + ", ktorý je na rovnakom mieste medzi výstupnými parametrami.");
                                        error = true;
                                    }
                                    break;
                            }
                        }
                    }
                    counterOutputs++;                        
                }
                #endregion
            }
            #endregion

            #region Working with actuall macro.
            // Creates copies of input parameters with the names from macro and saves them to list of all objects.
            string[] namesOfTemporaryInputs = new string[counterInputs];

            counterInputs = 0;
            counterOutputs = 0;

            Point tempp;
            Line templ;
            LineSegment templs;
            Circle tempc;
            Angle tempa;
            GeometricObject temp;

            partNow = PartsOfMacro.Inputs;

            foreach (string line in linesinMacro)
            {
                if (error)
                    break;
                if (string.IsNullOrWhiteSpace(line))
                {
                    partNow++;
                }
                #region Inputs.
                // Creates copies of input parameters with the names from macro and saves them to list of all objects.
                else if (partNow == PartsOfMacro.Inputs)
                {
                    stringOnLine = line.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                    input = inputs[counterInputs].Split(sepspace, StringSplitOptions.RemoveEmptyEntries);

                    namesOfTemporaryInputs[counterInputs] = stringOnLine[1];
                    switch (stringOnLine[0])
                    {
                        case "bod":
                            tempp = (Point)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new Point(stringOnLine[1], tempp.x, tempp.y));
                            break;
                        case "priamka":
                            templ = (Line)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new Line(stringOnLine[1], templ.point1, templ.point2));
                            break;
                        case "usecka":
                            templs = (LineSegment)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new LineSegment(stringOnLine[1], templs.point1, templs.point2, templs.half));
                            break;
                        case "polpriamka":
                            templs = (LineSegment)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new LineSegment(stringOnLine[1], templs.point1, templs.point2, templs.half));
                            break;
                        case "kruznica":
                            tempc = (Circle)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new Circle(stringOnLine[1], tempc.center, tempc.radius));
                            break;
                        case "obluk":
                            tempc = (Circle)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new Circle(stringOnLine[1], tempc.center, tempc.end1, tempc.end2));
                            break;
                        case "uhol":
                            tempa = (Angle)Reader.FoundObject(input[1])[0];
                            Reader.allObjects.Add(new Angle(stringOnLine[1], tempa.point1, tempa.vertex, tempa.point2));
                            break;
                    }
                    counterInputs++;
                }
                #endregion
                #region Commands.
                // Starts to do a commands in macro.
                else if (partNow == PartsOfMacro.Commands)
                {
                    Reader.ReadLine(line);
                }
                #endregion
                #region Outputs.
                // Rename all output parameters and draws them.
                else if (partNow == PartsOfMacro.Outputs)
                {
                    stringOnLine = line.Split(sepspace, StringSplitOptions.RemoveEmptyEntries);
                    output = outputs[counterOutputs].Split(sepspace, StringSplitOptions.RemoveEmptyEntries);

                    var tempo = Reader.FoundObject(stringOnLine[1]);
                    if (tempo == null)
                        tempo = new List<GeometricObject>();
                    foreach (GeometricObject o in tempo)
                    {
                        if (o is Point)
                        {
                            tempp = (Point)o;                            
                            if (tempp.secondName != "")
                            {
                                string num = tempp.secondName.Substring(tempp.firstName.Length);
                                tempp.firstName = output[1];
                                tempp.secondName = output[1]+num;                               
                            }
                            else
                            {
                                tempp.firstName = output[1];
                            }
                            Drawing.DrawPoint(tempp);
                        }
                        else if (o is Line)
                        {
                            templ = (Line)o;
                            if (templ.secondName != "" && templ.secondName.Contains(','))
                            {
                                string num = "";
                                if (templ.point1.secondName != "")
                                    num += templ.point1.secondName.Substring(templ.point1.firstName.Length);
                                if (templ.point2.secondName != "")
                                    num += templ.point2.secondName.Substring(templ.point2.firstName.Length);
                                templ.firstName = output[1];
                                templ.secondName = output[1] + num;
                            }
                            else if (templ.secondName != "")
                            {
                                string num = templ.secondName.Substring(templ.firstName.Length);
                                templ.firstName = output[1];
                                templ.secondName = output[1] + num;
                            }
                            else
                            {
                                templ.firstName = output[1];
                            }
                            Drawing.DrawLine(templ);
                        }
                        else if (o is LineSegment)
                        {
                            templs = (LineSegment)o;
                            if (templs.secondName != "" && templs.secondName.Contains(','))
                            {
                                string num = "";
                                if (templs.point1.secondName != "")
                                    num += templs.point1.secondName.Substring(templs.point1.firstName.Length);
                                if (templs.point2.secondName != "")
                                    num += templs.point2.secondName.Substring(templs.point2.firstName.Length);
                                templs.firstName = output[1];
                                templs.secondName = output[1] + num;
                            }
                            else if (templs.secondName != "")
                            {
                                string num = templs.secondName.Substring(templs.firstName.Length);
                                templs.firstName = output[1];
                                templs.secondName = output[1] + num;
                            }
                            else
                            {
                                templs.firstName = output[1];
                            }
                            Drawing.DrawLineSegment(templs);
                        }
                        else if (o is Circle)
                        {
                            tempc = (Circle)o;
                            if (tempc.secondName != "")
                            {
                                string num = tempc.secondName.Substring(tempc.firstName.Length);
                                tempc.firstName = output[1];
                                tempc.secondName = output[1] + num;
                            }
                            else
                            {
                                tempc.firstName = output[1];
                            }
                            Drawing.DrawCircle(tempc);
                        }
                        else if (o is Angle)
                        {
                            tempa = (Angle)o;
                            if (tempa.secondName != "" && tempa.secondName.Contains(','))
                            {
                                string num = "";
                                if (tempa.point1.secondName != "")
                                    num += tempa.point1.secondName.Substring(tempa.point1.firstName.Length);
                                if (tempa.vertex.secondName != "")
                                    num += tempa.vertex.secondName.Substring(tempa.vertex.firstName.Length);
                                if (tempa.point2.secondName != "")
                                    num += tempa.point2.secondName.Substring(tempa.point2.firstName.Length);
                                tempa.firstName = output[1];
                                tempa.secondName = output[1] + num;
                            }
                            else if (tempa.secondName != "")
                            {
                                string num = tempa.secondName.Substring(tempa.firstName.Length);
                                tempa.firstName = output[1];
                                tempa.secondName = output[1] + num;
                            }
                            else
                            {
                                tempa.firstName = output[1];
                            }
                            Drawing.DrawAngle(tempa);
                        }
                    }
                    counterOutputs++;
                }
                #endregion
            }

            // Deletes all temporary input parameters from the list of all objects.
            foreach (string name in namesOfTemporaryInputs)
            {
                if (error)
                    break;
                var tempall = Reader.FoundObject(name);
                foreach (GeometricObject o in tempall)
                {
                    Reader.allObjects.Remove(o);
                }
            }
            #endregion
        }        
    }
}
