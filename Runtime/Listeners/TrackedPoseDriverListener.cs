using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;
using UnityEngine.InputSystem.XR;

namespace OmiLAXR.UnityXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / <TrackedPoseDriver> Objects Listener (UnityXR)")]
    [Description("Provides all <TrackedPoseDriver> components to pipeline.")]
    public class TrackedPoseDriverListener : AutoListener<TrackedPoseDriver>
    {
        
    }
}