using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private IState _currentState;
    public IState CurrentState => _currentState;

    public void ChangeState(IState newState) {
        _currentState?.Exit();
        _currentState = newState;
        _currentState?.Enter();
    }

    private void Update() {
        _currentState?.Update(Time.deltaTime);
    }
}
