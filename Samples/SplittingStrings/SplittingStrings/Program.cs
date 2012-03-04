using System;

namespace SplittingStrings
{
    class Program
    {
        static void Main(string[] args)
        {
            // Code block for testing out the splitting of strings using a file extension delimiter
            var sampleOne = @"E:\Temp\Projects\FYP\DicomTest.dcm";
            var sampleTwo = @"E:\Temp\Projects\FYP\DicomTest.png";
            var firstDelimiter = new[] {".dcm"};
            var secondDelimiter = new[] {".png"};

            var firstResult = sampleOne.Split(firstDelimiter, 2, StringSplitOptions.RemoveEmptyEntries);
            var secondResult = sampleTwo.Split(secondDelimiter, 2, StringSplitOptions.RemoveEmptyEntries);

            Console.WriteLine("Path of first file: {0}\nSplitting with delimiter of '{1}' becomes {2}\n", sampleOne, firstDelimiter[0], firstResult[0]);
            Console.WriteLine("Path of second file: {0}\nSplitting with delimiter of '{1}' becomes {2}", sampleTwo, secondDelimiter[0], secondResult[0]);

            // Code block for testing out attaching a new file extension
            var newFileName = ((sampleOne.Split(firstDelimiter, 2, StringSplitOptions.RemoveEmptyEntries))[0]) += ".png";

            // Ensure that the console does not simply disppear
            Console.ReadLine();
        }
    }
}
