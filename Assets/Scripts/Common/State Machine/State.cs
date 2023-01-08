using UnityEngine;
using System.Collections;

public abstract class State : MonoBehaviour //abstract in order to emphasize creation of concrete subclasses for use
{
	public virtual void Enter ()
	{
		AddListeners();
	}

	public virtual void Exit ()
	{
		RemoveListeners();
	}

	protected virtual void OnDestroy()
    {
		RemoveListeners();
    }

	protected virtual void AddListeners()//virtual so as to not require implementation by subclasses
    {

    }

	protected virtual void RemoveListeners()//virtual so as to not require implementation by subclasses
	{

    }

}