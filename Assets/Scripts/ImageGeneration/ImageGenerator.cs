using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageGenerator : MonoBehaviour
{
    [SerializeField]
    private IRef<IImageRequest> request;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private Transform table;

    public void Generate()
    {
        if (request != null)
            Generate(request.I);
    }

    public void Generate(IImageRequest request)
    {
        var image = Instantiate(request.Image.PartProvider.GetUninstanciatedPart(), position, Quaternion.identity);
        image.transform.SetParent(table, true);
        image.PopulateBy(request);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(position, 0.1f);
    }
}
