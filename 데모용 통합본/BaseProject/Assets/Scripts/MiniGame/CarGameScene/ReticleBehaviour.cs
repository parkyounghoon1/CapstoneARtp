using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Unity.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ReticleBehaviour : MonoBehaviour
{
    public GameObject Child;
    public DrivingSurfaceManager DrivingSurfaceManager;
    public ARPlane CurrentPlane;

    private void Start()
    {
        Child = transform.GetChild(0).gameObject;
    }

    private void Update()
    {
        var screenCenter = Camera.main.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        DrivingSurfaceManager.RaycastManager.Raycast(screenCenter, hits, TrackableType.PlaneWithinBounds);

        CurrentPlane = null;
        ARRaycastHit? hit = null;
        if (hits.Count > 0)
        {
            var lockedPlane = DrivingSurfaceManager.LockedPlane;
            hit = lockedPlane == null
                ? hits[0]
                : hits.SingleOrDefault(x => x.trackableId == lockedPlane.trackableId);
        }
        if (hit.HasValue)
        {
            CurrentPlane = DrivingSurfaceManager.PlaneManager.GetPlane(hit.Value.trackableId);
            transform.position = hit.Value.pose.position;
        }
        Child.SetActive(CurrentPlane != null);
    }
}
