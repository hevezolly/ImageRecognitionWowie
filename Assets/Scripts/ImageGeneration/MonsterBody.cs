using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MonsterBody : BodyImagePart
{
    [SerializeField]
    private List<SpawnAreaData> partsSpawnAreas;

    [SerializeField]
    private float subpartsRadius;
    [SerializeField]
    private Vector2 minMaxRotation;
    [SerializeField]
    private Transform parentOverride;

    private List<SpawnAreaData> avalibleAreas;

    public override void PopulateBy(IImageRequest request)
    {
        avalibleAreas = new List<SpawnAreaData>(partsSpawnAreas);
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

    private List<Vector2> GetAreaSpawnPositions(SpawnAreaData area)
    {
        var positions = new List<Vector2>();
        var count = Random.Range(1, area.maxNumberOfElements + 1);
        for (var i = 0; i < count; i++)
        {
            for (var tryIndex = 0; tryIndex < 20; tryIndex++)
            {
                var pos = new Vector2(Random.Range(area.area.xMin, area.area.xMax),
                    Random.Range(area.area.yMin, area.area.yMax));
                if (positions.Count == 0 || positions.All(p => Vector2.Distance(p, pos) > subpartsRadius))
                {
                    positions.Add(pos);
                    break;
                }
            }
        }
        return positions;
    }

    private List<ImagePart> SpawnParts(IPartProvider provider) 
    {
        var area = SelectArea();
        var parts = new List<ImagePart>();
        foreach (var spawnPoint in GetAreaSpawnPositions(area))
        {
            var globalPos = transform.TransformPoint(spawnPoint);
            var inst = Instantiate(provider.GetUninstanciatedPart(), globalPos, GetRandomRotation());
            var parent = parentOverride ?? transform;
            inst.transform.SetParent(parent);
            parts.Add(inst);
        }

        return parts;
    }

    protected override List<ImagePart> InstanciateEyes(IPartProvider partProvider)
    {

        return SpawnParts(partProvider);
    }

    protected override List<ImagePart> InstanciateMouth(IPartProvider partProvider)
    {
        return SpawnParts(partProvider);
    }

    protected override List<ImagePart> InstanciateNose(IPartProvider partProvider)
    {
        return SpawnParts(partProvider);
    }

    


    private void OnDrawGizmosSelected()
    {
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.color = Color.red;
        foreach (var a in partsSpawnAreas)
        {
            Gizmos.DrawWireCube(a.area.center, a.area.size);
        }
        Gizmos.DrawWireSphere(Vector3.zero, subpartsRadius);
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
        public int maxNumberOfElements;
    }
}


