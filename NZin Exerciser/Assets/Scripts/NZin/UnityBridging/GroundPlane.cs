using UnityEngine;
using System;

namespace NZin {



/// <summary>
/// You typically want to attach this as a component of a big GameObject that sits at ground level to allow
/// detection of "ground touches."
/// It can persist across Modes. There might need to be some special attention paid when using it in multiple scenes.
/// </summary>
public class GroundPlane : GameObjectProxy, GameObjectProxyReceiver {    // TODO: See if this can be made into a Monoton
    public const int GROUND_PROXY_PRIORITY = 10;

    public const string RUNTIME_NAME = "_TransmodalGroundPlane";


    public Plane Plane          { get;  private set; }


    void Awake() {
        gameObject.name = RUNTIME_NAME;
        Receiver = this;        // <-- this lets us use the custom Touch handling derived rom GameObjectProxy instead of Unity's OnMouse*
        Plane = new Plane( this.transform.up, this.transform.position );

        // TODO: Look for the value in the inspector, but set the default value in the inspector to the default
        // HACK: for now just override it
        Assertion.That( Priority == 0 || Priority == 10, "You are breaking some hack assumptions. Only use priority 0 or 10 for now." );
        Priority = GROUND_PROXY_PRIORITY;
    }
    public static GroundPlane FindMe() {    // TODO: Kill this if this class becomes a Monoton
        return GameObject.Find( RUNTIME_NAME ).GetComponent<GroundPlane>();
    }


    public void TouchDown( Vector3 screenPosition ) {
		var worldPosition = ScreenToWorldPoint( screenPosition );
        StateManagerMaster.Instance.PokeAll( this, worldPosition );
    }
    public void TouchUp( Vector3 screenPosition ) {
		//
		//StateManagerMaster.Instance.ReleaseAll( this );
    }
    public void TouchDrag( Vector3 start, Vector3 now ) {
		Assertion.Unimplemented();
		//StateManagerMaster.Instance.ScrapeAll( this, start, now );
    }


    public void CollisionEnter( Collision collision ) {
    }
	
	
	Vector3 ScreenToWorldPoint( Vector3 screenPoint ) {
		var ray = ZinCam.Instance.ScreenPointToRay( screenPoint );
		float d;
		if( !Plane.Raycast( ray, out d )) {
			Assertion.UnexpectedEntry();
			return Vector3.zero;
		} else {
			return ray.GetPoint( d );
		}
	}
}

}