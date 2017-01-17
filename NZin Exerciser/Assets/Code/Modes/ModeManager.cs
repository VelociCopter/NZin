using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using StateMachines;


namespace NZin {

public class ModeManager : Singletinittable<ModeManager>, Initializable, Messagable {


	public void AddEdge<T>( Mode from, Mode to ) where T : Message {
		var tzn = new Transition( from, to, typeof(T) );
		fsm.RegisterTransition( tzn );
	}
	public void AddEdge<T>( Mode from, Mode to, TransitionTestDelegate test ) where T : Message {
		var tzn = new Transition( from, to, typeof(T), test );
		fsm.RegisterTransition( tzn );
	}
	public void ClearEdges() {
		fsm.ClearTransitions();
	}


	public Mode CurrentMode		                    { get { return fsm.Current as Mode; } }

    public bool IsInitialized   { get; private set; }
	public void Initialize() {
        IsInitialized = true;
	}

    public U ModeData<U>() where U : Glob {
        return CurrentMode.TransData as U;
    }

	public void JumpTo( Mode mode, Glob transState ) {
		fsm.JumpTo( mode, transState );
	}


	public void HandleMessage( Message msg ) {
		fsm.HandleMessage( msg );
	}

	
	StateMachine fsm = new StateMachine();
}

}