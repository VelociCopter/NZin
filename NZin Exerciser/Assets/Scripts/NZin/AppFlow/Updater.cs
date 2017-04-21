using System;



namespace NZin {

/// <summary>
/// A class that signals the update schedule for another object.
/// See MasterThinker for example usage.
/// </summary>
public abstract class Updater {

    public event Action Updated;


    /// <summary>
    /// Intended to be used internally by children of this class to kick off an Update.
    /// You could just call this to hack in an update if you are so inclined.
    /// </summary>
    public void SignalUpdate() {
        // Deliberately not checking for null, here. Since this is supposed to be more
        //  internal, you probably want to make sure you've wired up everything before
        //  firing off any updates.
        Updated();
    }
}


}