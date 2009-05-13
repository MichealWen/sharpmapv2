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

namespace SharpMap.Entities.xAl
{
    /// <remarks/>
    [Serializable]
    [XmlType(AnonymousType = true, Namespace = "urn:oasis:names:tc:ciq:xsdschema:xAL:2.0")]
    public class PremisePremiseNumberRange
    {
        private string indicatorField;

        private PremisePremiseNumberRangeIndicatorOccurence indicatorOccurenceField;

        private bool indicatorOccurenceFieldSpecified;

        private PremisePremiseNumberRangeNumberRangeOccurence numberRangeOccurenceField;

        private bool numberRangeOccurenceFieldSpecified;
        private PremisePremiseNumberRangePremiseNumberRangeFrom premiseNumberRangeFromField;

        private PremisePremiseNumberRangePremiseNumberRangeTo premiseNumberRangeToField;

        private string rangeTypeField;
        private string separatorField;

        private string typeField;

        /// <remarks/>
        public PremisePremiseNumberRangePremiseNumberRangeFrom PremiseNumberRangeFrom
        {
            get { return premiseNumberRangeFromField; }
            set { premiseNumberRangeFromField = value; }
        }

        /// <remarks/>
        public PremisePremiseNumberRangePremiseNumberRangeTo PremiseNumberRangeTo
        {
            get { return premiseNumberRangeToField; }
            set { premiseNumberRangeToField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string RangeType
        {
            get { return rangeTypeField; }
            set { rangeTypeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string Indicator
        {
            get { return indicatorField; }
            set { indicatorField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string Separator
        {
            get { return separatorField; }
            set { separatorField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public string Type
        {
            get { return typeField; }
            set { typeField = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public PremisePremiseNumberRangeIndicatorOccurence IndicatorOccurence
        {
            get { return indicatorOccurenceField; }
            set { indicatorOccurenceField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool IndicatorOccurenceSpecified
        {
            get { return indicatorOccurenceFieldSpecified; }
            set { indicatorOccurenceFieldSpecified = value; }
        }

        /// <remarks/>
        [XmlAttribute]
        public PremisePremiseNumberRangeNumberRangeOccurence NumberRangeOccurence
        {
            get { return numberRangeOccurenceField; }
            set { numberRangeOccurenceField = value; }
        }

        /// <remarks/>
        [XmlIgnore]
        public bool NumberRangeOccurenceSpecified
        {
            get { return numberRangeOccurenceFieldSpecified; }
            set { numberRangeOccurenceFieldSpecified = value; }
        }
    }
}