using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PersonaGame : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 position;
    private float timeCount = 0.0f;
    bool calculated = false;
    public GameObject goal;
    public GameObject interest;
    public GameObject painPoint;
    public GameObject Motivation;
    public GameObject answer1;
    public GameObject answer2;
    public GameObject answer3;
    public GameObject answer4;
    public GameObject answer5;
    public GameObject answer6;
    private float[] goalLoc;
    private float[] interestLoc;
    private float[] painPointLoc;
    private float[] MotivationLoc;
    private Vector2[] hiddenAnswersPosition;
    private Vector2[] answersPositions;
    private bool[] correctAnswers;
    public Canvas canvas;
    static bool interviewed = false;
    static bool isPersonaPage = false;
    static bool correctAnswer1 = false;
    static bool isUsed1 = false;
    static bool isUsed2 = false;
    static bool isUsed3 = false;
    static bool isUsed4 = false;
    static string usedAnswer1 = "";
    static string usedAnswer2 = "";
    static string usedAnswer3 = "";
    static string usedAnswer4 = "";
    static Vector3 pos1 = new Vector3();
    static Vector3 pos2 = new Vector3();
    static Vector3 pos3 = new Vector3();
    static Vector3 pos4 = new Vector3();
    static Vector3 pos5 = new Vector3();
    static Vector3 pos6 = new Vector3();
    static Vector3 startPos1 = new Vector3();
    static Vector3 startPos2 = new Vector3();
    static Vector3 startPos3 = new Vector3();
    static Vector3 startPos4 = new Vector3();
    static Vector3 startPos5 = new Vector3();
    static Vector3 startPos6 = new Vector3();
    static string stage = "stage1";
    static string selectedAbout = "";
    static string selectedQuote = "";

    void Start()
    {
        if(isPersonaPage)
        {
            UpdateAnsersPositions();
            if (!interviewed)
            {
                setAnswersColor();
            }
        }
    }

    public void restart()
    {
          //Vector3 position;
          timeCount = 0.0f;
          calculated = false;
          interviewed = false;
          isPersonaPage = true;
          correctAnswer1 = false;
          isUsed1 = false;
          isUsed2 = false;
          isUsed3 = false;
          isUsed4 = false;
          usedAnswer1 = "";
          usedAnswer2 = "";
          usedAnswer3 = "";
          usedAnswer4 = "";
          pos1 = new Vector3();
          pos2 = new Vector3();
          pos3 = new Vector3();
          pos4 = new Vector3();
          pos5 = new Vector3();
          pos6 = new Vector3();
          stage = "stage1";
          selectedAbout = "";
          selectedQuote = "";
    }

    public void Proceed()
    {
        if(isUsed1 && isUsed2 && isUsed3 && isUsed4 && stage == "stage1")
        {
            isPersonaPage = false;
            if (getCorrectAnswersCount() == 4 && stage != "stage3")
            {
                isPersonaPage = false;
                stage = "stage2";
                ChangeScene("stage");
            }
            else
            {
                SetInterviewStarted();
                ChangeScene("feedback");
            }
        } 
        else if(selectedAbout != "" && stage == "stage2")
        {
            if (selectedAbout == "answerA" || selectedAbout == "answerC")
            {
                stage = "stage3";
                ChangeScene("stage");
            }
            else
            {
                ChangeScene("feedback");
            }
        }
        else if (selectedQuote != "")
        {
            ChangeScene("finalFeedback");
        }
    }

    public void ChangeScene(string target)
    {
        if(target == "stage")
        {
            SceneManager.LoadScene(stage);
        } 
        else
        {
            SceneManager.LoadScene(target);
        }
        if (isPersonaPage)
        {
            pos1 = answer1.GetComponent<RectTransform>().position;
            pos2 = answer2.GetComponent<RectTransform>().position;
            pos3 = answer3.GetComponent<RectTransform>().position;
            pos4 = answer4.GetComponent<RectTransform>().position;
            pos5 = answer5.GetComponent<RectTransform>().position;
            pos6 = answer6.GetComponent<RectTransform>().position;
            correctAnswer1 = true;
        }
    }

    public void SetInterviewStarted()
    {
        interviewed = true;
        isPersonaPage = false;
    }

    public void setPersonaPage()
    {
        isPersonaPage = true;
    }

    public void OnDrag(PointerEventData data)
    {
        if (transform.name != "Canvas")
        {
            if(interviewed)
            {
                if (data.dragging)
                {
                    // Object is being dragged.
                    timeCount += Time.deltaTime;
                    if (timeCount > 0.25f)
                    {
                        timeCount = 0.0f;
                    }
                }
                transform.position = Input.mousePosition;
            }
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (transform.name != "Canvas" && interviewed)
        {
            Vector2 mouse = Input.mousePosition;
            if (!isUsed1 && interviewed && goalLoc[0] < mouse.x && goalLoc[2] < mouse.y && goalLoc[1] > mouse.x && goalLoc[3] > mouse.y)
            {
                goal.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                transform.position = hiddenAnswersPosition[0];
                clearPrevSpot();
                usedAnswer1 = transform.name;
                isUsed1 = true;
            }
            else if (!isUsed2 && interviewed && interestLoc[0] < mouse.x && interestLoc[2] < mouse.y && interestLoc[1] > mouse.x && interestLoc[3] > mouse.y)
            {
                interest.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                transform.position = hiddenAnswersPosition[1];
                clearPrevSpot();
                usedAnswer2 = transform.name;
                isUsed2 = true;
            }
            else if (!isUsed3 && interviewed && painPointLoc[0] < mouse.x && painPointLoc[2] < mouse.y && painPointLoc[1] > mouse.x && painPointLoc[3] > mouse.y)
            {
                painPoint.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                transform.position = hiddenAnswersPosition[2];
                clearPrevSpot();
                usedAnswer3 = transform.name;
                isUsed3 = true;
            }
            else if (!isUsed4 && interviewed && MotivationLoc[0] < mouse.x && MotivationLoc[2] < mouse.y && MotivationLoc[1] > mouse.x && MotivationLoc[3] > mouse.y)
            {
                Motivation.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
                transform.position = hiddenAnswersPosition[3];
                clearPrevSpot();
                usedAnswer4 = transform.name;
                isUsed4 = true;
            } else
            {
                clearPrevSpot();
                if (transform.name == "Answer1")
                {
                    transform.position = startPos1;
                }
                else if (transform.name == "Answer2")
                {
                    transform.position = startPos2;
                }
                else if (transform.name == "Answer3")
                {
                    transform.position = startPos3;
                }
                else if (transform.name == "Answer4")
                {
                    transform.position = startPos4;
                }
                else if (transform.name == "Answer5")
                {
                    transform.position = startPos5;
                }
                else if (transform.name == "Answer6")
                {
                    transform.position = startPos6;
                }
            }
        }
    }

    private void clearPrevSpot()
    {
        if(transform.name == usedAnswer1)
                {
            usedAnswer1 = "";
            isUsed1 = false;
            correctAnswers[0] = false;
            goal.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (transform.name == usedAnswer2)
        {
            usedAnswer2 = "";
            isUsed2 = false;
            correctAnswers[1] = false;
            interest.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (transform.name == usedAnswer3)
        {
            usedAnswer3 = "";
            isUsed3 = false;
            correctAnswers[2] = false;
            painPoint.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
        else if (transform.name == usedAnswer4)
        {
            usedAnswer4 = "";
            isUsed4 = false;
            correctAnswers[3] = false;
            Motivation.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
        }
    }

    private void UpdateAnsersPositions()
    {
        if (!calculated)
        {
            Vector2 position = goal.GetComponent<RectTransform>().position;
            Vector2 size = goal.GetComponent<RectTransform>().sizeDelta;
            goalLoc = new float[] { 
                position.x - (size.x / 2) * canvas.scaleFactor,
                position.x + (size.x / 2) * canvas.scaleFactor,
                position.y - (size.y / 2) * canvas.scaleFactor,
                position.y + (size.y / 2) * canvas.scaleFactor,
            };
            position = interest.GetComponent<RectTransform>().position;
            size = interest.GetComponent<RectTransform>().sizeDelta;
            interestLoc = new float[] {
                position.x - (size.x / 2) * canvas.scaleFactor,
                position.x + (size.x / 2) * canvas.scaleFactor,
                position.y - (size.y / 2) * canvas.scaleFactor,
                position.y + (size.y / 2) * canvas.scaleFactor,
            };
            position = painPoint.GetComponent<RectTransform>().position;
            size = painPoint.GetComponent<RectTransform>().sizeDelta;
            painPointLoc = new float[] {
                position.x - (size.x / 2) * canvas.scaleFactor,
                position.x + (size.x / 2) * canvas.scaleFactor,
                position.y - (size.y / 2) * canvas.scaleFactor,
                position.y + (size.y / 2) * canvas.scaleFactor,
            };
            position = Motivation.GetComponent<RectTransform>().position;
            size = Motivation.GetComponent<RectTransform>().sizeDelta;
            MotivationLoc = new float[] {
                position.x - (size.x / 2) * canvas.scaleFactor,
                position.x + (size.x / 2) * canvas.scaleFactor,
                position.y - (size.y / 2) * canvas.scaleFactor,
                position.y + (size.y / 2) * canvas.scaleFactor,
            };
            answersPositions = new Vector2[]
            {
                answer1.GetComponent<RectTransform>().position,
                answer2.GetComponent<RectTransform>().position,
                answer3.GetComponent<RectTransform>().position,
                answer4.GetComponent<RectTransform>().position,
                answer5.GetComponent<RectTransform>().position,
                answer6.GetComponent<RectTransform>().position
            };
            hiddenAnswersPosition = new Vector2[]
            {
                goal.GetComponent<RectTransform>().position,
                interest.GetComponent<RectTransform>().position,
                painPoint.GetComponent<RectTransform>().position,
                Motivation.GetComponent<RectTransform>().position,
            };
            correctAnswers = new bool[] { isUsed1, isUsed2, isUsed3, isUsed4 };
            calculated = true;
            SetAnswersPositions();
        }
    }

    private void SetAnswersPositions()
    {
        if (correctAnswer1)
        {
            if (usedAnswer1 == answer1.name || usedAnswer2 == answer1.name || usedAnswer3 == answer1.name || usedAnswer4 == answer1.name)
            {
                answer1.GetComponent<RectTransform>().position = pos1;
            }
            if (usedAnswer1 == answer2.name || usedAnswer2 == answer2.name || usedAnswer3 == answer2.name || usedAnswer4 == answer2.name)
            {
                answer2.GetComponent<RectTransform>().position = pos2;
            }
            if (usedAnswer1 == answer3.name || usedAnswer2 == answer3.name || usedAnswer3 == answer3.name || usedAnswer4 == answer3.name)
            {
                answer3.GetComponent<RectTransform>().position = pos3;
            }
            if (usedAnswer1 == answer4.name || usedAnswer2 == answer4.name || usedAnswer3 == answer4.name || usedAnswer4 == answer4.name)
            {
                answer4.GetComponent<RectTransform>().position = pos4;
            }
            if (usedAnswer1 == answer5.name || usedAnswer2 == answer5.name || usedAnswer3 == answer5.name || usedAnswer4 == answer5.name)
            {
                answer5.GetComponent<RectTransform>().position = pos5;
            }
            if (usedAnswer1 == answer6.name || usedAnswer2 == answer6.name || usedAnswer3 == answer6.name || usedAnswer4 == answer6.name)
            {
                answer6.GetComponent<RectTransform>().position = pos6;
            }
            if (isUsed1)
            {
                goal.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
            if (isUsed2)
            {
                interest.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
            if (isUsed3)
            {
                painPoint.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
            if (isUsed4)
            {
                Motivation.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
            }
        }
    }

    private void setAnswersColor()
    {
        answer1.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        answer2.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        answer3.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        answer4.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        answer5.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        answer6.GetComponent<Image>().color = new Color32(255, 255, 255, 0);
        startPos1 = answer1.GetComponent<RectTransform>().position;
        startPos2 = answer2.GetComponent<RectTransform>().position;
        startPos3 = answer3.GetComponent<RectTransform>().position;
        startPos4 = answer4.GetComponent<RectTransform>().position;
        startPos5 = answer5.GetComponent<RectTransform>().position;
        startPos6 = answer6.GetComponent<RectTransform>().position;
    }

    private int getCorrectAnswersCount()
    {
        int count = 0;
        if(usedAnswer1 == "Answer5")
        {
            count++;
        }
        if (usedAnswer2 == "Answer6")
        {
            count++;
        }
        if (usedAnswer3 == "Answer4")
        {
            count++;
        }
        if (usedAnswer4 == "Answer1")
        {
            count++;
        }
        return count;
   }

   public void UseAbout(string about)
    {
        selectedAbout = about;
    }

    public void UseQuote(string quote)
    {
        selectedQuote = quote;
    }

}
