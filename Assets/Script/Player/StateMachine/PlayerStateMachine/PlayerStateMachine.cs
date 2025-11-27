using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine : MonoBehaviour
{
    public Player player;

    private Dictionary<System.Type, StateBase> _states = new Dictionary<System.Type, StateBase>();
    private StateBase _currentState;

    #region Metodos Unity
    private void Awake()
    {
        if(player == null)
            player = GetComponent<Player>();
        _states.Add(typeof(IdleState), new IdleState(player, this));
        _states.Add(typeof(WalkState), new WalkState(player, this));
    }

    private void Start()
    {
        SwitchState(typeof(IdleState));
    }


    private void Update()
    {
        if(_currentState != null)
        {
            _currentState?.UpdateState();
        }
    }



    #endregion

    #region Switch

    //Variavel de checagem de Estados
    public void SwitchState(System.Type newState)
    {
        if (_currentState != null)
            _currentState.ExitState();

        if (_states.TryGetValue(newState, out StateBase stateInstance))
        {
            _currentState = stateInstance;
            _currentState.EnterState();
            Debug.Log($"Transição para o estado: {newState.Name}");
        }

        else
            Debug.Log($"Estado {newState.Name} não encontrado no dicionario");
    }



    #endregion
}
