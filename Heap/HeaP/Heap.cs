using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;

namespace Heap
{
    public class Heap<T> : IEnumerable<T>,ICollection<T> 
    {
        private T[] items = new T[10];
        private int size;
        private IComparer<T> comparer;

        public int Count => size;

        public bool IsReadOnly => false;

        public T this[int index] { get=>items[index]; set => throw new NotImplementedException(); }

        public Heap(IComparer<T> comparer)
        {
            this.comparer = comparer;
        }

        public void Insert(T value)
        {
            if (isFull())
            {
                throw new Exception("heap is full");
            }
            items[size++] = value;
            int index = size - 1;
            while (index > 0 && comparer.Compare(items[index], items[parentOfIndex(index)]) > 0)
            {
                swap(index, parentOfIndex(index));
                index = parentOfIndex(index);
            }

        }
        public T Remove()
        {
            if (isEmpty())
            {
                throw new Exception("heap is empty");
            }
            var node = items[0];
            items[0] = items[--size];
            int index = 0;
            while (index <= size && !validParent(index))
            {
                var indexOfLargerChild = largerChild(index);
                swap(index, indexOfLargerChild);
                index = indexOfLargerChild;
            }
            return node;
        }
        public T HighPriorityNode() => items[0];
        private int largerChild(int index)
        {
            if (!hasLeftChild(index))
            { return index; }
            if (!hasRighChild(index))
            { return indexOfLeftChild(index); }
            int indexOfLargerChild = comparer.Compare(items[indexOfLeftChild(index)], items[indexOfRightChild(index)]) > 0 ?
             indexOfLeftChild(index) : indexOfRightChild(index);
            return indexOfLargerChild;
        }
        private bool hasLeftChild(int index) => indexOfLeftChild(index) <= size;
        private bool hasRighChild(int index) => indexOfRightChild(index) <= size;
        private bool validParent(int index)
        {
            if (!hasLeftChild(index))
                return true;
            var valid = comparer.Compare(items[index], items[indexOfLeftChild(index)]) > 0;
            if (hasRighChild(index))
            {
                valid &= comparer.Compare(items[index], items[indexOfRightChild(index)]) > 0;
            }
            return valid;
        }
        private int indexOfLeftChild(int index) => index * 2 + 1;
        private int indexOfRightChild(int index) => index * 2 + 2;
        private void swap(int firstValue, int secondValue)
        {
            var temp = items[firstValue];
            items[firstValue] = items[secondValue];
            items[secondValue] = temp;
        }
        private int parentOfIndex(int index) => (index - 1) / 2;

        private bool isFull() => size == items.Length;
        private bool isEmpty() => size == 0;

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < size; i++)
            {
                yield return items[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        public void Add(T item)
        {
            Insert(item);
        }

        public void Clear()
        {
            size = 0;
        }

        public bool Contains(T item)
        {
          for(int i=0; i<size; i++)
            {
                if (comparer.Compare(item, items[i])==0)
                {
                    return true;
                }
            }
          return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            foreach(var item in items)
            {
                array[arrayIndex++] = item;
            }
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }
    }
}
