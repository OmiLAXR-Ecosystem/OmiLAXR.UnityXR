using System;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using Object = UnityEngine.Object;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    public class TeleportationProviderTrackingBehaviour : TrackingBehaviour<TeleportationProvider>
    {
        public TrackingBehaviourEvent OnTeleported;
        protected override void AfterFilteredObjects(TeleportationProvider[] tps)
        {
            foreach (var tp in tps)
            {
                tp.locomotionStarted += LocomotionStarted;
                tp.locomotionEnded += LocomotionEnded;
                tp.beforeStepLocomotion += StepLocomotion;
            }
        }

        private void LocomotionStarted(LocomotionProvider sender)
        {
            Debug.Log("Started Locomotion");
        }

        private void LocomotionEnded(LocomotionProvider sender)
        {
            Debug.Log("Ended Locomotion");
        }

        private void StepLocomotion(LocomotionProvider sender)
        {
            // MainCamera -> head
            // XR Origin -> origin
            // 
            Debug.Log("Step Locomotion");
            var xrOrigin = sender.mediator.xrOrigin;
            var c = xrOrigin.Camera;
            var trackingSpace = xrOrigin.Origin;
            var cameraYOffset = xrOrigin.CameraYOffset;
            var floorOffset = xrOrigin.CameraFloorOffsetObject;
            Debug.Log("Camera " + c.transform.position);
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