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
    [SerializeField]
    private Texture cameraOutput;
    [SerializeField]
    private Camera photoCamera;
    [SerializeField]
    private bool purge = true;

    [SerializeField]
    private GameObject realImage;

    private WaitForEndOfFrame wait;

    private void Awake()
    {
        photoCamera.gameObject.SetActive(false);
        wait = new WaitForEndOfFrame();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            Generate();
    }

    public void Generate()
    {
        if (request != null)
            Generate(request.I);
    }

    private IEnumerator TakePicture(Texture2D tex, GameObject image)
    {
        photoCamera.gameObject.SetActive(true);
        //photoCamera.Render();
        yield return wait;
        Graphics.CopyTexture(cameraOutput, tex);
        photoCamera.gameObject.SetActive(false);
        OnPictureTaken(tex, image);
    }

    private void OnPictureTaken(Texture2D picture, GameObject imageObject)
    {
        var s = Sprite.Create(picture, 
            new Rect(Vector2.zero, 
            new Vector2(picture.width, picture.height)), 
            Vector2.one / 2, picture.width);

        var realImageInst = Instantiate(realImage, position, Quaternion.identity);
        realImageInst.transform.SetParent(table, true);
        realImageInst.GetComponent<SpriteRenderer>().sprite = s;
        realImageInst.GetComponent<SortingOriderOffsetLinker>().Link();
        realImageInst.GetComponent<StandartPictureMovement>().MoveOnTop();
        if(purge)
            Destroy(imageObject);
    }

    public void Generate(IImageRequest request)
    {
        var pos = new Vector3(photoCamera.transform.position.x, photoCamera.transform.position.y);
        var image = Instantiate(request.Image.PartProvider.GetUninstanciatedPart(), pos, Quaternion.identity);
        image.PopulateBy(request);
        
        var tex = new Texture2D(cameraOutput.width, cameraOutput.height, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;

        StartCoroutine(TakePicture(tex, image.gameObject));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(position, 0.1f);
    }
}
