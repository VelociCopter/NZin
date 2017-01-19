using UnityEngine;
using System.Collections.Generic;




namespace NZin {


public class OfflineUpdater : Updater {

    public OfflineUpdater() {
        FindHelper().Register( this );
    }
        

    static UpdaterUnityWrapper FindHelper() {
        if( helper == null ) {
            var god = God.Instance;
            helper = god.gameObject.AddComponent<UpdaterUnityWrapper>();
        }
        return helper;
    }
    private static UpdaterUnityWrapper helper;
}



public class UpdaterUnityWrapper : MonoBehaviour {

    public void Register( Updater updater ) {
        updaters.Add( updater );
    }


    void Update() {
        foreach( var updater in updaters ) {
            updater.SignalUpdate();
        }
    }


    private static List<Updater> updaters = new List<Updater>();
}

}