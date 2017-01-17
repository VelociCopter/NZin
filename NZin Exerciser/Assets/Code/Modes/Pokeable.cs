using UnityEngine;
using System.Collections;
using NZin;
using NZin.Entities;


public interface Pokeable {
	
	void Poke( Entity entity );
	void Poke( GroundPlane ground );

}
