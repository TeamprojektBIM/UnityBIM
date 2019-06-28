using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using UnityEngine;

using GoogleARCore;

public class GlobalDataContainer : MonoBehaviour
{
    public Camera FirstPersonCamera;
    public GameObject arObject;

    [SerializeField]
    private GameObject[] menuPanels;

    private States currentState = States.MainMenu;
    private Rooms currentRoom = Rooms.P_004;

    private bool isInitialized = false;
    public void SetCurrentState(States newState)
    {
        //Logger.log("cs: " + currentState + ", ns: " + newState);
        currentState = newState;
        //Logger.log("Changing current state to: " + currentState.ToString());
        switch (newState)
        {
            case States.MainMenu:
                disableAllPanelsExcept(menuPanels[0]);
                break;
            case States.AboutUs:
                disableAllPanelsExcept(menuPanels[1]);
                break;
            case States.Tutorial:
                disableAllPanelsExcept(menuPanels[2]);
                break;
            case States.RoomSelection:
                disableAllPanelsExcept(menuPanels[3]);
                break;
            case States.PlaneDetection:
                disableAllPanelsExcept(menuPanels[4]);
                Instantiate(arObject);
                break;
            case States.PlaneConfirmation:
                disableAllPanelsExcept(menuPanels[5]);
                break;
            case States.MarkerPlacement:
                disableAllPanelsExcept(menuPanels[6]);
                break;
            case States.MarkerRotation:
                disableAllPanelsExcept(menuPanels[7]);
                break;
            case States.ModelPlacement:
                disableAllPanelsExcept(null);
                break;
        }
    }

    private void disableAllPanelsExcept(GameObject panel)
    {
        //Logger.log("List: " + panels.Count);
        foreach (GameObject p in menuPanels)
        {
            p.SetActive(false);
        }

        panel.SetActive(true);
    }

    public void SetCurrentRoom(Rooms room)
    {
        currentRoom = room;
    }

    public Rooms GetCurrentRoom( )
    {
        return currentRoom;
    }
}
