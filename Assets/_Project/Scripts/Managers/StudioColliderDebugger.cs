
using UnityEngine;

public class StudioColliderDebugger : MonoBehaviour
{
    [ContextMenu("Disable All Mesh Colliders")]
    public void DisableAll()
    {
        MeshCollider[] colliders =
            GetComponentsInChildren<MeshCollider>(true);

        foreach (MeshCollider col in colliders)
        {
            col.enabled = false;
        }

        Debug.Log(
            $"Disabled {colliders.Length} Mesh Colliders."
        );
    }

    [ContextMenu("Enable All Mesh Colliders")]
    public void EnableAll()
    {
        MeshCollider[] colliders =
            GetComponentsInChildren<MeshCollider>(true);

        foreach (MeshCollider col in colliders)
        {
            col.enabled = true;
        }

        Debug.Log(
            $"Enabled {colliders.Length} Mesh Colliders."
        );
    }
}
