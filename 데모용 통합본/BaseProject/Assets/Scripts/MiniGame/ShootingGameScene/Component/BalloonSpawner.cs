//--------------------------------------------------------------
// 파일명: BalloonSpawner.cs
// 작성자: 박영훈
// 작성일: (-)
// 설명: 
// - 화면에 풍선을 지속적으로 깔아주는 클래스.
// 수정 :
// - 이수민(2023-04-18) : 코드 문서화, 마이너한 변경
// - 이수민(2023-04-18) : Start() 제거
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonSpawner : MonoBehaviour {
    //--------------------------------------------------------------
    // 변수 리스트 :
    // <유니티 설정>
    // - balloons : 생성할 풍선 오브젝트, Assets/Resources/Prefabs/MiniGame/ShootingGame에 넣어둔거를 유니티 에디터에서 매칭시킬 것.
    // - arCamera : arCamera 오브젝트, AR 폴더에서 카메라 객체 받아오기
    // <내부 변수>
    // - spawnDistance : 오브젝트 생성 최소 거리
    // - spawnAngle : 오브젝트 생성 각도
    //--------------------------------------------------------------
    public GameObject[] Balloons;
    public Camera arCamera;
    public float spawnDistance = 5f;
    public float spawnAngle = 45f;


    //--------------------------------------------------------------
    // 메소드명 : OnEnable()
    // 설명 : 풍선 오브젝트 생성을 시작하는 메소드
    // --------------------------------------------------------------
    void OnEnable() {
        StartCoroutine(SpawnBalloon());
    }


    //--------------------------------------------------------------
    // 메소드명 : SpawnBalloon()
    // 설명 : 실질적으로 풍선을 생성하는 메소드
    // --------------------------------------------------------------
    IEnumerator SpawnBalloon() {
        yield return new WaitForSeconds(3); //스폰대기시간

        for (int i = 0; i < 3; i++) {
            int random = Random.Range(0, Balloons.Length);
            Vector3 spawnPos = GetRandomSpawnPosition(); // 스폰 위치 받아오기
            Instantiate(Balloons[random], spawnPos, Quaternion.identity);
        }
        StartCoroutine(SpawnBalloon());
    }


    //--------------------------------------------------------------
    // 메소드명 : GetRandomSpawnPosition()
    // 설명 : 풍선을 생성할 랜덤한 위치를 뱉어내는 하위 메소드
    // --------------------------------------------------------------
    Vector3 GetRandomSpawnPosition() {
        Vector3 cameraPos = arCamera.transform.position;
        Quaternion cameraRot = arCamera.transform.rotation;
        float angle = Random.Range(-spawnAngle, spawnAngle);
        float distance = Random.Range(0, spawnDistance);
        Vector3 spawnPos = cameraPos + cameraRot * Quaternion.Euler(0, angle, 0) * Vector3.forward * distance;
        return spawnPos;
    }
}
