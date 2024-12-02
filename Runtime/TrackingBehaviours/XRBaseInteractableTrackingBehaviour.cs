using OmiLAXR.TrackingBehaviours;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    public class XRBaseInteractableTrackingBehaviour : TrackingBehaviour<XRBaseInteractable>
    {
        protected override void AfterFilteredObjects(XRBaseInteractable[] interactables)
        {
            foreach (var interactable in interactables)
            {
                interactable.firstHoverEntered.AddListener(e =>
                {
                    Debug.Log("firstHoverEntered");
                });
                interactable.lastHoverExited.AddListener(e =>
                {
                    Debug.Log("lastHoverExited");
                });
                interactable.hoverEntered.AddListener(e =>
                {
                    Debug.Log("hoverEntered");
                });
                interactable.hoverExited.AddListener(e =>
                {
                    Debug.Log("hoverExited");
                });
                interactable.firstSelectEntered.AddListener(e =>
                {
                    Debug.Log("firstSelectEntered");
                });
                interactable.lastSelectExited.AddListener(e =>
                {
                    Debug.Log("lastSelectExited");
                });
                interactable.selectEntered.AddListener(e =>
                {
                    Debug.Log("selectEntered");
                });
                interactable.selectExited.AddListener(e =>
                {
                    Debug.Log("selectExited");
                });
                interactable.firstFocusEntered.AddListener(e =>
                {
                    Debug.Log("firstFocusEntered");
                });
                interactable.lastFocusExited.AddListener(e =>
                {
                    Debug.Log("lastFocusExited");
                });
                interactable.focusEntered.AddListener(e =>
                {
                    Debug.Log("focusEntered");
                });
                interactable.focusExited.AddListener(e =>
                {
                    Debug.Log("focusExited");
                });
                interactable.activated.AddListener(e =>
                {
                    Debug.Log("activated");
                });
                interactable.deactivated.AddListener(e =>
                {
                    Debug.Log("deactivated");
                });
                // if (interactable is XRGrabInteractable)
                // {
                //     var grabInteractable = interactable as XRGrabInteractable;
                //     grabInteractable.grabbed.AddListener(e =>
                //     {
                //         Debug.Log("grabbed");
                //     });
                //     grabInteractable.ungrabbed.AddListener(e =>
                //     {
                //         Debug.Log("ungrabbed");
                //     });
                // }
            }
        }
    }
}