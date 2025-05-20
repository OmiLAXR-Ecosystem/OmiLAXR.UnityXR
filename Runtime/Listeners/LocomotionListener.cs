using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;

#if UNITY_2021_3_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
#endif

namespace OmiLAXR.UnityXR.Listeners
{
#if UNITY_2021_3_OR_NEWER
    [AddComponentMenu("OmiLAXR / 1) Listeners / <LocomotionMediator> Objects Listener (UnityXR)")]
    [Description("Provides all <LocomotionMediator> components to pipeline.")]
    public class LocomotionListener : AutoListener<LocomotionMediator>
    {
        
    }
#else
    [AddComponentMenu("OmiLAXR / 1) Listeners / <LocomotionMediator> Objects Listener (UnityXR)")]
    [Description("Not supported in under Unity 2021.3")]
    public class LocomotionListener : Listener
    {
        public override void StartListening()
        {
            OmiLAXR.DebugLog.OmiLAXR.Warning("Locomotion Listener is not supported in Unity 2020.");
        }
    }
#endif
}