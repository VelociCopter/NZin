using NUnit.Framework;
using NZin;


public class TestGlobals {

    #region Mock Classes and Functions
    class Toggle {
        public bool State = false;

        public void SetTrue() {
            State = true;
        }
    }
    #endregion


    #region Tests
    /* Would be doing some tests like this, but we can't unit test GameObjects.
     * That means we can't test Monotons.
    [Test]
    public void RandomUpdates() {
        var toggle = new Toggle();
        God.Instance.RandomlyUpdated += toggle.SetTrue;

        God.Instance.Update();

        NUnit.Framework.Assert.That( toggle.State == true );
    }
    */
    #endregion

}
