using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static App.RandomTools;

public class Ball : MonoBehaviour
{
    [Header("Speed settings")]
    [SerializeField]
    [Range(1, 10)]
    private int _startSpeed = 10;
    [SerializeField]
    [Range(0, 1)]
    private float _speedIncrement = 0.5f;
    [SerializeField]
    [Range(1, 100)]
    private int _maxSpeed = 20;
    [SerializeField]
    [Range(0, 1)]
    private float _baseAngularSpeed = 5;
    private float _currentSpeed;

    private Vector2 _startPosition;

    private Rigidbody2D _rb;

    [Header("Appearance")]
    [SerializeField]
    private Color _maxSpeedColor = Color.black;
    private SpriteRenderer _renderer;
    private TrailRenderer _trailRenderer;
    [SerializeField]
    private Rotate _trailsRotate;
    private Gradient _startGradient;
    private AudioSource _hitSound;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _startPosition = _rb.position;
        _renderer = GetComponent<SpriteRenderer>();
        _trailRenderer = GetComponentInChildren<TrailRenderer>();
        _startGradient = _trailRenderer.colorGradient;
        ResetState();
        _hitSound = GetComponent<AudioSource>();
    }

    public void ResetState()
    {
        _trailRenderer.Clear();
        _rb.position = _startPosition;

        _rb.velocity = new Vector2(Choose(1, -1), Choose(1, -1, 0));
        _rb.velocity = _rb.velocity.normalized * _startSpeed;
        _currentSpeed = _startSpeed;
        UpdateAngularVelocity();
        UpdateColor(Color.white);
    }

    private void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag != "Paddles") {
            return;
        }
        bool maxSpeedReached = false;
        _currentSpeed += _speedIncrement;
        if (_currentSpeed >= _maxSpeed) {
            maxSpeedReached = true;
            _currentSpeed = _maxSpeed;
        }

        float diff = this.transform.position.y - other.transform.position.y;
        Vector2 dir = _rb.velocity.x > 0 ? Vector2.right : Vector2.left;
        _rb.velocity = Vector2.Lerp(Vector2.down + dir, Vector2.up + dir, (diff + 1) / 2).normalized * _currentSpeed;
        UpdateAngularVelocity();

        // Update ball color
        Paddle paddle = other.gameObject.GetComponent<Paddle>();
        if (paddle) {
            if (maxSpeedReached) {
                UpdateColor(_maxSpeedColor);
            } else {
                UpdateColor(paddle._color);
            }
        }
    }

    private void UpdateAngularVelocity()
    {
        _trailsRotate._rotationSpeed = Choose<float>(_baseAngularSpeed, -_baseAngularSpeed) * _currentSpeed;
    }

    private void UpdateColor(Color color)
    {
        _renderer.color = color;

        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(color, 0.0f),
                new GradientColorKey(color, 1.0f)
            },
            _startGradient.alphaKeys
        );
        _trailRenderer.colorGradient = gradient;
    }

    public float GetNormalizedSpeed()
    {
        return _currentSpeed / _maxSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        _hitSound.Play();
    }
}
