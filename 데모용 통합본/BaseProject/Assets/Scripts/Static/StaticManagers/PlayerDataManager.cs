//--------------------------------------------------------------
// 파일명: PlayerDataManager.cs
// 작성자: 이수민
// 작성일: 2023-03-29
// 설명: 로그인된 계정의 플레이어 데이터 조작 담당.
// 수정:
// - 이수민(2023-04-07) : 문서화 작업, 명칭 변경(BackendGameData->PlayerDataManager), 싱글턴 통합(Singleton.cs로 사용)
// - 이수민(2023-04-09) : 싱글턴 해제(StaticManager로 통합), public 해제
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using BackEnd;

public class PlayerDataManager : MonoBehaviour
{
    //--------------------------------------------------------------
    // 변수 리스트 :
    // - userData : 관리할 유저 데이터.
    // - gameDataRowInDate : 데이터 변경 일자에 대한 고유값.
    //--------------------------------------------------------------
    public UserData userData = new UserData();
    private string gameDataRowInDate = string.Empty;
    
    
    //--------------------------------------------------------------
    // 메소드명 : Init()
    // 설명 : 초기화 작업. 일단 지금은 공란임.
    //--------------------------------------------------------------
    public void Init() {

    }

    //--------------------------------------------------------------
    // 메소드명 : GameDataFind()
    // 설명 : 
    // - 메인 게임 씬 기동시, 서버에 저장된 플레이어 정보 찾는 메소드
    // - 있으면 가져오고, 없으면 새로 하나 만듦.
    //--------------------------------------------------------------
    public void GameDataFind(){
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        var bro = Backend.GameData.GetMyData("USER_DATA", new Where());
        if (bro.IsSuccess()) {
            Debug.Log("게임 정보 조회에 성공했습니다. : " + bro);
            LitJson.JsonData gameDataJson = bro.FlattenRows(); // Json으로 리턴된 데이터를 받아옵니다.
            if (gameDataJson.Count <= 0) {
                Debug.Log("데이터가 존재하지 않습니다. 하나 생성합니다.");
                GameDataInit(); // 예시용 데이터 생성.
                GameDataInsert(); // 서버에 예시용 데이터 주입.
                GameDataFind(); // 재귀 호출.
            }else{
                Debug.Log("데이터가 존재합니다. 해당 데이터로 정보를 초기화합니다.");
                GameDataGet(gameDataJson); // UserData 초기화.
            }
        }else {
            Debug.LogError("게임 정보 조회에 실패했습니다. : " + bro);
        }
    }


    //--------------------------------------------------------------
    // 메소드명 : GameDataInit()
    // 설명 : 
    // - 기본 데이터. 나중에 없어질지도?
    //--------------------------------------------------------------
    public void GameDataInit(){
        Debug.Log("데이터 초기화.");
        userData.nickname = "NKYL";
        userData.title = "졸업하고 싶으면 개추ㅋㅋ";
        userData.exp = 0;
        userData.money = 0;
    }


    //--------------------------------------------------------------
    // 메소드명 : GameDataInsert()
    // 설명 : userData 정보를 서버로 삽입
    //--------------------------------------------------------------
    public void GameDataInsert() {
        Debug.Log("뒤끝 업데이트 목록에 해당 데이터들을 추가.");
        Param param = new Param();
        param.Add("nickname", userData.nickname);
        param.Add("title", userData.title);
        param.Add("exp", userData.exp);
        param.Add("money", userData.money);

        Debug.Log("게임정보 데이터 삽입 요청.");
        var bro = Backend.GameData.Insert("USER_DATA", param);
        if (bro.IsSuccess()) {
            Debug.Log("게임정보 데이터 삽입에 성공했습니다. : " + bro);
            gameDataRowInDate = bro.GetInDate();
        } else {
            Debug.LogError("게임정보 데이터 삽입에 실패했습니다. : " + bro);
        }
    }
    

    //--------------------------------------------------------------
    // 메소드명 : GameDataGet()
    // 설명 : 서버에서 userData를 가져옴
    //--------------------------------------------------------------
    public void GameDataGet(LitJson.JsonData gameDataJson) {
        Debug.Log("게임 정보 조회 함수를 호출합니다.");
        gameDataRowInDate = gameDataJson[0]["inDate"].ToString(); //불러온 게임정보의 고유값입니다.
        userData = new UserData();
        userData.nickname = gameDataJson[0]["nickname"].ToString();
        userData.title = gameDataJson[0]["title"].ToString();
        userData.exp = int.Parse(gameDataJson[0]["exp"].ToString());
        userData.money = int.Parse(gameDataJson[0]["money"].ToString());

        Debug.Log(userData.ToString());
    }
    

    //--------------------------------------------------------------
    // 메소드명 : GameDataUpdate()
    // 설명 : 일단은 만들었는데... 아마 따로 세팅하는 편이 편할 거 같음. 각보고 지우셈.
    //--------------------------------------------------------------
    public void GameDataUpdate() {
        // 일단 비워두기. 나중에 UI 컴포넌트 만들면 같이 ㄱㄱ
    }
}
