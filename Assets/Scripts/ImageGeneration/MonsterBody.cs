using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterBody : BodyImagePart
{
    [SerializeField]
    private List<SpawnAreaData> partsSpawnAreas;


    [SerializeField]
    private int eyeMaxCount;

    [SerializeField]
    private int noseMaxCount;

    [SerializeField]
    private int mouthMaxCount;
    [SerializeField]
    private Vector2 minMaxRotation;
    [SerializeField]
    private Transform parentOverride;

    private List<SpawnAreaData> avalibleAreas;
    private List<Bounds> spawnedBounds;

    public override void PopulateBy(IImageRequest request)
    {
        avalibleAreas = new List<SpawnAreaData>(partsSpawnAreas);
        spawnedBounds = new List<Bounds>();
        base.PopulateBy(request);
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, 0, Mathf.Lerp(minMaxRotation.x, minMaxRotation.y, Random.value));
    }

    private SpawnAreaData SelectArea()
    {
        var i = Random.Range(0, avalibleAreas.Count);
        var area = avalibleAreas[i];
        avalibleAreas.RemoveAt(i);
        return area;
    }

    private Bounds GetBoundsOf(Rect rect)
    {
        var center = transform.TransformPoint(rect.center);
        var b = new Bounds(center, Vector3.zero);
        b.Encapsulate(transform.TransformPoint(rect.center + Vector2.right * rect.size.x / 2));
        b.Encapsulate(transform.TransformPoint(rect.center - Vector2.right * rect.size.x / 2));
        b.Encapsulate(transform.TransformPoint((Vector3)rect.center + Vector3.up * rect.size.y / 2 + Vector3.forward*10));
        b.Encapsulate(transform.TransformPoint((Vector3)rect.center - Vector3.up * rect.size.y / 2 - Vector3.forward * 10));
        return b;
    }

    private IEnumerable<System.Tuple<Vector2, Quaternion>> GetSpawnPositionAndRotation(SpawnAreaData area, ImagePart part, int maxNumOfElements, bool ignoreOthers = false)
    {
        var count = Random.Range(1, maxNumOfElements + 1);
        var selfBounds = GetBoundsOf(area.area);
        var dirtyPartBounds = part.GetBounds();
        for (var i = 0; i < count; i++)
        {
            for (var tryIndex = 0; tryIndex < 50; tryIndex++)
            {
                var pos = new Vector2(Random.Range(selfBounds.min.x + dirtyPartBounds.extents.x, 
                    selfBounds.max.x - dirtyPartBounds.extents.x),
                    Random.Range(selfBounds.min.y + dirtyPartBounds.extents.y,
                    selfBounds.max.y - dirtyPartBounds.extents.y));
                var rot = GetRandomRotation();
                part.transform.SetPositionAndRotation(pos, rot);
                var newBounds = part.GetBounds();
                if (selfBounds.max.x < newBounds.max.x || 
                    selfBounds.max.y < newBounds.max.y || 
                    selfBounds.min.x > newBounds.min.x ||
                    selfBounds.min.y > newBounds.min.y)
                    continue;
                if (ignoreOthers || spawnedBounds.Count == 0 || spawnedBounds.All(b => !b.Intersects(newBounds)))
                {
                    yield return new System.Tuple<Vector2, Quaternion>(pos, rot);
                    spawnedBounds.Add(newBounds);
                    break;
                }
            }
        }
    }

    private List<ImagePart> SpawnParts(IPartProvider provider, int maxNumber) 
    {
        var basObj = provider.GetUninstanciatedPart();
        var area = SelectArea();
        var parts = new List<ImagePart>();
        var data = GetSpawnPositionAndRotation(area, basObj, maxNumber).ToList();
        if (data.Count == 0)
            data = GetSpawnPositionAndRotation(area, basObj, maxNumber, true).ToList();
        if (data.Count == 0)
        {
            var pos = GetBoundsOf(area.area).center;
            var rot = GetRandomRotation();
            data = new List<System.Tuple<Vector2, Quaternion>>(
                Enumerable.Repeat(new System.Tuple<Vector2, Quaternion>(pos, rot), 1));
        }
            
        foreach (var spawnData in data)
        {
            var spawnPoint = spawnData.Item1;
            var rotation = spawnData.Item2;
            var inst = Instantiate(basObj, spawnPoint, rotation);
            var flip = Random.value < 0.5f;
            if (flip)
                inst.transform.localScale = new Vector3(-inst.transform.localScale.x,
                    inst.transform.localScale.y,
                    inst.transform.localScale.z);
            var parent = parentOverride ?? transform;
            inst.transform.SetParent(parent);
            parts.Add(inst);
        }

        return parts;
    }

    protected override List<ImagePart> InstanciateEyes(IPartProvider partProvider)
    {

        return SpawnParts(partProvider, eyeMaxCount);
    }

    protected override List<ImagePart> InstanciateMouth(IPartProvider partProvider)
    {
        return SpawnParts(partProvider, mouthMaxCount);
    }

    protected override List<ImagePart> InstanciateNose(IPartProvider partProvider)
    {
        return SpawnParts(partProvider, noseMaxCount);
    }

    


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        foreach (var a in partsSpawnAreas)
        {
            var b = GetBoundsOf(a.area);
            Gizmos.DrawWireCube(b.center, b.size);
        }

        Gizmos.matrix = transform.localToWorldMatrix;
        var center = Vector3.zero;
        var rot = Quaternion.Euler(0, 0, minMaxRotation.x);
        Gizmos.DrawLine(center, center + rot * transform.up);
        var rot2 = Quaternion.Euler(0, 0, minMaxRotation.y);
        Gizmos.DrawLine(center, center + rot2 * transform.up);
    }

    [System.Serializable]
    private class SpawnAreaData
    {
        public Rect area;
    }
}


