using System;
using UnityEngine;




namespace NZin {


/// <summary>
/// The global root for any pure code <--> Unity GameObject "cheat" code
/// </summary>
public class God : MonoBehaviour {

    /// <summary>
    /// Listen to this event for God Monobehaviour updates; should only be used to perform actions that do not need to be syncronized across all users–use an Updater for those types of updates
    /// </summary>
    public event Action RandomlyUpdated;


    public static God FindMe() {
        if( instance == null ) {
            var q = GameObject.Find( NAME );
            if( q == null ) {
                q = new GameObject();
                q.name = NAME;
                GameObject.DontDestroyOnLoad( q );
            }

            instance = q.AddComponent<God>();
        }
        return instance;
    }


    public T AddComponent<T>() where T : MonoBehaviour {
        return this.gameObject.AddComponent<T>();
    }


    void Update() {
        if( RandomlyUpdated != null ) {
            RandomlyUpdated();
        }
    }


    static God instance;
    const string NAME = "__GOD_";
}
}