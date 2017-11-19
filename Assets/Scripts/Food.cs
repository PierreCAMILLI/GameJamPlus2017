using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Food : MonoBehaviour {

    public enum Player
    {
        Player1 = 0,
        Player2 = 1
    }

    public enum FoodType
    {
        Fruit,
        Vegetable,
        Both
    }

    const string _balanceTag = "Balance";
    const string _foodTag = "Food";

    SpriteRenderer _renderer;
    Collider2D _collider2D;
    Rigidbody2D _rigidbody2D;

    public Player player;

    [SerializeField]
    private FoodType _foodType;
    public FoodType Type { get { return _foodType; } }

    [HideInInspector]
    public FoodSpawner spawner;

    public Transform floorTransform;

    [SerializeField]
    bool _swapped;
    public bool Swapped { get { return _swapped; } }

    [SerializeField]
    bool _usePhysicsGravity;
    public bool UsePhysicsGravity { get { return _usePhysicsGravity; } set { _usePhysicsGravity = value; } }
    [SerializeField]
    Vector2 _gravity;
    public Vector2 Gravity { get { return _gravity; } set { _gravity = value; } }
    [SerializeField]
    float _speed = 2f;
    public float Speed { get { return _speed; } }

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _collider2D = GetComponent<Collider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        ClampPosition();
		if(!UsePhysicsGravity)
		transform.localRotation = Quaternion.identity;
	}

    /// <summary>
    /// Clamp la position des fruits et légumes
    /// </summary>
    public void ClampPosition()
    {
        if (UsePhysicsGravity || _swapped)
            return;
        Vector2 pos = transform.localPosition;
        float halfWidth = (spawner.SpawnArea.width * 0.5f) - GameManager.Instance.OffMiddleZoneWidth;
        pos.x = Mathf.Clamp(pos.x, -halfWidth, halfWidth);
        transform.localPosition = pos;
    }

    public void MoveHorizontally(float move)
    {
        transform.Translate(new Vector2(move, 0f));
    }

    public void Swap()
    {
        Vector3 pos = transform.localPosition;
        // pos.x *= -1;
        switch (player)
        {
            case Player.Player1:
                transform.parent = FoodSpawner.Spawners[(int)Player.Player2].transform;
                break;
            case Player.Player2:
                transform.parent = FoodSpawner.Spawners[(int)Player.Player1].transform;
                break;
        }
        transform.localPosition = pos;
        _swapped = true;
		Collider2D[] colliders = new Collider2D[1];
		int nbOverlap = _collider2D.OverlapCollider (GameManager.Instance.colisionFilter, colliders);
		if (nbOverlap > 0) {
			spawner.ReadyToSpawn = true;

			GameObject go = colliders [0].gameObject;
			Food food = go.GetComponent<Food> ();

			if (food != null && !food.UsePhysicsGravity) {
				food.spawner.ReadyToSpawn = true;
			}

			Destroy(go);
			Destroy (gameObject);
		}
    }

    #region Gravity
    private void FixedUpdate()
    {
        UpdateGravity();
    }

    void UpdateGravity()
    {
        if (_usePhysicsGravity)
        {
            _rigidbody2D.gravityScale = 1.0f;
        }
        else
        {
            _rigidbody2D.gravityScale = 0f;
            transform.position += -transform.parent.up * _speed * GameManager.Instance.FallSpeedMultiplier * GameManager.Instance.PlayerStats[(int) player].FallSpeed * Time.deltaTime;
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (_usePhysicsGravity)
            return;
        if(     collision.gameObject.tag == _balanceTag
            ||  (collision.gameObject.tag == _foodTag && collision.gameObject.GetComponent<Food>().UsePhysicsGravity))
        {
            spawner.ReadyToSpawn = true;
            _usePhysicsGravity = true;
            transform.parent = null;

            // Check si le bloc est posé du bon côté de la balance
            if(!Balance.Instance.IsInGoodSide(Type, transform.position))
            {
                --(GameManager.Instance.PlayerStats[(int)player].FallenObjects);
                // MalusManager.Instance.Give<AccelerateFallMalus>((byte)player);
            }
            else
            {
                // MalusManager.Instance.RemoveMalus((byte)player);
            }
        }
    }

    private void OnBecameInvisible()
    {
        if(GameManager.Instance != null)
            --(GameManager.Instance.PlayerStats[(int) player].FallenObjects);
		if (!UsePhysicsGravity)
			spawner.ReadyToSpawn = true;
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position - 
            (UsePhysicsGravity ? -Physics2D.gravity.normalized : (Vector2)transform.parent.up));
    }
}
