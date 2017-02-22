using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NZin;



public class LoadingMode : Mode {

	
    public override void OnEnter() {
        base.OnEnter();

        AppMessenger.Instance.HandleMessage( new LoadingComplete() );
    }

}
