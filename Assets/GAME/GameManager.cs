using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class GameManager : MonoBehaviour {

    public bool recording = true;

    private bool isPaused = false;
    private bool systemPause = false;
    private float fixedTimeStep;

    void Start () {
        fixedTimeStep = Time.fixedDeltaTime;

        PlayerPrefsManager.UnlockLevel (2);
        print (PlayerPrefsManager.IsLevelUnlocked (1));
        print (PlayerPrefsManager.IsLevelUnlocked (2));
    }
    
   
	// Update is called once per frame
	void Update () {
        if (CrossPlatformInputManager.GetButton ("Fire1")) {
            recording = false;
        } else {
            recording = true;
        }

        if (Input.GetKeyDown (KeyCode.P) && !isPaused) {
            PauseGame ();
            isPaused = true;
        } else if (Input.GetKeyDown (KeyCode.P) && isPaused) {
            ResumeGame ();
            isPaused = false;
        }
        if (systemPause) {
            PauseGame ();
        } else if(!systemPause) {
            ResumeGame ();
        }

//        print ("update");
    }

    void PauseGame () {
        Time.timeScale = 0f;
        Time.fixedDeltaTime = 0f;
    }

    void ResumeGame () {
        Time.timeScale = 1f; // also useful for slowmo stuff
        Time.fixedDeltaTime = fixedTimeStep; // should be the same value as in the Project Settings => Fixed Timestep
    }
    private void OnApplicationPause (bool pause) {
        systemPause = pause; // if the OS pause the game, the game is actually paused
    }
}
