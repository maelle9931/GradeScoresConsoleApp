

namespace GradeScores
{
    public class PersonAndScore
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Score { get; set; } //example contains integers but requirements don't specify whether scores will be integers only so going with doubles

        //expects valid input
        public PersonAndScore(string lastName, string firstName, double score)
        {

            LastName = lastName;
            FirstName = firstName;
            Score = score;

        }


    }


}
