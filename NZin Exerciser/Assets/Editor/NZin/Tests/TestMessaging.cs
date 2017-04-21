using System;
using System.Collections.Generic;
using NUnit.Framework;
using NZin;


public class TestMessaging {

    #region Mock Classes
    class MockReceiverA : Receivable {
        public long RId { get { return 10; } }

        public void HandleMessage( Message msg ) {
            msg.Consume();
        }
    }
    class MockReceiverB : Receivable {
        public long RId { get { return 11; } }

        public void HandleMessage( Message msg ) {
            msg.Consume();
        }
    }
    class MockConsumableMessage : Message {
        public MockConsumableMessage( long receiverId )
            :base( 0, receiverId, true ) {
        }
    }
    #endregion

    #region System Management
    [SetUp]
    public void Setup() {
        AppMessenger.DEBUG_LOG = false;
    }
    [TearDown]
    public void Teardown() {
        AppMessenger.Instance.ClearReceivers();
    }
    #endregion

    #region Tests
    [Test]
    public void RegisterIdSuccessCase() {
        var receiverA = new MockReceiverA();
        AppMessenger.Instance.RegisterIdReceiver( receiverA );
        var msgA = new MockConsumableMessage( receiverA.RId );

        AppMessenger.Instance.HandleMessage( msgA );

        NUnit.Framework.Assert.That( msgA.IsConsumed );
    }

    [Test]
    public void RegisterIdFailureCase() {
        var receiverA = new MockReceiverA();
        var receiverB = new MockReceiverB();
        AppMessenger.Instance.RegisterIdReceiver( receiverA );
        AppMessenger.Instance.RegisterIdReceiver( receiverB );
        var msgA = new MockConsumableMessage( receiverA.RId );
        var msgB = new MockConsumableMessage( receiverB.RId );

        AppMessenger.Instance.HandleMessage( msgA );

        NUnit.Framework.Assert.That( !msgB.IsConsumed );
    }

    [Test]
    public void UnconditionalReceivers() {
        var receiverA = new MockReceiverA();
        var receiverB = new MockReceiverB();
        AppMessenger.Instance.RegisterUnconditionalReceiver( receiverB );   // <-- Unconditional
        var msgA = new MockConsumableMessage( receiverA.RId );

        AppMessenger.Instance.HandleMessage( msgA );

        NUnit.Framework.Assert.That( msgA.IsConsumed );
    }
    #endregion
}
