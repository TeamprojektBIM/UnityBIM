using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button selectRoomButton;
    public Button aboutUsButton;
    private GlobalDataContainer container;

    // Start is called before the first frame update
    void Start()
    {
        FindDataContainer();
        selectRoomButton.onClick.AddListener(() => OnClick(selectRoomButton));
        aboutUsButton.onClick.AddListener(() => OnClick(aboutUsButton));
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }

    void OnClick(Button button)
    {
        //Disable the main menu on every button press
        gameObject.SetActive(false);

        if (button == selectRoomButton)
        {
            container.CurrentState = States.RoomSelection;
        }
        else if (button == aboutUsButton)
        {
            //TODO
        }
    }
}
