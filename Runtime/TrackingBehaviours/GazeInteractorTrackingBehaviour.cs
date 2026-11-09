/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.UnityXR.
*/
using System.ComponentModel;
using OmiLAXR.TrackingBehaviours;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_2021_3_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Interactors;
#endif

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    /// <summary>
    /// Reports when the learner's gaze starts or stops hovering an interactable, using the hover events of
    /// XR Interaction Toolkit <c>XRGazeInteractor</c>s found by <see cref="Listeners.GazeInteractorListener"/>.
    /// The event arguments carry the gazed interactable (<c>interactableObject</c>).
    /// No xAPI composer handles these events yet; subscribe to them in your own composer.
    /// </summary>
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / Gaze Interactor Tracking Behaviour (UnityXR)"),
     Description("Tracks which interactables the learner gazes at by using <XRGazeInteractor> components.")]
    public class GazeInteractorTrackingBehaviour : TrackingBehaviour<XRGazeInteractor>
    {
        /// <summary>The gaze started hovering an interactable.</summary>
        [Gesture("Gaze"), Action("Enter")]
        public TrackingBehaviourEvent<HoverEnterEventArgs> OnHoverEnter = new TrackingBehaviourEvent<HoverEnterEventArgs>();

        /// <summary>The gaze stopped hovering an interactable.</summary>
        [Gesture("Gaze"), Action("Leave")]
        public TrackingBehaviourEvent<HoverExitEventArgs> OnHoverExit = new TrackingBehaviourEvent<HoverExitEventArgs>();

        protected override void AfterFilteredObjects(XRGazeInteractor[] objects)
        {
            foreach (var obj in objects)
            {
                obj.hoverEntered.AddListener(HandleHoverEntered);
                obj.hoverExited.AddListener(HandleHoverExited);
            }
        }

        private void HandleHoverEntered(HoverEnterEventArgs args) => OnHoverEnter.Invoke(this, args);

        private void HandleHoverExited(HoverExitEventArgs args) => OnHoverExit.Invoke(this, args);

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            foreach (var interactor in Select<XRGazeInteractor>(objects))
            {
                interactor.hoverEntered.RemoveListener(HandleHoverEntered);
                interactor.hoverExited.RemoveListener(HandleHoverExited);
            }
        }
    }
}