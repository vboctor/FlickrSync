using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;

namespace FlickrSync
{
    public class PathInfo
    {
        public string Path;
        public bool Open;
        public bool ManualAdd;

        public PathInfo()
        {
            Path = "";
            Open = false;
            ManualAdd = false;
        }

        public PathInfo(string pPath)
        {
            Path = pPath;
            Open = false;
            ManualAdd = false;
        }

        public PathInfo(string pPath, bool pOpen, bool pManualAdd)
        {
            Path = pPath;
            Open = pOpen;
            ManualAdd = pManualAdd;
        }

        public override bool Equals(object pi)
        {
            return Path.Equals(((PathInfo)pi).Path, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        private string XmlEncode(string text)
        {
            //TODO: Improve encoding
            return text.Replace("&", "&amp;").Replace("<", "&lt;");
        }

        private string XmlDecode(string text)
        {
            //TODO: Improve encoding
            return text.Replace("&lt;", "<").Replace("&amp;", "&");
        }

        public string GetXml()
        {
            string xml="  <PathInfo>\r";

            xml = xml + "    <Path>" + XmlEncode(Path) + "</Path>\r";

            if (Open)
                xml = xml + "    <Open>1</Open>\r";
            else
                xml = xml + "    <Open>0</Open>\r";

            if (ManualAdd)
                xml = xml + "    <ManualAdd>1</ManualAdd>\r";
            else
                xml = xml + "    <ManualAdd>0</ManualAdd>\r";

            xml = xml + "  </PathInfo>\r";
            
            return xml;
        }

        public void LoadFromXPath(XPathNavigator nav)
        {
            nav.MoveToFirstChild();

            do
            {
                if (nav.Name == "Path") Path = XmlDecode(nav.Value);
                else if (nav.Name == "Open") Open = nav.ValueAsBoolean;
                else if (nav.Name == "ManualAdd") ManualAdd = nav.ValueAsBoolean;
            } while (nav.MoveToNext());
        }

        public bool IsEmpty()
        {
            return !Open && !ManualAdd;
        }
    }
}
