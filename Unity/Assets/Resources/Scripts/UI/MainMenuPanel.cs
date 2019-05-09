using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuPanel : MonoBehaviour {

    public Button startButton;
    public Button tutorialButton;
    public Button aboutUsButton;

    // Use this for initialization
    void Start () {
        startButton.onClick.AddListener(() => OnClick(startButton));
        tutorialButton.onClick.AddListener(() => OnClick(tutorialButton));
        aboutUsButton.onClick.AddListener(() => OnClick(aboutUsButton));
    }


	
	// Update is called once per frame
	void Update () {
		
	}


    void OnClick(Button button)
    {
        //Logger.log("clicked");
        if (button == startButton)
        {
            Constants.dataContainer.SetCurrentState(States.RoomSelection);
        }
        else if (button == tutorialButton)
        {
            Constants.dataContainer.SetCurrentState(States.Tutorial);
        }
        else if (button == aboutUsButton)
        {
            Constants.dataContainer.SetCurrentState(States.AboutUs);
        }
    }
}
