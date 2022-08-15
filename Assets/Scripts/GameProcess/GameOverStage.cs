using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameOverStage : MonoBehaviour, IGameStage
{
    [SerializeField]
    private UnityEvent stageFinishedEvent;
    [SerializeField]
    private GameObject ExitPrompt;
    public UnityEvent onStageFinished => stageFinishedEvent;

    public void StartStage(GameStageArgs args)
    {
        ExitPrompt.SetActive(true);
    }
}
