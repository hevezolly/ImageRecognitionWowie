using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenedBook : StandartPictureMovement
{
    [SerializeField]
    private ClosedBook closedBook;
    private int numberOfPages = 0;
    private int currentPage = 0;
    private Vector2 selectPosition;

    [SerializeField]
    private BookImage correctPicture;
    [SerializeField]
    private BookImage incorrectPicture;

    [SerializeField]
    private Transform correctPlaceholder;
    [SerializeField]
    private Transform incorrectPlaceholder;

    [SerializeField]
    private TextMeshProUGUI smallNum;
    [SerializeField]
    private TextMeshProUGUI bigNum;

    [SerializeField]
    private TextMeshProUGUI textArea;

    private List<List<GameObject>> images = new List<List<GameObject>>();
    private List<string> prompts = new List<string>();
    public override void OnSelect()
    {
        base.OnSelect();
        selectPosition = input.Position;
    }

    public override void OnRelease()
    {
        base.OnRelease();
        if (Vector2.Distance(selectPosition, input.Position) < 0.01f)
        {
            var side = Vector3.Dot(localGrubOffset, Vector2.right);
            FlipPage((int)Mathf.Sign(side));
        }
    }

    private void FlipPage(int delta)
    {
        if (currentPage + delta < 0)
            Close(1);
        else if (currentPage + delta >= numberOfPages)
            Close(-1);
        else
            SetPage(currentPage + delta);

    }

    public void AddPage(BookPage page)
    {
        var newPage = new List<GameObject>();
        prompts.Add(page.prompt);
        foreach (var correctTex in page.correctImages)
        {
            var img = Instantiate(correctPicture, correctPlaceholder);
            img.SetTexture(correctTex);
            img.gameObject.SetActive(false);
            newPage.Add(img.gameObject);
        }
        foreach (var incorrect in page.incorrectImages)
        {
            var img = Instantiate(incorrectPicture, incorrectPlaceholder);
            img.SetTexture(incorrect);
            img.gameObject.SetActive(false);
            newPage.Add(img.gameObject);
        }
        images.Add(newPage);
        numberOfPages++;
    }

    private void Close(int side)
    {
        closedBook.transform.rotation = transform.rotation;
        var s = closedBook.transform.localScale;
        s.x = side * Mathf.Abs(s.x);
        closedBook.transform.localScale = s;
        closedBook.gameObject.SetActive(true);
        gameObject.SetActive(false);
        closedBook.MoveOnTop();
        closedBook.Open(transform.position);
    }

    public void OnOpen(int side)
    {
        if (numberOfPages == 0)
        {
            Close(-side);
            return;
        }
        if (side <= 0)
            SetPage(0);
        else
            SetPage(numberOfPages - 1);
    }

    private void SetPage(int index)
    {
        foreach (var i in images[currentPage])
            i.SetActive(false);
        currentPage = index;
        foreach (var i in images[currentPage])
            i.SetActive(true);
        textArea.text = prompts[currentPage];
        bigNum.text = ((index + 1) * 2).ToString();
        smallNum.text = ((index + 1) * 2 - 1).ToString();
    }
}

public class BookPage
{
    public List<TextureData> correctImages;
    public List<TextureData> incorrectImages;
    public string prompt;
}

public class TextureData
{
    public Texture2D texture;
    public bool useMask;
}
