
namespace NZin.StateMachines {



/// <summary>
/// Data that is shared across multiple states
/// </summary>
public interface TransStateData {
}



public interface State : Messageable {

	TransStateData Data		{ get; }
	void OnExit();
	void OnEnter();

}



}
