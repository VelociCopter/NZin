using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NZin;



public class MainMenuMode : Mode {

	
    public override void OnEnter() {
        base.OnEnter();

        /* We are having the main menu MODE load the correct scene, then dispatch a signal.
         * The HUD receives that signal and then transitions its state from there.
         * This execution order is useful since it allows other parts of a game to transition to 
         *      the Main Menu mode without worrying about waiting for the scene to load before the 
         *      HUD starts grabbing elements.
         */

        Debug.Log( "Loading Main Menu Scene...");
        loading = SceneManager.LoadSceneAsync( "MainMenuScene" );
		God.Instance.RandomlyUpdated += PollLoading;
    }


    public void PollLoading() {
		if( loading.isDone ) {
            Debug.Log( "...Scene Loading Complete");
			God.Instance.RandomlyUpdated -= PollLoading;
			AppMessenger.Instance.HandleMessage( new MenuSceneLoaded() );
		}
	}
	AsyncOperation loading;
}
