using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PropertyXMLBuilder
{
    class Program
    {
        static void Main(string[] args)
        {
            string configFileFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\BioWare\Mass Effect\Config";
            if (Directory.Exists(configFileFolder))
            {
                string biofile = "BioEngine.ini";

                var lines = File.ReadAllLines(configFileFolder + "\\" + biofile);
                XmlDocument doc = new XmlDocument();
                XmlElement currentSection = null;
                var root = doc.CreateElement("properties");
                foreach (string line in lines)
                {
                    if (line.StartsWith("[") && line.EndsWith("]"))
                    {
                        string sectionName = line.Substring(1, line.Length - 2);
                        if (sectionName == "IniVersion")
                        {
                            break;
                        }
                        currentSection = doc.CreateElement("Section");
                        XmlAttribute attr = doc.CreateAttribute("name");
                        attr.Value = sectionName;
                        SetAttrSafe(currentSection, attr);
                        root.AppendChild(currentSection);
                        continue;
                    }
                    int indexOfEquals = line.IndexOf("=");
                    string propName = "";
                    string propValue = "";
                    if (indexOfEquals > 1)
                    {
                        propName = line.Substring(0, indexOfEquals);
                        propValue = line.Substring(indexOfEquals + 1, line.Length - indexOfEquals - 1);

                        string elementname = "";

                        int intval;
                        float doubleval;
                        if (propValue.ToLower() == "true" || propValue.ToLower() == "false")
                        {
                            //bool prop
                            elementname = "boolproperty";
                        }
                        else if (int.TryParse(propValue, out intval))
                        {
                            elementname = "intproperty";
                        }
                        else if (float.TryParse(propValue, out doubleval))
                        {
                            elementname = "floatproperty";
                        }
                        else
                        {
                            elementname = "nameproperty";
                        }

                        XmlElement elem = doc.CreateElement(elementname);

                        elem.InnerText = propValue;
                        XmlAttribute attr = doc.CreateAttribute("propertyname");
                        attr.Value = propName;
                        SetAttrSafe(elem, attr);

                        attr = doc.CreateAttribute("friendlyname");
                        attr.Value = propName;
                        SetAttrSafe(elem, attr);

                        attr = doc.CreateAttribute("notes");
                        attr.Value = "Unknown at this time";
                        SetAttrSafe(elem, attr);



                        currentSection.AppendChild(elem);
                    }
                }
                doc.AppendChild(root);
                doc.Save(Path.GetFileNameWithoutExtension(biofile)+"Mapping.xml");

                //string bioEngineFile = "BIOGame.ini";
                //string biogameFile = "BIOGame.ini";
                //string biogameFile = "BIOGame.ini";

            }
        }

        static void SetAttrSafe(XmlNode node, params XmlAttribute[] attrList)
        {
            foreach (var attr in attrList)
            {
                if (node.Attributes[attr.Name] != null)
                {
                    node.Attributes[attr.Name].Value = attr.Value;
                }
                else
                {
                    node.Attributes.Append(attr);
                }
            }
        }
    }
}
