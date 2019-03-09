using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button selectRoomButton;
    public Button aboutUsButton;
    public GameObject roomSelectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        selectRoomButton.onClick.AddListener(() => OnClick(selectRoomButton));
        aboutUsButton.onClick.AddListener(() => OnClick(aboutUsButton));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick(Button button)
    {
        //Disable the main menu on every button press
        gameObject.SetActive(false);

        if (button == selectRoomButton)
        {
            roomSelectionPanel.SetActive(true);
        }
        else if (button == aboutUsButton)
        {
            //TODO
        }
    }
}
