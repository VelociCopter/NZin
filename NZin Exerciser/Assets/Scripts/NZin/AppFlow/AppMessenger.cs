using System.Collections.Generic;
using UnityEngine;


namespace NZin {
    
/// <summary>
/// The layer that takes messages from "anywhere" and routes them to the appropriate receivers.
/// There are likely many "unconditional recievers" (like the Game Mode or HUD) that get first dibs at ALL messages (regardless of recipient ID).
/// Then the other "standard" receivers only receive a message if it is addressed to them.
/// </summary>
public class AppMessenger : Singleton<AppMessenger>, Messageable {
#pragma warning disable 162
    public static bool DEBUG_LOG = true;


    public void HandleMessage( Message msg ) {
        if( DEBUG_LOG )
            Debug.Log( "AppMessenger handling msg="+msg );
        
        foreach( var receiver in unconditionalReceivers ) {
            receiver.HandleMessage( msg );
        }

        if( !msg.IsConsumed ) {
            var address = msg.ReceiverId;
            if( idReceivers.ContainsKey( address )) {
                idReceivers[ address ].HandleMessage( msg );
            }
        }
    }


    public void RegisterIdReceiver( Receivable receiver ) {
        idReceivers.Add( receiver.RId, receiver );
    }
    public void DeregisterIdReceiver( Receivable receiver ) {
        idReceivers.Remove( receiver.RId );
    }
	public void RegisterUnconditionalReceiver( Messageable receiver ) {
        unconditionalReceivers.Add( receiver );
    }
    public void ClearReceivers() {
        idReceivers.Clear();
        unconditionalReceivers.Clear();
    }


	List<Messageable> unconditionalReceivers = new List<Messageable>();
    Dictionary<long,Receivable> idReceivers = new Dictionary<long,Receivable>();
}

}