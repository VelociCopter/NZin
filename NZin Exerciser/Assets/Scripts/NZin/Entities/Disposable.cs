using System;
using UnityEngine;


namespace NZin.Entities {

// zzz td: This should be a part of proper Entities, just like Comparable.
// Or mabye a type of Base that Entities use

/// <summary>
/// A signal for any entity decoration to declare the whole chain should dispose of itself.
/// Due to linking issues, this decoration should be added as soon as possible so further decorations have a chance to subscribe.
/// </summary>
public class Disposable : Entity {
    public const bool DEBUG_LOG = true;


    public event Action<Entity> Disposed;

    public bool IsDisposed { get; private set; }


    public Disposable( Entity decoratee )
        :base( decoratee ) {
        IsDisposed = false;
    }


    public void Dispose() {
        if( DEBUG_LOG ) 
            Debug.Log( "Disposing "+Id+"\n"+this.PrettyPrint() );
            
        bool wasDisposed = IsDisposed;
        IsDisposed = true;

        if( Disposed != null && !wasDisposed ) {
            Disposed( this );
        }
    }


    public override string Print( int crumbs ) {
        return string.Format( "{0}[ Disposable IsDisposed?{1} ]",
            PrintPrefix( crumbs ), IsDisposed
        );
    }
}
}