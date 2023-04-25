//--------------------------------------------------------------
// 파일명: RankItem.cs
// 작성자: 염민규
// 작성일: 2023-04-15
// 설명: 랭킹 관련 데이터 저장용 클래스
// 수정:
// - 이수민(2023-04-17) - 문서화 작업, MinigameDataManager에서 분리
//--------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

// "원래는" 이거 private 시켜놔야 되는데, 지금은 public으로 해놨음. 나중에 빼놓지 말고 바꿔두기
    public class RankItem {
        //--------------------------------------------------------------
        // 변수 리스트 :
        // - gamerInDate : 유저 인데이트 변수
        // - nickname : 랭킹에 등록할 유저의 닉네임
        // - score : 유저의 점수
        // - index : 
        // - rank : 랭킹 순위
        // - extraData : 
        // - extraName : 
        // - totalCount : 
        //--------------------------------------------------------------
        public string gamerInDate = string.Empty;
        public string nickname = string.Empty;
        public string score = string.Empty;
        public string index = string.Empty;
        public string rank = string.Empty;
        public string extraData = string.Empty;
        public string extraName = string.Empty;
        public string totalCount = string.Empty;


        //--------------------------------------------------------------
        // 메소드명 : ToString()
        // 설명 : 디버깅용 메소드(Debug.log(~~~))
        //--------------------------------------------------------------
        public override string ToString() {
            string str = $"유저인데이트:{gamerInDate}\n닉네임:{nickname}\n점수:{score}\n정렬:{index}\n순위:{rank}\n총합:{totalCount}\n";
            if (extraName != string.Empty){
                str += $"{extraName}:{extraData}\n";
            }
            return str;
        }
    }
