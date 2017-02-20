#pragma warning disable 162


using System;
using UnityEngine;


namespace NZin {


/// <summary>
/// A signal for any entity decoration to declare the whole chain should dispose of itself.
/// Due to linking issues, this decoration should be added as soon as possible so further decorations have a chance to subscribe.
/// </summary>
public class Disposable<T> : Decorator<T>, Decoratable where T : Decoratable {
    public const bool DEBUG_LOG = false;


	public event Action<Disposable<T>> Disposed;

    public bool IsDisposed { get; private set; }


	public Disposable()
		:base() {
		Ctor();
	}
	public Disposable( Decorator<T> toDecorate )
		:base( toDecorate ) {
		Ctor();
	}
	private void Ctor() {
		IsDisposed = false;
	}


    public void Dispose() {
        if( DEBUG_LOG ) 
            Debug.Log( "Disposing\n"+this.PrettyPrint() );
            
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