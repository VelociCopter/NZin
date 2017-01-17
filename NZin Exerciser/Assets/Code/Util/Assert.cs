using UnityEngine;
using System.Collections;


namespace NZin {
public class Assert {


    public static void That( bool condition, string message="Assert failed." ) {
        if( !condition ) {
            Error( message );
        }
    }


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


    static void Error( string message ) {
        Debug.LogError( message );
    }


}
}