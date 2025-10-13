/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.UnityXR.
*/
using System.ComponentModel;
using OmiLAXR.Listeners;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
#if UNITY_2021_3_OR_NEWER
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
#endif

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