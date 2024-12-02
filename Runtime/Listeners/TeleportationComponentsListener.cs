using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace OmiLAXR.UnityXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / Teleportation Components Listener (UnityXR)")]
    [Description("Provides all <TeleportationProvider>, <BaseTeleportationInteractable> (<TeleportationAnchor>, <TeleportationArea>) components to pipeline.")]
    public class TeleportationComponentsListener : Listener
    {
        public override void StartListening()
        {
            Detect<BaseTeleportationInteractable>(); // TeleportArea & TeleportAnchor
            Detect<TeleportationProvider>();
        }
    }
}