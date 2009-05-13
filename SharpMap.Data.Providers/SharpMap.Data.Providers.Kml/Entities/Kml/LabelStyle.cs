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
    [XmlType(TypeName = "LabelStyleType", Namespace = "http://www.opengis.net/kml/2.2")]
    [XmlRoot("LabelStyle", Namespace = "http://www.opengis.net/kml/2.2", IsNullable = false)]
    public class LabelStyle : ColorStyleBase
    {
        private KmlObjectBase[] labelStyleObjectExtensionGroupField;
        private string[] labelStyleSimpleExtensionGroupField;
        private double scaleField;

        private bool scaleFieldSpecified;

        public LabelStyle()
        {
            scaleField = 1;
        }

        /// <remarks/>
        public double scale
        {
            get { return scaleField; }
            set { scaleField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool scaleSpecified
        {
            get { return scaleFieldSpecified; }
            set { scaleFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlElement("LabelStyleSimpleExtensionGroup")]
        public string[] LabelStyleSimpleExtensionGroup
        {
            get { return labelStyleSimpleExtensionGroupField; }
            set { labelStyleSimpleExtensionGroupField = value; }
        }

        /// <remarks/>
        [XmlElement("LabelStyleObjectExtensionGroup")]
        public KmlObjectBase[] LabelStyleObjectExtensionGroup
        {
            get { return labelStyleObjectExtensionGroupField; }
            set { labelStyleObjectExtensionGroupField = value; }
        }
    }
}