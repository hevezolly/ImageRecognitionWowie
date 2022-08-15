using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FalseStart : MonoBehaviour
{
    [SerializeField]
    private GameFlow flow;
    [SerializeField]
    private MainGameStage stage;

    public void StartFalse()
    {
        stage.StartStage(flow.args);
    }
}
