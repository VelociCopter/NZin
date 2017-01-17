using UnityEngine;
using System.Collections;
using NZin;


namespace StateMachines {

public interface State : Messagable {

    Glob TransData       { get; set; }
	void OnExit();
	void OnEnter();

}

}
