using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleARCore;

public class GlobalDataContainer : MonoBehaviour
{

    [SerializeField]
    private Camera firstPersonCamera;

    [SerializeField]
    private GameObject roomSelectionPanel;

    public GameObject RoomSelectionPanel
    {
        get
        {
            return roomSelectionPanel;
        }

        set
        {
            roomSelectionPanel = value;
        }
    }



    [SerializeField]
    private GameObject planeDetectionPanel;

    public GameObject PlaneDetectionPanel
    {
        get
        {
            return planeDetectionPanel;
        }

        set
        {
            planeDetectionPanel = value;
        }
    }


    [SerializeField]
    private GameObject bottomMenuPanel;

    public GameObject BottomMenuPanel
    {
        get
        {
            return bottomMenuPanel;
        }

        set
        {
            bottomMenuPanel = value;
        }
    }


    private States currentState;
    public States CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            if (currentState == value){
                return;
            }
            currentState = value;
            switch (value)
            {
                case States.RoomSelection:
                    roomSelectionPanel.SetActive(true);
                    planeDetectionPanel.SetActive(false);
                    bottomMenuPanel.SetActive(false);
                    break;

                case States.PlaneDetection:
                    roomSelectionPanel.SetActive(false);
                    planeDetectionPanel.SetActive(true);
                    bottomMenuPanel.SetActive(false);
                    break;

                case States.ModelPlacement:
                    roomSelectionPanel.SetActive(false);
                    planeDetectionPanel.SetActive(false);
                    bottomMenuPanel.SetActive(true);
                    break;

            }
        }
    }


    public Camera FirstPersonCamera
    {
        get
        {
            return firstPersonCamera;
        }

        set
        {
            firstPersonCamera = value;
        }
    }


}
