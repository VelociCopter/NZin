﻿using UnityEngine;
using System;
using System.Collections;


namespace NZin {


/// <summary>
/// You typically want to attach this as a component of a big GameObject that sits at ground level to allow
/// detection of "ground" touches.
/// </summary>
public class GroundPlane : GameObjectProxy, GameObjectProxyReceiver {
    public const int GROUND_PROXY_PRIORITY = 10;

    public const string RUNTIME_NAME = "_TransmodalGroundPlane";


    public Plane Plane          { get;  private set; }


    void Awake() {
        gameObject.name = RUNTIME_NAME;
        Receiver = this;        // <-- this lets us use the custom Touch handling derived rom GameObjectProxy instead of Unity's OnMouseX
        Plane = new Plane( this.transform.up, this.transform.position );

        // TODO: Look for the value in the inspector, but set the default value in the inspector to the default
        // HACK: for now just override it
        Assertion.That( Priority == 0 || Priority == 10, "You are breaking some hack assumptions" );
        Priority = GROUND_PROXY_PRIORITY;
    }
    public static GroundPlane FindMe() {
        return GameObject.Find( RUNTIME_NAME ).GetComponent<GroundPlane>();
    }


    public void TouchDown( Vector3 screenPosition ) {
        Action<Mode> tryPoke = ( mode ) => {
            var pokeable = mode as Pokeable;
            if( pokeable != null )
                pokeable.Poke( this );
        };

        tryPoke( ModeManager.Instance.CurrentMode );
    }
    public void TouchUp( Vector3 screenPosition ) {
        Action<Mode> tryRelease = ( mode ) => {
            var releaseable = mode as Releaseable;
            if( releaseable != null )
                releaseable.Release( this );
        };

        tryRelease( ModeManager.Instance.CurrentMode );
    }
    public void TouchDrag( Vector3 start, Vector3 diff ) {
        Action<Mode,Vector3,Vector3> tryScrape = ( mode, s, d ) => {
            var scrapeable = mode as Scrapeable;
            if( scrapeable != null ) {
                scrapeable.Scrape( this, s, d );
            }
        };

        tryScrape( ModeManager.Instance.CurrentMode, start, diff );
    }


    public void CollisionEnter( Collision collision ) {
    }
}
}