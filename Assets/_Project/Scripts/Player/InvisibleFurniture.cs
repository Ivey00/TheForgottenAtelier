
using UnityEngine;

public class InvisibleFurniture : MonoBehaviour
{
    [Header("Visibility")]
    [SerializeField] private bool hideOnStart = true;

    private void Awake()
    {
        if (hideOnStart)
        {
            HideFurniture();
        }
    }

    [ContextMenu("Hide Furniture")]
    public void HideFurniture()
    {
        Renderer[] renderers =
            GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = false;
        }

        Debug.Log(
            $"Hidden {renderers.Length} furniture renderers."
        );
    }

    [ContextMenu("Show Furniture")]
    public void ShowFurniture()
    {
        Renderer[] renderers =
            GetComponentsInChildren<Renderer>(true);

        foreach (Renderer renderer in renderers)
        {
            renderer.enabled = true;
        }

        Debug.Log(
            $"Shown {renderers.Length} furniture renderers."
        );
    }
}
