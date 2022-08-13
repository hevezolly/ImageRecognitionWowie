using UnityEngine;

[ExecuteInEditMode]
public class SortingLayerSetter : MonoBehaviour
{
    [SerializeField]
    private Renderer renderer;
    [SerializeField]
    private string sortingLayer = "Default";
    [SerializeField]
    private int sortingOrder;

    public int SortingOrder 
    { 
        get => sortingOrder; 
        set
        {
            sortingOrder = value;
            SetLayer();
        } 
    }

    // Use this for initialization
    void Start()
    {
        if (renderer == null)
        {
            renderer = this.GetComponent<Renderer>();
        }


        SetLayer();
    }

    private void OnValidate()
    {
        if (!Application.isPlaying)
            SetLayer();
    }

    private void SetLayer()
    {
        if (renderer == null)
        {
            renderer = this.GetComponent<Renderer>();
        }

        renderer.sortingLayerName = sortingLayer;
        renderer.sortingOrder = sortingOrder;
    }

}
