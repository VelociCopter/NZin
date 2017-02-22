using UnityEngine;

namespace NZin {



// NOTE: I would call this class "Assert", but that conflicts with NUnit.Framwork.Assert which makes it annoying to use in tests
public class Assertion {


    /// <summary>
    /// A typical code Assertion
    /// NOTE: As of early 2017, these do NOT compile out of your codebase.
    /// </summary>
    public static void That( bool condition, string message="Assert failed." ) {
        if( !condition ) {
            Error( message );
        }
    }



    #region AppFlow Errors

    public static void UnexpectedEntry( string extraInfo=null ) {
        var msg = "Unexpected code path entered!";
        if( extraInfo != null ) {
            msg += "\n"+extraInfo;
        }
        Error( msg );
    }

    public static void Unimplemented( string extraInfo=null ) {
        var msg = "This code is not yet fully implemented.";
        if( extraInfo != null ) {
            msg += "\n"+extraInfo;
        }
        Error( msg );
    }

    #endregion



    static void Error( string message ) {
        Debug.LogError( message );
    }

}

}