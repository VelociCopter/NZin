using UnityEngine;
using System.Collections;


namespace NZin {

/// <summary>
/// A fairly generic message. Used to communicate between layers of the NZin.
/// 
/// USAGE NOTES:
/// When a game agent is performing an action that is worthy of an app-wide signal (EG a unit is 
/// being selected), the real "source" should send the message (via 
/// AppMessenger.Instance.HandleMessage(...)). As an example, say a HUD class knows that a unit
/// was just clicked and it wanted to let the whole app know that a unit was clicked on. The
/// proper message flow would be:
/// HUD_Idle_Class receives a "Tapped" call
///     myUnitThatWasJustClicked.Select()
///         AppMessenger.Instance.HandleMessage( new SelectionMessage( this ))
///     Handle( SelectionMessage )
///         HUD.Instance.SwitchToState( HUD_Selected_Class )
/// 
/// PERFORMANCE NOTES:
/// This messaging system uses reflection to direct against state machine edges. This may not
/// be a big deal for smaller games & prototypes, but can be a problem for complex games if they
/// are relying on messaging in inner loops or performance-critical code.
/// </summary>
public class Message {

	public System.Type RoutingType			{ get { return this.GetType(); } }

	public const long NULL_ID = 0;
	public long SenderId					{ get; protected set; }
    public long ReceiverId					{ get; protected set; }
    public bool IsConsumable                { get; protected set; }

    public bool IsConsumed					{ get {
            return IsConsumable && consumed;
        }
    }
	public void Consume() {
        Assertion.That( !IsConsumable || !IsConsumed, "Already consumed this consumable message, or it is (wrongly?) consumable." );
		consumed = true;
	}
    bool consumed = false;

    public long MessageId {
        get {
            return messageId;
        }
    }
    int messageId = nextId++;
    static int nextId = 1;



    public Message( bool isConsumable )
        :this( 0, 0, isConsumable ) {
    }
	public Message( long fromId, long toId, bool isConsumable ) {
		this.SenderId = fromId;
		this.ReceiverId = toId;
        this.IsConsumable = isConsumable;
	}



    public override string ToString() {
        return string.Format( "[ Message: Id={0}, RoutingType={1}, SenderId={2}, ReceiverId={3}, IsConsumable={4}, Consumed={5} ]", 
            MessageId, RoutingType, SenderId, ReceiverId, IsConsumable, IsConsumed
        );
    }
}

}