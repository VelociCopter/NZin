using UnityEngine;



namespace NZin {



/// <summary>
/// A helper class. Implements a lot of common functionality that the individual world modes would want.
/// </summary>
public abstract class Mode : StateMachines.State {
    const bool DEBUG_LOG = true;


    public Glob Glob							{ get; set; }
	/// <summary>
	/// A more generic reference to this.Glob
	/// </summary>
	public StateMachines.TransStateData Data	{ get { return Glob; } }


	public virtual void OnExit() {
        if( DEBUG_LOG )
    		Debug.Log("Exited state: "+this);
	}
    public virtual void OnEnter() {
        if( DEBUG_LOG )
    		Debug.Log("Entered state: "+this);
	}


	public virtual void HandleMessage( Message msg ) {
		// no-op as a default implementation
	}
            
}



}