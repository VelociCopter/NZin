using UnityEngine;


namespace NZin {



public interface Scrapeable {

	void Scrape( GroundPlane ground, Vector3 start, Vector3 now );  
	void Scrape( Entity entity, Vector3 start, Vector3 now );

}



}