using UnityEngine;
using System;
using System.Collections;



namespace NZin {



/// <summary>
/// A class that signals the update schedule for another object
/// </summary>
public abstract class Updater {

    public event Action Updated;


    /// <summary>
    /// Intended to be used internally by children of this class to kick off an Update
    /// </summary>
    public void SignalUpdate() {
        Updated();
    }
}


}