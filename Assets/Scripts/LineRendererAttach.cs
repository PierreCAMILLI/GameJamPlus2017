using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class LineRendererAttach : MonoBehaviour {

    [SerializeField]
    LineRenderer _line;

    [SerializeField]
    Transform _attach1;
    [SerializeField]
    Transform _attach2;
	
	// Update is called once per frame
	void Update () {
        Attach();
	}

    void Attach()
    {
        if (_line != null && _attach1 != null && _attach2 != null)
        {
            Vector3[] points = new Vector3[2];
            points[0] = transform.InverseTransformPoint(_attach1.transform.position);
            points[1] = transform.InverseTransformPoint(_attach2.transform.position);
            _line.SetPositions(points);
        }
    }

    private void OnDrawGizmos()
    {
        Attach();
    }

}
