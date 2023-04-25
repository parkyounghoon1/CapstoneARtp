//--------------------------------------------------------------
// 파일명: MainSceneManager.cs
// 작성자: 이수민
// 작성일: 2023-03-28
// 설명: 메인 씬 화면 구성 클래스.
// 수정:
// - 이수민(2023-04-07) - 문서화 작업, 마이너한 기능 추가.
// - 이수민(2023-04-07) - Start() 제거
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class MainSceneManager : MonoBehaviour
{
    // 자체 인스턴스
    private static MainSceneManager _instance;
    public static MainSceneManager Instance {
        get {
            return _instance;
        }
    }
    
    //--------------------------------------------------------------
    // 변수 리스트 :
    // - _MainSceneUI_MainButton : 대표버튼 4개.
    // - _MainSceneUI_SideButton : 아이콘으로 표시된 사이드버튼.
    //--------------------------------------------------------------
    [SerializeField] private GameObject _MainSceneUI_User;
    [SerializeField] private GameObject _MainSceneUI_MainButton;
    [SerializeField] private GameObject _MainSceneUI_SideButton;


    //--------------------------------------------------------------
    // 메소드명 : Awake()
    // 설명 : instance 세팅
    //--------------------------------------------------------------
    void Awake() {
        if (_instance == null) {
            _instance = this;
        }
    }


    //--------------------------------------------------------------
    // 메소드명 : OnEnable()
    // 설명 : 화면 구성용 메소드 싹다 호출
    //--------------------------------------------------------------
    void OnEnable(){
        StaticManager.PlayerData.GameDataFind(); // 플레이어의 정보를 찾고, 내부 클래스 UserData 초기화.
        SetUIUser(); // 유저 정보 세팅
        SetUIMainButton(); // 메인 버튼 세팅
        SetUISideButton(); // 사이드버튼(일단은 속성밖에 없지만) 세팅
    }



    //--------------------------------------------------------------
    // 메소드명 : SetUIUser()
    // 설명 :
    // - 화면 좌상단 플레이어 정보 UI 세팅
    //--------------------------------------------------------------
    private void SetUIUser() {
        if (_MainSceneUI_User.activeSelf) {
            return;
        }
        _MainSceneUI_User.SetActive(true);

        Text[] Texts = _MainSceneUI_User.GetComponentsInChildren<Text>();

        Texts[0].text = StaticManager.PlayerData.userData.nickname;
        Texts[1].text = StaticManager.PlayerData.userData.title;
        Texts[2].text = "경험치 량 : " + StaticManager.PlayerData.userData.exp.ToString();
        Texts[3].text = "돈 량 : " +StaticManager.PlayerData.userData.money.ToString();
    }


    //--------------------------------------------------------------
    // 메소드명 : SetUIMainButton()
    // 설명 : 화면 중앙 메인 버튼 4개 세팅
    //--------------------------------------------------------------
    private void SetUIMainButton() {
        if (_MainSceneUI_MainButton.activeSelf) {
            return;
        }
        _MainSceneUI_MainButton.SetActive(true);

        Button[] buttons = _MainSceneUI_MainButton.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[0].onClick.AddListener(GameStart);
        buttons[1].onClick.AddListener(Shop);
        buttons[2].onClick.AddListener(Challenge);
        buttons[3].onClick.AddListener(GameQuit);
    }


    //--------------------------------------------------------------
    // 메소드명 : SetUISideButton()
    // 설명 : 화면 사이드의 아이콘 버튼 세팅
    //--------------------------------------------------------------
    private void SetUISideButton(){
        if (_MainSceneUI_SideButton.activeSelf) {
            return;
        }
         _MainSceneUI_SideButton.SetActive(true);

        Button[] buttons = _MainSceneUI_SideButton.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[0].onClick.AddListener(Settings);
    }


    //--------------------------------------------------------------
    // 이 아래부터는 각 버튼의 리스너들이 포진되어 있음.
    // 설명 :
    // <메인 버튼 리스너>
    // - GameStart : 게임 시작 버튼, 인게임 씬 불러오기.
    // - Shop : 상점 버튼, 상점 씬 불러오기.
    // - Challenge : 챌린지 버튼, 챌린지 씬 불러오기
    // - GameQuit : 게임 종료 버튼
    // <사이드 버튼 리스너>
    // - Settings : 세팅 버튼, 일단은 플레이어 정보 수정을 수행할 예정.
    //--------------------------------------------------------------
    private void GameStart(){
        SceneLoader.LoadScene("InGameScene");
    }
    private void Shop(){
    }
    private void Challenge(){
    }
    private void GameQuit(){
        // 여기에 UIManager로 팝업을 띄우기.
        Debug.Log("게임 종료 버튼");
    }
    private void Settings(){
        // 여기에 UIManager로 팝업을 띄우기.
        // 적용시 데이터 업데이트 후, SetUIUser 재호출.
    }
}
