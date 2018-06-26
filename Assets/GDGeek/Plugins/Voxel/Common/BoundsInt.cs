using GDGeek;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_2017_2_OR_NEWER
public class BoundsInt{

    Bounds _bounds;
    public BoundsInt() {

    }
    public BoundsInt(int cx, int cy, int cz, int sx, int sy, int sz)
    {
        _bounds = new Bounds(new Vector3(cx,cy,cz), new Vector3(sx, sy,sz));
    }
    public BoundsInt(Vector3Int center, Vector3Int size)
    {
        _bounds = new Bounds(new Vector3(center.x, center.y, center.z ), new Vector3(size.x, size.y, size.z));
    }
    public Vector3Int size
    {
        get {
            return new Vector3Int(_bounds.size);
        }
    }
    public Vector3Int min
    {
        get
        {
            return new Vector3Int(_bounds.min);
        }
    }
    public Vector3Int max
    {
        get
        {
            return new Vector3Int(_bounds.max);
        }
    }
}
#endif