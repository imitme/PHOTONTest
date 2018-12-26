using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon;

public class Player : PunBehaviour {

    public static GameObject LocalPlayerInstance;
    public CameraWork cameraWork;

    Animator animator;

    private void Awake()    //포톤 요구 사항
    {
        if(photonView.isMine) //내가 컨트롤한 나인지, 동기화된 나인지 포톤에 미러링돼
        {
            Player.LocalPlayerInstance = this.gameObject;   //참조값을 할당
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Use this for initialization
    void Start() {
        animator = GetComponent<Animator>();

        //카메라 워크가 시작할때 무조건 따라가는데, 제어 캐릭터인지 확인하고 따라가도록
        if(cameraWork != null)
        {
            if (photonView.isMine)
            {
                cameraWork.OnStartFollowing();
            }
        }
	}
	
	// Update is called once per frame
	void Update () {

        if(photonView.isMine == false && PhotonNetwork.connected == true)
        {
            return;
        }

        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        transform.Rotate(0, x * Time.deltaTime * 150.0f, 0);
        transform.Translate(0,0, z * Time.deltaTime * 3.0f);

        animator.SetFloat("Speed", z);

    }
}
