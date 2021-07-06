using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;

namespace nameSorter
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please either enter a .txt file on its own or a list of words separated by a comma and space");

            List<string> unsortedList = new List<String>();

            //Check if there any input
            if (args == null || args.Length == 0)
            {
                Console.WriteLine("You haven't give any input");
                return;
            }
            //If inputing a .txt file
            else if (args != null && args.Length == 1)
            {
                string[] fileToOpen = File.ReadAllLines(args[0]);
                ListOfNames takeUnsortedList = new ListOfNames();
                unsortedList = takeUnsortedList.NamesList(fileToOpen);
            }
            //If writing list in to args
            else if (args != null && args.Length > 1)
            {
                List<string> wordList = new List<string>();
                foreach (string word in args)
                {
                    wordList.Add(word.TrimEnd(','));
                }
                unsortedList = wordList;
            }

            //Sort list whichever way it is given (txt or args)
            ISortingNames theSorter = new AscendingSorter();
            ISortingNames theSorter2 = new DescendingSorter();

            var sortedListAsc = theSorter.Sort(unsortedList);
            var sortedListDesc = theSorter2.Sort(unsortedList);

            var finalList = sortedListAsc.Concat(sortedListDesc);

            //Save file
            SaveToFile finalSave = new SaveToFile();
            var fileToSave = finalSave.SaveSortedList(finalList.ToList());

            //Keep the console window open
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();

        }//End of Main

    }//End of Program

    public class ListOfNames
    {
        public List<string> NamesList(string[] textInFile)
        {
            List<string> nameList = new List<string>();

            //Add each line of the file to the name list
            foreach (string line in textInFile)
            {
                //Make sure the name has at least 2 names (first/last) but does not exceed 4 (3first/1last max) 
                if (line.Count(Char.IsWhiteSpace) <= 3 && line.Count(Char.IsWhiteSpace) >= 1)
                {
                    nameList.Add(line);
                }
                else
                {
                    Console.WriteLine("Invalid name inserted");
                }
            }
            return nameList;

        }//End of NamesList

    }//End of ListOfNames


    public interface ISortingNames
    {
        List<string> Sort(List<string> listToSort);
    }//End of ISortingNames

    public class AscendingSorter : ISortingNames
    {
        public List<string> Sort(List<string> listToSort)
        {
            //Order the names list first by surname, then by first name if surnames are the same
            listToSort = listToSort.OrderBy(x => x.Split(' ').Last()).ThenBy(x => x).ToList();
            //Print to Console
            Console.WriteLine("Names in ascending order:");
            listToSort.ForEach(i => Console.WriteLine("{0}\n", i));
            return listToSort;

        }//End of Sort

    }//End of AscendingSorter

    public class DescendingSorter : ISortingNames
    {
        public List<string> Sort(List<string> listToSort)
        {
            //Order the names list first by surname, then by first name if surnames are the same
            listToSort = listToSort.OrderByDescending(x => x.Split(' ').Last()).ThenBy(x => x).ToList();
            //Print to Console
            Console.WriteLine("Names in descending order:");
            listToSort.ForEach(i => Console.WriteLine("{0}\n", i));
            return listToSort;

        }//End of Sort 

    }//End of DescendingSorter

    public class SaveToFile
    {
        public List<string> SaveSortedList(List<string> fileToSave)
        {
            //Save file in same dir as unsorted list
            string path = Directory.GetCurrentDirectory() + @"\" + "sorted-names-list.txt";
            using (StreamWriter newNamesList = File.CreateText(path))
            {
                foreach (string orderedName in fileToSave)
                {
                    newNamesList.WriteLine(orderedName);
                }
            }
            return fileToSave;

        }//End of SaveSortedList

    }//End of SaveToFile
}
