using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using Object = UnityEngine.Object;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / Teleportation Tracking Behaviour (UnityXR)"), 
     Description("Tracks teleportation events of <TeleportationProvider>, <TeleportationArea> and <TeleportationAnchor> components.")]
    public class TeleportationTrackingBehaviour : TrackingBehaviour
    {
        public TrackingBehaviourEvent OnTeleported;

        private void LocomotionStarted(LocomotionProvider sender)
        {
            Debug.Log("=====Started Locomotion=====");
            DestructXROrigin(sender.mediator.xrOrigin);
        }

        private void LocomotionEnded(LocomotionProvider sender)
        {
            Debug.Log("=====Ended Locomotion=====");
            DestructXROrigin(sender.mediator.xrOrigin);
        }

        private void StepLocomotion(LocomotionProvider sender)
        {
            // MainCamera -> head
            // XR Origin -> origin
            // 
            Debug.Log("=====Step Locomotion=====");
            DestructXROrigin(sender.mediator.xrOrigin);
        }

        private void DestructXROrigin(XROrigin xrOrigin)
        {
            var c = xrOrigin.Camera;
            var trackingSpace = xrOrigin.Origin;
            var cameraYOffset = xrOrigin.CameraYOffset;
            var floorOffset = xrOrigin.CameraFloorOffsetObject;
            Debug.Log(c.transform.position);
            Debug.Log(trackingSpace.transform.position);
            Debug.Log(cameraYOffset);
            Debug.Log(floorOffset.transform.position);
        }

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var tps = Select<TeleportationProvider>(objects);
            foreach (var tp in tps)
            {
                tp.locomotionStarted += LocomotionStarted;
                tp.locomotionEnded += LocomotionEnded;
                tp.beforeStepLocomotion += StepLocomotion;
            }

            var tis = Select<BaseTeleportationInteractable>(objects);
            foreach (var ti in tis)
            {
                
                ti.teleporting.AddListener(e =>
                {
                    
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var tps = Select<TeleportationProvider>(objects);
            // Clear events
            foreach (var tp in tps)
            {
                tp.locomotionStarted -= LocomotionStarted;
                tp.locomotionEnded -= LocomotionEnded;
                tp.beforeStepLocomotion -= StepLocomotion;
            }
        }
    }
}