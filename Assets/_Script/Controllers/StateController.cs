using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateController : MonoBehaviour
{
    IState currentState;

    public LevelStateStart levelStateStart = new();
    public LevelStatePhase_1 levelStatePhase_1 = new();
    public LevelStatePhase_2 levelStatePhase_2 = new();
    public LevelStateEnd levelStateEnd = new();

    private void Start()
    {
        ChangeState(levelStateStart);
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState(this);
        }
    }

    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.OnExit(this);
        }
        currentState = newState;
        currentState.OnEnter(this);
    }
}

public interface IState
{
    public LevelController LevelController { get; }
    public void OnEnter(StateController controller);

    public void UpdateState(StateController controller);

    public void OnHurt(StateController controller);

    public void OnExit(StateController controller);
}
