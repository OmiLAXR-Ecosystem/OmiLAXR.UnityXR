using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / Locomotion Tracking Behaviour (UnityXR)"),
     Description("Tracks locomotion of XR Origin by using <LocomotionMediator>.")]
    public class LocomotionTrackingBehaviour : TrackingBehaviour<LocomotionMediator>
    {
        protected override void AfterFilteredObjects(LocomotionMediator[] objects)
        {
            foreach (var lm in objects)
            {
                
            }
        }
    }
}