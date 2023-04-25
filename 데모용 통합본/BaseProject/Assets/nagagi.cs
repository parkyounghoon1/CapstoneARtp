using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nagagi : MonoBehaviour
{
    public void NagagiBtnClicked()
    {
        SceneTransManager.Instance.GoToScene(m_Contents.SCENE_WORLD, new List<GameObject>());
    }

}
