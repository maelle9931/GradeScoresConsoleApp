using System;
using System.IO;


namespace GradeScores
{
    class Program
    {
        static void Main(string[] args)
        {

            const string OUTPUT_FILE_ENDING = "-graded.txt";
           
            try
            {

                string fileLocation = args[0]; //get file location from first argument, ignore any subsequent arguments


                //check file exists
                if (!File.Exists(fileLocation))
                {
                    Console.WriteLine("File not found.");
                }
                else
                {

                    //create a new Grader and import file into it

                    var grader = new Grader();
                    using (StreamReader file = new StreamReader(fileLocation)) { 
                        grader.ImportScoresFromFile(file);
                    }

                    //sort score list
                    grader.SortByScoreAndName();

                    //export newly sorted list into output file - if the file exists already it will be overwritten
                    string outputFileLocation = Path.Combine(Path.GetDirectoryName(fileLocation), Path.GetFileNameWithoutExtension(fileLocation) + OUTPUT_FILE_ENDING);
                    using (StreamWriter sw = File.CreateText(outputFileLocation))
                    {
                        grader.ExportToFile(sw);
                    }

                    Console.WriteLine("Finished. Created " + outputFileLocation);
                   
                }
                
            }
            catch (Exception e)
            {
                Console.WriteLine("An error has occurred. Exception message is: " + e.Message);
            }
            
        }
    }
}
