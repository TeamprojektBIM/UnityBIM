using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class RoomSelectionPanel : MonoBehaviour
{

    public Dropdown dropDown;
    public Button nextButton;
    // Start is called before the first frame update
    void Start()
    {
        List<Rooms> rooms = Enum
        .GetValues(typeof(Rooms))
        .Cast<Rooms>()
        .ToList();
        List<string> roomsNames = rooms
            .Select(r => r.ToString().Replace("_"," "))
            .ToList();

        dropDown.ClearOptions();
        dropDown.AddOptions(roomsNames);
        dropDown.onValueChanged.AddListener(v => {
            Constants.dataContainer.SetCurrentRoom(rooms[v]);
        });

        nextButton.onClick.AddListener(() => 
            Constants.dataContainer.SetCurrentState(States.PlaneDetection)
        );
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Constants.dataContainer.SetCurrentState(States.MainMenu);
        }   
    }

    
}
