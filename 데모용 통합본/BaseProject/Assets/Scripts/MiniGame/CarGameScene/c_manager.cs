using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.EventSystems;

public class c_manager : MonoBehaviour
{
    public GameObject indicator;
    public GameObject myCar;
    public float relocateDistance = 1.0f;

    ARRaycastManager arManager;
    GameObject placedObject;

    void Start()
    {
        arManager = GetComponent<ARRaycastManager>();

        indicator.SetActive(false);
    }

    void Update()
    {
        DetectGround();

        if (EventSystem.current.currentSelectedGameObject)
        {
            return;
        }

        if (indicator.activeInHierarchy && Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (placedObject == null)
                {
                    placedObject = Instantiate(myCar, indicator.transform.position, indicator.transform.rotation);
                }
                else
                {
                    if (Vector3.Distance(placedObject.transform.position, indicator.transform.position) > relocateDistance)
                    {
                        //placedObject.transform.SetPositionAndRotation(indicator.transform.position, indicator.transform.rotation);
                        placedObject.transform.position = indicator.transform.position;
                        placedObject.transform.rotation = indicator.transform.rotation;
                    }
                }
            }
        }
    }

    void DetectGround()
    {
        Vector2 screenSize = new Vector2(Screen.width * 0.5f, Screen.height * 0.5f);

        List<ARRaycastHit> hitInfos = new List<ARRaycastHit>();

        if (arManager.Raycast(screenSize, hitInfos, TrackableType.Planes))
        {
            indicator.SetActive(true);

            indicator.transform.position = hitInfos[0].pose.position;
            indicator.transform.rotation = hitInfos[0].pose.rotation;

            indicator.transform.position += indicator.transform.up * 0.01f;
        }
        else
        {
            indicator.SetActive(false);
        }
    }

}
