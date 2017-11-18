using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Food : MonoBehaviour {

    const string _balanceTag = "Balance";
    const string _foodTag = "Food";

    SpriteRenderer _renderer;
    Collider2D _collider2D;
    Rigidbody2D _rigidbody2D;

    public Transform floorTransform;

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

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void MoveHorizontally(float move)
    {
        transform.Translate(new Vector2(move, 0f));
    }

    public void Swap()
    {
        Vector3 pos = transform.localPosition;
        pos.x *= -1;
        transform.localPosition = pos;
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
            transform.position += -transform.parent.up * _speed * Time.deltaTime;
            //_rigidbody2D.AddForce(_gravity * _rigidbody2D.mass * Physics2D.gravity.magnitude);
        }
    }
    #endregion

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (UsePhysicsGravity)
            return;
        if(     collision.gameObject.tag == _balanceTag
            ||  (collision.gameObject.tag == _foodTag && collision.gameObject.GetComponent<Food>().UsePhysicsGravity))
        {
            _usePhysicsGravity = true;
            transform.parent = null;
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position - 
            (UsePhysicsGravity ? -Physics2D.gravity.normalized : (Vector2)transform.parent.up));
    }
}
