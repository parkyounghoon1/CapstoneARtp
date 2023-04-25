using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class DrivingSurfaceManager : MonoBehaviour
{
    public ARRaycastManager RaycastManager;
    public ARPlaneManager PlaneManager;
    public ARPlane LockedPlane;

    public void LockPlane(ARPlane keepPlane)
    {
        var arPlane = keepPlane.GetComponent<ARPlane>();
        foreach (var plane in PlaneManager.trackables)
        {
            if (plane != arPlane)
            {
                plane.gameObject.SetActive(false);
            }
        }

        LockedPlane = arPlane;
        PlaneManager.planesChanged += DisableNewPlanes;
    }

    private void Start()
    {
        RaycastManager = GetComponent<ARRaycastManager>();
        PlaneManager = GetComponent<ARPlaneManager>();
    }

    private void Update()
    {
        if (LockedPlane?.subsumedBy != null)
        {
            LockedPlane = LockedPlane.subsumedBy;
        }

    }

    private void DisableNewPlanes(ARPlanesChangedEventArgs args)
    {
        foreach (var plane in args.added)
        {
            plane.gameObject.SetActive(false);
        }
    }
}
