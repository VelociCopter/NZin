

namespace NZin.StateMachines {



	public delegate bool TransitionTestDelegate();



	public class Transition {

		public State From							{ get; private set; }
		public State To								{ get; private set; }
		public TransitionTestDelegate TestPasses	{ get; private set; }


		public Transition( State from, State to, System.Type type )
			:this( from, to, type, AlwaysPass ) {
		}
		public Transition( State from, State to, System.Type type, TransitionTestDelegate ifPasses ) {
			From = from;
			To = to;
			TestPasses = ifPasses;
			myType = type;
		}



		public bool RespondsToMessageType( Message msg ) {
			return msg.GetType() == myType;
		}



		static bool AlwaysPass() {
			return true;
		}



		System.Type myType;
	}

}