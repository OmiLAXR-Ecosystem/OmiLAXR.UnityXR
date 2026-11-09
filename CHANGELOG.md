# Changelog

## [1.1.1] - 2026-11-09

### Changed
- Updated package version metadata.
- XML summaries for `GazeInteractorListener` and `GazeInteractorTrackingBehaviour`. They appear on docs.omilaxr.dev.
- `GazeInteractorListener` and `GazeInteractorTrackingBehaviour` are in the Add Component menu (`OmiLAXR / 1) Listeners` and `OmiLAXR / 3) Tracking Behaviours`).

### Fixed
- `GazeInteractorTrackingBehaviour` raises `OnHoverEnter` when an `XRGazeInteractor` starts hovering an interactable. Before, the event was never raised (and was null), so the behaviour produced no data. The new `OnHoverExit` is raised when the hover ends; both are tagged `[Gesture("Gaze")]` with the actions `Enter` and `Leave`. Disposing unsubscribes from the interactors.
- `UnityXRTeleportationTrackingBehaviour` now unsubscribes from `locomotionEnded`/`endLocomotion` on dispose instead of subscribing a second time.

## [1.1.0] - 2025-10-13

### Added
- `CITATION.cff`, `CODE_OF_CONDUCT.md`, `LICENSE` and SPDX license headers in all source files.

### Changed
- Made OmiLAXR v2.2.0 compatible. The core dependency is now `com.rwth.unity.omilaxr` 2.2.0.

## [1.0.1] - 2025-05-21

### Added
- `GazeInteractorListener` and `GazeInteractorTrackingBehaviour` for XR gaze interactors.

### Changed
- Compatible with Unity 2020: lowered the dependencies to `com.unity.inputsystem` 1.0.0, `com.unity.xr.core-utils` 2.0.0 and `com.unity.xr.interaction.toolkit` 2.3.0.
- Renamed the prefab `OmiLAXR.UnityXR (full)` to `OmiLAXR (UnityXR Variant)`.

### Removed
- An unneeded component from the `OmiLAXR.UnityXR` prefab.

## [1.0.0] - 2025-05-17

### Added
- `UnityXRLearnerPipelineExtension`, which registers the UnityXR listeners and tracking behaviours with the learner pipeline.
- `Learner Pipeline Extension` and `OmiLAXR.UnityXR (full)` prefabs.

### Changed
- Updated the default prefabs and component descriptions.

## [0.0.2-beta] - 2025-02-10

### Added
- `XROriginListener`.

### Changed
- Moved the remaining parts of `OmiLAXR.UnityXR.xAPI` into this package, which deprecates `OmiLAXR.UnityXR.xAPI`.
- Renamed `TeleportationTrackingBehaviour` to `UnityXRTeleportationTrackingBehaviour`.

### Removed
- `InputSystemTrackingBehaviour`, `LocomotionTrackingBehaviour`, `TrackedPoseDriverTrackingBehaviour` and `TrackedPoseDriverListener`. Input system tracking now lives in the OmiLAXR core.

### Fixed
- Teleportation tracking takes the `XROrigin` from the scene instead of the teleport mediator.

## [0.0.1-beta] - 2024-12-03

### Added
- `OnInteracted` and `OnGrabbed` events in `XRBaseInteractableTrackingBehaviour`.

## [0.0.1] - 2024-12-03

First release of OmiLAXR.UnityXR.

### Added
- `XRBaseInteractableListener` and `XRBaseInteractableTrackingBehaviour` for XR Interaction Toolkit interactables.
- `TeleportationComponentsListener`, `LocomotionListener` and `TeleportationTrackingBehaviour` with a teleport event.
- `InputSystemTrackingBehaviour`, `LocomotionTrackingBehaviour` and `TrackedPoseDriverTrackingBehaviour` with `TrackedPoseDriverListener`.
- `OmiLAXR.UnityXR` prefab.
