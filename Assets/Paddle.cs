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

    private float GIZMO_LINE_LENGTH = 2;


    // Start is called before the first frame update
    void Start()
    {
        _rb = this.GetComponent<Rigidbody2D>();
        GetComponent<SpriteRenderer>().color = _color;
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

    void OnDrawGizmosSelected()
    {
        // Draws a blue line from this transform to the target
        Gizmos.color = _color;
        Gizmos.DrawLine(transform.position, transform.position + GIZMO_LINE_LENGTH * GetFacingDirection());
    }

    public Vector3 GetFacingDirection() {
        return transform.right * (_isFacingLeft ? -1 : 1);
    }
}
