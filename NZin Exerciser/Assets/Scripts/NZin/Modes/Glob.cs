using UnityEngine;
using System.Collections;





namespace NZin {

public class Glob : StateMachines.TransStateData {

    /* TODO zzz td
	// zzz I want to compare the different Globs from:
	//  Minimal Strategy Game
	// 	Flow
	//	L. Battler
	public SelectableEn Selected		{ get; private set; }
	public GroundPlane Ground			{ get; private set; }


	public TransModalState( GroundPlane ground ) {
		this.Ground = ground;
	}


	public void SelectAndDispatch( EntityDecorator entity ) {
		this.SelectAndDispatch( entity.Decoration<SelectableEn>( ));
	}
	public void SelectAndDispatch( SelectableEn entity )	{
		var old = Selected;
		Selected = entity;
		var msg = new SelectionMsg( old, entity );
		WorldMessenger.Instance.Dispatch( msg );
	}

*/
}

}