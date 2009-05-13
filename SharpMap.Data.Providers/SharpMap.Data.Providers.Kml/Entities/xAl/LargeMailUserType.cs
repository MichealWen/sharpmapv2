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
using System.Xml;
using System.Xml.Serialization;

namespace SharpMap.Entities.xAl
{
    /// <remarks/>
    [Serializable]
    [XmlType(Namespace = "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0")]
    public class LargeMailUserType
    {
        private XmlAttribute[] anyAttrField;
        private XmlElement[] anyField;

        private BuildingNameType[] buildingNameField;

        private Department departmentField;
        private LargeMailUserTypeLargeMailUserIdentifier largeMailUserIdentifierField;
        private PostalCode postalCodeField;

        private PostBox postBoxField;

        private Thoroughfare thoroughfareField;

        private string typeField;

        /// <remarks/>
        [XmlElement("AddressLine")]
        public xAlTypedElementBase[] AddressLine { get; set; }

        /// <remarks/>
        [XmlElement("LargeMailUserName")]
        public xAlTypedElementBase[] LargeMailUserName { get; set; }

        /// <remarks/>
        public LargeMailUserTypeLargeMailUserIdentifier LargeMailUserIdentifier
        {
            get { return largeMailUserIdentifierField; }
            set { largeMailUserIdentifierField = value; }
        }

        /// <remarks/>
        [XmlElement("BuildingName")]
        public BuildingNameType[] BuildingName
        {
            get { return buildingNameField; }
            set { buildingNameField = value; }
        }

        /// <remarks/>
        public Department Department
        {
            get { return departmentField; }
            set { departmentField = value; }
        }

        /// <remarks/>
        public PostBox PostBox
        {
            get { return postBoxField; }
            set { postBoxField = value; }
        }

        /// <remarks/>
        public Thoroughfare Thoroughfare
        {
            get { return thoroughfareField; }
            set { thoroughfareField = value; }
        }

        /// <remarks/>
        public PostalCode PostalCode
        {
            get { return postalCodeField; }
            set { postalCodeField = value; }
        }

        /// <remarks/>
        [XmlAnyElement]
        public XmlElement[] Any
        {
            get { return anyField; }
            set { anyField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string Type
        {
            get { return typeField; }
            set { typeField = value; }
        }

        /// <remarks/>
        [XmlAnyAttribute]
        public XmlAttribute[] AnyAttr
        {
            get { return anyAttrField; }
            set { anyAttrField = value; }
        }
    }
}