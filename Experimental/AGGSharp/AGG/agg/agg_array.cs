
/*
 *	Portions of this file are  � 2008 Newgrove Consultants Limited, 
 *  http://www.newgrove.com; you can redistribute it and/or modify it under the terms 
 *  of the current GNU Lesser General Public License (LGPL) as published by and 
 *  available from the Free Software Foundation, Inc., 
 *  59 Temple Place, Suite 330, Boston, MA 02111-1307 USA: http://fsf.org/    
 *  This program is distributed without any warranty; 
 *  without even the implied warranty of merchantability or fitness for purpose.  
 *  See the GNU Lesser General Public License for the full details. 
 *  
 *  Author: John Diss 2008
 *
 *  Original notices below.
 * 
 */
//----------------------------------------------------------------------------
// Anti-Grain Geometry - Version 2.4
// Copyright (C) 2002-2005 Maxim Shemanarev (http://www.antigrain.com)
//
// C# Port port by: Lars Brubaker
//                  larsbrubaker@gmail.com
// Copyright (C) 2007
//
// Permission to copy, use, modify, sell and distribute this software 
// is granted provided this copyright notice appears in all copies. 
// This software is provided "as is" without express or implied
// warranty, and with no claim as to its suitability for any purpose.
//
//----------------------------------------------------------------------------
// Contact: mcseem@antigrain.com
//          mcseemagg@yahoo.com
//          http://www.antigrain.com
//----------------------------------------------------------------------------
namespace AGG
{
    public interface IDataContainer<T>
    {
        T[] Array { get; }
        void RemoveLast();
    }

    public class ArrayPOD<T> : IDataContainer<T>
    {
        public ArrayPOD()
            : this(64)
        {
        }

        public ArrayPOD(uint size)
        {
            m_array = new T[size];
            m_size = size;
        }

        public ArrayPOD(int size)
        {
            m_array = new T[size];
            m_size = (uint)size;
        }

        public void RemoveLast()
        {
            throw new System.NotImplementedException();
        }

        public ArrayPOD(ArrayPOD<T> v)
        {
            m_array = (T[])v.m_array.Clone();
            m_size = v.m_size;
        }

        public void Resize(int size)
        {
            if (size != m_size)
            {
                m_array = new T[size];
            }
        }

        public uint Size() { return m_size; }

        public T this[int Index]
        {
            get
            {
                return m_array[Index];
            }

            set
            {
                m_array[Index] = value;
            }
        }

        public T[] Array
        {
            get
            {
                return m_array;
            }
        }
        private T[] m_array;
        private uint m_size;
    };


    //--------------------------------------------------------------pod_vector
    // A simple class template to store Plain Old Data, a vector
    // of a fixed size. The data is contiguous in memory
    //------------------------------------------------------------------------
    public class VectorPOD<T> : IDataContainer<T>
    {
        protected uint m_size;
        private uint m_capacity;
        private T[] m_array;

        public VectorPOD()
        {
        }

        public VectorPOD(uint cap)
            : this(cap, 0)
        {
        }

        public VectorPOD(uint cap, uint extra_tail)
        {
            Allocate(cap, extra_tail);
        }

        public virtual void RemoveLast()
        {
            if (m_size != 0)
            {
                m_size--;
            }
        }

        // Copying
        public VectorPOD(VectorPOD<T> v)
        {
            m_size = v.m_size;
            m_capacity = v.m_capacity;
            m_array = (T[])v.m_array.Clone();
        }

        public void CopyFrom(VectorPOD<T> v)
        {
            Allocate(v.m_size);
            if (v.m_size != 0)
            {
                v.m_array.CopyTo(m_array, 0);
            }
        }

        // Set new capacity. All data is lost, size is set to zero.
        public void Capacity(uint cap)
        {
            Capacity(cap, 0);
        }

        public void Capacity(uint cap, uint extra_tail)
        {
            m_size = 0;
            if (cap > m_capacity)
            {
                m_array = null;
                m_capacity = cap + extra_tail;
                if (m_capacity != 0)
                {
                    m_array = new T[m_capacity];
                }
            }
        }

        public uint Capacity() { return m_capacity; }

        // Allocate n elements. All data is lost, 
        // but elements can be accessed in range 0...size-1. 
        public void Allocate(uint size)
        {
            Allocate(size, 0);
        }

        public void Allocate(uint size, uint extra_tail)
        {
            Capacity(size, extra_tail);
            m_size = size;
        }

        // Resize keeping the content.
        public void Resize(uint new_size)
        {
            if (new_size > m_size)
            {
                if (new_size > m_capacity)
                {
                    T[] newArray = new T[new_size];
                    if (m_array != null)
                    {
                        for (uint i = 0; i < m_array.Length; i++)
                        {
                            newArray[i] = m_array[i];
                        }
                    }
                    m_array = newArray;
                    m_capacity = new_size;
                }
            }
        }

#pragma warning disable 649
        static T zeroed_object;
#pragma warning restore 649

        public void Zero()
        {
            int NumItems = m_array.Length;
            for (int i = 0; i < NumItems; i++)
            {
                m_array[i] = zeroed_object;
            }
        }

        public virtual void Add(T v)
        {
            if (m_array == null || m_array.Length < (m_size + 1))
            {
                Resize(m_size + (m_size / 2) + 16);
            }
            m_array[m_size++] = v;
        }

        public void PushBack(T v) { m_array[m_size++] = v; }
        public void InsertAt(uint pos, T val)
        {
            if (pos >= m_size)
            {
                m_array[m_size] = val;
            }
            else
            {
                for (uint i = 0; i < m_size - pos; i++)
                {
                    m_array[i + pos + 1] = m_array[i + pos];
                }
                m_array[pos] = val;
            }
            ++m_size;
        }

        public void IncSize(uint size) { m_size += size; }
        public uint Size() { return m_size; }

        public T this[uint i]
        {
            get
            {
                return m_array[i];
            }
        }
        public T this[int i]
        {
            get
            {
                return m_array[i];
            }
        }

        public T[] Array
        {
            get
            {
                return m_array;
            }
        }

        public T ItemAt(uint i) { return m_array[i]; }
        public T ValueAt(uint i) { return m_array[i]; }

        public T[] Data() { return m_array; }

        public void RemoveAll() { m_size = 0; }
        public void Clear() { m_size = 0; }
        public void CutAt(uint num) { if (num < m_size) m_size = num; }
    };

    //----------------------------------------------------------range_adaptor
    public class VectorPODRangeAdaptor
    {
        VectorPOD<uint> m_array;
        uint m_start;
        uint m_size;

        public VectorPODRangeAdaptor(VectorPOD<uint> array, uint start, uint size)
        {
            m_array = (array);
            m_start = (start);
            m_size = (size);
        }

        public uint Size() { return m_size; }
        public uint this[uint i]
        {
            get
            {
                return m_array.Array[m_start + i];
            }

            set
            {
                m_array.Array[m_start + i] = value;
            }
        }
        public uint ItemAt(uint i) { return m_array.Array[m_start + i]; }
        public uint ValueAt(uint i) { return m_array.Array[m_start + i]; }
    };
}
