using UnityEngine;
using System.Collections;



namespace NZin {



public class GroundedModeMixin  {

    /* TODO: Is this class still relevant? Seems kind of obsolete. Keep it around until the Exercisor has demonstrated:
			// 1) a Mode that has a mouse-clickable ground
			// 2) draw GL lines on the ground

    public Vector3 CameraPositionAtEnter            { get; private set; }


    public GroundedModeMixin( WorldMode owner ) {
		this.owner = owner;


        if( debugGoPrefab == null ) {
            debugGoPrefab = Resources.Load( "World/Cursor" ) as GameObject;
        }
        debugGo = GameObject.Instantiate( debugGoPrefab );
        debugGo.name = "__GroundedModeBall";
        debugGo.SetActive( false );
        DynamicGoCollator.Instance.Take( debugGo );
		debugGo.transform.localScale = Vector3.one * 0.2f;

        if( lineRendererPrefab == null ) {
            lineRendererPrefab = Resources.Load( "World/LineRenderer" ) as GameObject;
        }

        GameObject lineGo = GameObject.Instantiate( lineRendererPrefab );
        lineGo.name = "__GroundedModeLine";
        DynamicGoCollator.Instance.Take( lineGo );
		lineRenderer = lineGo.AddComponent<LineRenderer>();
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetColors( Color.red, Color.black );
		lineRenderer.SetWidth( 0.1f, 0.1f );
	}


	public void ResetTracking() {
		var selected = owner.GetTransStateData().Selected;
        if( selected != null ) {
            var center = selected.Decoration<NodeWrappedEn>().Node.transform.position;
            debugGo.transform.position = center;
            RenderLine( center );
        }

        CameraPositionAtEnter = ZinCam.Instance.Position;
	}
	public void HideHandles() {
		lineRenderer.SetVertexCount(0);
		debugGo.SetActive( false );
	}


	/// <summary>
	/// Tracks the cursor.
	/// </summary>
	/// <returns>The cursor in world position at its intersection with this mode's ground</returns>
	public Vector3? TrackCursor( Vector3 scrapePosition, bool alsoRenderLine ) {
		return SnapCursorToProjectedLocation( scrapePosition, owner.GetTransStateData().Ground.Plane, alsoRenderLine );
	}
    public void ClearCursor() {
        debugGo.SetActive( false );
    }
	
	Vector3? SnapCursorToProjectedLocation( Vector3 cameraPosition, Plane ground, bool alsoRenderLine ) {
		var ray = ZinCam.Instance.ScreenPointToRay( cameraPosition );
		float d;
		Vector3? target = null;
		if( ground.Raycast ( ray, out d )) {
			target = ray.GetPoint( d );
            debugGo.SetActive( true );
			debugGo.transform.position = target.Value;
			
			if( alsoRenderLine) {
				RenderLine( target.Value );
			}
		}
		return target;
	}



	
	void RenderLine( Vector3 to ) {
		debugGo.SetActive( true );
		lineRenderer.SetVertexCount(2);
		lineRenderer.SetPosition(0, owner.GetTransStateData().Selected.Decoration<NodeWrappedEn>().Node.transform.position);
		lineRenderer.SetPosition(1, to);
	}



	

	WorldMode owner;
    static GameObject debugGoPrefab;
	GameObject debugGo;
    static GameObject lineRendererPrefab;
	LineRenderer lineRenderer;
    float distanceAtStart;
    */


}
}