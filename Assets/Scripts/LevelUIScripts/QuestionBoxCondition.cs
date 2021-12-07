//FileName: QuestionBoxCondition.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: QuestionBoxCondition contains the function to display the questions and check if the correct answer is chosen or not.   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Linq;
using System;
using UnityEngine.UI;
public class QuestionBoxCondition : MonoBehaviour
{
    QuestionInteraction questionInteraction;
    LevelManager levelManager;
    FeatureButtonDetection featureButtonDetection;
    public bool activated = false;
    public int QuestionNumber;
    string question;
    string answer1;
    string answer2;
    string answer3;
    string answer4;
    string correctAnswer;
    private GameObject[] buttons;
    /* Method Name: Start()
     * Summary: Get the game object "LevelManager"'s script "QuestionInteraction" (a script attatched to the LevelManager) and other game elements. 
     * @param N/A
     * @return N/A
     * Special Effects: Assign variables to different game elements.
     */
    void Start()
    {
        questionInteraction= GameObject.Find("LevelManager").GetComponent<QuestionInteraction>();
        levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        featureButtonDetection = GameObject.Find("LevelManager").GetComponent<FeatureButtonDetection>();
        buttons = levelManager.buttons;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    /* Method Name: checkBoxQuestionStatus()
     * Summary: If the question box wasn't answered yet, it set the question canvas to active. It calls functions in questionInteraction
     *          scripts to get the string value to display on the buttons and question panel, and check which button is correct or not.
     * @param N/A
     * @return N/A
     * Special Effects: Assign different texts to buttons and question panel.
     */
    public void checkBoxQuestionStatus()
    {
        if (!activated) {
            activated = true;
            levelManager.QuestionCanvas.transform.GetChild(1).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            levelManager.QuestionCanvas.transform.GetChild(2).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            levelManager.QuestionCanvas.transform.GetChild(3).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            levelManager.QuestionCanvas.transform.GetChild(4).gameObject.GetComponent<Image>().color = new Vector4(1, 1, 1, 1);
            question =questionInteraction.getQuestion(QuestionNumber);
            answer1=questionInteraction.getAnswer1(QuestionNumber);
            answer2=questionInteraction.getAnswer2(QuestionNumber);
            answer3=questionInteraction.getAnswer3(QuestionNumber);
            answer4=questionInteraction.getAnswer4(QuestionNumber);
            correctAnswer = questionInteraction.getCorrectAnswer(QuestionNumber);
            questionInteraction.QuestionBox.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = question;
            questionInteraction.AnswerButton1.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer1;
            questionInteraction.AnswerButton2.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer2;
            questionInteraction.AnswerButton3.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer3;
            questionInteraction.AnswerButton4.transform.GetChild(0).gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = answer4;
            checkCorrectAnswer(questionInteraction.AnswerButton1, 1);
            checkCorrectAnswer(questionInteraction.AnswerButton2, 2);
            checkCorrectAnswer(questionInteraction.AnswerButton3, 3);
            checkCorrectAnswer(questionInteraction.AnswerButton4, 4);
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i].GetComponent<Button>().interactable = false;
            }
            levelManager.QuestionCanvas.SetActive(true);
        }
    }
    /* Method Name: checkCorrectAnswer(GameObject button, int buttonNum)
     * Summary: Check if the button is the correct answer or not.
     * @param button: The answer button.
     * @param buttonNum: The number of the answer button.
     * @return N/A
     * Special Effects: Assign right or wrong to the button's RightOrWrong variable.
     */
    void checkCorrectAnswer(GameObject button, int buttonNum) {

        if (buttonNum == int.Parse(correctAnswer)) {
            button.GetComponent<ButtonRightOrWrong>().RightOrWrong = "right";
        } else{
            button.GetComponent<ButtonRightOrWrong>().RightOrWrong = "wrong";
        }
    }
}
