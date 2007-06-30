// Copyright 2006, 2007 - Rory Plaire (codekaizen@gmail.com)
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
using NPack;
using IMatrixD = NPack.Interfaces.IMatrix<NPack.DoubleComponent>;
using IVectorD = NPack.Interfaces.IVector<NPack.DoubleComponent>;

namespace SharpMap.Rendering.Rendering2D
{
    /// <summary>
    /// Represents a 2 dimensional affine transform matrix (a 3x3 matrix).
    /// </summary>
    [Serializable]
    public class ViewMatrix2D : AffineMatrix<DoubleComponent>
    {
        public new readonly static ViewMatrix2D Identity
            = new ViewMatrix2D(
                1, 0, 0,
                0, 1, 0,
                0, 0, 1);

        public new readonly static ViewMatrix2D Zero
            = new ViewMatrix2D(
                0, 0, 0,
                0, 0, 0,
                0, 0, 0);

        #region Constructors
        public ViewMatrix2D()
            : this(Identity) { }

        public ViewMatrix2D(double x1, double x2, double x3,
            double y1, double y2, double y3,
            double w1, double w2, double w3)
            :base(3)
        {
            X1 = x1; X2 = x2; X3 = x3;
            Y1 = y1; Y2 = y2; Y3 = y3;
            W1 = w1; W2 = w2; W3 = w3;
        }

        public ViewMatrix2D(IMatrixD matrixToCopy)
            : base(3)
        {
            if (matrixToCopy == null) throw new ArgumentNullException("matrixToCopy");

            for (int i = 0; i < RowCount; i++)
            {
                Array.Copy(matrixToCopy.Elements, Elements, matrixToCopy.Elements.Length);
            }
        }

        #endregion

        #region ToString
        public override string ToString()
        {
            return String.Format("[ViewMatrix2D] [ [{0:N3}, {1:N3}, {2:N3}], [{3:N3}, {4:N3}, {5:N3}], [{6:N3}, {7:N3}, {8:N3}] ]",
                X1, X2, X3, Y1, Y2, Y3, W1, W2, W3);
        }
        #endregion

        #region GetHashCode
        public override int GetHashCode()
        {
            return unchecked(this[0, 0].GetHashCode() + 24243 ^ this[0, 1].GetHashCode() + 7318674
                ^ this[0, 2].GetHashCode() + 282 ^ this[1, 0].GetHashCode() + 54645
                ^ this[1, 1].GetHashCode() + 42 ^ this[1, 2].GetHashCode() + 244892
                ^ this[2, 0].GetHashCode() + 8464 ^ this[2, 1].GetHashCode() + 36565 ^ this[2, 2].GetHashCode() + 3210186);
        }
        #endregion

        public new ViewMatrix2D Clone()
        {
            return new ViewMatrix2D(this);
        }

        private readonly DoubleComponent[] _transferPoints = new DoubleComponent[2];

        public ViewPoint2D TransformVector(double x, double y)
        {
            _transferPoints[0] = x;
            _transferPoints[1] = y;
            MatrixProcessor<DoubleComponent>.Instance.Operations.Multiply(this, _transferPoints);
            return new ViewPoint2D((double)_transferPoints[0], (double)_transferPoints[1]);
        }

        #region Equality Computation

        public override bool Equals(object obj)
        {
            if (obj is ViewMatrix2D)
            {
                return Equals(obj as ViewMatrix2D);
            }

            if (obj is IMatrixD)
            {
                return Equals(obj as IMatrixD);
            }

            return false;
        }

        #region IEquatable<ViewMatrix2D> Members

        public bool Equals(ViewMatrix2D other)
        {
            return X1 == other.X1 &&
                X2 == other.X2 &&
                X3 == other.X3 && 
                Y1 == other.Y1 &&
                Y2 == other.Y2 &&
                Y3 == other.Y3 &&
                W1 == other.W1 &&
                W2 == other.W2 &&
                W3 == other.W3;
        }

        #endregion
        #endregion

        #region Properties
        public double X1
        {
            get { return (double)this[0, 0]; }
            set { this[0, 0] = value; }
        }

        public double X2
        {
            get { return (double)this[0, 1]; }
            set { this[0, 1] = value; }
        }

        public double X3
        {
            get { return (double)this[0, 2]; }
            set { this[0, 2] = value; }
        }

        public double Y1
        {
            get { return (double)this[1, 0]; }
            set { this[1, 0] = value; }
        }

        public double Y2
        {
            get { return (double)this[1, 1]; }
            set { this[1, 1] = value; }
        }

        public double Y3
        {
            get { return (double)this[1, 2]; }
            set { this[1, 2] = value; }
        }

        public double W1
        {
            get { return (double)this[2, 0]; }
            set { this[2, 0] = value; }
        }

        public double W2
        {
            get { return (double)this[2, 1]; }
            set { this[2, 1] = value; }
        }

        public double W3
        {
            get { return (double)this[2, 2]; }
            set { this[2, 2] = value; }
        }
        #endregion
    }
}
