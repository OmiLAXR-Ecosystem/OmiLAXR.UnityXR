using OmiLAXR.TrackingBehaviours;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

#if UNITY_2021_3_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Interactors;
#endif

namespace OmiLAXR.UnityXR.TrackingBehaviours
{
    public class GazeInteractorTrackingBehaviour : TrackingBehaviour<XRGazeInteractor>
    {
        public TrackingBehaviourEvent<HoverEnterEventArgs> OnHoverEnter;
        protected override void AfterFilteredObjects(XRGazeInteractor[] objects)
        {
            foreach (var obj in objects)
            {
                obj.hoverEntered.AddListener(OnHoverEntered);
            }
        }
        
        private void OnHoverEntered(HoverEnterEventArgs args) {}

        protected override void Dispose(Object[] objects)
        {
            base.Dispose(objects);
            foreach (var obj in objects)
            {
                
            }
        }
    }
}