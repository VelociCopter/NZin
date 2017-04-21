using System;


namespace NZin
{

    /// <summary>
    /// The global root for any pure code <--> Unity GameObject "cheat" code.
    /// This is basically a dedicated place to hang hacks or get around some Unity thing you don't want.
    /// </summary>
    public class God : Monoton<God> {


    /// <summary>
    /// Listen to this event for God Monobehaviour updates. It should only be used to perform actions that do not need to be syncronized across all users–
    ///     use an Updater for those types of updates. This is more intended for debug or aesthetic things that want updates that don't already have them.
    /// </summary>
    public event Action RandomlyUpdated;



    protected override void Update() {
        base.Update();

        if( RandomlyUpdated != null ) {
            RandomlyUpdated();
        }
    }
        
}

}