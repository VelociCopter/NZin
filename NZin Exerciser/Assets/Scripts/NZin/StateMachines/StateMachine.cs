using UnityEngine;
using System.Collections.Generic;



namespace NZin.StateMachines {



public class StateMachine : Messageable {
#pragma warning disable 162
    const bool DEBUG_LOG = false;


	public State Current			{ get; private set; }
    public TransStateData Data      { get; private set; }



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


	public void JumpTo( State state, TransStateData data ) {
        if( DEBUG_LOG ) {
            if( Current == null ) {
                Debug.Log( string.Format( "StateMachine JumpingTo initial state {0}. Data={1}",
                    state, data
                ));
            } else {
                Debug.Log( string.Format( "StateMachine about to JumpTo next state. From {0} To {1}. Data={2} ",
                    Current, state, data
                ));
            }
        }

		if( Current != null )
			Current.OnExit();
		Current = state;
        Data = data;
        if( Current != null ) {
			Current.OnEnter();
        }
	}


	public bool CanTransition( State from, Message msg, out State to ) {
		List<Transition> nextCandidates;
		to = null;
		if( transitions.TryGetValue( from, out nextCandidates )) {
			foreach( var nextCandidate in nextCandidates ) {
				if( nextCandidate.RespondsToMessageType( msg ) && nextCandidate.TestPasses() ) {
					to = nextCandidate.To;
					return true;
				}
			}
		}
		return false;
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



	Dictionary<State,List<Transition>> transitions = new Dictionary<State,List<Transition>>();
}



}