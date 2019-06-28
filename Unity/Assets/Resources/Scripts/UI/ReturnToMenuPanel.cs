using UnityEngine;
using UnityEngine.UI;

public class ReturnToMenuPanel : MonoBehaviour
{
    public Button returnButton;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.onClick.AddListener(() => OnClick(returnButton));
    }

    void OnClick(Button button)
    {
        //Logger.log("clicked");
        if (button == returnButton)
        {
            Constants.dataContainer.SetCurrentState(States.MainMenu);
        }
    }
}
