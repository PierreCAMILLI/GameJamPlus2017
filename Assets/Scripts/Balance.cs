using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balance : SingletonBehaviour<Balance> {

	public Vector2 Up { get { return transform.up; } }

    public Vector2 TransformPoint(Vector2 point)
    {
        return transform.TransformPoint(point);
    }

    public Vector2 InverseTransformPoint(Vector2 point)
    {
        return transform.InverseTransformPoint(point);
    }
}
