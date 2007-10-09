// Portions copyright 2005, 2006 - Morten Nielsen (www.iter.dk)
// Portions copyright 2006, 2007 - Rory Plaire (codekaizen@gmail.com)
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
using System.Collections.Generic;
using SharpMap.Styles;

namespace SharpMap.Rendering.Rendering2D
{
    /// <summary>
    /// Encapsulates a label glyph on a map.
    /// </summary>
    public class Label2D : ILabel<Point2D, Size2D, Rectangle2D, Path2D>, IComparable<Label2D>, IComparer<Label2D>
    {
        private string _text;
        private Point2D _location;
        private Point2D _offset;
        private StyleFont _font;
        private int _priority;
        private float _rotation;
        private LabelStyle _style;
        private Path2D _labelPath;
        private Size2D _collisionBuffer;

        /// <summary>
        /// Initializes a new Label instance.
        /// </summary>
        /// <param name="text">Text to write.</param>
        /// <param name="position">Position of label.</param>
        /// <param name="style">The style to use in rendering the label.</param>
        public Label2D(string text, Point2D position, LabelStyle style)
            : this(text, position, 0, 0, Size2D.Empty, style)
        {
            _text = text;
            _location = position;
            _style = style;
        }

        /// <summary>
        /// Initializes a new Label instance.
        /// </summary>
        /// <param name="text">Text to write</param>
        /// <param name="position">Position of label</param>
        /// <param name="rotation">Rotation</param>
        /// <param name="priority">Label priority used for collision detection.</param>
        /// <param name="collisionArea">Area around label for collision detection.</param>
        /// <param name="style">The style to use in rendering the label.</param>
        public Label2D(string text, Point2D position, float rotation, int priority, Size2D collisionArea,
                       LabelStyle style)
        {
            _text = text;
            _location = position;
            _rotation = rotation;
            _priority = priority;
            _collisionBuffer = collisionArea;
            _style = style ?? new LabelStyle();
        }

        /// <summary>
        /// Creates a string representation of the label.
        /// </summary>
        /// <returns>A string which represents the label instance.</returns>
        public override string ToString()
        {
            return
                String.Format("[{0}] Text: {1}; LabelPoint: {2}; Font: {3}; Rotation: {4:N}; Priority: {5}; Box: {6}",
                              GetType(), Text, Location, Font, Rotation, Priority, CollisionBuffer);
        }

        /// <summary>
        /// Gets or sets the region which collision is computed for.
        /// </summary>
        public Size2D CollisionBuffer
        {
            get { return _collisionBuffer.IsEmpty ? _style.CollisionBuffer : _collisionBuffer; }
            set { _collisionBuffer = value; }
        }

        /// <summary>
        /// Gets or sets the path on which to flow the label.
        /// </summary>
        public Path2D FlowPath
        {
            get { return _labelPath; }
            set { _labelPath = value; }
        }

        /// <summary>
        /// Gets or sets the font to render the label with.
        /// </summary>
        public StyleFont Font
        {
            get { return _font ?? Style.Font; }
            set { _font = value; }
        }

        /// <summary>
        /// Gets or sets the label position.
        /// </summary>
        public Point2D Location
        {
            get { return _location; }
            set { _location = value; }
        }

        public Point2D Offset
        {
            get { return _offset.IsEmpty ? _style.Offset : _offset; }
            set { _offset = value; }
        }

        /// <summary>
        /// Gets or sets the relative priority in layout of the label.
        /// </summary>
        public int Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// Gets or sets the rotation the label is rendered with. Represented as radians counter-clockwise.
        /// </summary>
        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }

        /// <summary>
        /// Gets or sets the <see cref="SharpMap.Styles.LabelStyle"/> of this label.
        /// </summary>
        public LabelStyle Style
        {
            get { return _style; }
            set { _style = value; }
        }

        /// <summary>
        /// Gets or sets the text of the label.
        /// </summary>
        public string Text
        {
            get { return _text; }
            set { _text = value; }
        }

        #region IComparable<Label> Members

        /// <summary>
        /// Tests if two label boxes intersects
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(Label2D other)
        {
            if (this == other)
            {
                return 0;
            }
            else if (_collisionBuffer == Size2D.Empty)
            {
                return -1;
            }
            else if (other.CollisionBuffer == Size2D.Empty)
            {
                return 1;
            }
            else
            {
                return _collisionBuffer.CompareTo(other.CollisionBuffer);
            }
        }

        #endregion

        #region IComparer<Label> Members

        /// <summary>
        /// Checks if two labels intersect
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public int Compare(Label2D x, Label2D y)
        {
            return x.CompareTo(y);
        }

        #endregion
    }
}