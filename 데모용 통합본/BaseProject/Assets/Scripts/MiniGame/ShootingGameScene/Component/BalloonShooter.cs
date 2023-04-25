//--------------------------------------------------------------
// 파일명: BalloonShooter.cs
// 작성자: 박영훈
// 작성일: (-)
// 설명: 
// - 풍선을 사격하는 기능 구현을 담당하는 클래스.
// - 카메라로 정중앙에 갖다 댄 뒤, 사격하면 해당 풍선이 터지는 방식.
// 수정 :
// - 염민규(2023-04-15) : 랭킹 등록/게임 관리 기능 추가
// - 이수민(2023-04-18) : 랭킹 등록 및 게임 관리 부분 분리(ShootingGameSceneManager)
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BalloonShooter : MonoBehaviour {  
    //--------------------------------------------------------------
    // 변수 리스트 :
    // <유니티 설정>
    // - arCamera : arCamera 오브젝트, AR 폴더에서 카메라 객체 받아오기.
    // - smoke : 풍선 터트리기에 성공했을 때, 동작시킬 프리팹 오브젝트. Assets/Resources/Prefabs/MiniGame/ShootingGame에 넣어놨음.
    //--------------------------------------------------------------
    [SerializeField] GameObject arCamera;
    [SerializeField] GameObject smoke;


    //--------------------------------------------------------------
    // 메소드명 : ShootBalloon()
    // 설명 : 카메라를 이용하여 풍선 터뜨리는 메소드 + 터뜨릴 때마다 점수 추가
    // --------------------------------------------------------------
    public void ShootBalloon() {
        RaycastHit hit;
        if(Physics.Raycast(arCamera.transform.position, arCamera.transform.forward, out hit)) {
            if(hit.transform.tag == "Balloon") {
                Destroy(hit.transform.gameObject);
                Instantiate(smoke, hit.point, Quaternion.LookRotation(hit.normal));
                ShootingGameSceneManager.Instance.IncreaseScore();
            }
        }
    }
}
