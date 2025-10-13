/*
* SPDX-License-Identifier: AGPL-3.0-or-later
* Copyright (C) 2025 Sergej Görzen <sergej.goerzen@gmail.com>
* This file is part of OmiLAXR.UnityXR.
*/
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