using UnityEngine;
using System.Collections;
using NZin;


namespace StateMachines {

public interface State : Messagable {

	void OnExit();
	void OnEnter();

}

}
