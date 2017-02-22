
using System;



namespace NZin {



public class Aligned : Entity {

    public Aligned( Entity decoratee, int team )
        :base( decoratee ) {
        Alignment = new AlignmentMixin( team );
    }
    public AlignmentMixin Alignment     { get; set; }


    public override string Print( int crumbs ) {
        return string.Format( "{0}[ Aligned Team={1} ]",
            PrintPrefix( crumbs ),
            Alignment.Code );
    }

}



public interface Agreeable {
    AlignmentMixin Alignment     { get; }
}


/// <summary>
/// There are 3 dispositions towards other teams. 
/// Perhaps counter-intuitively, these dispositions are mutually exclusive.
///     --In other words, If you are Friendly towards Bob then you are NOT neutral towards Bob.
/// Each disposition describes threat to you & to them. Therefore there is no sub-relationship.
/// If you are looking for levels of threat, don't compare Friendly/Hostile/Neutral towards: instead
///     use IsThreatenedBy? or CanHurt? 
/// 
///                 Is a threat?    Can hurt?
///     Friendly        x               x
///     Neutral         x               YES
///     Hostile         YES             YES
/// 
/// </summary>
public class AlignmentMixin : Agreeable, IEquatable<AlignmentMixin> {

    public static AlignmentMixin Neutral { get {
            return n;
        } 
    }
    static AlignmentMixin n = new AlignmentMixin( NEUTRAL );


    public AlignmentMixin Alignment      { get { return this; } }
    public const int NEUTRAL = -1;

    public virtual int Code         { get; private set; }


    public AlignmentMixin( int teamCode ) {
        Code = teamCode;
    }


    public bool IsFriendlyTowards( Entity that ) {
        return IsFriendlyTowards( that.Decoration<Aligned>().Alignment );
    }
    public virtual bool IsFriendlyTowards( Agreeable that ) {
        return this.Code != NEUTRAL && that.Alignment.Code == this.Code;
    }

    public bool IsHostileTowards( Entity that ) {
        return IsHostileTowards( that.Decoration<Aligned>().Alignment );
    }
    public virtual bool IsHostileTowards( Agreeable that ) {
        return this.Code != NEUTRAL && that.Alignment.Code != NEUTRAL && that.Alignment.Code != this.Code;
    }

    public bool IsNeutralTowards( Entity that ) {
        return IsNeutralTowards( that.Decoration<Aligned>().Alignment );
    }
    public virtual bool IsNeutralTowards( Agreeable that ) {
        return this.Code == NEUTRAL || that.Alignment.Code == NEUTRAL;
    }
        

    public bool IsThreatenedBy( Agreeable that ) {
        return this.IsHostileTowards( that );
    }
        
    public bool CanHurt( Agreeable that ) {
        return !this.IsFriendlyTowards( that );
    }


    #region Equatable Implementation

    public override bool Equals( object that ) {
        return Equals( that as AlignmentMixin );
    }
    public virtual bool Equals( AlignmentMixin that ) {
        if( that == null )
            return false;
        else
            return this.Code == that.Code;
    }
    public virtual bool Equals( int team ) {
        return this.Code == team;
    }

    public static bool Equals( AlignmentMixin a, AlignmentMixin b ) {
        if( a == null && b == null ) {
            return true;
        } else if( a == null || b == null ) {
            return false;
        } else {
            return a.Equals( b );
        }
    }

    public static bool operator ==( AlignmentMixin a, AlignmentMixin b ) {
        if (System.Object.ReferenceEquals(a, b)) {
            return true;
        }
        if (((object)a == null) || ((object)b == null)) {
            return false;
        }

        return a.Equals( b );
    }
    public static bool operator !=( AlignmentMixin a, AlignmentMixin b ) {
        return !(a == b);
    }

    public override int GetHashCode() {
        return Code;
    }

    #endregion


    public override string ToString() {
        return string.Format( "[ AlignmentMixin: Code={0} ]", Code );
    }
}


}