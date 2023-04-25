//--------------------------------------------------------------
// 파일명: ShootingGameSceneManager.cs
// 작성자: 이수민
// 작성일: 2023-04-18
// 설명: 
// - 풍선 사격 게임을 관리하는 미니게임 관리 클래스.
// - 화면 구성, 게임 점수 관리, 랭킹 등록등의 작업을 수행함.
// <사용 약어 정리>
// - MDM : MinigameDataManager
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using BackEnd;

public class ShootingGameSceneManager : MonoBehaviour {
    // 자체 인스턴스
    private static ShootingGameSceneManager _instance;
    public static ShootingGameSceneManager Instance {
        get {
            return _instance;
        }
    }

    //--------------------------------------------------------------
    // 변수 리스트 :
    // <UI 설정>
    // - UI : 점수창/조준선/사격/결과 등록버튼(임시)등 인게임 UI 묶음.
    // - scoreText : 점수창
    // - GameStartButton : 게임 시작 버튼, 임시
    // <하위 컴포넌트>
    // - BalloonShooter/BalloonSpawner : ㅇㅇ
    // <내부 변수>
    // - score : 해당 세션에서 얻은 점수 보관용 변수.
    //--------------------------------------------------------------
    [SerializeField] GameObject UI;
    [SerializeField] Text scoreText;
    [SerializeField] GameObject GameStartButton;

    [SerializeField] GameObject BalloonShooter;
    [SerializeField] GameObject BalloonSpawner;

    int score = 0;


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
    // 메소드명 : GameInit()
    // 설명 : 
    // - 게임 시작을 위한 기본적인 밑작업을 수행하는 구간
    // - MDM과 통신하여 기본적인 랭킹 정보를 세팅하고, 기타 게임의 UI를 세팅하는 일을 함.
    // - 일단은 게임 시작 버튼 누르면 발동하도록 세팅해두긴 했는데, 나중에 더 좋은 방법 있으면 바뀔 수도 있음.
    //--------------------------------------------------------------
    public void GameInit() {        
        StaticManager.MinigameData.SetRankData("ShootingGameScene");

        //내부 점수 초기화.
        score = 100;
        //UI 세팅
        GameStartButton.SetActive(false);
        UI.SetActive(true);

        BalloonShooter.SetActive(true);
        BalloonSpawner.SetActive(true);
        StartGame();
    }


    //--------------------------------------------------------------
    // 메소드명 : StartGame()
    // 설명 : 
    // - 실질적인 게임 제어 작업이 들어가는 공간.
    // - 나중에는 타이머로 시간을 제어하거나, 하는 제어 코드가 들어갈 수 있음.
    //--------------------------------------------------------------
    void StartGame() {
    }


    //--------------------------------------------------------------
    // 메소드명 : RegisterGameScore()
    // 설명 : 
    // - 게임 완료시 호출되어, 해당 세션에서의 미니게임 결과를 MDM에 세팅하는 변수.
    // - 지금은 인게임 화면에서 점수 저장 버튼으로 동작하도록 만들었지만, 나중에는 StartGame의 타이머 트리거등으로 자동으로 호출되도록 변경할 수도 있음.
    //--------------------------------------------------------------
    public void RegisterGameScore() {
        StaticManager.MinigameData.SetGameResult(score);
    }


    //--------------------------------------------------------------
    // 메소드명 : IncreaseScore()
    // 설명 : 
    // - 점수를 올리는 메소드
    // - 나중에는 터트린 풍선 데이터를 매개변수로 받아서, 금색 풍선의 경우 +200점 형태로 구현할 수도 있을 듯??? "아님 말고"
    //--------------------------------------------------------------
    public void IncreaseScore() {
        score += 100;
        scoreText.text = "점수 : " + score.ToString();
    }
}
