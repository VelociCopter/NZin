using UnityEngine;
using System;
using System.Collections.Generic;



public class ComparableDecorator<T> : Decorator<T>, Decoratable, IEquatable<ComparableDecorator<T>> where T : Decoratable {


    /// <summary>
    /// The comparable ID. Note that any Decoration derived from this will inherit it, and therefore make it 
    /// ambigious as to whether MyDecorator.CId refers to the "whole chain" or just that individual Decorator.
    /// This property is intended to be used for logical equivalence between 2 (non-homogenous) decorators that
    /// are decorated with Comparables. The correct usage is:
    /// MyDecorator.Decorator<ComparableDecorator>().CId
    /// --NOT--
    /// MyDecorator.CId   // <-- this will refer to the single Decorator component
    /// </summary>
    public long CId   { get {
            return id;
        }
    }


    public ComparableDecorator( Decorator<T> toDecorate )
        :base( toDecorate ) {
    }



    public override bool Equals( object that ) {
        return Equals( that as Decorator<T> );
    }
	public virtual bool Equals( long thatId ) {
		return this.id == thatId;
	}
	public virtual bool Equals( Decorator<T> that ) {
		if( that == null ) return false;
		
		ComparableDecorator<T> comp;
		if( that.HasDecoration( out comp )) {
			return Equals( comp );
		} else {
			return false;
		}
	}
	public bool Equals( ComparableDecorator<T> that ) {
		return Equals( this, that );
	}
	public static bool Equals( ComparableDecorator<T> a, ComparableDecorator<T> b ) {
		if( a == null && b == null ) {
			return true;
		} else if( a == null || b == null ) {
			return false;
		} else {
			return a.CId == b.CId;
        }
	}

	
	public override int GetHashCode() {
		return (int)CId;
	}
	
	
    public override string Print( int crumbs=0 ) {
        return string.Format( "{0}[ {1} _{2} ]", 
            PrintPrefix( crumbs ),
            this.GetType()+"?",
            id );
    }
	
	
	long id = nextId++;
	static long nextId = 1;
}
