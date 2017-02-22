﻿using System;
using UnityEngine;



namespace NZin {



public class Button : GameObjectProxy, GameObjectProxyReceiver {

    #region Inspector
    #endregion


    public event Action<Button> Used;


    
    protected virtual void Start() {
        // Pipes our own input through GameObjectProxy Bookkeeper so it can be prioritized against other input that frame
        this.Receiver = this;
        GetComponent<UnityEngine.UI.Button>().onClick.AddListener( OnUnityButtonClick );
    }
    protected virtual void OnDestroy() {
        GetComponent<UnityEngine.UI.Button>().onClick.RemoveListener( OnUnityButtonClick );
    }



    public virtual void Use() {
        if( Used != null )
            Used( this );
    }



    #region Input
    public void TouchDown( Vector3 screenPosition ) {
    }
    public void TouchUp( Vector3 screenPosition ) {
        Use();
    }
    public void TouchDrag( Vector3 start, Vector3 now ) {
    }
    public void CollisionEnter( Collision collision ) {
    }

    void OnUnityButtonClick() {
        GameObjectProxyBookkeeper.Instance.Released( this );
    }
    #endregion

}



}