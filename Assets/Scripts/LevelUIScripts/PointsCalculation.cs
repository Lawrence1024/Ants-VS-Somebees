//FileName: PointsCalculation.cs
//FileType: C# File
//Author: Karen Shieh, Lawrence Shieh
//Date: Feb. 26, 2021
//Description: PointsCalculation contains the function to count down points in a game level.   
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsCalculation : MonoBehaviour
{
    LevelManager levelManager;
    FeatureButtonDetection featureButtonDetection;
    public int points = 1000;
    public bool levelComplete = false;
    public bool gamePause = false;
    /* Method Name: Start()
     * Summary: Set the poitns value on the canvas to the input points (1000). Call pointsCountDown() to count down the points.
     * @param N/A
     * @return N/A
     * Special Effects: Initial display of the points. 
     */
    void Start()
    {
        levelManager= GameObject.Find("LevelManager").GetComponent<LevelManager>();
        featureButtonDetection = GameObject.Find("LevelManager").GetComponent<FeatureButtonDetection>();
        points = 1000;
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
        StartCoroutine(pointsCountDown());
    }
    // Update is called once per frame
    void Update()
    {

    }
    /* Method Name: pointsCountDown()
     * Summary: The point value minus 1 every .12 seconds. If the game is paused or completed or the points value is <=0 the counter
     *          stops counting down.
     * @param N/A
     * @return N/A
     * Special Effects: Display of the points. 
     */
    public IEnumerator pointsCountDown()
    {
        if (!levelComplete && !gamePause&& points > 0) {
            points--;
        }
        yield return new WaitForSeconds(.12f);
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text =points.ToString();
        if (points > 0&& !levelComplete&&!gamePause)
        {
            StartCoroutine(pointsCountDown());
        }else if (points<=0) {
            gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = points.ToString();
            featureButtonDetection.tipButton.SetActive(true);
        }
    }
}
