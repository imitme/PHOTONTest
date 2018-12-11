using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;   //포톤 추가
using UnityEngine.UI;   //UI추가

public class IntroManager : PunBehaviour
{  //포톤서버 통신, UI업데이트 하기위해!상속 변경

    public Button createButton; //방 생성 버튼
    public GameObject createRoomPanelPrefab;    //방 생성 팝업
    public GameObject createCellPrefab;    //cell 생성 팝업
    public Canvas canvas;

    string serverVer = "0.1";//서버는 앱 아이디와 버전으로 구분되기에.
    CreateRoomPanelManager createRoomPanelManager;

    private void Awake()
    {
        createButton.interactable = false;

        //포톤Photon 초기화
        PhotonNetwork.logLevel = PhotonLogLevel.Full;
        PhotonNetwork.autoJoinLobby = true;
        PhotonNetwork.automaticallySyncScene = true;

    }
    private void Start()
    {
        Connect();
    }

    //서버접속
    void Connect()
    {
        if (!PhotonNetwork.connected)
        {
            PhotonNetwork.ConnectUsingSettings(serverVer);
        }
    }

    void CreateRoom(string roomName)
    {
        if (string.IsNullOrEmpty(roomName))
        {
            return;
        }
        RoomOptions option = new RoomOptions();
        option.IsOpen = true;
        option.IsVisible = true;
        option.MaxPlayers = 10;

        PhotonNetwork.CreateRoom(roomName, option, TypedLobby.Default);
    }

    public void OnClickCreateButton()
    {
        if (createRoomPanelManager == null)  //팝업뜨면 할당되니, 안뜬것
        {
            GameObject createroomPanelGameObject = Instantiate(createRoomPanelPrefab);
            createroomPanelGameObject.transform.SetParent(canvas.transform, false);    //생성위치
            createRoomPanelManager = createroomPanelGameObject.GetComponent<CreateRoomPanelManager>();  //매니저할당.

            createRoomPanelManager.createRoomDelegate = CreateRoom; //방이름 할당해 주는 함수 넘겻!?
        }
    }

    public override void OnJoinedLobby()
    {
        //TODO : Create Room 버튼 활성화
        createButton.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        //TODO : GameScene으로 전환
        //방만들기 위한 팝업 창 닫기
        Debug.Log("## OnJoined Room()");
        if(createRoomPanelManager != null)
        {
            createRoomPanelManager.CreateRoomSuccess();
        }

        PhotonNetwork.isMessageQueueRunning = false;
        PhotonNetwork.LoadLevel("Game"); //씬 매니저 아닌 포톤으로 한닷?

    }

    
    public override void OnPhotonJoinRoomFailed(object[] codeAndMsg)
    {
        //TODO : 방릉 선택할 수 있게 UI 갱신
        //이미 만들어진 방에 참여하다 실패 한 경우
    }

    //방 생성 실패시 호출되는 매서드
    public override void OnPhotonCreateRoomFailed(object[] codeAndMsg)
    {
        //TODO : 유저에게 방 생성 실패 알리기
        if (createRoomPanelManager != null)     //방 있는지 확인하고
        {
            createRoomPanelManager.CreateRoomFailed();
        }
    }

    public override void OnReceivedRoomListUpdate()     //방정보 변경 감지하는 함수, 무슨방 있는지도 확인가능
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        Debug.Log(rooms);
    }

}
