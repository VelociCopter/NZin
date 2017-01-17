using System.Collections.Generic;
using UnityEngine;


namespace NZin {
    
/// <summary>
/// The layer that takes messages from "anywhere" and routes them to the appropriate receivers
/// </summary>
public class AppMessenger : Singleton<AppMessenger>, Messagable {
    public const bool DEBUG_LOG = true;
#pragma warning disable 162

    public void HandleMessage( Message msg ) {
        if( DEBUG_LOG )
            Debug.Log( "AppMessenger handling msg="+msg );
        
        foreach( var receiver in unconditionalReceivers ) {
            receiver.HandleMessage( msg );
        }

        if( !msg.Consumed ) {
            var address = msg.ReceiverId;
            if( idReceivers.ContainsKey( address )) {
                idReceivers[ address ].HandleMessage( msg );
            }
        }
    }

    public void RegisterIdReceiver( Receivable receiver ) {
        idReceivers.Add( receiver.Id, receiver );
    }
    public void DeregisterIdReceiver( Receivable receiver ) {
        idReceivers.Remove( receiver.Id );
    }
    public void RegisterUnconditionalReceiver( Messagable receiver ) {
        unconditionalReceivers.Add( receiver );
    }


    List<Messagable> unconditionalReceivers = new List<Messagable>();
    Dictionary<long,Receivable> idReceivers = new Dictionary<long,Receivable>();
}

}