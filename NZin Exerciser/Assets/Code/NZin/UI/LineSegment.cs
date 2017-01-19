using UnityEngine;

public class LineSegment {

    public Vector3 Start    { get; private set; }
    public Vector3 End      { get; private set; }
    public Color Color      { get; private set; }
    public int Frames       { get; set; }


    public LineSegment( Vector3 start, Vector3 end, Color color, int frames ) {
        Start = start;
        End = end;
        Color = color;
        Frames = frames;
    }
}
