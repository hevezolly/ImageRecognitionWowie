using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageObject : FullImage
{
    [SerializeField]
    private Vector2 localSpawnCenter;
    [SerializeField]
    private float spawnRange;
    [SerializeField]
    private Vector2 MinMaxRotation;
    [SerializeField]
    private Transform ParentOverride;
    [SerializeField]
    private Material materialToUse;
    [SerializeField]
    private List<Renderer> renderers;

    private Material _material;

    public override Material usedMaterial => _material;
    
    protected override void Awake()
    {
        base.Awake();
        _material = new Material(materialToUse);
        foreach (var r in renderers)
        {
            r.material = _material;
        }
    }

    protected override void PopulateSelf(SinglePartRequest request)
    {
        base.PopulateSelf(request);
    }

    private Vector2 GetRandomLocalPosition() 
    {
        return Random.insideUnitCircle * spawnRange;
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Mathf.Lerp(MinMaxRotation.x, MinMaxRotation.y, Random.value));
    }

    protected override ImagePart InstanciateBody(IPartProvider provider)
    {
        var bodyPart = provider.GetUninstanciatedPart();
        if (bodyPart == null)
            return null;
        var position = transform.TransformPoint(GetRandomLocalPosition());
        var rotation = transform.rotation * GetRandomRotation();
        var instance = Instantiate(bodyPart, position, rotation);
        var parent = ParentOverride ?? transform;
        instance.transform.SetParent(parent, true);
        return instance;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawWireSphere(localSpawnCenter, spawnRange);
        Gizmos.matrix = Matrix4x4.identity;
        var center = transform.TransformPoint(localSpawnCenter);
        var rot = Quaternion.Euler(0, 0, MinMaxRotation.x);
        Gizmos.DrawLine(center, center + rot * transform.up);
        var rot2 = Quaternion.Euler(0, 0, MinMaxRotation.y);
        Gizmos.DrawLine(center, center + rot2 * transform.up);
    }
}
