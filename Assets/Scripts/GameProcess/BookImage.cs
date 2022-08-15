using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookImage : MonoBehaviour
{
    [SerializeField]
    private RawImage holder;
    [SerializeField]
    private Mask mask;
    [SerializeField]
    private Image image;
    [SerializeField]
    private GameObject GoodMark;
    [SerializeField]
    private GameObject BadMark;

    public void SetTexture(TextureData data)
    {
        holder.texture = data.texture;
        if (data.useMask)
        {
            mask.enabled = true;
            image.enabled = true;
        }
        if (data.type == SpawType.Correct)
            GoodMark.SetActive(true);
        else if (data.type == SpawType.Incorrect)
            BadMark.SetActive(true);
    }
}
