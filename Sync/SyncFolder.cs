using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.XPath;
using System.Globalization;

namespace FlickrSync
{
    public class SyncFolder
    {
        public enum Methods { SyncFilename = 0, SyncDateTaken, SyncTitleOrFilename };
        public enum FilterTypes { FilterNone = 0, FilterIncludeTags, FilterStarRating };
        public enum OrderTypes { OrderDefault = 0, OrderDateTaken, OrderTitle, OrderTag };

        public string FolderPath;
        public string SetId;
        public string SetTitle;
        public string SetDescription;
        public DateTime LastSync;
        public Methods SyncMethod;
        public FilterTypes FilterType;
        public string FilterTags;
        public int FilterStarRating;
        public FlickrSync.Permissions Permission;
        public bool NoDelete;
        public bool NoDeleteTags;
        public OrderTypes OrderType;
        public bool NoInitialReplace;

        public SyncFolder()
        {
            FolderPath = "";
            LastSync = new DateTime(2000, 1, 1);

            SetId = "";
            SetTitle = "";
            SetDescription = "";
            SyncMethod = StringToMethod(Properties.Settings.Default.Method);
            FilterType = FilterTypes.FilterNone;
            FilterTags = "";
            FilterStarRating = 0;
            Permission = FlickrSync.Permissions.PermDefault;
            NoDelete = Properties.Settings.Default.NoDelete;
            NoDeleteTags = Properties.Settings.Default.NoDeleteTags;
            OrderType = OrderTypes.OrderDefault;
            NoInitialReplace = false;
        }

        public SyncFolder(string pFolderPath)
        {
            FolderPath = pFolderPath;
            LastSync = new DateTime(2000, 1, 1);

            SetId = "";
            SetTitle = "";
            SetDescription = "";
            SyncMethod = StringToMethod(Properties.Settings.Default.Method);
            FilterType = FilterTypes.FilterNone;
            FilterTags = "";
            FilterStarRating = 0;
            Permission = FlickrSync.Permissions.PermDefault;
            NoDelete = Properties.Settings.Default.NoDelete;
            NoDeleteTags = Properties.Settings.Default.NoDeleteTags;
            OrderType = OrderTypes.OrderDefault;
            NoInitialReplace = false;
        }

        static public Methods StringToMethod(string str)
        {
            if (str == "SyncFilename")
                return Methods.SyncFilename;
            else if (str == "SyncDateTaken")
                return Methods.SyncDateTaken;
            else if (str == "SyncTitleOrFilename")
                return Methods.SyncTitleOrFilename;
            else
                return Methods.SyncFilename; //default
        }

        static public OrderTypes StringToOrderType(string str)
        {
            if (str == "OrderTag")
                return OrderTypes.OrderTag;
            else if (str == "OrderDateTaken")
                return OrderTypes.OrderDateTaken;
            else if (str == "OrderTitle")
                return OrderTypes.OrderTitle;
            else
                return OrderTypes.OrderDefault; //default
        }

        public override bool Equals(object sf)
        {
            return FolderPath.Equals(((SyncFolder)sf).FolderPath, StringComparison.CurrentCultureIgnoreCase);
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
            string xml="  <SyncFolder>\r";

            xml = xml + "    <FolderPath>" + XmlEncode(FolderPath) + "</FolderPath>\r";
            xml = xml + "    <LastSync>" + LastSync.ToString("yyyy-MM-dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + "</LastSync>\r";
            xml = xml + "    <SetId>" + SetId + "</SetId>\r";
            xml = xml + "    <SetTitle>" + XmlEncode(SetTitle) + "</SetTitle>\r";
            xml = xml + "    <SetDescription>" + XmlEncode(SetDescription) + "</SetDescription>\r";
            xml = xml + "    <SyncMethod>" + SyncMethod.ToString() + "</SyncMethod>\r";
            xml = xml + "    <FilterType>" + FilterType.ToString() + "</FilterType>\r";
            xml = xml + "    <FilterTags>" + FilterTags + "</FilterTags>\r";
            xml = xml + "    <FilterStarRating>" + FilterStarRating + "</FilterStarRating>\r";
            xml = xml + "    <Permissions>" + Permission.ToString() + "</Permissions>\r";

            if (NoDelete)
                xml = xml + "    <NoDelete>1</NoDelete>\r";
            else
                xml = xml + "    <NoDelete>0</NoDelete>\r";

            if (NoDeleteTags)
                xml = xml + "    <NoDeleteTags>1</NoDeleteTags>\r";
            else
                xml = xml + "    <NoDeleteTags>0</NoDeleteTags>\r";

            xml = xml + "    <OrderType>" + OrderType.ToString() + "</OrderType>\r";

            if (NoInitialReplace)
                xml = xml + "    <NoInitialReplace>1</NoInitialReplace>\r";
            else
                xml = xml + "    <NoInitialReplace>0</NoInitialReplace>\r";

            xml = xml + "  </SyncFolder>\r";
            
            return xml;
        }

        public void LoadFromXPath(XPathNavigator nav)
        {
            nav.MoveToFirstChild();

            do
            {
                if (nav.Name == "FolderPath") FolderPath = XmlDecode(nav.Value);
                else if (nav.Name == "LastSync")
                {
                    try
                    {
                        LastSync = DateTime.ParseExact(nav.Value, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
                    }
                    catch (Exception)
                    {
                        LastSync = DateTime.Parse(nav.Value); //old method
                    }
                }
                else if (nav.Name == "SetId") SetId = nav.Value;
                else if (nav.Name == "SetTitle") SetTitle = XmlDecode(nav.Value);
                else if (nav.Name == "SetDescription") SetDescription = XmlDecode(nav.Value);
                else if (nav.Name == "SyncMethod")
                {
                    if (nav.Value == "SyncFilename") SyncMethod = SyncFolder.Methods.SyncFilename;
                    else if (nav.Value == "SyncDateTaken") SyncMethod = SyncFolder.Methods.SyncDateTaken;
                    else if (nav.Value == "SyncTitleOrFilename") SyncMethod = SyncFolder.Methods.SyncTitleOrFilename;
                }
                else if (nav.Name == "FilterType")
                {
                    if (nav.Value == "FilterNone") FilterType = SyncFolder.FilterTypes.FilterNone;
                    else if (nav.Value == "FilterIncludeTags") FilterType = SyncFolder.FilterTypes.FilterIncludeTags;
                    else if (nav.Value == "FilterStarRating") FilterType = SyncFolder.FilterTypes.FilterStarRating;
                }
                else if (nav.Name == "FilterTags") FilterTags = nav.Value;
                else if (nav.Name == "FilterStarRating") FilterStarRating = nav.ValueAsInt;
                else if (nav.Name == "Permissions")
                {
                    if (nav.Value == "PermDefault") Permission = FlickrSync.Permissions.PermDefault;
                    else if (nav.Value == "PermPublic") Permission = FlickrSync.Permissions.PermPublic;
                    else if (nav.Value == "PermFamilyFriends") Permission = FlickrSync.Permissions.PermFamilyFriends;
                    else if (nav.Value == "PermFriends") Permission = FlickrSync.Permissions.PermFriends;
                    else if (nav.Value == "PermFamily") Permission = FlickrSync.Permissions.PermFamily;
                    else if (nav.Value == "PermPrivate") Permission = FlickrSync.Permissions.PermPrivate;
                }
                else if (nav.Name == "NoDelete") NoDelete = nav.ValueAsBoolean;
                else if (nav.Name == "NoDeleteTags") NoDeleteTags = nav.ValueAsBoolean;
                else if (nav.Name == "OrderType")
                {
                    if (nav.Value == "OrderDefault") OrderType = OrderTypes.OrderDefault;
                    else if (nav.Value == "OrderDateTaken") OrderType = OrderTypes.OrderDateTaken;
                    else if (nav.Value == "OrderTitle") OrderType = OrderTypes.OrderTitle;
                    else if (nav.Value == "OrderTag") OrderType = OrderTypes.OrderTag;
                }
                else if (nav.Name == "NoInitialReplace") NoInitialReplace = nav.ValueAsBoolean;
            } while (nav.MoveToNext());
        }
    }
}
