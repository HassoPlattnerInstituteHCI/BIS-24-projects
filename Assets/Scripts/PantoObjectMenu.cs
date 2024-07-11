using UnityEditor;
using UnityEngine;
using DualPantoToolkit;

public class PantoObjectMenu : MonoBehaviour
{
    // Add a menu item to create custom GameObjects.
    // Priority 1 ensures it is grouped with the other menu items of the same kind
    // and propagated to the hierarchy dropdown and hierarchy context menus.
    [MenuItem("GameObject/Panto/MeHandle", false, 10)]
    static void CreatePantoPlayer(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject go = GameObject.CreatePrimitive(PrimitiveType.Cube);
        go.name = "MeHandle";
        go.AddComponent<MeHandle>();
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(go, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }

    [MenuItem("GameObject/Panto/Wall", false, 9)]
    static void CreatePantoWall(MenuCommand menuCommand)
    {
        // Create a custom game object
        GameObject wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        wall.name = "Wall";
        wall.AddComponent<PantoBoxCollider>();
        // Ensure it gets reparented if this was a context click (otherwise does nothing)
        GameObjectUtility.SetParentAndAlign(wall, menuCommand.context as GameObject);
        // Register the creation in the undo system
        Undo.RegisterCreatedObjectUndo(wall, "Create " + wall.name);
        Selection.activeObject = wall;
    }
}
