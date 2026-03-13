using System;

namespace Lab3;
class Program
{
    static void Main(string[] args)
    {
        int[] array = { 1, 2, 3, 4, 5, 6, 7 };
        MaxHeap<int> heap1 = new MaxHeap<int>(array);
        // Assert.AreEqual(1, heap1.ExtractMin());
        System.Console.WriteLine($"Expected: 1, Actual: {heap1.ExtractMin()}");
        // Assert.AreEqual(2, heap1.ExtractMin());
        System.Console.WriteLine($"Expected: 2, Actual: {heap1.ExtractMin()}");
        // Assert.AreEqual(3, heap1.ExtractMin());
        System.Console.WriteLine($"Expected: 3, Actual: {heap1.ExtractMin()}");

        
    }
}


