using UnityEngine;
using System.Collections;

//maintains a reference to the current state and switches between states
public class StateMachine : MonoBehaviour 
{
	public virtual State CurrentState
	{
		get
		{
			return _currentState;
		}
		set
		{
			Transition(value);
		}
	}
	protected State _currentState;
	protected bool _inTransition;
	public virtual T GetState<T> () where T : State
	{
		T target = GetComponent<T>();
		if (target == null)
			target = gameObject.AddComponent<T>();
		return target;
	}

	public virtual void ChangeState<T> () where T : State
	{
		CurrentState = GetState<T>();
	}

	protected virtual void Transition (State value)
    {
		if (_currentState == value || _inTransition)
			return;

		_inTransition = true;//Mark the beginning of a transition

		if (_currentState != null)//If the previous state is not null, it is sent a message to exit
			_currentState.Exit();

		_currentState = value;//The backing field is set to the value passed along in the setter

		if (_currentState != null)//If the new state is not null, it is sent a message to enter
			_currentState.Enter();

		_inTransition = false;//mark the end of a transition
	}
}