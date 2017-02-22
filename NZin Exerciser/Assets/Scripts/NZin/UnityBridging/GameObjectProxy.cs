using UnityEngine;
using System;

namespace NZin {



/// <summary>
/// A GameObject that receives touches & collisions from a GameObjectProxy
/// </summary>
public interface GameObjectProxyReceiver {
	void TouchDown( Vector3 screenPosition );
	void TouchUp( Vector3 screenPosition );
	void TouchDrag( Vector3 start, Vector3 now );
    void CollisionEnter( Collision collision );
}



/// <summary>
/// A GameObject that relays clicking and stuff to the assigned GameObjectProxyReceiver
/// </summary>
public class GameObjectProxy : MonoBehaviour, IComparable {
    const int DEFAULT_PRIORITY = 0;

    
    #region Inspector

    public bool OccludesInput = true;

    /// <summary>
    /// When the bookkeeper process the events it has received in an update, it does so by the prioritized order.
    /// </summary>
    /// <value>The priority.</value>
    public int Priority = DEFAULT_PRIORITY;

    #endregion



    public Vector3? TapStartedAt                { get; set; }
	public GameObjectProxyReceiver Receiver		{ get; set; }



	void OnMouseDown() {
        TapStartedAt = Input.mousePosition;
        GameObjectProxyBookkeeper.Instance.Poked( this );
	}
	protected virtual void OnMouseUp() {
        TapStartedAt = null;
        GameObjectProxyBookkeeper.Instance.Released( this );
	}
	void OnMouseDrag() {
		var nowAt = Input.mousePosition;
        if( !TapStartedAt.HasValue )
            TapStartedAt = nowAt;

        if( Receiver != null ) {
            const double TOLERANCE_SQUARED = 1.0;
            var fullDragVector = TapStartedAt.Value-nowAt;
            if( fullDragVector.sqrMagnitude > TOLERANCE_SQUARED )
                GameObjectProxyBookkeeper.Instance.Scraped( this );
        }
	}



    // NOTE: This doesn't route through GameObjectProxyBookkeeper. If prioritizing collisions is a desired feature, pipe these through the bookeeper like the input functions.
    void OnCollisionEnter( Collision c ) {
        if( Receiver != null )
            Receiver.CollisionEnter( c );
    }



    public int CompareTo( object thatObj ) {
        var thatProxy = thatObj as GameObjectProxy;
        return this.Priority.CompareTo( thatProxy.Priority);
    }

}

}