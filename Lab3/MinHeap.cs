using System;

namespace Lab3;

public class MinHeap<T> where T : IComparable<T>
{
    private T[] array;
    private const int initialSize = 8;

    public int Count { get; private set; }

    public int Capacity => array.Length;

    public bool IsEmpty => Count == 0;


    public MinHeap(T[] initialArray = null)
    {
        array = new T[initialSize];

        if (initialArray == null) return;

        foreach (var item in initialArray)
        {
            Add(item);
        }

    }

    /// <summary>
    /// Returns the min item but does NOT remove it.
    /// Time complexity: O( 1 )
    /// </summary>
    public T Peek()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        return array[0];
    }

    /// <summary>
    /// Adds given item to the heap.
    /// Time complexity: O(log(n)) ***BUT*** it might be O(N) if we have to resize
    /// </summary>
    public void Add(T item)
    {
        array[Count] = item;
        TrickleUp(Count);
        Count++;

        if (Count == Capacity)
        {
            DoubleArrayCapacity();
        }
    }

    public T Extract()
    {
        return ExtractMin();
    }


    /// <summary>
    /// Removes and returns the max item in the min-heap.
    /// Time complexity: O( n )
    /// </summary>
    public T ExtractMax()
    {
        int maximumIndex = 0;
        int i = 1;
        while (i < Count)
        {
            if (array[i].CompareTo(array[maximumIndex]) > 0) maximumIndex = i;
            i++;
        }
        T maximum = array[maximumIndex];

        // swap with last
        Swap(maximumIndex, Count-1);
        Count--;

        if (i < Count)
        {
            // trickleX
            if (array[i].CompareTo(array[Parent(i)]) < 0)
            {
                TrickleUp(i);
            }
            TrickleDown(i);
        }
        
        return maximum;
    }

    /// <summary>
    /// Removes and returns the min item in the min-heap.
    /// Time complexity: O( log(n) )
    /// </summary>
    public T ExtractMin()
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        // save the min from the root
        T min = array[0];
        // System.Console.WriteLine($"Removing {min}, replaced with {array[Count-1]}.");

        // swap the min with the last item
        Swap(0, Count - 1);

        // remove the "last" item
        Count--;

        // trickle down from root
        TrickleDown(0);

        return min;
    }

    /// <summary>
    /// Returns true if the heap contains the given value; otherwise false.
    /// Time complexity: O( n )
    /// </summary>
    public bool Contains(T value)
    {
        if (value.CompareTo(array[0]) < 0) return false;

        for (int i = 0; i < Count; i++)
        {
            if (array[i].CompareTo(value) == 0)
            {
                return true;
            }
        }

        return false;
    }

    // TODO
    /// <summary>
    /// Updates the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Update(T oldValue, T newValue)
    {
        if (IsEmpty || oldValue.CompareTo(array[0]) < 0)
        {
            throw new InvalidOperationException();
        }

        // find the node to update - O(n)
        for (int i = 0; i < Count; i++)
        {
            if (oldValue.CompareTo(array[i]) == 0)
            {
                array[i] = newValue;

                // trickleX
                if (array[i].CompareTo(array[Parent(i)]) < 0)
                {
                    TrickleUp(i);
                }
                TrickleDown(i);
                return;
            }
        }
        throw new InvalidOperationException();

    }


    /// <summary>
    /// Removes the first element with the given value from the heap.
    /// Time complexity: O( n )
    /// </summary>
    public void Remove(T value)
    {
        if (IsEmpty)
        {
            throw new InvalidOperationException();
        }

        // find the node to remove
        for (int i = 0; i < Count; i++)
        {
            if (value.CompareTo(array[i]) == 0)
            {
                // swap with last
                Swap(i, Count-1);
                Count--;

                // If it was the last element, skip
                if (i < Count)
                {
                    // trickleX
                    if (array[i].CompareTo(array[Parent(i)]) < 0)
                    {
                        TrickleUp(i);
                    }
                    TrickleDown(i);
                }

                return;
            }
        }
    }


    // Time Complexity: O( log n )
    private void TrickleUp(int index)
    {
        if (index == 0) return;

        if (array[index].CompareTo(array[Parent(index)]) < 0)
        {
            Swap(index, Parent(index));
            TrickleUp(Parent(index));
        }
    }


    // Time Complexity: O( log n )
    private void TrickleDown(int index)
    {
        int leftIndex = LeftChild(index);
        int rightIndex = RightChild(index);

        // Following commented code shows what the children are and whether or not they're Out Of Bounds
        // if (leftIndex > Count-1)
        // {
        //     if (rightIndex > Count-1)
        //     {
        //         System.Console.WriteLine($"> Trickle {array[index]}, Left: OOB, Right: OOB");
        //     }
        //     else System.Console.WriteLine($"> Trickle {array[index]}, Left: OOB, Right: {array[rightIndex]}");
        // }
        // else if (rightIndex > Count-1) System.Console.WriteLine($"> Trickle {array[index]}, Left: {array[leftIndex]}, Right: OOB");
        // else System.Console.WriteLine($"> Trickle {array[index]}, Left: {array[leftIndex]}, Right: {array[rightIndex]}");

        // If the left child is the last in the list, then there is no right child or further children and we can safely end with it on the left.
        // System.Console.WriteLine($"Left index last in list: {leftIndex == Count-1}, {array[index]} > {array[leftIndex]} = {array[index].CompareTo(array[leftIndex]) > 0}");
        if (leftIndex == Count-1 && array[index].CompareTo(array[leftIndex]) > 0)
        {
            // System.Console.WriteLine($"Setting down {array[index]} to left {array[leftIndex]} because {array[index]} > {array[leftIndex]} = {array[index].CompareTo(array[leftIndex]) > 0}.");
            Swap(index, leftIndex);
            return;
        }

        // If the left child or right child are out of bounds, then there is nothing left to do.
        if (leftIndex >= Count || rightIndex >= Count)
        {
            // System.Console.WriteLine($"Did not trickle down {array[index]}.");
            return;
        }

        // Decide which of the children to swap with and continue trickling.
        if (array[leftIndex].CompareTo(array[rightIndex]) < 0 && array[index].CompareTo(array[leftIndex]) > 0)
        {
            // System.Console.WriteLine($"Trickling down {array[index]} to {array[leftIndex]}.");
            Swap(index, leftIndex);
            TrickleDown(leftIndex);
        }
        else if (array[index].CompareTo(array[rightIndex]) > 0)
        {
            // System.Console.WriteLine($"Trickling down {array[index]} to {array[rightIndex]}.");
            Swap(index, rightIndex);
            TrickleDown(rightIndex);
        }
    }

    /// <summary>
    /// Gives the position of a node's parent, the node's position in the heap.
    /// </summary>
    private static int Parent(int position)
    {
        return (position - 1) / 2;
    }


    /// <summary>
    /// Returns the position of a node's left child, given the node's position.
    /// </summary>
    private static int LeftChild(int position)
    {
        return 2 * position + 1;
    }


    /// <summary>
    /// Returns the position of a node's right child, given the node's position.
    /// </summary>
    private static int RightChild(int position)
    {
        return 2 * position + 2;
    }

    private void Swap(int index1, int index2)
    {
        var temp = array[index1];

        array[index1] = array[index2];
        array[index2] = temp;
    }

    private void DoubleArrayCapacity()
    {
        Array.Resize(ref array, array.Length * 2);
    }
}