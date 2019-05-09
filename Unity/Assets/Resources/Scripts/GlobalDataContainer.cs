using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

using GoogleARCore;

public class GlobalDataContainer : MonoBehaviour
{
    public Camera FirstPersonCamera;

    [SerializeField]
    private GameObject mainMenuPanel;
    [SerializeField]
    private GameObject roomSelectionPanel;
    [SerializeField]
    private GameObject tutorialPanel;
    [SerializeField]
    private GameObject aboutUsPanel;
    [SerializeField]
    private GameObject planeDetectionPanel;

    private States currentState = States.MainMenu;
    private Rooms currentRoom = Rooms.F_202;

    private bool isInitialized = false;
    public void SetCurrentState(States newState)
    {
        if(!isInitialized){
            Initialize();
        }
        //Logger.log("cs: " + currentState + ", ns: " + newState);
        currentState = newState;
        //Logger.log("Changing current state to: " + currentState.ToString());
        switch (newState)
        {
            case States.MainMenu:
                disableAllPanelsExcept(mainMenuPanel);
                break;
            case States.AboutUs:
                disableAllPanelsExcept(aboutUsPanel);
                break;
            case States.Tutorial:
                disableAllPanelsExcept(tutorialPanel);
                break;
            case States.RoomSelection:
                disableAllPanelsExcept(roomSelectionPanel);
                break;
            case States.PlaneDetection:
                disableAllPanelsExcept(planeDetectionPanel);
                break;
            case States.ModelPlacement:
                disableAllPanelsExcept(null);
                break;
        }
    }

    public void SetCurrentRoom(Rooms room){
        currentRoom = room;
    }

    private List<GameObject> panels = new List<GameObject>();
    
    private void Initialize()
    {
        panels.Add(mainMenuPanel);
        panels.Add(roomSelectionPanel);
        panels.Add(tutorialPanel);
        panels.Add(aboutUsPanel);
        panels.Add(planeDetectionPanel);

        isInitialized = true;
    }

    private void disableAllPanelsExcept(GameObject panel)
    {
        //Logger.log("List: " + panels.Count);
        foreach (GameObject p in panels)
        {
            if (panel != null && !panel.Equals(p) && p != null)
            {
                p.SetActive(false);
            }
        }
        panel.SetActive(true);
    }



}
