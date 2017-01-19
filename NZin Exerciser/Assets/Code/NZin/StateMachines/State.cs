using UnityEngine;
using System.Collections;
using NZin;


namespace StateMachines {

public interface State : Messagable {

//zzz moved to the StateMachine itself    Glob TransData       { get; set; }
	void OnExit();
	void OnEnter();

}

}
