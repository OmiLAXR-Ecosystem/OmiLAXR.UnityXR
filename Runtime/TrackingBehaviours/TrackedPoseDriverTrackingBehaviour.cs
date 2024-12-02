using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / Tracked Pose Driver Tracking Behaviour (UnityXR)"), 
     Description("Tracks XR controllers by using <TrackedPoseDriver> components.")]
    public class TrackedPoseDriverTrackingBehaviour : TrackingBehaviour<TrackedPoseDriver>
    {
        protected override void AfterFilteredObjects(TrackedPoseDriver[] tpds)
        {
            foreach (var tp in tpds)
            {
                
            }
        }
    }
}