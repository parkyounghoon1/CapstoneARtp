using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UI_manager : MonoBehaviour
{
    [SerializeField] Text pointText;
    public static int point = 0;
    public int Point //선물박스랑 부딪히면 얻는 포인트
    {
        get { return point; }
        set { point = value; }
    }
    void Update()
    {
        updatePoint();
    }

    public void updatePoint()
    {
        pointText.text = Point.ToString();
    }
}
