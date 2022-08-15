using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[CreateAssetMenu(menuName = "game stage/main")]
public class MainGameStage : ScriptableGameStage
{
    [SerializeField]
    private ScriptableValue<int> currentNumberOfPictures;
    [SerializeField]
    private ScriptableValue<Requirement> currentRequirement;
    [SerializeField]
    public Requirement requirement;
    [SerializeField]
    private List<IRef<IImageRequest>> correctRequests;
    [SerializeField]
    private List<IRef<IImageRequest>> incorrectRequests;
    [SerializeField]
    public Vector2Int minMaxTotalImages;
    [SerializeField]
    public int correctImages;
    [SerializeField]
    private bool showAnswers;

    private BookPage page;

    protected override void Start()
    {
        currentNumberOfPictures.ValueChangeEvent.AddListener(OnPictureNumberChange);
        FalseStart();
    }

    public void FalseStart()
    {
        var totalImages = Random.Range(minMaxTotalImages.x, minMaxTotalImages.y+1);
        var requests = new List<IImageRequest>(totalImages);
        for (var i = 0; i < correctImages; i++)
        {
            if (i < correctRequests.Count)
                requests.Add(correctRequests[i].I);
            else
            {
                var request = args.requestGenerator.RequestCorrect(requirement);
                requests.Add(request);
            }
        }
        for (var i = 0; i < totalImages - correctImages; i++)
        {
            if (i < incorrectRequests.Count)
                requests.Add(incorrectRequests[i].I);
            else
                requests.Add(args.requestGenerator.RequestIncorrect(requirement));
        }
        currentRequirement.Value = requirement;
        var prompt = args.requirementGenerator.GeneratePromptFromRequirement(requirement);
        page = new BookPage();
        page.Images = new List<TextureData>();
        page.prompt = prompt;
        args.imageGen.NewPictureSpawnedEvent.AddListener(OnNewPictureSpawned);
        args.imageGen.GenerateBunch(requests.OrderBy(v => Random.value), showAnswers);
    }

    private void OnNewPictureSpawned(ImageDummy picture)
    {
        var data = new TextureData()
        {
            texture = picture.MonsterTexture,
            useMask = picture.hasMask,
        };
        var type = SpawType.Incorrect;
        if (!showAnswers)
            type = SpawType.Nutral;
        else if (picture.Description.IsFitForRequirement(requirement))
            type = SpawType.Correct;
        data.type = type;
        page.Images.Add(data);
    }

    public void Finish()
    {
        args.book.AddPage(page);
        args.imageGen.NewPictureSpawnedEvent.RemoveListener(OnNewPictureSpawned);
        currentNumberOfPictures.ValueChangeEvent.RemoveListener(OnPictureNumberChange);
    }

    private void OnPictureNumberChange(int current)
    {
        if (current == 0)
        {
            Finish();
            onStageFinished?.Invoke();
        }
    }

    private void OnValidate()
    {
        minMaxTotalImages.x = Mathf.Max(minMaxTotalImages.x, correctImages);
        minMaxTotalImages.y = Mathf.Max(minMaxTotalImages.x, minMaxTotalImages.y);
    }
}
