using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ScriptableGameStage : ScriptableObject, IGameStage
{
    [SerializeField]
    private UnityEvent stageFinishEvent;
    public UnityEvent onStageFinished => stageFinishEvent;

    private GameStageArgs _args;
    protected GameStageArgs args => _args;

    public void StartStage(GameStageArgs args)
    {
        _args = args;
        Start();
    }

    protected abstract void Start();
}
