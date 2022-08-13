using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class MeshImagePart : SelfPopulatingPart
{
    [SerializeField]
    private Sprite baseSprite;
    [SerializeField]
    private MeshFilter filter;
    [SerializeField]
    private MeshRenderer renderer;
    [SerializeField]
    private List<SecondaryTexture> secondaryTextures;
    [SerializeField]
    private List<ImagePartModification> minorRandomizationBodyModifications;

    protected override void PopulateSelf(SinglePartRequest request)
    {
        GenerateMesh();
        base.PopulateSelf(request);
    }

    public void GenerateMesh()
    {
        SetMeshFrom(baseSprite, secondaryTextures);
    }

    public void SetMeshFrom(Sprite sprite, List<SecondaryTexture> secondaryTextures)
    {
        var mesh = filter.sharedMesh;
        if (mesh == null)
            mesh = new Mesh();
        mesh.Clear();
        mesh.vertices = sprite.vertices.Select(v => (Vector3)v).ToArray();
        mesh.triangles = sprite.triangles.Select(v => (int)v).ToArray();
        mesh.SetUVs(0, sprite.uv);
        mesh.SetNormals(Enumerable.Repeat(-Vector3.forward, sprite.uv.Length).ToArray());
        filter.sharedMesh = mesh;
        var mat = renderer.sharedMaterial;
        if (Application.isPlaying)
            mat = renderer.material;
        mat.SetTexture("_MainTex", sprite.texture);
        foreach (var s in secondaryTextures)
        {
            mat.SetTexture(s.name, s.texture);
        }
        foreach (var m in minorRandomizationBodyModifications)
        {
            ApplyModification(m);
        }
    }
}

[System.Serializable]
public class SecondaryTexture
{

    public Texture2D texture;

    public string name;
}
