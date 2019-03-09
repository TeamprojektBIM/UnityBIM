using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NextButton : MonoBehaviour
{

    public GameObject roomSelectionPanel;
    public GameObject planeDetectionPanel;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClick()
    {
        roomSelectionPanel.SetActive(false);
        planeDetectionPanel.SetActive(true);
    }
}
