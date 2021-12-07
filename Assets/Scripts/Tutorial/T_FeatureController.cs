//FileName: T_FeatureController.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: Control the feature buttons in the tutorial 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class T_FeatureController : MonoBehaviour
{
    public GameObject TCanvas;
    public T_TutorialFlowController TFController;
    public GameObject backBut;
    public GameObject resetBut;
    // Start is called before the first frame update
    /* Method Name: Start()
     * Summary: Initialize the TutorialFlowController variable.
     * @param N/A
     * @return N/A
     * Special Effects: N/A
     */
    void Start()
    {
        TFController = TCanvas.GetComponentInChildren<T_TutorialFlowController>();
    }

    // Update is called once per frame
    /* Method Name: Update()
    * Summary: Activate and deactivate different button at different tutorial steps.
    * @param N/A
    * @return N/A
    * Special Effects: N/A
    */
    void Update()
    {
        if (TFController.currentStep < 22 && (backBut.GetComponent<Button>().interactable == true || resetBut.GetComponent<Button>().interactable == true))
        {
            deactivateButtons();
        }
        if (TFController.currentStep == 22)
        {
            backBut.GetComponent<Button>().interactable = true;
        }
        else if (TFController.currentStep == 23)
        {
            backBut.GetComponent<Button>().interactable = false;
        }
        else if (TFController.currentStep == 24)
        {
            resetBut.GetComponent<Button>().interactable = true;
        }
        else if (TFController.currentStep == 25)
        {
            resetBut.GetComponent<Button>().interactable = false;
        }
    }
    /* Method Name: deactivateButtons()
    * Summary: To deactivate the back and reset button.
    * @param N/A
    * @return N/A
    * Special Effects: N/A
    */
    public void deactivateButtons()
    {
        backBut.GetComponent<Button>().interactable = false;
        resetBut.GetComponent<Button>().interactable = false;
    }
}
