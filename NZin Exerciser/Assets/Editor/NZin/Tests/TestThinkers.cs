using System;
using System.Collections.Generic;
using NUnit.Framework;
using NZin;


public class TestThinkers {

    #region Mock Classes & Systems
    class MockThinker : Thinker {
        public long TId { get { return 1; } }
        public bool Thought = false;
        public void Think() {
            Thought = true;
        }
    }
    static Updater MockUpdater;
    [SetUp]
    public void Setup() {
        MockUpdater = new ManualUpdater();
        MasterThinker.Instance.Initialize( MockUpdater );
    }
    [TearDown]
    public void Teardown() {
        MasterThinker.Instance.Terminate();
    }
    #endregion

    #region Tests
    [Test]
    public void UpdatesThinkers() {
        var thinker = new MockThinker();
        MasterThinker.Instance.Register( thinker );

        MockUpdater.SignalUpdate();

        NUnit.Framework.Assert.That( thinker.Thought );
    }
    #endregion

}