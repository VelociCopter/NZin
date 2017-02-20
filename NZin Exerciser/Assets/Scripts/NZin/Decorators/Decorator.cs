using UnityEngine;
using System;
using System.Collections.Generic;
using NZin;



public class Decorator<T> where T : Decoratable {


	public Decorator() {
		Clean();
	}
	public Decorator( Decorator<T> toDecorate ) {
        Assertion.That( toDecorate != null, "Cannot decorate null." );

		head = toDecorate.head;

		AddThisTypeToMap( toDecorate );
	}


    public virtual void Clean() {
        head = this;
        map = new Dictionary<Type,Decorator<T>>();
        AddThisTypeToMap( this );
    }


    /// <summary>
    /// Set this map to use v's map. Then add this type to the (shared) map.
    /// </summary>
    void AddThisTypeToMap( Decorator<T> v ) {
		if( v == null ) return;

        this.map = v.map;
        map.Add( this.GetType(), this );
	}


	public Decorator<U> Decorate<U>( T toDecorate ) where U : T {
		Decorator<U> decorated = null;
		return decorated;
	}

	
	public U Decoration<U>() where U : Decorator<T> {
		return (U)map[typeof(U)];
	}
	public bool HasDecoration<U>() where U : Decorator<T> {
		return map.ContainsKey( typeof(U) );
	}
	public bool HasDecoration<U>( out U d ) where U : Decorator<T> {
		if( map.ContainsKey( typeof(U) ))
			d = (U)map[typeof(U)];
		else
			d = null;

		return d != null;
	}
    /// <summary>
    /// Removes the decorator of the given type, returning a possibly new head containing all Decorators that are not the given type.
    /// </summary>
    /// <returns>The decoration.</returns>
    /// <typeparam name="U">The 1st type parameter.</typeparam>
    public Decorator<T> RemoveDecoration<U>() where U : Decorator<T> {
        // TODO: Get rid of the head check when possible
        // TODO: Call a standard Destroy(...) fn on the INDIVIDUAL DECORATOR (not disposing the whole chain)
        if( this != head ) {
            return head.RemoveDecoration<U>();
        } else {
            if( !map.ContainsKey( typeof( U ))) {
                throw new KeyNotFoundException();
            }

            // (un)Patch each item's hash
            var old = map[ typeof(U) ];
            map.Remove( typeof(U) );

            return old;
        }
    }
   

    public int DecoratorCount {
        get { return map.Count; }
    }



    protected Decorator<T> Head { get { return head; } }

    Dictionary<Type,Decorator<T>> map;			// For faster lookup. Only works on the "head" Decorator!
    Decorator<T> head;                          // Make sure each decorator knows how to get back to the head. Useful for printing, comparison & processing



    public override string ToString() {
        var str = Print();
        str += "\nUse PrettyPrint() for more details about the other decorators.";
        return str;
    }

    /// <summary>
    /// Print information about this specific Decorator. For printing the entire list, use PrettyPrint() instead.
    /// </summary>
    public virtual string Print( int crumbs=0 ) {
        return string.Format( "{0}[ {1}? ]",
            PrintPrefix( crumbs ), this.GetType() );
    }
    public string PrettyPrint() {
        return PrettyPrint( 0 );
    }
    protected string PrettyPrint( int crumbs ) {
        var str = "";
        foreach( var typeAndDecorator in map ) {
            str += typeAndDecorator.Value.Print( crumbs );
        }
        return str;
    }
    public static string PrintPrefix( int crumbs ) {
        if( crumbs < 0 )
            return "";
        
        var rv = "\n";
        for( int i=0; i<crumbs; i++ ) {
            rv += LEADING_BIT;
        }
        return rv;
    }
    protected readonly int NO_CRUMBS = -1;
    const string LEADING_BIT = "-";
}


public interface Decoratable {
}
