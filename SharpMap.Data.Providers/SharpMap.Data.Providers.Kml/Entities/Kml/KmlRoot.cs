// /*
//  *  The attached / following is part of SharpMap.Data.Providers.Kml
//  *  SharpMap.Data.Providers.Kml is free software � 2008 Newgrove Consultants Limited, 
//  *  www.newgrove.com; you can redistribute it and/or modify it under the terms 
//  *  of the current GNU Lesser General Public License (LGPL) as published by and 
//  *  available from the Free Software Foundation, Inc., 
//  *  59 Temple Place, Suite 330, Boston, MA 02111-1307 USA: http://fsf.org/    
//  *  This program is distributed without any warranty; 
//  *  without even the implied warranty of merchantability or fitness for purpose.  
//  *  See the GNU Lesser General Public License for the full details. 
//  *  
//  *  Author: John Diss 2009
//  * 
//  */
using System;
using System.Xml.Serialization;

namespace SharpMap.Entities.Ogc.Kml
{
    /// <remarks/>
    [Serializable]
    [XmlType(TypeName = "KmlType", Namespace = "http://www.opengis.net/kml/2.2")]
    [XmlRoot("kml", Namespace = "http://www.opengis.net/kml/2.2", IsNullable = false)]
    public class KmlRoot
    {
        private string hintField;
        private FeatureBase itemField;

        private KmlObjectBase[] kmlObjectExtensionGroupField;
        private string[] kmlSimpleExtensionGroupField;
        private NetworkLinkControl networkLinkControlField;

        /// <remarks/>
        public NetworkLinkControl NetworkLinkControl
        {
            get { return networkLinkControlField; }
            set { networkLinkControlField = value; }
        }

        /// <remarks/>
        [XmlElement("NetworkLink", typeof (NetworkLink))]
        [XmlElement("Placemark", typeof (Placemark))]
        public FeatureBase Item
        {
            get { return itemField; }
            set { itemField = value; }
        }

        /// <remarks/>
        [XmlElement("KmlSimpleExtensionGroup")]
        public string[] KmlSimpleExtensionGroup
        {
            get { return kmlSimpleExtensionGroupField; }
            set { kmlSimpleExtensionGroupField = value; }
        }

        /// <remarks/>
        [XmlElement("KmlObjectExtensionGroup")]
        public KmlObjectBase[] KmlObjectExtensionGroup
        {
            get { return kmlObjectExtensionGroupField; }
            set { kmlObjectExtensionGroupField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string hint
        {
            get { return hintField; }
            set { hintField = value; }
        }
    }
}