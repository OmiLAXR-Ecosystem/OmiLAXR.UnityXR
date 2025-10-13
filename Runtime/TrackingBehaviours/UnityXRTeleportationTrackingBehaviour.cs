/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.UnityXR.
*/
using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using OmiLAXR.TrackingBehaviours.Learner;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.XR;
#if UNITY_2021_3_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
#else
using UnityEngine.XR.Interaction.Toolkit;
#endif


namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / Teleportation Tracking Behaviour (UnityXR)"), 
     Description("Tracks teleportation events of <TeleportationProvider>, <TeleportationArea> and <TeleportationAnchor> components.")]
    public class UnityXRTeleportationTrackingBehaviour : TeleportationTrackingBehaviour
    { 
        private XROrigin _xrOrigin;
        private Vector3 _destinationPosition;
        private Vector3 _destinationRotation;
        private float _requestTime;
        private TeleportationProvider _teleportationProvider;
        private BaseTeleportationInteractable _target;
        private int _isTriggeredEndFramesCatch = -1;
        
        private TeleportationPlayerState _beginState;

     
#if UNITY_2021_3_OR_NEWER
         private void LocomotionEnded(LocomotionProvider sender)
#else
        private void LocomotionEnded(LocomotionSystem sender)
#endif
        {
            // Debug.Log("=====Ended Locomotion=====");
            _isTriggeredEndFramesCatch = 0;
        }
        
        private TeleportationPlayerState GetPlayerState(XROrigin xrOrigin)
        {
            var c = xrOrigin.Camera;
            var cameraYOffset = xrOrigin.CameraYOffset;
            var floorOffset = xrOrigin.CameraFloorOffsetObject;
            var gazeDirection = c.transform.forward.normalized;

            var floorPosition = xrOrigin.CurrentTrackingOriginMode == TrackingOriginModeFlags.Device
                ? c.transform.position - Vector3.up * cameraYOffset
                : c.transform.position;
            
            return new TeleportationPlayerState()
            {
                FloorPosition = floorPosition,
                CameraGazeDirection = gazeDirection,
            };
        }
        
        protected override void AfterFilteredObjects(Object[] objects)
        {
            var tp = First<TeleportationProvider>(objects);
            _xrOrigin = First<XROrigin>(objects);
            
            if (tp)
            {
#if UNITY_2021_3_OR_NEWER
                tp.locomotionEnded += LocomotionEnded;
#else
                tp.endLocomotion += LocomotionEnded;
#endif

            }
            
            var tis = Select<BaseTeleportationInteractable>(objects);
            foreach (var ti in tis)
            {
                ti.teleporting.AddListener(OnRequestTeleportation);
            }
        }
        
        private void OnRequestTeleportation(TeleportingEventArgs args)
        {
            // Debug.Log($"Teleporting: {args} -> {args.teleportRequest.destinationPosition}, Request Time: {args.teleportRequest.requestTime}");
            _destinationPosition = args.teleportRequest.destinationPosition;
            _requestTime = args.teleportRequest.requestTime;
            _target = (BaseTeleportationInteractable)args.interactableObject;        
            _beginState = GetPlayerState(_xrOrigin);
        }
        
        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var tps = Select<TeleportationProvider>(objects);
            // Clear events
            foreach (var tp in tps)
            {
#if UNITY_2021_3_OR_NEWER
                tp.locomotionEnded += LocomotionEnded;
#else
                tp.endLocomotion += LocomotionEnded;
#endif
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

            var targetType = TeleportationTargetType.Floor;

            if (_target is TeleportationArea)
            {
                targetType = TeleportationTargetType.Area;
            }
            else if (_target is TeleportationAnchor)
            {
                targetType = TeleportationTargetType.Anchor;
            }
            
            OnTeleported?.Invoke(this, new TeleportationArgs()
            {
                StartState = _beginState,
                EndState = GetPlayerState(_xrOrigin),
                Target = _target.transform.gameObject,
                TargetType = targetType,
                DestinationPosition = _destinationPosition,
                DestinationRotation = _destinationRotation,
                RequestTime = _requestTime,
                CameraYOffset = _xrOrigin.CameraYOffset
            });
            
            _isTriggeredEndFramesCatch = -1;
        }
    }
}