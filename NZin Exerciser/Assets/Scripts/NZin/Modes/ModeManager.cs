using NZin.StateMachines;


namespace NZin {



/// <summary>
/// Handles overall App Flow
/// USAGE NOTES:
/// 	- Implement the entire app flowchart as a StateMachine
/// 	- Set up the FSM's Edges which will respond to receiving a type of Messge by transitioning along the Edge
/// 	- For conditional transitions, you can add a TransitionTestDelegate to an edge
/// </summary>
public class ModeManager : Singletinittable<ModeManager>, Initializable, Messageable {


	public Mode CurrentMode		            	{ get { return fsm.Current as Mode; } }


    public U ModeData<U>() where U : Glob {
        if( CurrentMode == null )
            return null;
        else 
            return fsm.Data as U;
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


	public void JumpTo( Mode mode, Glob glob ) {
		fsm.JumpTo( mode, glob );
	}



	#region Initializable Impl
    public bool IsInitialized   				{ get; private set; }
	public void Initialize() {
        IsInitialized = true;
	}
	#endregion


	#region Messageable Impl
	public void HandleMessage( Message msg ) {
		fsm.HandleMessage( msg );
	}
	#endregion


	
	StateMachine fsm = new StateMachine();
}



}