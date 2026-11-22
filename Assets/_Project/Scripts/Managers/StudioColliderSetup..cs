
using UnityEngine;

public class StudioColliderSetup : MonoBehaviour
{
    [ContextMenu("Generate Mesh Colliders")]
    public void GenerateMeshColliders()
    {
        MeshFilter[] meshFilters =
            GetComponentsInChildren<MeshFilter>(true);

        int added = 0;

        foreach (MeshFilter meshFilter in meshFilters)
        {
            if (meshFilter.sharedMesh == null)
                continue;

            MeshCollider meshCollider =
                meshFilter.GetComponent<MeshCollider>();

            if (meshCollider == null)
            {
                meshCollider =
                    meshFilter.gameObject.AddComponent<MeshCollider>();

                added++;
            }

            meshCollider.sharedMesh = meshFilter.sharedMesh;
            meshCollider.convex = false;
            meshCollider.isTrigger = false;
        }

        Debug.Log(
            $"Studio Collider Setup: {added} Mesh Colliders added."
        );
    }
}
