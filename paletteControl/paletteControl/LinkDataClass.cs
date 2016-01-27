using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
namespace paletteControl
{
    [DataContract]
    public class LinkDataClass
    {
        public enum LType {
            EXE,
            LINK,
            TEXT
        }

        [IgnoreDataMember]
        public System.Drawing.Icon MyIcon { get; set; }
        [DataMember]
        public string MyLink { get; set; }
        [DataMember]
        public LType MyType { get; set; }
        [DataMember]
        public string Execute { get; set; }

        public LinkDataClass(string name, System.Drawing.Icon icon,LType i)
        {
            this.MyLink = name;
            this.MyIcon = icon;
            this.MyType = i;
        }

        public LinkDataClass(string name, System.Drawing.Icon icon, LType i, string execute)
        {
            this.MyLink = name;
            this.MyIcon = icon;
            this.MyType = i;
            this.Execute = execute;
        }
    }
}
