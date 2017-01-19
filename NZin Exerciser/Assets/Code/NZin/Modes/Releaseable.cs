using UnityEngine;
using System.Collections;
using NZin;
using NZin.Entities;


public interface Releaseable {
    
	void Release( GroundPlane ground );
	void Release( Entity entity );
	
}
