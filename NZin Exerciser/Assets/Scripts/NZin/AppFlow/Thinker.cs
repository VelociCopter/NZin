using System.Collections.Generic;
using System.Collections.ObjectModel;
using UnityEngine;





namespace NZin {


public interface Thinker {
    long TId    { get; }
    void Think();
}


/// <summary>
/// NOTES On IDs:
///     -All thinkers need to implement their TId (Thinker ID)
///     -TIds must be unique. It is the responsibility of the game to ensure that for Entities & Stuff.
///     -TIds should be > 0 to avoid collisions with SYSTEM IDs.
///     -SYSTEM IDs (which are still TIds) are assumed to be any ID < 0
///     -Systems (like managers) can call the NextSystemId() fnc to get the next available SYSTEM ID.
/// </summary>
public class MasterThinker : Singleton<MasterThinker> {


    public ReadOnlyCollection<Thinker> Thinkers { get {
            return thinkers.AsReadOnly();
        }
    }


    public void Initialize( Updater upr ) {
        updater = upr;
        updater.Updated += Update;
    }
    public void Terminate() {
        ClearRegistry();

        updater.Updated -= Update;
        updater = null;
    }


    public void Register( Thinker thinker ) {
        thinkersWaitingToBeAdded.Add( thinker );
    }
    public void Deregister( Thinker thinker ) {
        thinkersWaitingToBeRemoved.Add( thinker.TId );
    }
    public void ClearRegistry() {
        thinkers.Clear();
        thinkersWaitingToBeAdded.Clear();
        thinkersWaitingToBeRemoved.Clear();
    }


    public long NextSystemId() {
        return nextSystemId--;
    }
    static long nextSystemId = -1;



    void Update() {
        thinkers.AddRange( thinkersWaitingToBeAdded );
        thinkersWaitingToBeAdded.Clear();

        thinkers.RemoveAll( thinker => thinkersWaitingToBeRemoved.Contains( thinker.TId ));
        thinkersWaitingToBeRemoved.Clear();

        foreach( var thinker in thinkers ) {
            thinker.Think();
        }
    }



    List<Thinker> thinkersWaitingToBeAdded = new List<Thinker>();
    List<long> thinkersWaitingToBeRemoved = new List<long>();
    List<Thinker> thinkers = new List<Thinker>();
    Updater updater;
}

}