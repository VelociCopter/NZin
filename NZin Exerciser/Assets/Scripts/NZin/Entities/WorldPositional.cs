using System;
using UnityEngine;



namespace NZin {
    


/// <summary>
/// A class that can figure out where it is in world space
/// </summary>
public interface WorldPositionTracker {
    Vector3 WorldPosition    { get; }
    event Action<Vector3> Moved;
    string Print( int crumbs );
}



/// <summary>
/// An Entity that has a world position. 
/// NOTES:
///     - This is read only
///     - This class doesn't directly know where it is, but defers that knowledge to its Tracker
///     - To set position, call SetTracker(...) with a mutatable Tracker (See WorldPositionTracker)
/// </summary>
public class WorldPositional : Entity, WorldPositionTracker {


    public event Action<Vector3> Moved;


    public Vector3 WorldPosition { get {
            return tracker.WorldPosition;
        }
    }



    public WorldPositional( Entity entity )
        :base( entity ) {

        entity.Disposed += Destroy;
    }
    // zzz Better integrate w/ Disposable
    void Destroy( Entity e ) {
        tracker.Moved -= OnPositionTrackerMoved;
        Decoration<Entity>().Disposed -= Destroy;
    }



    public void SetTracker( WorldPositionTracker tracker ) {
        this.tracker = tracker;
        tracker.Moved += OnPositionTrackerMoved;
    }



    void OnPositionTrackerMoved( Vector3 v ) {
        if( Moved != null )
            Moved( v );
    }


    
    public override string Print( int crumbs=0 ) {
        return string.Format( "{0}[ {1}_{2} Tracker={3} ]", 
            PrintPrefix( crumbs ),
            "WorldPositional",
            CId,
            tracker.Print( crumbs+1 )
        );
    }


    WorldPositionTracker tracker;
}



}