using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Add or remove the bellow character distinction as needed
using UnityStandardAssets.Characters.ThirdPerson;

public class DialogueTrigger : MonoBehaviour {

    List<string> dialogToUse = new List<string>();
    public List<string> dialogueLines = new List<string>();
    public List<string> altDialogueLines = new List<string>();
    public bool altDialogueOption = false;
    public bool alreadyTriggered = false;
    public float characterTimeout;

    // Below, add your character controller name. e.g. CharControlCustom charController;
    ThirdPersonUserControl_Custom charUserControl;
    public Text dialogueBox;
    public GameObject textBackground;

    int currentLine = 0;
    bool currentlyActive, cr_running = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by " + other.gameObject);
        if (other.tag == "Player" && !alreadyTriggered)
        {
            if (altDialogueOption)
            {
                dialogToUse = altDialogueLines;
            }
            else
            {
                dialogToUse = dialogueLines;
            }

            textBackground.SetActive(true);
            //Insert character controller name to dissable controls during dialogue 
            charUserControl = other.GetComponent<ThirdPersonUserControl_Custom>();
            if (charUserControl != null)
            {
                charUserControl.userHasControl = false;
            }

            StartCoroutine(DisplayText(dialogToUse[currentLine++]));
            alreadyTriggered = true;
            currentlyActive = true;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Fire1") && other.tag == "Player" && currentlyActive /*&& (Time.time > lastClickTime + 0.1f)*/ &&!cr_running)
        {
            dialogueBox.text = "";
            Debug.Log("next line triggered by " + other.gameObject + " at time " + Time.time);
            if (dialogToUse.Count > currentLine)
            {
                StartCoroutine(DisplayText(dialogToUse[currentLine++]));
            }
            else
            {
                textBackground.SetActive(false);
                charUserControl.userHasControl = true;
                currentlyActive = false;
            }
        }
    }

    IEnumerator DisplayText(string text)
    {
        cr_running = true;
        foreach(char c in text)
        {
            dialogueBox.text += c;
            yield return new WaitForSecondsRealtime(characterTimeout);
        }
        cr_running = false;
    }

}
