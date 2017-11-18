using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    [System.Serializable]
    struct Rect2D
    {
        public float width, height;
    }

    [Tooltip("Liste des instances de fruits/légumes.")]
    [SerializeField]
    Food[] _foodInstances;

    [Tooltip("Zone d'apparition des fruits/légumes.")]
    [SerializeField]
    Rect2D _spawnArea;

    [Tooltip("Moyenne d'intervalle d'apparition entre deux fruits/légumes.")]
    [SerializeField]
    float _foodPerSecond;
    public float FoodPerSecond { get { return _foodPerSecond; } }
    
    [SerializeField]
    Transform _floorTransform;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        UpdateSpawn();
	}

    void UpdateSpawn()
    {
        // Check si un fruit apparait
        while(Random.Range(0f,1f) <= _foodPerSecond * Time.deltaTime)
        {
            Food pickedFood = _foodInstances[Random.Range(0, _foodInstances.Length)];
            Food instance = Instantiate(pickedFood, transform);

            // Fait apparaitre l'objet sur une position aléatoire
            Vector2 pos = new Vector2(
                Random.Range(- _spawnArea.width * 0.5f, _spawnArea.width * 0.5f),
                Random.Range(-_spawnArea.height * 0.5f, _spawnArea.height * 0.5f)
                );
            instance.transform.position = transform.TransformPoint(pos);
            instance.UsePhysicsGravity = false;
            instance.Gravity = -_floorTransform.up;
            instance.floorTransform = transform;
        }
    }

    struct _Bounds2D
    {
        public Vector2 _topLeft, _topRight, _bottomLeft, _bottomRight;

        public _Bounds2D(Rect2D rect)
        {
            _topLeft = new Vector2(-rect.width * 0.5f, rect.height * 0.5f);
            _topRight = new Vector2(rect.width * 0.5f, rect.height * 0.5f);
            _bottomLeft = new Vector2(-rect.width * 0.5f, -rect.height * 0.5f);
            _bottomRight = new Vector2(rect.width * 0.5f, -rect.height * 0.5f);
        }

        public static _Bounds2D operator +(_Bounds2D bounds, Vector2 translation)
        {
            _Bounds2D _bounds = bounds;
            _bounds._bottomLeft += translation;
            _bounds._bottomRight += translation;
            _bounds._topLeft += translation;
            _bounds._topRight += translation;
            return _bounds;
        }

        public _Bounds2D Transform(Transform transform)
        {
            _Bounds2D bounds = new _Bounds2D();
            bounds._bottomLeft = transform.TransformPoint(_bottomLeft);
            bounds._bottomRight = transform.TransformPoint(_bottomRight);
            bounds._topLeft = transform.TransformPoint(_topLeft);
            bounds._topRight = transform.TransformPoint(_topRight);
            return bounds;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        _Bounds2D bounds = new _Bounds2D(_spawnArea).Transform(transform);
        Gizmos.DrawLine(bounds._topLeft, bounds._topRight);
        Gizmos.DrawLine(bounds._topRight, bounds._bottomRight);
        Gizmos.DrawLine(bounds._bottomRight, bounds._bottomLeft);
        Gizmos.DrawLine(bounds._bottomLeft, bounds._topLeft);
    }
}
