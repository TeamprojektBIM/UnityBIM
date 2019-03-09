using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{

    public GameObject roomSelectionPanel;
    public GameObject planeDetectionPanel;
    private GlobalDataContainer container;
    

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
        FindDataContainer();
    }

    private void FindDataContainer()
    {
        GameObject containerObject = GameObject.Find(Constants.GlobalDataContainer);
        container = containerObject.GetComponent<GlobalDataContainer>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        roomSelectionPanel.SetActive(false);
        container.RoomSelected = false;
        planeDetectionPanel.SetActive(true);
    }
}
