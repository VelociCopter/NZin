

namespace NZin {



public interface Messageable {

    /// <summary>
    /// Handles the message. 
    /// NOTE: If the message should be consumed, the handler should call msg.Consume().
    /// </summary>
    void HandleMessage( Message msg );

}



/// <summary>
/// Something that can receive a directed message
/// </summary>
public interface Receivable : Messageable {

    /// <summary>
    /// Receiver ID. This is used to match against the Reciver address on Messages.
    /// </summary>
    long RId     { get; }

}



}