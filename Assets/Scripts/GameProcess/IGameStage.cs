using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IGameStage
{
    UnityEvent onStageFinished { get; }
    void StartStage(GameStageArgs args);
}

public class GameStageArgs
{
    public ImageGenerator imageGen;
    public OpenedBook book;
    public RequirementGenerator requirementGenerator;
    public RequestFromRequirement requestGenerator;
}
