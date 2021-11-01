using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace SSO_TEST.Utility
{
    public static class XmlHandler 
    {
        public static string GetNodeValue(string path, string nodeName)
        {
            XmlTextReader xmlTextReader = new XmlTextReader(path);

            while (xmlTextReader.Read())
            {
                if(xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == nodeName)
                {
                    string res = xmlTextReader.ReadElementContentAsString();
                    return res;
                }
            }
            return "";

        }

        public static string GetAttributeValue(string path, string nodeName, string attribute)
        {
            XmlTextReader xmlTextReader = new XmlTextReader(path);

            while (xmlTextReader.Read())
            {
                if (xmlTextReader.NodeType == XmlNodeType.Element && xmlTextReader.Name == nodeName)
                {
                    string res = xmlTextReader.GetAttribute(attribute);
                    return res;
                }
            }
            return "";

        }

        //public void GetSamlData(string path)
        //{
        //    List<string> nodeTypes = new List<string>
        //    {
        //       "X509Data",
        //       "SingleSignOnService",
        //    };

        //}
    }
}
