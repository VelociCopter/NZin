

namespace NZin.Entities {


public class LockableInput : Entity, Thinker {

    public const int NEVER_EXPIRE       = -1;
    public const int POKE               = 1 << 0;
    public const int RELEASE            = 1 << 1;
    public const int DRAG               = 1 << 2;

    public int Flags        { get; private set; }

    /// <summary>
    /// Use the Component ID for the Thinker ID. 
    /// Can't use Entity ID because different decoration thinkers attached to the same entity 
    ///     would return the same value.
    /// </summary>
    /// <value>The T identifier.</value>
    public long TId { get {
            return this.CId;
        } 
    }



    public LockableInput( Entity decoratee, int flags, int expireAfterTicks=NEVER_EXPIRE )
        :base( decoratee ) {
        Flags = flags;

        remainingTicks = expireAfterTicks;
        if( remainingTicks != NEVER_EXPIRE && remainingTicks >= 0 ) {
            MasterThinker.Instance.Register( this );
            Decoration<Disposable>().Disposed += Destroy;
        }
    }
    void Destroy( Entity e ) {
        MasterThinker.Instance.Deregister( this );
        if( HasDecoration<LockableInput>() )
            RemoveDecoration<LockableInput>();
    }


    public static bool ScreenFor( int flags, Entity entity ) {
        LockableInput li;
        if( entity.HasDecoration<LockableInput>( out li )) {
            if( (li.Flags & flags) != 0 )
                return true;
        }

        return false;
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