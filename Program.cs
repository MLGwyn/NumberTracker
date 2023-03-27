using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace NumberTracker
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Number Tracker");

            // var numbers = new List<int>();
            // if (File.Exists("numbers.csv"))
            // {
            // Creates a stream reader to get information from our file
            var fileReader = new StreamReader("numbers.csv");
            // Create a configuration that indicates this CSV file has no header
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                // Tell the reader not to interpret the first
                // row as a "header" since it is just the
                // first number.
                HasHeaderRecord = false,
            };
            // Create a CSV reader to parse the stream into CSV format
            var csvReader = new CsvReader(fileReader, config);
            // Creates a list of numbers we will be tracking
            //
            //            reader
            //                      read rows from the stream
            //                                 each row is an int
            //                                        Give me back a List (List<int>)
            var numbers = csvReader.GetRecords<int>().ToList();

            // Close the reader
            fileReader.Close();
            // }

            // old code before adding fileReader
            // - Create an empty list of numbers.
            // var numbers = new List<int>();



            // Controls if we are still running our loop asking for more numbers
            var isRunning = true;

            // While we are running
            while (isRunning)
            {
                // Show the list of numbers
                Console.WriteLine("------------------");
                foreach (var number in numbers)
                {
                    Console.WriteLine(number);
                }
                Console.WriteLine($"Our list has: {numbers.Count()} entries");
                Console.WriteLine("------------------");

                // Ask for a new number or the word quit to end
                Console.Write("Enter a number to store, or 'quit' to end: ");
                var input = Console.ReadLine().ToLower();

                if (input == "quit")
                {
                    // If the input is quit, turn off the flag to keep looping
                    isRunning = false;
                }
                else
                {
                    // Parse the number and add it to the list of numbers
                    var number = int.Parse(input);
                    numbers.Add(number);
                }

            }
            var fileWriter = new StreamWriter("numbers.csv");
            // Create an object that can write CSV to the fileWriter
            var csvWriter = new CsvWriter(fileWriter, CultureInfo.InvariantCulture);
            // Ask our csvWriter to write out our list of numbers
            csvWriter.WriteRecords(numbers);
            // Tell the file we are done
            fileWriter.Close();
        }

    }
}
