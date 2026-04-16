# AR-paws (DS 579)

Use Unity **2022.3 LTS** with **AR Foundation**. This folder contains **reusable scripts** only, not a complete Unity project.

**Character direction**: The team agreed on a **biped, humanoid-rig** companion. Free animations (e.g. Mixamo) target humanoid skeletons, so a **bear**-style character fits the pipeline well. Scripts work regardless of species or mesh.

## How to add this to your project

1. Create a new **3D (Built-in)** or **3D (URP)** project in Unity Hub.
2. In **Window → Package Manager**, add (your editor may suggest **5.0.x** family versions):
   - `AR Foundation`
   - `Google ARCore XR Plugin` (Android)
   - `Apple ARKit XR Plugin` (iOS)
3. Follow official **AR Foundation** samples or docs for a “Session + Plane” setup: place **XR Origin** (or your session root), **AR Session**, **AR Plane Manager**, and **AR Raycast Manager** in the scene.
4. Copy this repo’s `Assets/ARPaws` folder into your project’s `Assets` folder.
5. Add `PetNeedsController` to an empty GameObject. If you have Canvas sliders, wire them with `PetNeedsUIBindings`.
6. Add `ARPetsSurfacePlacer` to the **XR Origin** (or the object that has **AR Raycast Manager**) and assign a **pet prefab** (temporary mesh is fine).
7. On the pet prefab, add `PetCompanionBrain` and connect:
   - `needs` → the `PetNeedsController` in scene
   - `animator` → pet Animator
   - optional `bedSpot`, `foodSpot`, `playSpot` (see next step)
8. Place optional world markers with `PetDesignatedSpot` (bed/food/play). These become target points for AI movement.
9. Add `PetInteractionTarget` to toys/food/brush props and call `Interact()` from your hand/collider event pipeline.
10. Optional quick test path: add `PetScreenTapInteractor` to a scene object and click/tap colliders to trigger interaction actions.

## Script overview

| Script | Role |
|--------|------|
| `PetNeedsController` | Hunger, cleanliness, happiness, playfulness decay; feed / clean / pet / mini-game boosts |
| `PetNeedsPersistence` | Save/load JSON under `Application.persistentDataPath` + offline time decay on next launch |
| `PetNeedsUIBindings` | Updates sliders and mood label |
| `PetInteractionPanel` | UI buttons call interaction methods |
| `ARPetsSurfacePlacer` | First tap on a plane spawns the pet (mouse click in Editor) |
| `PetCompanionBrain` | Mood/interaction-driven roaming, spot targeting, optional Animator triggers |
| `PetDesignatedSpot` | Optional world anchors (bed, food, play) used by companion behavior |
| `PetInteractionTarget` | Interactable world props that forward Feed/Clean/Pet/Play actions |
| `PetScreenTapInteractor` | Touch/mouse raycast bridge that invokes nearby `PetInteractionTarget` |

## Licensing

Before submitting the course prototype, confirm **redistribution** rules for third-party assets (Sketchfab, Asset Store, etc.) with your team.
