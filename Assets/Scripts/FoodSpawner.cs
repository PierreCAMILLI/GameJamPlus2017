using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FoodSpawner : MonoBehaviour {

    private static FoodSpawner[] _spawners;
    public static IList<FoodSpawner> Spawners { get { return _spawners; } }

    [System.Serializable]
    public struct Rect2D
    {
        public float width, height;
    }

    [Tooltip("Liste des instances de fruits/légumes.")]
    [SerializeField]
    Food[] _foodInstances;

    [Tooltip("Zone d'apparition des fruits/légumes.")]
    [SerializeField]
    Rect2D _spawnArea;
    public Rect2D SpawnArea { get { return _spawnArea; } }

    [SerializeField]
    Food.Player _player;

    private Food _lastFoodSpawned = null;

    //[Tooltip("Moyenne d'intervalle d'apparition entre deux fruits/légumes.")]
    //[SerializeField]
    //float _foodPerSecond;
    //public float FoodPerSecond { get { return _foodPerSecond; } }
    
    [SerializeField]
    Transform _floorTransform;

    private bool _readyToSpawn = true;
    public bool ReadyToSpawn { get { return _readyToSpawn; } set { _readyToSpawn = value; } }

	// Use this for initialization
	void Awake () {
        if(_spawners == null)
            _spawners = new FoodSpawner[System.Enum.GetNames(typeof(Food.Player)).Length];
        _spawners[(int)_player] = this;
	}
	
	// Update is called once per frame
	void Update () {
        //UpdateSpawn();
	}

    void UpdateSpawn()
    {
        // Check si un fruit apparait
        //while(Random.Range(0f,1f) <= _foodPerSecond * Time.deltaTime)
        if(_lastFoodSpawned == null || _lastFoodSpawned.UsePhysicsGravity || _lastFoodSpawned.Swapped)
        {
            Spawn();
        }
    }

    public void Spawn()
    {
        Food pickedFood = _foodInstances[Random.Range(0, _foodInstances.Length)];
        Food instance = Instantiate(pickedFood, transform);

        FoodController controller = instance.GetComponent<FoodController>();
        if (controller == null)
            controller = instance.gameObject.AddComponent<FoodController>();

        // Fait apparaitre l'objet sur une position aléatoire
        Vector2 pos = new Vector2(
            Random.Range(-_spawnArea.width * 0.5f, _spawnArea.width * 0.5f),
            Random.Range(-_spawnArea.height * 0.5f, _spawnArea.height * 0.5f)
            );
        pos.x = pos.x - Mathf.Repeat(pos.x, FoodController._moveStep);

        // Détermine si la pièce appartient au premier ou deuxième joueur
        instance.player = _player;
        controller.PlayerNumber = (byte)instance.player;

        // Set les différentes informations du fruit ou légume
        instance.transform.position = transform.TransformPoint(pos);
        instance.UsePhysicsGravity = false;
        instance.floorTransform = Balance.Instance.transform;
        instance.spawner = this;

        _lastFoodSpawned = instance;
        _readyToSpawn = false;
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
