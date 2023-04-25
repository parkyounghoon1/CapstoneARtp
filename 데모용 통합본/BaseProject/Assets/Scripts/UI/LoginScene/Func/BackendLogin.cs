//--------------------------------------------------------------
// 파일명: BackendLogin.cs
// 작성자: 이수민
// 작성일: 2023-03-28
// 설명: 뒤끝을 통한 로그인/회원가입 작업 수행.
// 수정:
// - 이수민(2023-04-07) : 문서화 작업
// - 이수민(2023-04-09) : 팝업 기능 추가
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;

public class BackendLogin {
    // 자체 인스턴스
    private static BackendLogin _instance = null;
    public static BackendLogin Instance {
        get {
            if (_instance == null) {
                _instance = new BackendLogin();
            }
            return _instance;
        }
    }


    //--------------------------------------------------------------
    // 메소드명 : LoginProcess()
    // 입력 :
    // - id : 아이디
    // - pw : 패스워드
    // 설명 : 받아온 id, pw를 이용해 뒤끝으로 로그인 시도.
    //--------------------------------------------------------------
    public bool LoginProcess(string id, string pw) {
        Debug.Log("로그인을 요청합니다.");
        var bro = Backend.BMember.CustomLogin( id, pw);
        if (bro.IsSuccess()) {
            Debug.Log("로그인 성공!");
            return true;
        } else {
            Debug.LogError("로그인이 실패했습니다. : " + bro);
            return false;
        }
    }



    //--------------------------------------------------------------
    // 메소드명 : SignUpProcess()
    // 입력 :
    // - id : 아이디
    // - pw : 패스워드
    // 설명 : 받아온 id, pw를 이용해 뒤끝으로 회원가입 시도.
    //--------------------------------------------------------------
    public bool SignUpProcess(string id, string pw) {
        Debug.Log("회원가입을 요청합니다.");
        var bro = Backend.BMember.CustomSignUp(id, pw);
        if (bro.IsSuccess()) {
            Debug.Log("회원가입에 성공했습니다. : " + bro);
            return true;
        } else {
            Debug.LogError("회원가입에 실패했습니다. : " + bro);
            return false;
        }
    }
}
