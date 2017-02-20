using System;
using UnityEngine;
using NZin;


// zzz kill this class. Redo something like it (but better done) later if desired.
public class UiRepulsor : MonoBehaviour {

    #region Inspector
    public Transform RepelledObject;
    public Vector2 ScreenRepulsionBounds;
    #endregion

    public Vector3 ScreenPosition   { get {
            return RepelledObject.transform.position;
        }
    }
    public Vector3 LastTickRepelInfluence       { get; private set; }



    void Start() {
        MasterHudRepulsor.Instance.Register( this );
    }
    void OnDestroy() {
        MasterHudRepulsor.Instance.Deregsiter( this );
    }


    public void AddRepellantScreenVector( Vector3 moveTowards ) {
        repelScreenInfluence += moveTowards;
    }
    Vector3 repelScreenInfluence = Vector3.zero;



    public bool CollidesWith( UiRepulsor that ) {
        // TODO: This is a pretty bad implementation. I could use a lot of helpers from RectTransform instead
        var thisMinX = ScreenPosition.x - ScreenRepulsionBounds.x / 2f;
        var thisMaxX = ScreenPosition.x + ScreenRepulsionBounds.x / 2f;
        var thisMinY = ScreenPosition.y - ScreenRepulsionBounds.y / 2f;
        var thisMaxY = ScreenPosition.y + ScreenRepulsionBounds.y / 2f;

        var thatMinX = that.ScreenPosition.x - that.ScreenRepulsionBounds.x / 2f;
        var thatMaxX = that.ScreenPosition.x + that.ScreenRepulsionBounds.x / 2f;
        var thatMinY = that.ScreenPosition.y - that.ScreenRepulsionBounds.y / 2f;
        var thatMaxY = that.ScreenPosition.y + that.ScreenRepulsionBounds.y / 2f;

        return thisMinX <= thatMaxX
            && thisMaxX >= thatMinX
            && thisMinY <= thatMaxY
            && thisMaxY >= thatMinY;
    }


    /* NOTES:
     * This (Unity) Update will occur "mid-tick" so it may get some of its proper UiRepulsor influence,
     *      but will likey not get the rest of it until mid-frame. This may lead to some occilating
     *      equilibria. This could be fixed by making the Thinker capable of dispatching prioritized
     *      updates then having UiRepulsor do its updating in that fn rather than this Unity Update
     */
    void Update() {
//        Debug.Log("zzz update. RepelScreenInf="+repelScreenInfluence);

        /* NOTES:
         * This function assumes the camera is orthographic. However, it should still work (a little off, but overall
         *      acceptable) with a perspective camera.
         */
        /*
        Func<Vector3,Vector3> screenPointToWorld = ( screenPoint ) => {
            var ray = ZinCam.Instance.ScreenPointToRay( screenPoint );
            float t;
            GroundPlane.FindMe().Plane.Raycast( ray, out t );
            return ray.GetPoint( t );
        };
            
        var originWorldPoint = screenPointToWorld( Vector3.zero );
        var destinationWorldPoint = screenPointToWorld( LastTickRepelInfluence );
        var moveWorld = destinationWorldPoint - originWorldPoint;
        moveWorld *= 10f; // zzz hack speed

        if( moveWorld.sqrMagnitude > 0 )
            RepelledObject.position = moveWorld;
        else
            RepelledObject.localPosition -= RepelledObject.localPosition*0.01f;
        */
        LastTickRepelInfluence = repelScreenInfluence.normalized;
        //repelScreenInfluence = Vector3.zero;
    }


}
