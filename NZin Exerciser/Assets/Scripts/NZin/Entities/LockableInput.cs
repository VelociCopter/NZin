

namespace NZin {


// TODO: Test coverage on this class
// TODO: This needs an Exercise example
public class LockableInput : Entity, Thinker {

    /// <summary>
    /// Use this in place of the # of ticks for a LockableInput to last if you want the Lock to last forever.
    /// </summary>
    public const int NEVER_EXPIRE       = -1;


    // Flags to determine what type of input is locked {
    public int Flags        { get; private set; }

    public const int POKE               = 1 << 0;
    public const int RELEASE            = 1 << 1;
    public const int DRAG               = 1 << 2;

    /// <summary>
    /// Test if an Entity has been marked with any of the given Flags.
    /// </summary>
    /// <param name="flags">A bitfield of the flags that are tested for</param>
    /// <param name="entity">The Entity to examine</param>
    /// <returns>True if the given Entity has any of the given flags</returns>
    public static bool ScreenFor( int flags, Entity entity ) {
        LockableInput li;
        if( entity.HasDecoration<LockableInput>( out li )) {
            if( (li.Flags & flags) != 0 )
                return true;
        }

        return false;
    }
    // } Flags



    /// <summary>
	/// Thinker ID
    /// </summary>
    public long TId { get {
            /// This needs to be unique per thinker (see Thinker.cs:MasterThinker),
            /// and there can be n thinkers : 1 entity (since multiple Decorations may be independent thinkers)
        
            // Return the CId to accomplish a unique per-thinker ID. 
            return this.CId;
        } 
    }



    public LockableInput( Entity decoratee, int flags, int expireAfterTicks=NEVER_EXPIRE )
        :base( decoratee ) {
        Flags = flags;

        remainingTicks = expireAfterTicks;
        if( remainingTicks != NEVER_EXPIRE && remainingTicks >= 0 ) {
            MasterThinker.Instance.Register( this );
            decoratee.Disposed += Destroy;
        }
    }
    // zzz TODO: Use Disposable behavior instead (or integrate Disposable better?)
    void Destroy( Entity e ) {
        MasterThinker.Instance.Deregister( this );
        if( HasDecoration<LockableInput>() )
            RemoveDecoration<LockableInput>();
    }



    public void Think() {
        if( remainingTicks == NEVER_EXPIRE )
            return;
        
        remainingTicks--;
        if( remainingTicks <= 0 ) {
            Destroy( this );
        } else {
        }
    }



    int remainingTicks = NEVER_EXPIRE;
}


}