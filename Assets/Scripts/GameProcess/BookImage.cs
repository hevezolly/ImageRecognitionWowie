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

    public void SetTexture(TextureData data)
    {
        holder.texture = data.texture;
        if (data.useMask)
        {
            mask.enabled = true;
            image.enabled = true;
        }
    }
}
