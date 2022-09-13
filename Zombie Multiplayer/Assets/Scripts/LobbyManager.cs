using Photon.Pun; // 유니티용 포톤 컴포넌트들
using Photon.Realtime; // 포톤 서비스 관련 라이브러리
using UnityEngine;
using UnityEngine.UI;

// 마스터(매치 메이킹) 서버와 룸 접속을 담당
public class LobbyManager : MonoBehaviourPunCallbacks { // MonoBehaviour 기반에 PUN 기능이 섞인 것.
    private static readonly string GAME_VERSION = "1"; // 게임 버전

    public Text ConnectionInfoText; // 네트워크 정보를 표시할 텍스트
    public Button JoinButton; // 룸 접속 버튼

    // 게임 실행과 동시에 마스터 서버 접속 시도
    private void Start() 
    {
        // 접속에 필요한 정보를 설정함
        PhotonNetwork.GameVersion = GAME_VERSION;
        // 마스터 서버로 접속을 시도
        PhotonNetwork.ConnectUsingSettings();

        // 조인 버튼 비활성화
        JoinButton.interactable = false;
        // 접속 시도 중임을 텍스트로 표시
        ConnectionInfoText.text = "마스터 서버에 접속 중...";
    }

    // 마스터 서버(매치메이킹 서버) 접속 성공시 자동 실행
    public override void OnConnectedToMaster() 
    {
        // UI 표시
        JoinButton.interactable = true;
        ConnectionInfoText.text = "Online: Connection Success..!";
    }

    // 마스터 서버 접속 실패시 자동 실행
    public override void OnDisconnected(DisconnectCause cause) {
        // UI 표시
        JoinButton.interactable = false;
        ConnectionInfoText.text = "Connnection Fail... Reconnecting...";

        //재접 시도 (계속 시도하게 됨!!)
        // 원래는 네트워크가 연결되어 있는지 확인하고, 다시 시도함
        PhotonNetwork.ConnectUsingSettings();
    }

    // 룸 접속 시도
    public void Connect() 
    {
        // Join Button에서 세팅함

        // 접속 버튼을 비활성화
        JoinButton.interactable = false;


        // 서버에 접속 중이냐
        if(PhotonNetwork.IsConnected)
        {
            // 접속을 실행
            ConnectionInfoText.text = "Online: Searching For Room...";

            // 접속을 실행
            PhotonNetwork.JoinRandomRoom();
        } 
        // 아니라면
        else
        {
            // 다시 마스터 서버에 재접속 시도
            ConnectionInfoText.text = "Connnection Fail... Reconnecting...";

            //재접 시도 (계속 시도하게 됨!!)
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    private static readonly RoomOptions ROOM_OPTIONS = new RoomOptions() { MaxPlayers = 4 };
    // (빈 방이 없어)랜덤 룸 참가에 실패한 경우 자동 실행
    public override void OnJoinRandomFailed(short returnCode, string message) 
        // returncode: 리턴 코드 message: 오류 메시지
    {
        // UI 표시
        ConnectionInfoText.text = "Online: Joinning Room Fail... Creating Room...";

        // 방 만들기
        PhotonNetwork.CreateRoom(null, ROOM_OPTIONS); 
        // 이름: 이름을 줘야 하는데, 이름을 알아야 할 필요가 없기 때문에 null로
        // RoomOptions: 방에 대한 조건을 준다.
        // expactedPlayer: 티어별 등등 플레이어 조건 같은 것
    }

    // 룸에 참가 완료된 경우 자동 실행
    public override void OnJoinedRoom() 
    {
        // UI 표시
        ConnectionInfoText.text = "Online: Joinned Room";

        // 모튼 클라이언트 Main씬 로드
        // LoadLevel([씬 넘버/씬 이름]): 비동기로 진행하고, 모두가 로드가 되었을 때까지 기다린다.
        PhotonNetwork.LoadLevel("Main");

    }
}