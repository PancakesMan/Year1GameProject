using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Add or remove the bellow character distinction as needed
using UnityStandardAssets.Characters.ThirdPerson;

public class DialogueTrigger : MonoBehaviour {



    public List<string> dialogueLines = new List<string>();
    public List<string> altDialogueLines = new List<string>();
    List<string> dialogToUse = new List<string>();
    public bool altDialogueOption = false;

    public bool alreadyTriggered = false;
    // Bellow, add your character controller name. e.g. CharControlCustom charController;
    ThirdPersonUserControl_Custom charUserControl;
    public Text dialogueBox;
    int currentLine = 0;
    bool currentlyActive;
    float lastClickTime;
    public GameObject textBackground;
    public static bool changeTextIf = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by " + other.gameObject);
        if (other.tag == "Player" && !alreadyTriggered)
        {
            if (changeTextIf && altDialogueOption)
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
               // charUserControl.enabled = false;
            }            //charController.enabled = false;
            dialogueBox.text = dialogToUse[0];
            currentLine = 0;
            alreadyTriggered = true;
            currentlyActive = true;
            //Input system seems to be bugging out and giving getButtonDown message over multiple frames
            //putting in hacky time check to set a minimum time between dialogue lines to get around this
            lastClickTime = Time.time;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetButtonDown("Fire1") && other.tag == "Player" && currentlyActive && (Time.time > lastClickTime + 0.1f))
        {
            lastClickTime = Time.time;
            Debug.Log("next line triggered by " + other.gameObject + " at time " + Time.time);
            currentLine++;
            if (dialogToUse.Count > currentLine)
            {
                dialogueBox.text = dialogToUse[currentLine];
            }
            else
            {
                textBackground.SetActive(false);
                dialogueBox.text = "";
                charUserControl.userHasControl = true;
                currentlyActive = false;
            }
        }
    }

}
