using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDataContainer : MonoBehaviour
{

    
    private bool roomSelected;
    [SerializeField]
    private Camera firstPersonCamera;
    

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

    public bool RoomSelected
    {
        get
        {
            return roomSelected;
        }

        set
        {
            roomSelected = value;
        }
    }

    
}
