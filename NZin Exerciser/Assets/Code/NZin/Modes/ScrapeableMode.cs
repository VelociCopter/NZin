using UnityEngine;
using System.Collections;
using NZin.Entities;


namespace NZin {

public interface Scrapeable : Mode {

	void Scrape( GroundPlane ground, Vector3 start, Vector3 now );  
	void Scrape( Entity entity, Vector3 start, Vector3 now );

}

}