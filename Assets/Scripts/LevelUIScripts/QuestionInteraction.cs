//FileName: QuestionInteraction.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: QuestionInteraction gets the infromation of the question bank. 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;
public class QuestionInteraction : MonoBehaviour
{
    List<QuestionLine> lines;
    LevelManager levelManager;
    public GameObject QuestionBox;
    public GameObject AnswerButton1;
    public GameObject AnswerButton2;
    public GameObject AnswerButton3;
    public GameObject AnswerButton4;
    struct QuestionLine {
        public string question;
        public string answer1;
        public string answer2;
        public string answer3;
        public string answer4;
        public string correctAnswer;
        public QuestionLine(string q, string a1, string a2, string a3, string a4, string ca)
        {
            question = q;
            answer1 = a1;
            answer2 = a2;
            answer3 = a3;
            answer4 = a4;
            correctAnswer = ca;
        }
    }
    /* Method Name: Start()
     * Summary: Get the game object "LevelManager"'s script "LevelManager" (a script attatched to the LevelManager). Call 
     *          LoadQuestion(string filename) to load the csv (question bank) file.
     * @param N/A
     * @return N/A
     * Special Effects: Create a new lines list.
     */
    void Start()
    {
        levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        lines = new List<QuestionLine>();
        LoadQuestion("QuestionData.csv");
    }
    // Update is called once per frame
    void Update()
    {

    }
    /* Method Name: loadQuestion()
     * Summary: Call LoadQuestion(string filename) to load the csv (question bank) file.
     * @param N/A
     * @return N/A
     * Special Effects: Create a new lines list.
     */
    public void loadQuestion() {
        lines = new List<QuestionLine>();
        LoadQuestion("QuestionData.csv");
        
    }
    /* Method Name: getQuestion(int num)
     * Summary: Get the line number question.
     * @param int num: The line number of the question.
     * @return lines[num].question: A string that contains the question.
     * Special Effects: N/A
     */
    public string getQuestion(int num) {
        return lines[num].question;
    }
    /* Method Name: getAnswer1(int num)
      * Summary: Get the line number aswer.
      * @param int num: The line number of the answer.
      * @return lines[num].answer1: A string that contains the answer.
      * Special Effects: N/A
      */
    public string getAnswer1(int num)
    {
        return lines[num].answer1;
    }
    /* Method Name: getAnswer2(int num)
      * Summary: Get the line number aswer.
      * @param int num: The line number of the answer.
      * @return lines[num].answer1: A string that contains the answer.
      * Special Effects: N/A
      */
    public string getAnswer2(int num)
    {
        return lines[num].answer2;
    }
    /* Method Name: getAnswer3(int num)
      * Summary: Get the line number aswer.
      * @param int num: The line number of the answer.
      * @return lines[num].answer1: A string that contains the answer.
      * Special Effects: N/A
      */
    public string getAnswer3(int num)
    {
        return lines[num].answer3;
    }
    /* Method Name: getAnswer4(int num)
      * Summary: Get the line number aswer.
      * @param int num: The line number of the answer.
      * @return lines[num].answer1: A string that contains the answer.
      * Special Effects: N/A
      */
    public string getAnswer4(int num)
    {
        return lines[num].answer4;
    }
    /* Method Name: getCorrectAnswer(int num)
      * Summary: Get the correct answer of the question.
      * @param int num: The line number of the answer.
      * @return lines[num].answer1: A string that contains the answer number.
      * Special Effects: N/A
      */
    public string getCorrectAnswer(int num)
    {
        return lines[num].correctAnswer;
    }
    /* Method Name: LoadQuestion(string filename)
      * Summary: Load the question and answer and put them into a list. 
      * @param filename: The name of the csv file.
      * @return N/A
      * Special Effects: List added
      */
    void LoadQuestion(string filename)
    {
        TextAsset questionData = Resources.Load<TextAsset>("QuestionData");
        string contents = questionData.text;
        byte[] byteArray = Encoding.UTF8.GetBytes(contents);
        MemoryStream stream = new MemoryStream(byteArray);
        string line;
        StreamReader r = new StreamReader(stream);
        using (r)
        {
            do
            {
                line = r.ReadLine();
                if (line != null)
                {
                    string[] line_values = SplitCsvLine(line);
                    QuestionLine line_entry = new QuestionLine(line_values[0].ToString(), line_values[1].ToString(), line_values[2].ToString(), line_values[3].ToString(), line_values[4].ToString(), line_values[5].ToString());
                    lines.Add(line_entry);
                }
            }
            while (line != null);
            r.Close();
        }
    }
    /* Method Name: SplitCsvLine(string line)
      * Summary: Split up the csv file. 
      * @param line: The line to split the csv file.
      * @return values: The cell value in the csv file. 
      * Special Effects: N/A
      */
    string[] SplitCsvLine(string line)
    {
        string pattern = @"
     # Match one value in valid CSV string.
     (?!\s*$)                                      # Don't match empty last value.
     \s*                                           # Strip whitespace before value.
     (?:                                           # Group for value alternatives.
       '(?<val>[^'\\]*(?:\\[\S\s][^'\\]*)*)'       # Either $1: Single quoted string,
     | ""(?<val>[^""\\]*(?:\\[\S\s][^""\\]*)*)""   # or $2: Double quoted string,
     | (?<val>[^,'""\s\\]*(?:\s+[^,'""\s\\]+)*)    # or $3: Non-comma, non-quote stuff.
     )                                             # End group of value alternatives.
     \s*                                           # Strip whitespace after value.
     (?:,|$)                                       # Field ends on comma or EOS.
     ";
        string[] values = (from Match m in Regex.Matches(line, pattern,
            RegexOptions.ExplicitCapture | RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline)
                           select m.Groups[1].Value).ToArray();
        return values;
    }
}
