using Cinemachine; // 시네머신 관련 코드
using Photon.Pun; // PUN 관련 코드
using UnityEngine;

// 시네머신 카메라가 로컬 플레이어를 추적하도록 설정
public class CameraSetup : MonoBehaviourPun 
    // MonoBehaviour에 PhotonView가 추가된 것에 불과함
{
    void Start() 
    {
        // 나만(로컬만) 따라 다녀야 한다.
        if(photonView.IsMine) // 로컬이라면?
        {
            //이러면 카메라가 나만 따라옴
            CinemachineVirtualCamera followCam = FindObjectOfType<CinemachineVirtualCamera>();
            followCam.Follow = transform;
            followCam.LookAt = transform;
        }
    }
}