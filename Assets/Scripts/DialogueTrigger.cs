using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//Add or remove the bellow character distinction as needed
using UnityStandardAssets.Characters.ThirdPerson;

public class DialogueTrigger : MonoBehaviour {

    public List<string> dialogueLines = new List<string>(); // Dialogue being shown to the player
    public bool alreadyTriggered = false;                   // Has this cutscene been triggered before?
    public bool enableModeChangeUponCompletion = true;      // Can user switch from third to first person mode after?
    public float characterTimeout;                          // Time to wait between printing each letter of the line
    public bool autoAdvance = false;                               // Automatically play the next line

    ThirdPersonUserControl_Custom charUserControl;          // Player's Controller Script
    _31Toggle modeController;                               // Player's mode toggle script
    public Text dialogueBox;
    public GameObject textBackground;                       

    int currentLine = 0;       // Line of dialogue currently showing
    bool currentlyActive;      // Is the cutscene active?                            
    bool cr_running = false;   // Is the coroutine running?

    void Start()
    {
        modeController = GameObject.Find("PlagueDoctorPrefab").GetComponent<_31Toggle>();
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Trigger entered by " + other.gameObject);
        if (other.tag == "Player" && !alreadyTriggered)
        {
            modeController.active = false;  // Disable changing first/third person mode
            textBackground.SetActive(true); // active background for text

            // Get the player's controller script component
            charUserControl = other.GetComponent<ThirdPersonUserControl_Custom>();
            charUserControl.userHasControl = false; // Disable player controls

            // Start coroutine to display text 1 letter at a time
            StartCoroutine(DisplayText(dialogueLines[currentLine++]));
            alreadyTriggered = true; // Set cutscene trigger to activated
            currentlyActive = true;  // Cutscene is currently activated
        }
    }

    void OnTriggerStay(Collider other)
    {
        if ((Input.GetButtonDown("Fire1") || autoAdvance) && other.tag == "Player" && currentlyActive && !cr_running)
        {
            dialogueBox.text = "";
            Debug.Log("next line triggered by " + other.gameObject + " at time " + Time.time);
            if (dialogueLines.Count > currentLine)
            {
                StartCoroutine(DisplayText(dialogueLines[currentLine++]));
            }
            else
            {
                textBackground.SetActive(false);
                charUserControl.userHasControl = true;
                modeController.active = enableModeChangeUponCompletion;
                gameObject.SetActive(false);
                currentlyActive = false;
            }
        }
    }

    // Displays string one character at a time
    IEnumerator DisplayText(string text)
    {
        cr_running = true;       // Coroutine is running
        foreach(char c in text)
        {
            dialogueBox.text += c;
            yield return new WaitForSecondsRealtime(characterTimeout);
        }
        cr_running = false;      // Coroutine is not running
    }

}
