using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //UI 사용

public class CreateRoomPanelManager : MonoBehaviour
{

    public InputField roomNameInputField;
    public Button okButton;
    public Button cancelButton;
    public Text errorText;

    public delegate void CreateRoomDelegate(string roomName); //델리이름으로 할당하면, 그 함수 호출 형태로 동작시킬 것이다.
    public CreateRoomDelegate createRoomDelegate;

    void Start()
    {
        errorText.text = "";
        okButton.interactable = false;
        roomNameInputField.onValueChanged.AddListener(delegate { CheckRoomName(); });   //값이 변경되면,

    }

    void CheckRoomName()
    {
        if (!string.IsNullOrEmpty(roomNameInputField.text))
        {
            okButton.interactable = true;
        }
        else
        {
            okButton.interactable = false;
        }
    }

    public void OnClickOKButton()
    {
        errorText.text = "";

        if (createRoomDelegate == null)//하지않았다면, 잽싸게 빠져나와
        {
            return;
        }
        //변경불가상태
        okButton.interactable = false;
        cancelButton.interactable = false;
        roomNameInputField.interactable = false;

        //이름을 전달
        createRoomDelegate(roomNameInputField.text);
    }
    public void OnClickCancelButton()
    {
        Destroy(this.gameObject);
    }
    public void CreateRoomFailed()
    {
        errorText.text = "방 생성 실패";
        cancelButton.interactable = true;
        roomNameInputField.interactable = true;
        roomNameInputField.text = "";
    }
    public void CreateRoomSuccess()
    {
        Destroy(this.gameObject);
    }
}
