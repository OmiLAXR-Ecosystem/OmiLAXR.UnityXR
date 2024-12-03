using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using Object = UnityEngine.Object;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / Teleportation Tracking Behaviour (UnityXR)"), 
     Description("Tracks teleportation events of <TeleportationProvider>, <TeleportationArea> and <TeleportationAnchor> components.")]
    public class TeleportationTrackingBehaviour : TrackingBehaviour
    {
        public struct TeleportationPlayerState
        {
            public Vector3 FloorPosition;
            public Vector3 CameraGazeDirection;
        }

        public struct TeleportationArgs
        {
            public float CameraYOffset;
            public TeleportationPlayerState StartState;
            public TeleportationPlayerState EndState;
            public BaseTeleportationInteractable Target;
            public Vector3 DestinationPosition;
            public Vector3 DestinationRotation;
            public float RequestTime;
        }

        // as end teleportation throws same end position like start position, we are detecting change on our own
        private TeleportationPlayerState _beginState;
        
        private XROrigin _teleportingXrOrigin;
        private Vector3 _destinationPosition;
        private Vector3 _destinationRotation;
        private float _requestTime;
        private TeleportationProvider _teleportationProvider;
        private BaseTeleportationInteractable _target;
        private int _isTriggeredEndFramesCatch = -1;
        
        [Gesture("XRController"), Action("Teleport")]
        public readonly TrackingBehaviourEvent<TeleportationArgs> OnTeleported 
            = new TrackingBehaviourEvent<TeleportationArgs>();

        private void LocomotionEnded(LocomotionProvider sender)
        {
            Debug.Log("=====Ended Locomotion=====");
            _isTriggeredEndFramesCatch = 0;
        }

        private TeleportationPlayerState GetPlayerState(XROrigin xrOrigin)
        {
            var c = xrOrigin.Camera;
            var cameraYOffset = xrOrigin.CameraYOffset;
            var floorOffset = xrOrigin.CameraFloorOffsetObject;
            var gazeDirection = c.transform.forward.normalized;
            // Debug.Log("Position " + c.transform.position);
            // Debug.Log("CameraYOffset " + cameraYOffset);
            // Debug.Log("FloorOffset " + floorOffset.transform.position);
            // Debug.Log("GazeDirection " + gazeDirection);

            var floorPosition = xrOrigin.CurrentTrackingOriginMode == TrackingOriginModeFlags.Device
                ? c.transform.position - Vector3.up * cameraYOffset
                : c.transform.position;

            // Debug.Log("FloorPosition " + floorPosition);
            
            return new TeleportationPlayerState()
            {
                FloorPosition = floorPosition,
                CameraGazeDirection = gazeDirection,
            };
        }

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var tp = First<TeleportationProvider>(objects);
            if (tp)
            {
                tp.locomotionEnded += LocomotionEnded;
                _teleportingXrOrigin = tp.mediator.xrOrigin;
            }
            
            var tis = Select<BaseTeleportationInteractable>(objects);
            foreach (var ti in tis)
            {
                ti.teleporting.AddListener(OnRequestTeleportation);
            }
        }

        private void OnRequestTeleportation(TeleportingEventArgs args)
        {
            if (!_teleportingXrOrigin)
                return;
            // Debug.Log($"Teleporting: {args} -> {args.teleportRequest.destinationPosition}, Request Time: {args.teleportRequest.requestTime}");
            _destinationPosition = args.teleportRequest.destinationPosition;
            _requestTime = args.teleportRequest.requestTime;
            _target = (BaseTeleportationInteractable)args.interactableObject;        
            _beginState = GetPlayerState(_teleportingXrOrigin);
        }

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var tps = Select<TeleportationProvider>(objects);
            // Clear events
            foreach (var tp in tps)
            {
                tp.locomotionEnded -= LocomotionEnded;
            }
            var tis = Select<BaseTeleportationInteractable>(objects);
            foreach (var ti in tis)
            {
                ti.teleporting.RemoveListener(OnRequestTeleportation);
            }
            OnTeleported.Dispose();
        }

        private void Update()
        {
            if (_isTriggeredEndFramesCatch < 0)
                return;
            if (_isTriggeredEndFramesCatch < 3)
            {
                _isTriggeredEndFramesCatch++;
                return;
            }
            OnTeleported?.Invoke(this, new TeleportationArgs()
            {
                StartState = _beginState,
                EndState = GetPlayerState(_teleportingXrOrigin),
                Target = _target,
                DestinationPosition = _destinationPosition,
                DestinationRotation = _destinationRotation,
                RequestTime = _requestTime,
                CameraYOffset = _teleportingXrOrigin.CameraYOffset
            });
            _isTriggeredEndFramesCatch = -1;
        }
    }
}