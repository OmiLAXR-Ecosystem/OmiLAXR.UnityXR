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
using UnityEngine.XR.Interaction.Toolkit.Interactables;
#endif

namespace OmiLAXR.UnityXR.Listeners
{
    [AddComponentMenu("OmiLAXR / 1) Listeners / <XRBaseInteractable> Objects Listener (UnityXR)"),
     Description("Provides all <XRBaseInteractable> components to pipeline.")]
    public class XRBaseInteractableListener : AutoListener<XRBaseInteractable>
    {
        
    }
}