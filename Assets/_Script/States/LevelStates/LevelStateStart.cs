using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStateStart : IState
{
    public LevelController LevelController { get; }
    public void OnEnter(StateController controller)
    {
        
    }

    public void OnExit(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    public void OnHurt(StateController controller)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateState(StateController controller)
    {
        throw new System.NotImplementedException();
    }
}
