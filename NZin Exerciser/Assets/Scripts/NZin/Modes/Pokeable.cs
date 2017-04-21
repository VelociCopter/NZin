using UnityEngine;
using System.Collections;

namespace NZin {


public interface Pokeable {
	
	void Poke( Entity entity );
	void Poke( GroundPlane ground, Vector3 atWorldPosition );

}

}