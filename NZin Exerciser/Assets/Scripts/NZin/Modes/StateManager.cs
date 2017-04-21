using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using NZin.StateMachines;


namespace NZin {



/// <summary>
/// This class (automatically) keeps track of all StateManager(Mixins). It is mostly useful for things like UI code that want
///		to dispatch events to ALL managers (eg both a Game Mode manager and a HUD manager if you have implemented them as
///		separate FSMs).
/// </summary>
public class StateManagerMaster : Singleton<StateManagerMaster> {


	public void RegisterStateManager( StateManager mgr ) {
		stateManagers.Add( mgr );
	}
	List<StateManager> stateManagers = new List<StateManager>();

	
// zzz todo: Make version of these 3 calls for Entities as well
	public void PokeAll( GroundPlane ground, Vector3 atWorldPosition ) {
/*
		foreach( var pokeable in pokeables ) {
			pokeable.Poke( ground );
		}
*/
		foreach( var mgr in stateManagers ) {
			var pokeable = mgr.CurrentMode as Pokeable;
			if( pokeable != null ) {
				pokeable.Poke( ground, atWorldPosition );
			}
		}
	}
	public void ReleaseAll( GroundPlane ground ) {
		foreach( var releaseable in releaseables ) {
			releaseable.Release( ground );
		}
	}
	public void ScrapeAll( GroundPlane ground, Vector3 start, Vector3 now ) {
		foreach( var scrapeable in scrapeables ) {
			scrapeable.Scrape( ground, start, now );
		}
	}

// zzz do the other ones, too
// and deregister!
// actually, nvm. kill this?
	public void RegisterPokeable( Pokeable pokeable ) {
		pokeables.Add( pokeable );
	}

	List<Pokeable> pokeables = new List<Pokeable>();
	List<Releaseable> releaseables = new List<Releaseable>();
	List<Scrapeable> scrapeables = new List<Scrapeable>();
}

// zzz usage notes?
public class StateManager : Initializable, Messageable {

	public StateManager() {
		StateManagerMaster.Instance.RegisterStateManager( this );
	}


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

    public bool IsInitialized   { get; private set; } // zzz used??
	public void Initialize() {
        IsInitialized = true;
	}

    public U ModeData<U>() where U : Glob {
        if( CurrentMode == null )
            return null;
        else 
            return fsm.Data as U;
    }

	public void JumpTo( Mode mode, Glob glob ) {
		fsm.JumpTo( mode, glob );
	}


	public void HandleMessage( Message msg ) {
		fsm.HandleMessage( msg );
	}

	
	StateMachine fsm = new StateMachine();
}

}