using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterBody : BodyImagePart
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
    protected override List<ImagePart> InstanciateEyes(IPartProvider partProvider)
    {
        //throw new System.NotImplementedException();
        return Enumerable.Empty<ImagePart>().ToList();
    }

    protected override List<ImagePart> InstanciateMouth(IPartProvider partProvider)
    {
        //throw new System.NotImplementedException();
        return Enumerable.Empty<ImagePart>().ToList();
    }

    protected override List<ImagePart> InstanciateNose(IPartProvider partProvider)
    {
        //throw new System.NotImplementedException();
        return Enumerable.Empty<ImagePart>().ToList();
    }

    private void GenerateMesh()
    {
        SetMeshFrom(baseSprite, secondaryTextures);
    }

    public void SetMeshFrom(Sprite sprite, List<SecondaryTexture> secondaryTextures)
    {
        var mesh = filter.mesh;
        if (mesh == null)
            mesh = new Mesh();
        mesh.SetVertices(sprite.vertices.Select(v => (Vector3)v).ToArray());
        mesh.SetUVs(0, sprite.uv);
        mesh.SetTriangles(sprite.triangles, 0);
        mesh.SetNormals(Enumerable.Repeat(-Vector3.forward, sprite.uv.Length).ToArray());
        filter.mesh = mesh;
        renderer.material.SetTexture("_MainTex", sprite.texture);
        foreach (var s in secondaryTextures)
        {
            renderer.material.SetTexture(s.name, s.texture);
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
