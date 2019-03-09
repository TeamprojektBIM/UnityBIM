using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BottomMenu : MonoBehaviour
{

    public Button menuButton;
    public GameObject mainMenuPanel;

    // Start is called before the first frame update
    void Start()
    {
        menuButton.onClick.AddListener(() => OnClick(menuButton));
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick(Button button)
    {
        if (button == menuButton)
        {
            //Toggle the main Menu
            mainMenuPanel.SetActive(!mainMenuPanel.activeSelf);
        }
    }
}
