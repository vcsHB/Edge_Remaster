// Assets/Editor/CanvasAABBValidator.cs
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public static class CanvasAABBValidator
{
    static CanvasAABBValidator()
    {
        Canvas.willRenderCanvases += ValidateAllCanvasRenderers;
    }

    static void ValidateAllCanvasRenderers()
    {
        foreach (var cr in Object.FindObjectsOfType<CanvasRenderer>())
        {
            // Unity 6+ : GetMesh() → 인수 없이 Mesh 반환 :contentReference[oaicite:0]{index=0}
            Mesh mesh = cr.GetMesh();
            if (mesh == null) 
                continue;  // Mesh가 아직 생성되지 않은 경우 넘어가기

            // Bounds 검사
            Bounds b = mesh.bounds;
            bool invalid = float.IsNaN(b.min.x) || float.IsNaN(b.min.y) || float.IsNaN(b.min.z)
                         || float.IsNaN(b.max.x) || float.IsNaN(b.max.y) || float.IsNaN(b.max.z)
                         || float.IsInfinity(b.min.x) || float.IsInfinity(b.min.y) || float.IsInfinity(b.min.z)
                         || float.IsInfinity(b.max.x) || float.IsInfinity(b.max.y) || float.IsInfinity(b.max.z);

            if (invalid)
            {
                Debug.LogError(
                    $"[Invalid AABB] GameObject: {cr.gameObject.name}\n" +
                    $"Bounds.min={b.min}, Bounds.max={b.max}",
                    cr.gameObject
                );
            }
        }
    }
}
