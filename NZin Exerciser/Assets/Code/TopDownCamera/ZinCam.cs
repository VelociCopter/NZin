using UnityEngine;
using System.Collections;
using System.Collections.Generic;


/// <summary>
/// A class that wraps around Camera.main to provide typical top-down functionality
/// </summary>
public class ZinCam : Monoton<ZinCam> { // TODO: This might not want to be a Monoton–or perhaps alter Monoton. The problem occurs when we swtich scenes, we end up carrying THIS camera into the new scene, which is not always desired


    public delegate void MoveEvent();
    public event MoveEvent Moved;

    public Camera UnityCamera           { get; private set; }
    public float ScrollXMaxHard, ScrollXMinHard, ScrollYMaxHard, ScrollYMinHard;
    public float ScrollXMaxSoft, ScrollXMinSoft, ScrollYMaxSoft, ScrollYMinSoft;
    public Vector3 CoastMomentum        { get; private set; }
    public bool MomentumEnabled = false;
    public Vector3 LastPosition         { get; private set; }
    public Vector3 LastScrollPosition   { get; private set; }



    public void Push( Vector3 direction ) {
        CoastMomentum += direction;
    }


    
    public void Scroll( Vector3 startPositionScreen, Vector3 instantVectorScreenSpace, Plane ground, bool trackMomentum ) {
        float dOrigin, dNow;
        var originRay = Camera.main.ScreenPointToRay( Vector3.zero );
        if( ground.Raycast( originRay, out dOrigin )) {
            var scrollToRay = Camera.main.ScreenPointToRay( instantVectorScreenSpace );
            if( ground.Raycast( scrollToRay, out dNow )) {
                var worldOrigin = originRay.GetPoint( dOrigin );
                var worldNow = scrollToRay.GetPoint( dNow );
                var newPosition = (worldOrigin-worldNow) + startPositionScreen;
                this.Position = ConstrainToScrollExtents( newPosition );
            }
        }

        if( trackMomentum ) {
            var m = this.Position - LastScrollPosition;
            CoastMomentum = m*.9f + CoastMomentum*.1f;
        }
        LastScrollPosition = this.Position;
    }


    protected override void Update() {
        if( UnityCamera == null ) {
            // Got no camera. Can't do much. So no-op.
        } else {
            if( MomentumEnabled ) {
                this.Position += CoastMomentum;
                this.Position = ConstrainToScrollExtents( this.Position );
                CoastMomentum += RepelEdges( this.Position );
                CoastMomentum *= 0.9f;
            }

            if( LastPosition != Position && Moved != null ) {
                Moved();
            }

            LastPosition = Position;

            base.Update();
        }
    }


    void Awake() {
        this.gameObject.name = "__ZinCam";
        ScrollXMaxHard = 2f;
        ScrollXMinHard = -ScrollXMaxHard;
        ScrollYMaxHard = .5f;
        ScrollYMinHard = -2.5f;
        const float SOFT_MARGIN = 0.5f;
        ScrollXMaxSoft = ScrollXMaxHard - SOFT_MARGIN;
        ScrollXMinSoft = ScrollXMinHard + SOFT_MARGIN;
        ScrollYMaxSoft = ScrollYMaxHard - SOFT_MARGIN;
        ScrollYMinSoft = ScrollYMinHard + SOFT_MARGIN;

        RefreshAfterReload();
    }
    /// <summary>
    /// Call this after a scene change or reload to re-grab basic Unity stuff (like the Main Camera!)
    /// </summary>
    public void RefreshAfterReload() {
        this.enabled = true;
        UnityCamera = Camera.main;
    }
	

    Vector3 ConstrainToScrollExtents( Vector3 point ) {
        return new Vector3(
            Mathf.Max( ScrollXMinHard, Mathf.Min( ScrollXMaxHard, point.x )),
            Mathf.Max( ScrollYMinHard, Mathf.Min( ScrollYMaxHard, point.y )),
            point.z );
    }
    Vector3 RepelEdges( Vector3 point ) {
        float xMinSoft, xMaxSoft, yMinSoft, yMaxSoft;
        xMinSoft = ScrollXMinHard + .1f;
        xMaxSoft = ScrollXMaxHard - .1f;
        yMinSoft = ScrollYMinHard + .1f;
        yMaxSoft = ScrollYMaxHard - .1f;
        var rv = new Vector3(0,0,0);
        if( point.x < xMinSoft ) {
            rv += new Vector3(.04f,0,0);
        }
        if( point.x > xMaxSoft ) {
            rv += new Vector3(-.04f,0,0);
        }
        if( point.y < yMinSoft ) {
            rv += new Vector3(0,.04f,0);
        }
        if( point.y > yMaxSoft ) {
            rv += new Vector3(0,-.04f,0);
        }
        return rv;
    }


    public void DrawLine( Vector3 from, Vector3 to, Color color, int frames=10 ) {
        linesToDraw.Add( new LineSegment( from, to, color, frames ));
    }
    public void OnPostRender() {
        // Unity has a built-in shader that is useful for drawing
        // simple colored things.
        var shader = Shader.Find("Hidden/Internal-Colored");
        var lineMaterial = new Material(shader);
        lineMaterial.hideFlags = HideFlags.HideAndDontSave;
        // Turn on alpha blending
        lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        // Turn backface culling off
        lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
        // Turn off depth writes
        lineMaterial.SetInt("_ZWrite", 0);

        GL.PushMatrix();
        lineMaterial.SetPass( 0 );

        GL.Begin( GL.LINES );

        foreach( var line in linesToDraw ) {
            GL.Color( line.Color );
            GL.Vertex( line.Start );
            GL.Vertex( line.End );

            line.Frames--;
        }

        GL.End();
        GL.PopMatrix();

        linesToDraw.RemoveAll( l => l.Frames <= 0 );
    }


    List<LineSegment> linesToDraw = new List<LineSegment>();


    #region Unity Camera wrappers
    public Vector3 WorldToScreenPoint( Vector3 point ) {
        return UnityCamera.WorldToScreenPoint( point );
    }
    public Ray ScreenPointToRay( Vector3 point ) {
        return UnityCamera.ScreenPointToRay( point );
    }
    public Vector3 Position {
        get {
            return UnityCamera.transform.position;
        }
        set {
            UnityCamera.transform.position = value;
        }
    }
    #endregion



}
