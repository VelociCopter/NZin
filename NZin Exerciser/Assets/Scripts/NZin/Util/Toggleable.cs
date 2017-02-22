namespace NZin {


public delegate void ToggleEvent();


/// <summary>
/// Something with an & or off state
/// </summary>
public class Toggleable {


	public event ToggleEvent TurnedOn;
	public event ToggleEvent TurnedOff;


    public bool Value { get {
            return v;
        }
    }


	public void Toggle() {
		Set( !v );
	}
	public void TurnOff() {
		v = false;
		Dispatch( TurnedOff );
	}
	public void TurnOn() {
		v = true;
		Dispatch( TurnedOn );
	}
	public void Set( bool value ) {
		if( value ) {
			TurnOn();
		} else {
			TurnOff();
		}
	}
	public void Reset() {
		v = false;
	}


	void Dispatch( ToggleEvent e ) {
		if( e != null )
			e();
	}

	bool v = false;
}

}