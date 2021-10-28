using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    [SerializeField]
    public float _rotationSpeed = 0;

    private void FixedUpdate() {
        this.transform.Rotate(0, 0, _rotationSpeed);
    }
}
