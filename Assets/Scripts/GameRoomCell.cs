using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public struct GameRoomInfo  //구조체 선언
{
    public string roomName;
    public int maxPlayer;
    public int playerCount;

    public GameRoomInfo(string roomName, int maxPlayer, int playerCount)    //생성자
    {
        this.roomName = roomName;
        this.maxPlayer = maxPlayer;
        this.playerCount = playerCount;
    }
}

public class GameRoomCell : MonoBehaviour {
    public Text roomNameText;
    public Text totalPlayerText;

    public void SetRoomInfo(GameRoomInfo roomInfo)
    {
        roomNameText.text = roomInfo.roomName;
        totalPlayerText.text = string.Format("{0}/{1}", roomInfo.playerCount, roomInfo.maxPlayer);
    }
}
