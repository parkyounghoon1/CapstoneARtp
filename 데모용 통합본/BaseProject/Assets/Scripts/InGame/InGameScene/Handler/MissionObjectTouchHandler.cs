//--------------------------------------------------------------
// 파일명: MissionObjectTouchHandler.cs
// 작성자: 이수민
// 작성일: 2023-04-08
// 설명: 휴대폰에서 미션 포인트 터치 이벤트 처리용 핸들러
// 수정:
// - 이수민(2023-04-12) : 디버깅을 위한 극한의 단순화
// - 이수민(2023-04-18) : 더블터치 방지용 코드 추가
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MissionObjectTouchHandler : MonoBehaviour
{
    public delegate void TouchEventHandler();
    public event TouchEventHandler OnTouch; // 터치 이벤트 핸들러

    void OnMouseDown() {
        if (!EventSystem.current.IsPointerOverGameObject()) {
            OnTouch?.Invoke();
        }
    }
}