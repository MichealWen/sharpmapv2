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
using System.Collections.Generic;
using GeoAPI.CoordinateSystems;
using GeoAPI.CoordinateSystems.Transformations;
using NPack.Interfaces;

namespace ProjNet.CoordinateSystems.Transformations
{
    internal class ConcatenatedTransform<TCoordinate> : MathTransform<TCoordinate>
        where TCoordinate : ICoordinate, IEquatable<TCoordinate>, IComparable<TCoordinate>, IComputable<TCoordinate>, IConvertible
    {
        protected IMathTransform<TCoordinate> _inverse;
        private readonly List<ICoordinateTransformation<TCoordinate>> _transforms;

        public ConcatenatedTransform()
            : this(new ICoordinateTransformation<TCoordinate>[0]) { }

        public ConcatenatedTransform(IEnumerable<ICoordinateTransformation<TCoordinate>> transforms)
        {
            _transforms = new List<ICoordinateTransformation<TCoordinate>>(transforms);
        }

        public IEnumerable<ICoordinateTransformation<TCoordinate>> Transforms
        {
            get { return _transforms; }
            set
            {
                _transforms.Clear();
                _transforms.AddRange(value);
                _inverse = null;
            }
        }

        /// <summary>
        /// Transforms a point
        /// </summary>
        public override TCoordinate Transform(TCoordinate point)
        {
            foreach (ICoordinateTransformation<TCoordinate> ct in _transforms)
            {
                point = ct.MathTransform.Transform(point);
            }

            return point;
        }

        /// <summary>
        /// Transforms a list point
        /// </summary>
        public override IEnumerable<TCoordinate> Transform(IEnumerable<TCoordinate> points)
        {
            foreach (TCoordinate point in points)
            {
                yield return Transform(point);
            }
        }

        /// <summary>
        /// Returns the inverse of this conversion.
        /// </summary>
        /// <returns>IMathTransform that is the reverse of the current conversion.</returns>
        public override IMathTransform<TCoordinate> Inverse()
        {
            if (_inverse == null)
            {
                _inverse = new ConcatenatedTransform<TCoordinate>(_transforms);
                _inverse.Invert();
            }

            return _inverse;
        }

        /// <summary>
        /// Reverses the transformation
        /// </summary>
        public override void Invert()
        {
            _transforms.Reverse();

            foreach (ICoordinateTransformation<TCoordinate> ic in _transforms)
            {
                ic.MathTransform.Invert();
            }
        }

        /// <summary>
        /// Gets a Well-Known Text representation of this object.
        /// </summary>
        /// <value></value>
        public override string Wkt
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets an XML representation of this object.
        /// </summary>
        /// <value></value>
        public override string Xml
        {
            get { throw new NotImplementedException(); }
        }
    }
}