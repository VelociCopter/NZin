﻿using System;



namespace NZin {

/// <summary>
/// A symbolic declaration of intent to decorate an Entity
/// </summary>
public interface EntityDecoratable : Decoratable {
}




/// <summary>
/// USAGE: Provide some extra state or behavior to an entity chain by decorating it
/// </summary>
public class Entity : ComparableDecorator<EntityDecoratable>, EntityDecoratable {


    /// <summary>
    /// This is a unique ID per Entity. Any Decorations on this Entity are all associated with this ENTITY Id
    /// </summary>
    public long Id   { get {
            // Use THIS (Entity) Component ID, since :
            //      1) it will be unqiue against other Entities
            //      2) other Decorators on this Entity will return the same ID
            return Decoration<Entity>().CId;
        }
    }
    

    public Entity( Entity decoratee )
        :base( decoratee ) {
		Ctor();
    }
	public Entity()
		:base() {
		Ctor();
	}
	private void Ctor() {
		// Add a concrete Entity if necessary. 
        // Useful for "leaf" Entity Decorators that extend Entity
        //  ––since they still need a concrete Entity for Comparison and Disposal
		Entity entity;
		if( !HasDecoration<Entity>( out entity )) {
			entity = new Entity( this );
		}

		// Add disposable behavor, since that should be implemented by practically(?) all entities
		Disposable<EntityDecoratable> disposable;
		if( !HasDecoration<Disposable<EntityDecoratable>>( out disposable )) {
	        disposable = new Disposable<EntityDecoratable>( this );
		}
        disposable.Disposed += OnInnerDisposed;
	}


    #region Disposable Delegation
    
    public event Action<Entity> Disposed;
    public bool IsDisposed { get { return Decoration<Disposable<EntityDecoratable>>().IsDisposed; } }
    public void Dispose() {
        Decoration<Disposable<EntityDecoratable>>().Dispose();
    }
    
    void OnInnerDisposed( Disposable<EntityDecoratable> disposable ) {
        if( this.Disposed != null ) {
            Disposed( this );
        }
    }

    #endregion

    #region Comparisons

    public override bool Equals( System.Object o ) {
        return Equals( o as Entity );
    }
    public virtual bool Equals( Entity that ) {
        if( that != null ) {
			return this.Id == that.Id;
        }
        return false;
    }

    public static bool operator ==(Entity a, Entity b) {
        if (System.Object.ReferenceEquals(a, b)) {
            return true;
        }
        if (((object)a == null) || ((object)b == null)) {
            return false;
        }

        return a.Equals( b );
    }
    public static bool operator !=(Entity a, Entity b) {
        return !(a == b);
    }

    public override int GetHashCode() {
        return base.GetHashCode();
    }

    #endregion

    #region Printing

    /// <summary>
    /// Use PrettyPrint() if you want to see the entire chain
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="NZin.Entity"/>.</returns>
    public override string ToString() {
        return string.Format( "[ Entity_{0} ]->[...]", Id );
    }

    #endregion
}



}