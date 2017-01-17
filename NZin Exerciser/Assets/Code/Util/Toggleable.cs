using UnityEngine;
using System.Collections;


public delegate void ToggleEvent();

public class Toggleable {


	public event ToggleEvent TurnedOn;
	public event ToggleEvent TurnedOff;


    public bool Value { get {
            return v;
        }
    }


	public void Poke() {
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
