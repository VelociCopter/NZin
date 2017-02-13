using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NZin;



namespace StateMachines {


public class StateMachine : Messagable {
#pragma warning disable 162
    const bool DEBUG_LOG = false;


	public State Current			{ get; private set; }
    public Glob Data                { get; private set; }


	public void RegisterTransition( Transition newTransition ) {
		var stateToTransitionFrom = newTransition.From;
		if( !transitions.ContainsKey( stateToTransitionFrom )) {
			transitions.Add( stateToTransitionFrom, new List<Transition>() );
		}
		var transitionsFromThisState = transitions[newTransition.From];
		transitionsFromThisState.Add( newTransition );
	}
	public void ClearTransitions() {
		transitions.Clear();
	}



    /// <summary>
    /// Examines the message to determine if any state changes are in order.
    /// After that–if the message is not yet consumed–check if the current state can handle it.
    /// </summary>
    /// <param name="msg">Message.</param>
	public void HandleMessage( Message e ) {
		State next;
		if( CanTransition( Current, e, out next )) {
			e.Consume();
            JumpTo( next, Data );
		}

        if( !e.IsConsumed ) {
            Current.HandleMessage( e );
        }
	}


	public void JumpTo( State state, Glob glob ) {
        if( DEBUG_LOG ) {
            if( Current == null ) {
                Debug.Log( string.Format( "StateMachine JumpingTo initial state {0}. Data={1}",
                    state, glob
                ));
            } else {
                Debug.Log( string.Format( "StateMachine about to JumpTo next state. From {0} To {1}. Data={2} ",
                    Current, state, glob
                ));
            }
        }

		if( Current != null )
			Current.OnExit();
		Current = state;
        Data = glob;
        if( Current != null ) {
			Current.OnEnter();
        }
	}


	public bool CanTransition( State from, Message msg, out State to ) {
		List<Transition> nextCandidates;
		to = null;
		if( transitions.TryGetValue( from, out nextCandidates )) {
			foreach( var nextCandidate in nextCandidates ) {
				if( nextCandidate.TypesMatch( msg ) && nextCandidate.TestPasses() ) {
					to = nextCandidate.To;
					return true;
				}
			}
		}
		return false;
	}


	Dictionary<State,List<Transition>> transitions = new Dictionary<State,List<Transition>>();
}

}