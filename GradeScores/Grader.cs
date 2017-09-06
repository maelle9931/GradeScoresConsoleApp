using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GradeScores
{
    /* This class provides functionality for importing scores from a file and sorting them 
     input format should be FullName, FirstName, Score

    */

    public class Grader
    {
        public List<PersonAndScore> ScoreList { get; set; }

        public Grader()
        {
            //when a new grader is created, create a new empty ScoreList
            ScoreList = new List<PersonAndScore>();
        }

        public void ImportScoresFromFile(StreamReader file)
        {

            string line;
            int lineCounter = 1; //to log line number in case of invalid input




            while ((line = file.ReadLine()) != null)
            {
                //read each line

                //if line only made of whitespace chars, ignore it, otherwise validate and parse

                if (line.Trim() != String.Empty)
                {
                    string[] arr = line.Split(',');

                    if (arr.Length == 3) //should have three different parts
                    {
                        //make sure the last part is numerical - requirements don't say whether negative scores are allowed so allowing them
                        double x = 0;
                        if (double.TryParse(arr[2].Trim(), out x))
                        {
                            //all seems valid, add it to the list
                            PersonAndScore p = new PersonAndScore(arr[0].Trim(), arr[1].Trim(), x);
                            ScoreList.Add(p);
                        }
                        else
                        {
                            throw new Exception(String.Format("Invalid input at line {0}. Score must be a numerical value.", lineCounter));
                        }
                    }
                    else
                    {
                        throw new Exception(String.Format("Invalid input at line {0}.", lineCounter));
                    }
                }
                lineCounter++;
            }


        }



        //this will sort Scores by Score, LastName and First Name respectively

        public void SortByScoreAndName()
        {
            ScoreList = ScoreList.OrderByDescending(p => p.Score).ThenBy(p => p.LastName).ThenBy(p => p.FirstName).ToList();
        }


        //exports list of scores to file at fileLocation in the format LastName, FirstName, Score
        public void ExportToFile(StreamWriter sw)
        {

            // Create a file to write to.


            foreach (PersonAndScore p in ScoreList)
            {
                sw.WriteLine(p.LastName + ", " + p.FirstName + ", " + p.Score.ToString());
            }


        }

    }
}
