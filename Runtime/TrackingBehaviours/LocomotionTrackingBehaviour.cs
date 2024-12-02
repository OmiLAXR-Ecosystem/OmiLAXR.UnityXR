using OmiLAXR.TrackingBehaviours;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;

namespace OmiLAXR.Modules.YOUR_MODULE_NAME
{
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