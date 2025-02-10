using System.ComponentModel;
using OmiLAXR.Listeners;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace OmiLAXR.UnityXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / <XROrigin> Objects Listener (UnityXR)"),
     Description("Provides all <XROrigin> components to pipeline.")]
    public class XROriginListener : AutoListener<XROrigin>
    {
        
    }
}