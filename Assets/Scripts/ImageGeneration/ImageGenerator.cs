using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ImageGenerator : MonoBehaviour
{
    [SerializeField]
    private ScriptableValue<Requirement> currentRequirement;
    [SerializeField]
    private ScriptableValue<int> currentNumberOfPictures;
    [SerializeField]
    private IRef<IImageRequest> request;
    [SerializeField]
    private Vector2 position;
    [SerializeField]
    private float radius;
    [SerializeField]
    private Transform table;
    [SerializeField]
    private Texture cameraOutput;
    [SerializeField]
    private Camera photoCamera;
    [SerializeField]
    private bool purge = true;
    [SerializeField]
    private float generationDelay;

    [SerializeField]
    private GameObject realImage;

    private WaitForEndOfFrame wait;
    private WaitForSeconds generateWait;

    public UnityEvent<ImageDummy> NewPictureSpawnedEvent;


    private void Awake()
    {
        photoCamera.gameObject.SetActive(false);
        wait = new WaitForEndOfFrame();
        generateWait = new WaitForSeconds(generationDelay);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //    Generate();
    }

    public void Generate()
    {
        if (request != null)
            Generate(request.I);
    }

    private IEnumerator TakePicture(PictureArgs args)
    {
        photoCamera.gameObject.SetActive(true);
        //photoCamera.Render();
        yield return wait;
        Graphics.CopyTexture(cameraOutput, args.tex);
        photoCamera.gameObject.SetActive(false);
        OnPictureTaken(args);
    }

    private Vector2 GetRandomPosition()
    {
        return Random.insideUnitCircle * radius + position;
    }

    private void OnPictureTaken(PictureArgs args)
    {
        var s = Sprite.Create(args.tex, 
            new Rect(Vector2.zero, 
            new Vector2(args.tex.width, args.tex.height)), 
            Vector2.one / 2, args.tex.width);

        var realImageInst = Instantiate(realImage, GetRandomPosition(), Quaternion.identity);
        realImageInst.transform.SetParent(table, true);
        realImageInst.GetComponent<SpriteRenderer>().sprite = s;
        realImageInst.GetComponent<SortingOriderOffsetLinker>().Link();
        realImageInst.GetComponent<StandartPictureMovement>().MoveOnTop();
        var dummy = realImageInst.GetComponent<ImageDummy>();
        dummy.Initiate(args.description, args.tex);
        if (args.showAnswers)
            dummy.DisplayAnswer(currentRequirement.Value);
        dummy.PopulateBy(args.request);
        if(purge)
            Destroy(args.imageObject);
        currentNumberOfPictures.Value += 1;
        NewPictureSpawnedEvent?.Invoke(dummy);
    }

    public void GenerateBunch(IEnumerable<IImageRequest> request, bool showAnswers = false)
    {
        StartCoroutine(GenerateProcess(request, showAnswers));
    }

    private IEnumerator GenerateProcess(IEnumerable<IImageRequest> request, bool showAnswers = false)
    {
        foreach (var r in request)
        {
            Generate(r, showAnswers);
            yield return generateWait;
        }
    }

    public void Generate(IImageRequest request, bool showAnswers = false)
    {
        var pos = new Vector3(photoCamera.transform.position.x, photoCamera.transform.position.y);
        var image = Instantiate(request.Image.PartProvider.GetUninstanciatedPart(), pos, Quaternion.identity);
        image.PopulateBy(request);
        
        var tex = new Texture2D(cameraOutput.width, cameraOutput.height, TextureFormat.RGBA32, false);
        tex.filterMode = FilterMode.Point;

        var description = new ImageDescription();
        image.FillDescription(description);

        var args = new PictureArgs()
        {
            tex = tex,
            imageObject = image.gameObject,
            description = description,
            request = request,
            showAnswers = showAnswers
        };

        StartCoroutine(TakePicture(args));
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(position, radius);
    }

    private struct PictureArgs
    {
        public Texture2D tex;
        public GameObject imageObject;
        public ImageDescription description;
        public IImageRequest request;
        public bool showAnswers;
    }

    private void OnApplicationQuit()
    {
        currentNumberOfPictures.Value = 0;
    }
}
