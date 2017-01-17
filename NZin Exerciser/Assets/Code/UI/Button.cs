using System;
using UnityEngine;
using NZin.Entities;


namespace NZin {

// TODO: Remove WorldPositionTracker from this class and put it somewhere more generic
public class Button : GameObjectProxy, GameObjectProxyReceiver, WorldPositionTracker {

    #region Temp WorldPositionTracker Impl
    public Vector3 WorldPosition { get {
            return transform.position;
        }
    }
    public event Action<Vector3> Moved;  // zzz deliberately unused for now
    public string Print( int crumbs ) {
        return "TODO";
    }
    #endregion

    #region Inspector
    public bool AutoRepulse = false;
    #endregion

    public event Action<Button> Used;


    public virtual void Use() {
        if( Used != null )
            Used( this );
    }


    #region Input
    protected virtual void Start() {
        // Pipes our own input through GameObjectProxy Bookkeeper so it can be prioritized against other input that frame
        this.Receiver = this;
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener( OnUnityButtonClick );

        if( AutoRepulse ) {
            AddRepulsor();
        }
    }
    protected virtual void OnDestroy() {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener( OnUnityButtonClick );
    }
    void OnUnityButtonClick() {
        GameObjectProxyBookkeeper.Instance.Released( this );
    }


    public void TouchDown( Vector3 screenPosition ) {
    }
    public void TouchUp( Vector3 screenPosition ) {
        Use();
    }
    public void TouchDrag( Vector3 start, Vector3 now ) {
    }
    public void CollisionEnter( Collision collision ) {
    }
    #endregion



    // TODO: I probably want to abstract this out somewhere
    void AddRepulsor() {
        /* NOTES:
         * This class was originally written to be attached directly to Unity.GameObject stuff, so it kind of
         * monkeypatchs on an Entity. It should be more elegantly integrated into the Entity framework.
         */
        /* zzz
        repulsor = new Entity();
        repulsor = new Disposable( repulsor );
        repulsor = new WorldPositional( repulsor );
        repulsor = new WorldMover( repulsor.Decoration<WorldPositional>() );
        repulsor = new HudRepulsor( repulsor );

        var mover = repulsor.Decoration<WorldMover>();
        mover.Moved += HackUpdatePos;
        var worldRay = ZinCam.Instance.ScreenPointToRay( this.transform.position );
        float d;
        GroundPlane.FindMe().Plane.Raycast( worldRay, out d );
        var worldPos = worldRay.GetPoint( d );
        mover.Move( worldPos );
        */
    }
    Entity repulsor;

    void HackUpdatePos( Vector3 worldPos ) {
        var screenPos = ZinCam.Instance.WorldToScreenPoint( worldPos );
        this.transform.position = screenPos;
    }
}
}