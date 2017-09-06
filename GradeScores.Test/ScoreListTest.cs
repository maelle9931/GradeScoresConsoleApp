using System;
using System.Text;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace GradeScores.Test
{

    [TestFixture]
    public class GraderTest
    {

        const string SEPARATOR = ", ";


        #region ImportScoresFromFile_Tests
        [Test]
        public void ImportScoresFromFile_WithValidInput_ReturnsGraderWithScoreList()
        {
            const string FIRST_LINE = "KING, MADISON, 85";
            const string SECOND_LINE = "BUNDY, TERESSA, 88";


            StringBuilder sb = new StringBuilder();
            sb.Append(FIRST_LINE);
            sb.Append(Environment.NewLine);
            sb.Append(SECOND_LINE);


            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //we now need to check that the grader's scorelist matches what we loaded into the stream reader
                    Assert.IsTrue(grader.ScoreList.Count == 2);

                    //first item should be equal to the first line;
                    Assert.That(FIRST_LINE, Is.EqualTo(grader.ScoreList[0].LastName + SEPARATOR + grader.ScoreList[0].FirstName + SEPARATOR + grader.ScoreList[0].Score));

                    //second item should be equal to the second line
                    Assert.That(SECOND_LINE, Is.EqualTo(grader.ScoreList[1].LastName + SEPARATOR + grader.ScoreList[1].FirstName + SEPARATOR + grader.ScoreList[1].Score));
                }
            }
        }

        //Assuming negative numbers are a valid score
        [Test]
        public void ImportScoresFromFile_WithNegativeNumbers_ReturnsGraderWithScoreList()
        {
            const string FIRST_LINE = "KING, MADISON, -5";
            const string SECOND_LINE = "BUNDY, TERESSA, -1";


            StringBuilder sb = new StringBuilder();
            sb.Append(FIRST_LINE);
            sb.Append(Environment.NewLine);
            sb.Append(SECOND_LINE);


            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //we now need to check that the grader's scorelist matches what we loaded into the stream reader
                    Assert.IsTrue(grader.ScoreList.Count == 2);

                    //first item should be equal to the first line;
                    Assert.That(FIRST_LINE,Is.EqualTo(grader.ScoreList[0].LastName + SEPARATOR + grader.ScoreList[0].FirstName + SEPARATOR + grader.ScoreList[0].Score));

                    //second item should be equal to the second line
                    Assert.That(SECOND_LINE, Is.EqualTo(grader.ScoreList[1].LastName + SEPARATOR + grader.ScoreList[1].FirstName + SEPARATOR + grader.ScoreList[1].Score));
                }
            }
        }

        [Test]
        public void ImportScoresFromFile_WithJustOneLine_ReturnsGraderWithCorrectScoreList()
        {
            const string FIRST_LINE = "KING, MADISON, 85";



            StringBuilder sb = new StringBuilder();
            sb.Append(FIRST_LINE);


            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //we now need to check that the grader's scorelist matches what we loaded into the stream reader
                    Assert.IsTrue(grader.ScoreList.Count == 1);

                    //first item should be equal to the first line;
                    Assert.That(FIRST_LINE, Is.EqualTo(grader.ScoreList[0].LastName + SEPARATOR + grader.ScoreList[0].FirstName + SEPARATOR + grader.ScoreList[0].Score));


                }
            }
        }

        [Test]
        public void ImportScoresFromFile_WithEmptyFile_ReturnsEmptyScoreList()
        {



            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(String.Empty)))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //list should have a count of zero
                    Assert.IsTrue(grader.ScoreList.Count == 0);

                }
            }
        }

        [Test]
        public void ImportScoresFromFile_WithBlankLinesInInput_ReturnsGraderWithCorrectScoreList()
        {
            const string FIRST_LINE = "KING, MADISON, 85";
            const string SECOND_LINE = "BUNDY, TERESSA, 88";


            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(FIRST_LINE);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);
            sb.Append(SECOND_LINE);
            sb.Append(Environment.NewLine);
            sb.Append(Environment.NewLine);


            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //we now need to check that the grader's scorelist matches what we loaded into the stream reader
                    Assert.IsTrue(grader.ScoreList.Count == 2);

                    //first item should be equal to the first line;
                    Assert.That(FIRST_LINE, Is.EqualTo(grader.ScoreList[0].LastName + SEPARATOR + grader.ScoreList[0].FirstName + SEPARATOR + grader.ScoreList[0].Score));

                    //second item should be equal to the second line
                    Assert.That(SECOND_LINE, Is.EqualTo(grader.ScoreList[1].LastName + SEPARATOR + grader.ScoreList[1].FirstName + SEPARATOR + grader.ScoreList[1].Score));
                }
            }
        }

        [Test]
        public void ImportScoresFromFile_WithInvalidInput_ThrowsException()
        {
            const string FIRST_LINE = "KING, MADISON, 85";
            const string SECOND_LINE = "BUNDY, TERESSA, 88";

            bool expectedExceptionThrown = false;

            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(FIRST_LINE);
            sb.Append(SECOND_LINE);
            sb.Append("invalid input");
            sb.Append(Environment.NewLine);

            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();

                    try
                    {
                        grader.ImportScoresFromFile(streamReader);
                    }
                    catch (Exception e)
                    {
                        //exception has been thrown as expected
                        expectedExceptionThrown = true;


                    }

                    if (!expectedExceptionThrown)
                    {
                        Assert.Fail();
                    }

                }

            }
        }

        [Test]
        public void ImportScoresFromFile_WithInvalidScore_ThrowsException()
        {
            const string FIRST_LINE = "KING, MADISON, 85";
            const string SECOND_LINE = "BUNDY, TERESSA, 88";

            bool expectedExceptionThrown = false;

            StringBuilder sb = new StringBuilder();
            sb.Append(Environment.NewLine);
            sb.Append(FIRST_LINE);
            sb.Append(SECOND_LINE);
            sb.Append("LASTNAME, FIRSTNAME, INVALIDSCORE");
            sb.Append(Environment.NewLine);

            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();

                    try
                    {
                        grader.ImportScoresFromFile(streamReader);
                    }
                    catch (Exception e)
                    {
                        //exception has been thrown as expected
                        expectedExceptionThrown = true;


                    }

                    if (!expectedExceptionThrown)
                    {
                        Assert.Fail();
                    }

                }

            }
        }

        [Test]
        public void ImportScoresFromFile_WithLongList_ReturnsGraderCorrectCountOfScores()
        {
            const string FIRST_LINE = "KING, MADISON, 85";
            int numberOfLines = 100000;

            StringBuilder sb = new StringBuilder();
            for (int i = 1; i <= numberOfLines; i++)
            {
                sb.Append(FIRST_LINE);
                sb.Append(Environment.NewLine);
            }



            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //we now need to check that the grader's scorelist matches what we loaded into the stream reader
                    Assert.IsTrue(grader.ScoreList.Count == numberOfLines);

                }
            }
        }


        [Test]
        public void ImportScoresFromFile_WithUnixEndOfLine_ReturnsGraderCorrectCountOfScores()
        {
            const string FIRST_LINE = "KING, MADISON, 85";
            const string SECOND_LINE = "BUNDY, TERESSA, 88";


            StringBuilder sb = new StringBuilder();
            sb.Append(FIRST_LINE);
            sb.Append("\n");
            sb.Append(SECOND_LINE);



            //creating a streamReader to feed into ImportScoresFromFile
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(sb.ToString())))
            {

                using (var streamReader = new StreamReader(stream))
                {

                    var grader = new Grader();
                    grader.ImportScoresFromFile(streamReader);

                    //we now need to check that the grader's scorelist matches what we loaded into the stream reader
                    Assert.IsTrue(grader.ScoreList.Count == 2);
                }
            }

        }
        #endregion

        #region SortByScoreAndName_Tests


        [Test]
        public void SortByScoreAndName_SortsByScoreThenLastNameThenFullName()
        {
            //let's put in a good range so we check that it is sorting by score and full name and first name





            var grader = new Grader();
            var p1 = new PersonAndScore("SMITH", "ALLAN", -5);
            var p2 = new PersonAndScore("SMITH", "LEO", 100);
            var p3 = new PersonAndScore("BUNDY", "TERESA", 89);
            var p4 = new PersonAndScore("KING", "MADISON", 89);
            var p5 = new PersonAndScore("BUNDY", "SOPHIA", 89);
            var p6 = new PersonAndScore("BUNDY", "SOPHIA", 83);


            grader.ScoreList.Add(p1);
            grader.ScoreList.Add(p2);
            grader.ScoreList.Add(p3);
            grader.ScoreList.Add(p4);
            grader.ScoreList.Add(p5);
            grader.ScoreList.Add(p6);

            grader.SortByScoreAndName();

            //check that it has been sorted as expected: p2,p5,p3,p4,p6,p1

            Assert.AreEqual(p2, grader.ScoreList[0]);
            Assert.AreEqual(p5, grader.ScoreList[1]);
            Assert.AreEqual(p3, grader.ScoreList[2]);
            Assert.AreEqual(p4, grader.ScoreList[3]);
            Assert.AreEqual(p6, grader.ScoreList[4]);
            Assert.AreEqual(p1, grader.ScoreList[5]);


        }

        [Test]
        public void SortByScoreAndName_WithEmptyList_DoesntFail()
        {
            var grader = new Grader();
            grader.SortByScoreAndName();

            Assert.IsTrue(grader.ScoreList.Count == 0);

        }

        [Test]
        public void SortByScoreAndName_WithDuplicateInput_DoesntFail()
        {
            var grader = new Grader();
            var p1 = new PersonAndScore("SMITH", "ALLAN", 83);
            var p2 = new PersonAndScore("SMITH", "ALLAN", 83);
            grader.ScoreList.Add(p1);
            grader.ScoreList.Add(p2);
            grader.SortByScoreAndName();

            Assert.IsTrue(grader.ScoreList.Count == 2);

        }
        #endregion


        #region ExportToFile_Tests()

        [Test]
        public void ExportToFile_ReturnsCorrectOutputinStream()
        {
            var grader = new Grader();
            grader.ScoreList.Add(new PersonAndScore("BUNDY", "TERESA", 89));
            grader.ScoreList.Add(new PersonAndScore("BUNDY", "LUCY", 88));

            int expectedNumOfLines = grader.ScoreList.Count;

            using (var stream = new MemoryStream())
            {

                using (var streamWriter = new StreamWriter(stream))
                {
                    grader.ExportToFile(streamWriter);

                    //let's read what has been written

                    streamWriter.Flush();
                    stream.Position = 0;
                    using (var sr = new StreamReader(stream))
                    {
                        string output = sr.ReadToEnd();

                        //let's check output is as expected
                        string[] lines = output.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                        Assert.IsTrue(lines.Length == expectedNumOfLines + 1); //one more because of end of line at the end of the file
                        Assert.AreEqual("BUNDY" + SEPARATOR + "TERESA" + SEPARATOR + 89, lines[0]);
                        Assert.AreEqual("BUNDY" + SEPARATOR + "LUCY" + SEPARATOR + 88, lines[1]);
                    }

                }
            }
        }

        #endregion

    }
}
