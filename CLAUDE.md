# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

Working rules (versioning, CHANGELOG, `.meta` files, licenses, compatibility) are in @AGENTS.md. Follow them.

## What this is

`com.rwth.unity.omilaxr.unityxr`: Adapter module that tracks Unity XR Interaction Toolkit components (interactables, gaze interactor, teleportation, locomotion, XR Origin) in an OmiLAXR learner pipeline.

## Commands

There is no command-line build or linter: the package compiles inside a Unity project. Paths below are relative to the workspace root (`..`).

- **Develop:** open `OmiLAXR_Dev/` in Unity 6000.3.13f1. Its `Packages/manifest.json` links `com.rwth.unity.omilaxr.unityxr` from disk (`file:`), so edits recompile live.
- **Tests** (`Tests/`, assembly `OmiLAXR.UnityXR.Tests.Runtime`): the test asmdef is a runtime assembly, so the tests are PlayMode-only. Run them in the Unity Test Runner (PlayMode tab).
- **CLI test run.** Batch mode fails while the editor has the project open. In that case, run against an APFS copy (`cp -c -R OmiLAXR_Dev <scratch>/Dev`).

  ```bash
  /Applications/Unity/Hub/Editor/6000.3.13f1/Unity.app/Contents/MacOS/Unity -batchmode \
    -projectPath OmiLAXR_Dev -runTests -testPlatform PlayMode \
    -assemblyNames OmiLAXR.UnityXR.Tests.Runtime -testResults results.xml
  # single test: replace -assemblyNames … with -testFilter OmiLAXR.UnityXR.Tests.<Class>.<Method>
  ```

## Architecture

- **`UnityXRLearnerPipelineExtension`** (`PipelineExtension<LearnerPipeline>`) adds this module's listeners and tracking behaviours to the learner pipeline. The prefab is in `Resources/Prefabs`.
- **Listeners** (`Runtime/Listeners`) are mostly `AutoListener<T>`, which finds all `T` in the scene: `XRBaseInteractable`, `XRGazeInteractor`, `XROrigin`, `LocomotionMediator`, and teleportation components.
- **Tracking behaviours** (`Runtime/TrackingBehaviours`) subclass the core abstract behaviours, such as `InteractableTrackingBehaviour` and `TeleportationTrackingBehaviour`. The existing `OmiLAXR.xAPI` composers therefore handle them; this package has no composers and does not depend on xAPI. The exception is `GazeInteractorTrackingBehaviour` (`TrackingBehaviour<XRGazeInteractor>`, events `OnHoverEnter`/`OnHoverExit`): no xAPI composer handles it yet.
