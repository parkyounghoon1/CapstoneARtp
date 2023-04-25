//--------------------------------------------------------------
// 파일명: LoginSceneManager.cs
// 작성자: 이수민
// 작성일: 2023-03-27
// 설명: 로그인 씬 화면 구성 클래스
// 수정:
// - 이수민(2023-04-07) : 문서화 작업, 마이너한 명칭 변경
// - 이수민(2023-04-11) : 로그인/회원가입에서의 간단한 팝업 UI 적용(최종 버전 아님)
// - 이수민(2023-04-19) : Start() 제거
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class LoginSceneManager : MonoBehaviour
{
    // 자체 인스턴스
    private static LoginSceneManager _instance;
    public static LoginSceneManager Instance {
        get {
            return _instance;
        }
    }


    //--------------------------------------------------------------
    // 변수 리스트 :
    // - _touchStartButton : Touch to Start 이미지.
    // - _loginButtonGroup : 로그인 관련 버튼 묶음집. 아마 나중에 UIManager쪽으로 통합할 가능성 높음.
    // - _playerIDInput : 아이디 입력 칸, 아마 나중에 UIManager쪽으로 통합할 가능성 높음.
    // - _playerPWInput : 비번 입력 칸, 아마 나중에 UIManager쪽으로 통합할 가능성 높음.
    //--------------------------------------------------------------
    [SerializeField] private GameObject _loginButtonGroup;
    [SerializeField] private GameObject _touchStartButton;
    [SerializeField] private InputField _playerIDInput;
    [SerializeField] private InputField _playerPWInput;


    //--------------------------------------------------------------
    // 메소드명 : Awake()
    // 설명 : instance 세팅 및 StaticManager 클래스 기동
    //--------------------------------------------------------------
    void Awake() {
        if (_instance == null) {
            _instance = this;
        }

        if (FindObjectOfType(typeof(StaticManager)) == null) {
            var obj = Resources.Load<GameObject>("Prefabs/StaticManager");
            Instantiate(obj);
        }
    }


    //--------------------------------------------------------------
    // 메소드명 : OnEnable()
    // 설명 : 터치 투 스타트 기동
    //--------------------------------------------------------------
    void OnEnable() {
        SetTouchStartButton(); // 터치 투 스타트 기동
    }


    //--------------------------------------------------------------
    // 메소드명 : SetTouchStartButton()
    // 설명 : 터치 투 스타트 버튼 활성화. 터치시 SetButton()을 통해 기본적인 컴포넌트 세팅
    //--------------------------------------------------------------
    private void SetTouchStartButton() {
        if (_touchStartButton.activeSelf) {
            return;
        }
        _touchStartButton.SetActive(true);
        _touchStartButton.GetComponent<Button>().onClick.AddListener(() => {
            Destroy(_touchStartButton);
            _touchStartButton = null;
            SetButton();    // 버튼 세팅
        });
    }


    //--------------------------------------------------------------
    // 메소드명 : SetButton()
    // 설명 : 로그인/회원가입에 필요한 기본적인 UI 컴포넌트 세팅.
    //--------------------------------------------------------------
    private void SetButton() {
        if (_loginButtonGroup.activeSelf) {
            return;
        }
        _loginButtonGroup.SetActive(true);

        Button[] buttons = _loginButtonGroup.GetComponentsInChildren<Button>();

        for (int i = 0; i < buttons.Length; i++) {
            buttons[i].onClick.RemoveAllListeners();
        }
        buttons[0].onClick.AddListener(LoginProcess);
        buttons[1].onClick.AddListener(SignUpProcess);
    }



    //--------------------------------------------------------------
    // 메소드명 : LoginProcess()
    // 설명 : 
    // - 입력된 ID/PW를, BackendLogin클래스의 LoginProcess로 전송시키는 메소드.
    // - 로그인 성공시, 메인 화면으로 씬 전환
    //--------------------------------------------------------------
    public void LoginProcess() {
        string playerID;
        string playerPW;
        playerID = _playerIDInput.GetComponent<InputField>().text;
        playerPW = _playerPWInput.GetComponent<InputField>().text;
        if (BackendLogin.Instance.LoginProcess(playerID,playerPW)) {
            StaticManager.PopUpUI.PopUp("로그인 성공",()=>{SceneLoader.LoadScene("MainScene");});
        } else {
            StaticManager.PopUpUI.PopUp("로그인 실패");
        }
    }


    //--------------------------------------------------------------
    // 메소드명 : SignUpProcess()
    // 설명 : 입력된 ID/PW를, BackendLogin클래스의 SignUpProcess로 전송시키는 메소드.
    //--------------------------------------------------------------
    public void SignUpProcess() {
        string playerID;
        string playerPW;
        playerID = _playerIDInput.GetComponent<InputField>().text;
        playerPW = _playerPWInput.GetComponent<InputField>().text;
        if (BackendLogin.Instance.SignUpProcess(playerID,playerPW)) {
            StaticManager.PopUpUI.PopUp("회원가입 성공");
        } else {
            StaticManager.PopUpUI.PopUp("회원가입 실패");
        }
    }
}
