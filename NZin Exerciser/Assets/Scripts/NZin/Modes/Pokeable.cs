using UnityEngine;
using System.Collections;
using NZin;


public interface Pokeable {
	
	void Poke( Entity entity );
	void Poke( GroundPlane ground );

}
