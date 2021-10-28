using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    [Range(0, 10)]
    private float _speed = 1;

    [SerializeField]
    public Color _color;

    [SerializeField]
    private bool _isFacingLeft = false;

    public enum Direction {
        UP,
        DOWN,
        NONE
    }

    private Direction _currentDirection = Direction.NONE;
    private Rigidbody2D _rb;
    private SpriteRenderer _sr;
    private float GIZMO_LINE_LENGTH = 2;


    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
        _sr.color = _color;
    }

    private void FixedUpdate() {
        switch (_currentDirection)
        {
            case Direction.UP:
                _rb.velocity = _speed * Vector2.up;
                break;
            case Direction.DOWN:
                _rb.velocity = _speed * Vector2.down;
                break;
            default:
                _rb.velocity = Vector2.zero;
                break;
        }
    }

    public void Move(Direction dir)
    {
        _currentDirection = dir;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = _color;
        Gizmos.DrawLine(transform.position, transform.position + GIZMO_LINE_LENGTH * GetFacingDirection());
    }

    public Vector3 GetFacingDirection() {
        return transform.right * (_isFacingLeft ? -1 : 1);
    }

    public Vector2 GetPosition()
    {
        return _rb.position;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        _sr.color = Color.white;
    }

    private void OnCollisionExit2D(Collision2D other) {
        _sr.color = _color;
    }
}
