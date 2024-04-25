using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TutorialScript : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI tutorialText;
    [SerializeField] private GameObject tutorialTextObj;
    [SerializeField] private GameObject earthOrbPrefab;
    [SerializeField] private GameObject fireOrbPrefab;
    [SerializeField] private GameObject tutorialBackground;
    [SerializeField] private GameObject tutorialSkipButton;
    [SerializeField] private string[] tutorialMergeArray;
    [SerializeField] private string[] tutorialReactionArray;
    [SerializeField] private string[] tutorialEndingArray;
    private Vector3 startPos = new Vector3(0, -7.779999f, 0);
    private Vector3 fallPos = new Vector3(0, -2.779999f, 0);
    private float textSpeed = 0.1f;
    private int mergeTaps = 0;
    private int reactTaps = 0;
    private int endingTaps = 0;
    private int mergeIndex;
    private int reactIndex;
    private int endingIndex;
    private bool isReactTutorial;
    private bool isTutorialEnding;
    public bool tutorialEnded;

    private void Start()
    {
        TutorialSetUp();
    }
    private void Update()
    {
        if(GameData.Instance.hasDoneTutorial == false)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Ended)
                {
                    if (isReactTutorial == false)
                    {
                        if (tutorialText.text == tutorialMergeArray[mergeIndex])
                        {
                            NextLine();
                        }
                        else
                        {
                            StopAllCoroutines();
                            tutorialText.text = tutorialMergeArray[mergeIndex];
                        }
                        if (tutorialText.text == tutorialMergeArray[3])
                        {
                            mergeTaps++;
                            if (mergeTaps == 2)
                            {
                                Instantiate(earthOrbPrefab, fallPos, transform.rotation);
                                StartCoroutine(ReactWaitTimer());

                            }
                        }
                    }
                    if (isReactTutorial && isTutorialEnding == false)
                    {
                        if (tutorialText.text == tutorialReactionArray[reactIndex])
                        {
                            NextLine();
                        }
                        else
                        {
                            StopAllCoroutines();
                            tutorialText.text = tutorialReactionArray[reactIndex];
                        }
                        if (tutorialText.text == tutorialReactionArray[5])
                        {
                            reactTaps++;
                            if (reactTaps == 2)
                            {
                                Instantiate(fireOrbPrefab, fallPos, transform.rotation);
                                StartCoroutine(EndingWaitTimer());
                            }
                        }
                    }
                    if (isTutorialEnding)
                    {
                        if (tutorialText.text == tutorialEndingArray[endingIndex])
                        {
                            NextLine();
                        }
                        else
                        {
                            StopAllCoroutines();
                            tutorialText.text = tutorialEndingArray[endingIndex];
                        }
                        if (tutorialText.text == tutorialEndingArray[2])
                        {
                            endingTaps++;
                            if (endingTaps == 2)
                            {
                                GameData.Instance.hasDoneTutorial = true;
                                GameData.Instance.Save();
                                this.enabled = false;
                                tutorialEnded = true;
                            }
                        }
                    }
                }
            }
        }
    }
    void StartDialouge()
    {
        if(isReactTutorial == false && isTutorialEnding == false)
        {
            mergeIndex = 0;
        }
        else if(isReactTutorial == true && isTutorialEnding == false)
        {
            reactIndex = 0;
        }
        else if(isTutorialEnding == true)
        {
            endingIndex = 0;
        }
        StartCoroutine(TypeLine());
    }
    IEnumerator TypeLine()
    {
        if(isReactTutorial == false && isTutorialEnding == false)
        {
            foreach (char c in tutorialMergeArray[mergeIndex].ToCharArray())
            {
                tutorialText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else if(isReactTutorial == true && isTutorialEnding == false)
        {
            foreach (char c in tutorialReactionArray[reactIndex].ToCharArray())
            {
                tutorialText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
        else if(isTutorialEnding)
        {
            foreach (char c in tutorialEndingArray[endingIndex].ToCharArray())
            {
                tutorialText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }
        }
    }
    IEnumerator ReactWaitTimer()
    {
        yield return new WaitForSeconds(4);
        tutorialText.text = string.Empty;
        isReactTutorial = true;
        tutorialBackground.SetActive(true);
        tutorialTextObj.SetActive(true);
        tutorialSkipButton.SetActive(true);
        StartDialouge();
    }
    IEnumerator EndingWaitTimer()
    {
        yield return new WaitForSeconds(4);
        tutorialText.text = string.Empty;
        isTutorialEnding = true;
        tutorialBackground.SetActive(true);
        tutorialTextObj.SetActive(true);
        tutorialSkipButton.SetActive(true);
        StartDialouge();
    }

    private void NextLine()
    {
        if (isReactTutorial == false && isTutorialEnding == false)
        {
            if (mergeIndex < tutorialMergeArray.Length - 1)
            {
                mergeIndex++;
                tutorialText.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                tutorialBackground.SetActive(false);
                tutorialTextObj.SetActive(false);
                tutorialSkipButton.SetActive(false);
            }
        }
        else if (isReactTutorial == true && isTutorialEnding == false)
        {
            if (reactIndex < tutorialReactionArray.Length - 1)
            {
                reactIndex++;
                tutorialText.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                tutorialBackground.SetActive(false);
                tutorialTextObj.SetActive(false);
                tutorialSkipButton.SetActive(false);
            }
        }
        else if (isTutorialEnding)
        {
            if (endingIndex < tutorialEndingArray.Length - 1)
            {
                endingIndex++;
                tutorialText.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                tutorialBackground.SetActive(false);
                tutorialTextObj.SetActive(false);
                tutorialSkipButton.SetActive(false);
            }
        }
    }
    public void TutorialSetUp()
    {
        mergeTaps = 0;
        reactTaps = 0;
        endingTaps = 0;
        isReactTutorial = false;
        isTutorialEnding = false;
        tutorialEnded = false;
        tutorialText.text = string.Empty;
        if (GameData.Instance.hasDoneTutorial == false)
        {
            tutorialBackground.SetActive(true);
            tutorialTextObj.SetActive(true);
            tutorialSkipButton.SetActive(true);
            Instantiate(earthOrbPrefab, startPos, transform.rotation);
            StartDialouge();
        }
    }
}
