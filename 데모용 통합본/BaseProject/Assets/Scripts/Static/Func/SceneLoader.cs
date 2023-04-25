//--------------------------------------------------------------
// 파일명: SceneLoader.cs
// 작성자: 박영훈
// 작성일: (-)
// 설명: 다른 씬을 비동기적으로 로드하면서, 로딩 씬을 불러와 로딩 창을 보여주기 위한 클래스.
// 수정:
// - 이수민(2023-04-07) - 문서화 작업, 씬 전환 기능 통합
//--------------------------------------------------------------


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class SceneLoader : MonoBehaviour {
    //--------------------------------------------------------------
    // 변수 리스트 :
    // - nextScene : 넘어갈 다음 씬.
    // - progressBar : 로딩 바.
    //--------------------------------------------------------------
    static string nextScene;
    [SerializeField] Image progressBar;


    //--------------------------------------------------------------
    // 메소드명 : LoadScene()
    // 입력 : 
    // - sceneName : 넘어갈 다음 씬 이름.
    // 설명 : sceneName 세팅하고, 로딩 씬 불러오기.
    //--------------------------------------------------------------
    public static void LoadScene(string sceneName) {
        nextScene = sceneName;
        SceneManager.LoadScene("LoadingScene");
    }


    //--------------------------------------------------------------
    // 메소드명 : LoadSceneProcess()
    // 설명 : 
    // - 로딩창 동작 도중, 비동기적으로 다음 씬 로딩.
    // - 로딩 진행 수치에 따라 로딩바 변화
    // - 로딩 종료시 코루틴 종료 후 다음 씬으로 넘어감.
    //--------------------------------------------------------------
    IEnumerator LoadSceneProcess() {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float timer = 0f;
        while(!op.isDone)
        {
            yield return null;

            if(op.progress < 0.9f ) {
                progressBar.fillAmount = op.progress;
            } else {
                timer += Time.unscaledDeltaTime;
                progressBar.fillAmount = Mathf.Lerp(0.9f, 1f, timer);
                if(progressBar.fillAmount >= 1f) {
                    op.allowSceneActivation = true;
                    yield break;
                }
            }    
        }
    }


    //--------------------------------------------------------------
    // 메소드명 : Start()
    // 설명 : LoadSceneProcess() 코루틴 호출
    //--------------------------------------------------------------
    void Start() {
        StartCoroutine(LoadSceneProcess());
    }
}


