﻿using System;
using System.Xml.Serialization;

namespace SharpMap.Expressions
{
    [Serializable]
    [XmlType(Namespace = "http://www.opengis.net/ogc", TypeName = "PropertyIsBetweenType")]
    [XmlRoot("PropertyIsBetween", Namespace = "http://www.opengis.net/ogc", IsNullable = false)]
    public class PropertyIsBetweenExpression : ComparisonExpression
    {
        private Expression _expression;
        private ExpressionType _expressionType;
        private LowerBoundaryExpression _lowerBoundaryField;
        private UpperBoundaryExpression _upperBoundaryField;

        [XmlElement("Add", typeof (BinaryExpression))]
        [XmlElement("Div", typeof (BinaryExpression))]
        [XmlElement("Function", typeof (FunctionExpression))]
        [XmlElement("Literal", typeof (LiteralExpression))]
        [XmlElement("Mul", typeof (BinaryExpression))]
        [XmlElement("PropertyName", typeof (PropertyNameExpression))]
        [XmlElement("Sub", typeof (BinaryExpression))]
        [XmlChoiceIdentifier("ExpressionElementType")]
        public Expression Expression
        {
            get { return _expression; }
            set { _expression = value; }
        }

        [XmlIgnore]
        public ExpressionType ExpressionElementType
        {
            get { return _expressionType; }
            set { _expressionType = value; }
        }

        public LowerBoundaryExpression LowerBoundary
        {
            get { return _lowerBoundaryField; }
            set { _lowerBoundaryField = value; }
        }

        public UpperBoundaryExpression UpperBoundary
        {
            get { return _upperBoundaryField; }
            set { _upperBoundaryField = value; }
        }
    }
}