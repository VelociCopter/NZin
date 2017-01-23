using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NZin;



/* NOTE: This class demonstrates usage of various parts of the NZin framework.
 */


public class EntryPoint : MonoBehaviour {

	
	void Start () {

        Debug.Log( "Beginning NZin Exerciser" );
	
        SetupAppStructure();
        UtilizeGod();

	}
	
	
	void Update () {
		
	}



    /// <summary>
    /// This example shows how you may want to set up a game with a single player update loop. This OfflineUpdater() essentially just uses MonoBehviour.Update() to 
    ///     trigger updates. However there are more exotic update methods, which may be useful for things like syncronous multiplayer or turn based games.
    /// </summary>
    void SetupAppStructure() {
        // Make the standard single-player update loop
        Updater upr = new OfflineUpdater();
        MasterThinker.Instance.Initialize( upr );

        // Create some shared data objects. 
        // These are kind of like global variables, but they are scoped to anything that can access the current Mode 
        // (which is everything, so they're basically just globals.
        Glob glob = new Glob();

        // Now setup the "Mode"
        // This can be treated simply as an FSM that drives the HUD depending on what part of the game the user is in.
        // It can also be used more extensively to drive things like scenes and data loading.
        // You may choose to only have a single MODE FSM to drive everything (including the HUD), but keeping them separate allows for more flexibility.
        AppFlowFactory.CreateModes( glob );
        //zzz td ExampleHudFactory.CreateHud( glob );
    }



    void UtilizeGod() {
        Debug.Log( "Demonstrating God" );

        God.Instance.RandomlyUpdated += IJustWantAnUpdate;
    }
    void IJustWantAnUpdate() {
        // This is a very non-formal type of update. You probably don't want to do anything serious, here. Use an Updater instead.
        Debug.Log( "A mock hack update loop was triggered" );
        God.Instance.RandomlyUpdated -= IJustWantAnUpdate;
    }
}
