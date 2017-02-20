using System;
using UnityEngine;




namespace NZin {
    
/// <summary>
/// An entity that has a world position. This may be view only.
/// Note this class doesn't really know where it is, but defers that knowledge to its Tracker.
/// </summary>
public class WorldPositional : Entity {


    public event Action<Vector3> Moved;


    public WorldPositional( Entity entity )
        :base( entity ) {

        entity.Disposed += Destroy;
    }


    public void SetTracker( WorldPositionTracker tracker ) {
        this.tracker = tracker;
        tracker.Moved += OnPositionTrackerMoved;
    }

    void Destroy( Entity e ) {
        tracker.Moved -= OnPositionTrackerMoved;
        Decoration<Entity>().Disposed -= Destroy;
    }


    public Vector3 WorldPosition { get {
            return tracker.WorldPosition;
        }
    }



    void OnPositionTrackerMoved( Vector3 v ) {
        if( Moved != null )
            Moved( v );
    }


    WorldPositionTracker tracker;


    public override string Print( int crumbs=0 ) {
        return string.Format( "{0}[ {1}_{2} Tracker={3} ]", 
            PrintPrefix( crumbs ),
            "WorldPositional",
            CId,
            tracker.Print( crumbs+1 )
        );
    }
}



/// <summary>
/// Definition for a class that can figure out where it is in world space
/// </summary>
public interface WorldPositionTracker {
    Vector3 WorldPosition    { get; }
    event Action<Vector3> Moved;
    string Print( int crumbs );
}

}