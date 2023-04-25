//--------------------------------------------------------------
// 파일명: MinigameDataManager.cs
// 작성자: 염민규
// 작성일: 2023-04-15
// 설명: 
// - 미니게임의 결과 저장 및 랭킹 등록 기능을 제공하는 클래스
// - 각 미니게임 관리 매니저에서 데이터 관련 기능 마려울 때마다 이 클래스를 호출하는 방식으로 사용.
// 수정 :
// - 이수민(2023-04-17) : 
//      1. 문서화 양식 통일
//      2. 마이너한 명칭 변경(My 붙어있는거 싹다 이름 변경)
//      3. 싱글턴 편입(
//                  미니게임 전체에서 같은 형식을 쓰는데다, 
//                  자체적인 내부 데이터 클래스도 포함하고 있고,
//                  나중에 메인씬 허브쪽에서 사용할 때도 편하니 이쪽이 맞다고 봄
//                  사용법 : Static.MinigameData.메소드이름(매개변수);, 각 미니게임 매니저의 주석 참조)
//      4. 메소드 분할 및 미니게임 관리 클래스와의 협업을 위한 마이너한 설계 변경.
//--------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using LitJson;
using UnityEngine.UI;
using System;


public class MinigameDataManager : MonoBehaviour
{
    //--------------------------------------------------------------
    // 변수 리스트 :
    // <내부 변수> :
    // - tableName : 접근한 미니게임의 랭킹 테이블 이름
    // - rankUUID : 접근한 미니게임의 rankUUID값
    // - last_score : 접근한 미니게임의 기존 본인 최고 점수, RankUpdate()에서 사용됨.
    //--------------------------------------------------------------
    string tableName;
    string rankUUID;
    string last_score;

    //--------------------------------------------------------------
    // 메소드명 : Init()
    // 설명 : 초기화 작업. 일단 지금은 공란임.
    //--------------------------------------------------------------
    public void Init() {

    }


    //--------------------------------------------------------------
    // 메소드명 : SetRankData(string minigameName)
    // 입력 :
    // - minigameName : 사용자가 접근한 미니게임의 이름.
    // 설명 :
    // - 각 미니게임에 최초 접근시 미니게임 매니저가 호출하는 부분.
    // - 해당 미니게임에 맞도록 내부 변수 값 tableName과 rankUUID를 세팅한다.
    // - 추가적으로 하위 메소드 GetRankData를 호출하여, 기존 자신의 최고점수인 last_score까지 세팅한다.
    //--------------------------------------------------------------
    public void SetRankData(string minigameName) {

        // 미니게임 구분
        if (minigameName == "ShootingGameScene") {
            tableName = "SCORE_SHOOTING";
            rankUUID = "81f2b860-db39-11ed-9ee7-099cf31f7d52";
        } else if(minigameName == "BasketBallGameScene") {
            tableName = "SCORE_BASKETBALL";
            rankUUID = "79e12350-db39-11ed-9ee7-099cf31f7d52";
        } else {
            Debug.LogError("이름 오류, 해당 미니게임 매니저의 호출 부분을 확인하세요!");
            return;
        }
        GetRankData(tableName, rankUUID);
    }


    //--------------------------------------------------------------
    // 메소드명 : GetRankData()
    // 입력 :
    // - tableName : 접근한 미니게임의 랭킹 테이블 이름
    // - rankUUID : 접근한 미니게임의 rankUUID 값
    // 설명 : SetRankData의 하위 메소드로, 기존 자신의 최고점수인 last_score를 세팅하는 메소드.
    // 수정 : 
    // - 이수민(2023-04-17) : 마이너한 설계 변경 및 메소드 분할(SetRankData)
    //--------------------------------------------------------------
    void GetRankData(string tableName, string rankUUID) {
        // 테이블 데이터 가져오기. 최종적으로 플레이어의 랭킹정보를 가져옴
        var bro = Backend.GameData.Get(tableName, new Where(), 1);

        // 실패 걸러내기
        if (bro.IsSuccess() == false){
            Debug.LogError("게임 정보 조회 실패: ");
            return;
        }
        Debug.Log("게임 정보 조회 성공: " + bro);
        var data = bro.FlattenRows();
        if (data.Count < 0) {
            Debug.LogError("게임정보가 비어있습니다.");
            return;
        }

        var indate = data[0]["inDate"].ToString();
        string userUuid = indate;
        List<RankItem> rankItemList = new List<RankItem>();

        BackendReturnObject bro2 = Backend.URank.User.GetMyRank(rankUUID);
        if (bro2.IsSuccess()) {
            JsonData rankListJson = bro2.GetFlattenJSON();
            string extraName = string.Empty;
            for (int i = 0; i < rankListJson["rows"].Count; i++) {
                RankItem rankItem = new RankItem();

                rankItem.gamerInDate = rankListJson["rows"][i]["gamerInDate"].ToString();
                rankItem.nickname = rankListJson["rows"][i]["nickname"].ToString();
                rankItem.score = rankListJson["rows"][i]["score"].ToString();
                rankItem.index = rankListJson["rows"][i]["index"].ToString();
                rankItem.rank = rankListJson["rows"][i]["rank"].ToString();
                rankItem.totalCount = rankListJson["totalCount"].ToString();

                if (rankListJson["rows"][i].ContainsKey(rankItem.extraName)){
                    rankItem.extraData = rankListJson["rows"][i][rankItem.extraName].ToString();
                }
                rankItemList.Add(rankItem);
                last_score = rankItem.score;
            }
        } else {
            Debug.LogError("랭크 데이터 호출 실패");
        }
    }


    // --------------------------------------------------------------
    // 함수명 : SetGameResult()
    // 입력 :
    // - minigameName : 수행한 미니게임 이름, 그쪽 미니게임 매니저에 의해 호출될 예정
    // - score : 해당 미니게임 세션에서 얻어낸 플레이어의 점수
    // 설명 : 
    // - 접근한 미니게임의 관리 매니저에 의해 SetRankData()가 미리 수행되어 있음을 전제로 함.
    // - 자기가 수행한 미니게임 정보(일단은 점수만)를 테이블에 등록하는 클래스.
    // - 추가적으로 기존 랭킹 데이터가 없거나(null), 기존 최고 점수보다 높은 점수를 얻으면 랭킹 갱신
    // 수정 : 
    // - 이수민(2023-04-17) : 마이너한 설계 변경(매개변수)
    // --------------------------------------------------------------
    public void SetGameResult(int score) {
        Param param = new Param();
        param.Add("점수", score);
        // 일단 등록함.
        Backend.GameData.Insert(tableName, param);

        if (last_score == null || score >= Int32.Parse(last_score)) {
            RankUpdate(score);
            Debug.Log("최고 score로 갱신됨");
        } else {
            Debug.Log("이전 점수보다 낮아 랭킹 등록 X");
        }
    }



    // --------------------------------------------------------------
    // 메소드명 : RankUpdate()
    // 입력 :
    // - score : 랭킹에 등록하려는 점수
    // 설명 : MyRankUpdate() 하위 메소드. 랭킹에 등록 가능한 상황이라, 얻은 점수를 랭킹에 등록할 때 작동
    // --------------------------------------------------------------
    void RankUpdate(int score) {
        string rowInDate = string.Empty;
        // 랭킹을 삽입하기 위해서는 게임 데이터에서 사용하는 데이터의 inDate값이 필요합니다.
        // 따라서 데이터를 불러온 후, 해당 데이터의 inDate값을 추출하는 작업을 해야합니다.
        Debug.Log("데이터 조회를 시도합니다.");
        var bro = Backend.GameData.GetMyData(tableName, new Where());
        if (bro.IsSuccess() == false) {
            Debug.LogError("데이터 조회 중 문제가 발생했습니다 : " + bro);
            return;
        }
        Debug.Log("데이터 조회에 성공했습니다 : " + bro);
        if (bro.FlattenRows().Count > 0) {
            rowInDate = bro.FlattenRows()[0]["inDate"].ToString();
        }
        else  {
            Debug.Log("데이터가 존재하지 않습니다. 데이터 삽입을 시도합니다.");
            var bro2 = Backend.GameData.Insert(tableName);
            if (bro2.IsSuccess() == false) {
                Debug.LogError("데이터 삽입 중 문제가 발생했습니다 : " + bro2);
                return;
            }
            Debug.Log("데이터 삽입에 성공했습니다 : " + bro2);
            rowInDate = bro2.GetInDate();
        }
        Debug.Log("내 게임 정보의 rowInDate : " + rowInDate); // 추출된 rowIndate의 값은 다음과 같습니다.

        Param param = new Param();
        param.Add("score", score);
        // 추출된 rowIndate를 가진 데이터에 param값으로 수정을 진행하고 랭킹에 데이터를 업데이트합니다.
        Debug.Log("랭킹 삽입을 시도합니다.");
        var rankBro = Backend.URank.User.UpdateUserScore(rankUUID, tableName, rowInDate, param);

        if (rankBro.IsSuccess() == false){
            Debug.LogError("랭킹 등록 중 오류가 발생했습니다. : " + rankBro);
            return;
        }
        Debug.Log("랭킹 삽입에 성공했습니다. : " + rankBro);
    }
}


