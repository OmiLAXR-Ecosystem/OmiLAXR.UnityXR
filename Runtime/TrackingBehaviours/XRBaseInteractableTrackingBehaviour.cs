using System.ComponentModel;
using OmiLAXR.TrackingBehaviours.Learner;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using Object = UnityEngine.Object;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    [AddComponentMenu("OmiLAXR / 3) Tracking Behaviours / XRBaseInteractable Tracking Behaviour (UnityXR)"), 
     Description("A <InteractableTrackingBehaviour> for tracking XR controllers by using <XRBaseInteractable> components.")]
    public class XRBaseInteractableTrackingBehaviour : InteractableTrackingBehaviour
    {
        private UnityAction<HoverEnterEventArgs> _hoverEnterAction;
        private UnityAction<HoverExitEventArgs> _hoverExitAction;

        private void SelectEnter(SelectEnterEventArgs e)
        {
            // Debug.Log("selectEntered");
            OnGrabbed.Invoke(this, new InteractableEventArgs(e.interactableObject.transform.gameObject));
        }

        private void SelectExit(SelectExitEventArgs e)
        {
            Debug.Log("selectExited");
            OnInteracted.Invoke(this, new InteractableEventArgs(e.interactableObject.transform.gameObject));
        }

        protected override void AfterFilteredObjects(Object[] objects)
        {
            var interactables = Select<XRBaseInteractable>(objects);
            foreach (var interactable in interactables)
            {
                interactable.firstHoverEntered.AddListener(e =>
                {
                    // Debug.Log("firstHoverEntered");
                });
                interactable.lastHoverExited.AddListener(e =>
                {
                    // Debug.Log("lastHoverExited");
                });
                interactable.hoverEntered.AddListener(_hoverEnterAction = e =>
                {
                    Debug.Log("hoverEntered");
                    OnTouched.Invoke(this, new InteractableEventArgs(e.interactableObject.transform.gameObject));
                });
                interactable.hoverExited.AddListener(_hoverExitAction = e =>
                {
                    Debug.Log("hoverExited");
                    OnReleased.Invoke(this, new InteractableEventArgs(e.interactableObject.transform.gameObject));
                });
                interactable.firstSelectEntered.AddListener(e =>
                {
                    // Debug.Log("firstSelectEntered");
                });
                interactable.lastSelectExited.AddListener(e =>
                {
                    // Debug.Log("lastSelectExited");
                });
                interactable.selectEntered.AddListener(SelectEnter);
                interactable.selectExited.AddListener(SelectExit);
                interactable.firstFocusEntered.AddListener(e =>
                {
                    // Debug.Log("firstFocusEntered");
                });
                interactable.lastFocusExited.AddListener(e =>
                {
                    // Debug.Log("lastFocusExited");
                });
                interactable.focusEntered.AddListener(e =>
                {
                    // Debug.Log("focusEntered");
                });
                interactable.focusExited.AddListener(e =>
                {
                    // Debug.Log("focusExited");
                });
                interactable.activated.AddListener(e =>
                {
                    // Debug.Log("activated");
                });
                interactable.deactivated.AddListener(e =>
                {
                    // Debug.Log("deactivated");
                });
            }
        }

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            var interactables = Select<XRBaseInteractable>(objects);
            foreach (var interactable in interactables)
            {
                interactable.hoverEntered.RemoveListener(_hoverEnterAction);
                interactable.hoverExited.RemoveListener(_hoverExitAction);
                interactable.selectEntered.RemoveListener(SelectEnter);
                interactable.selectExited.RemoveListener(SelectExit);
            }
        }
    }
}