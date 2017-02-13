﻿


namespace NZin.Entities {

/// <summary>
/// A symbolic declaration of intent to decorate an Entity
/// </summary>
public interface EntityDecoratable : Decoratable {
}

/// <summary>
/// A simple entity. Provides no function other than a starting point for entity chains (if they want to refer to
/// </summary>
public class EmptyEntityCore : EntityDecoratable { // zzz kill this when I can
    public override string ToString() {
        return "[core]";
    }
}

/// <summary>
/// USAGE: Provide some extra state or behavior to an entity chain by decorating it
/// </summary>
public class Entity : ComparableDecorator<EmptyEntityCore>, EntityDecoratable {


    // This is a hackish extension of Comparable IDs
    // HACK {
    public long Id   { get {
            return Decoration<Entity>().CId;
        }
    }
    // } HACK

    public Entity( Entity decoratee )
        :base( decoratee ) {
    }

	public Entity()
		:base() {
	}

    public override bool Equals( System.Object o ) {
        return Equals( o as Entity );
    }
    public virtual bool Equals( Entity that ) {
        if( that != null ) {
            var thisE = this.Decoration<Entity>();
            var thatE = that.Decoration<Entity>();
            return thisE.Id == thatE.Id;
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
     

    /// <summary>
    /// Use PrettyPrint() if you want to see the entire chain
    /// </summary>
    /// <returns>A <see cref="System.String"/> that represents the current <see cref="NZin.Entities.Entity"/>.</returns>
    public override string ToString() {
        return string.Format( "[ Entity_{0} ]->[...]", Id );
    }
}

}