// Copyright 2006 - Morten Nielsen (www.iter.dk)
//
// This file is part of SharpMap.
// SharpMap is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
// 
// SharpMap is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU Lesser General Public License for more details.

// You should have received a copy of the GNU Lesser General Public License
// along with SharpMap; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA 

using System;
using SharpMap.Data;
using SharpMap.Styles;

namespace SharpMap.Rendering.Thematics
{
    /// <summary>
    /// The CustomTheme class is used for defining your own 
    /// thematic rendering by using a custom delegate for value-based
    /// style generation.
    /// </summary>
    public class CustomTheme : ITheme
    {
        #region Nested type

        /// <summary>
        /// Custom Style Delegate method
        /// </summary>
        /// <remarks>
        /// The GetStyle delegate is used for determining the style of 
        /// a feature using the <see cref="StyleDelegate"/> property.
        /// The method must take a <see cref="FeatureDataRow"/> and 
        /// return an <see cref="SharpMap.Styles.IStyle"/>.
        /// If the method returns null, the default style will be 
        /// used for rendering.
        /// <para>
        /// <example>
        /// The following example can used for highlighting all 
        /// features where the attribute "NAME" starts with "S".
        /// <code lang="C#">
        /// using SharpMap.Rendering.Thematics;
        /// using SharpMap.Styles;
        /// // [...]
        /// CustomTheme theme = new CustomTheme(generateCustomStyle);
        /// VectorStyle defaultStyle = new VectorStyle(); // Create default render style
        /// defaultStyle.Fill = Brushes.Gray;
        /// theme.DefaultStyle = defaultStyle;
        /// 
        /// // [...]
        /// 
        /// // Set up delegate for determining fill-color.
        /// // Delegate will fill all objects with a yellow color where the attribute "NAME" starts with "S".
        /// private static VectorStyle generateCustomStyle(FeatureDataRow row)
        /// {
        /// 	if (row["NAME"].ToString().StartsWith("S"))
        /// 	{
        /// 		VectorStyle style = new VectorStyle();
        /// 		style.Fill = Brushes.Yellow;
        /// 		return style;
        /// 	}
        /// 	else
        /// 	{	
        ///         return null; // Return null which will render the default style
        ///     }
        /// }
        /// </code>
        /// </example>
        /// </para>
        /// </remarks>
        /// <param name="dr">Feature</param>
        /// <returns>Style to be applied to feature.</returns>
        public delegate IStyle GetStyleMethod(FeatureDataRow dr);

        #endregion

        #region Instance fields

        private IStyle _defaultStyle;
        private GetStyleMethod _getStyleDelegate;
        #endregion

        #region Object constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="CustomTheme"/> class
        /// </summary>
        /// <param name="getStyleMethod"></param>
        public CustomTheme(GetStyleMethod getStyleMethod)
        {
            _getStyleDelegate = getStyleMethod;
        }
        #endregion

        #region Public properties

        /// <summary>
        /// Gets or sets the default style when the custom style doesn't apply.
        /// </summary>
        public IStyle DefaultStyle
        {
            get { return _defaultStyle; }
            set { _defaultStyle = value; }
        }

        /// <summary>
        /// Gets or sets the style delegate used for determining the style of a feature
        /// </summary>
        /// <remarks>
        /// The delegate must take a <see cref="FeatureDataRow"/> and return an <see cref="SharpMap.Styles.IStyle"/>.
        /// If the method returns null, the default style will be used for rendering.
        /// <example>
        /// The example below creates a delegate that can be used for assigning the rendering of a road theme. If the road-class
        /// is larger than '3', it will be rendered using a thick red line.
        /// <code lang="C#">
        /// using SharpMap.Styles;
        /// // [...]
        /// private static VectorStyle GetRoadStyle(FeatureDataRow row)
        /// {
        ///		VectorStyle style = new VectorStyle();
        /// 
        ///     Int32 roadClass = (Int32)row["RoadClass"];
        /// 
        ///		if(roadClass > 3)
        ///     {
        ///			style.Line = new Pen(Color.Red,5f);
        ///     }
        ///		else
        ///		{
        ///     	style.Line = new Pen(Color.Black,1f);
        ///     }
        /// 
        ///		return style;
        /// }
        /// </code>
        /// </example>
        /// </remarks>
        /// <seealso cref="GetStyleMethod"/>
        public GetStyleMethod StyleDelegate
        {
            get { return _getStyleDelegate; }
            set { _getStyleDelegate = value; }
        }
        #endregion

        #region ITheme Members

        /// <summary>
        /// Returns the <see cref="IStyle">style</see> 
        /// based on a value in the feature, as computed by the 
        /// <see cref="StyleDelegate"/> method.
        /// </summary>
        /// <param name="row">Feature to compute style for.</param>
        /// <returns>Feature value dependent style.</returns>
        public IStyle GetStyle(FeatureDataRow row)
        {
            IStyle style = _getStyleDelegate(row);

            if (style != null)
            {
                return style;
            }
            else
            {
                return _defaultStyle;
            }
        }

        #endregion

        #region Explicit Interface Implementation
        #region ITheme Members
        IStyle ITheme.GetStyle(IFeatureDataRecord row)
        {
            if (!(row is FeatureDataRow))
            {
                throw new ArgumentException("Parameter 'row' must be of type FeatureDataRow");
            }

            return GetStyle(row as FeatureDataRow);
        }
        #endregion
        #endregion
    }
}