//--------------------------------------------------------------
// 파일명: BalloonBehaviour.cs
// 작성자: 박영훈
// 작성일: (-)
// 설명: 
// - 생성된 풍선 오브젝트의 동작 제어 클래스.
// 수정 :
// - 이수민(2023-04-18) : 코드 문서화
//--------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBehaviour : MonoBehaviour
{
    //--------------------------------------------------------------
    // 메소드명 : Update()
    // 설명 : 풍선이 지속적으로 두둥실 뜨도록 만드는 메소드
    // --------------------------------------------------------------
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 0.2f);
    }
}
