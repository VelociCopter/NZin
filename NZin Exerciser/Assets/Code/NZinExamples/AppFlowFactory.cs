using NZin;



public class AppFlowFactory {

    public static void CreateModes( Glob glob ) {
        ModeManager mgr = ModeManager.Instance;
        // NOTE: We could implement it such that all state-trigger-type messages have a unique ReceiverID that matches the ModeManager instead of piping all messages through.
        AppMessenger.Instance.RegisterUnconditionalReceiver( mgr );


        // Initial and setup
        var loading = new LoadingMode();
        var mainMenu = new MainMenuMode();


        mgr.AddEdge<LoadingComplete>( loading, mainMenu );


        mgr.JumpTo( loading, glob );
    }
}
