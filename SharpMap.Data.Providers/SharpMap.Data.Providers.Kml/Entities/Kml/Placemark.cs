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
    [XmlType(TypeName = "PlacemarkType", Namespace = "http://www.opengis.net/kml/2.2")]
    [XmlRoot("Placemark", Namespace = "http://www.opengis.net/kml/2.2", IsNullable = false)]
    public class Placemark : FeatureBase
    {
        private GeometryBase itemField;

        private KmlObjectBase[] placemarkObjectExtensionGroupField;
        private string[] placemarkSimpleExtensionGroupField;

        /// <remarks/>
        [XmlElement("LineString", typeof (LineString))]
        [XmlElement("LinearRing", typeof (LinearRing))]
        [XmlElement("Model", typeof (Model))]
        [XmlElement("MultiGeometry", typeof (MultiGeometry))]
        [XmlElement("Point", typeof (Point))]
        [XmlElement("Polygon", typeof (Polygon))]
        public GeometryBase Item
        {
            get { return itemField; }
            set { itemField = value; }
        }

        /// <remarks/>
        [XmlElement("PlacemarkSimpleExtensionGroup")]
        public string[] PlacemarkSimpleExtensionGroup
        {
            get { return placemarkSimpleExtensionGroupField; }
            set { placemarkSimpleExtensionGroupField = value; }
        }

        /// <remarks/>
        [XmlElement("PlacemarkObjectExtensionGroup")]
        public KmlObjectBase[] PlacemarkObjectExtensionGroup
        {
            get { return placemarkObjectExtensionGroupField; }
            set { placemarkObjectExtensionGroupField = value; }
        }
    }
}