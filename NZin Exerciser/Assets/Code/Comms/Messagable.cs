using UnityEngine;
using System.Collections;

namespace NZin {
public interface Messagable {

	/// <summary>
	/// Handles the message. NOTE: If the message should be consumed, the handler should call msg.Consume()
	/// </summary>
	void HandleMessage( Message msg );

}

/// <summary>
/// Something that can be sent a directed message
/// </summary>
public interface Receivable : Messagable {
    long Id     { get; }
}

}