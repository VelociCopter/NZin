using NZin;



public class AppFlowFactory {

    public static void SetupAppModes( Glob glob ) {

        /* NOTE: We could implement it such that all state-trigger-type messages have a unique ReceiverID 
         *  that matches the ModeManager instead of piping all messages through. For the game modes (the
         *  current "app state" if you will, we will use Unconditional Receivers.
         */
        var mgr = new StateManager();
        AppMessenger.Instance.RegisterUnconditionalReceiver( mgr );


        // Initialize each mode
        var loading = new LoadingMode();
        var mainMenu = new MainMenuMode();


        // Set up transitions between modes
        mgr.AddEdge<LoadingComplete>( loading, mainMenu );


        // Go to the initial mode
        mgr.JumpTo( loading, glob );
    }


    public static void SetupHudModes( Glob glob ) {
        var mgr = new StateManager();
        AppMessenger.Instance.RegisterUnconditionalReceiver( mgr );


        var idle = new IdleHud();
        var menu = new MainMenuHud();


        mgr.AddEdge<MenuSceneLoaded>( idle, menu );


        mgr.JumpTo( idle, glob );
    }
}
