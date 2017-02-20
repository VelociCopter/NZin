using UnityEngine;
using System.Collections;
using NZin;


public interface Releaseable {
    
	void Release( GroundPlane ground );
	void Release( Entity entity );
	
}
