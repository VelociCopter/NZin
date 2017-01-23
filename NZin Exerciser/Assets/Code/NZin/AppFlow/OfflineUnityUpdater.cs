using UnityEngine;
using System.Collections.Generic;




namespace NZin {


/// <summary>
/// An updater that signals an update once per standard Unity Update cycle.
/// </summary>
public class OfflineUnityUpdater : Updater {

    public OfflineUnityUpdater() {
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



/// <summary>
/// Helper class for OfflineUnityUpdaters
/// </summary>
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