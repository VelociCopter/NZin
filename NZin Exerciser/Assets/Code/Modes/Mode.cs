using UnityEngine;
using System.Collections;
using StateMachines;


namespace NZin {

public interface Mode : State {
}


/// <summary>
/// A helper class. Implements a lot of common functionality that the individual world modes would want.
/// </summary>
public class GameMode : Mode {
    const bool DEBUG_LOG = true;


    public Glob TransData			{ get; set; }


	public virtual void OnExit() {
        if( DEBUG_LOG )
    		Debug.Log("Exited state: "+this);
	}
    public virtual void OnEnter() {
        if( DEBUG_LOG )
    		Debug.Log("Entered state: "+this);
	}

	public virtual void HandleMessage( Message msg ) {
	}
            
}

}