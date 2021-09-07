using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOverTime : MonoBehaviour
{
    [SerializeField] private Vector3 direction;
    [SerializeField] private float wiggleSpeed;
    [SerializeField] private float wiggleDistance;
    private float offset;

    void Start()
    {
        offset = Random.Range(0, 5.0f);
        
        direction.Normalize();
    }

    void Update()
    {
        transform.localPosition = direction * wiggleDistance * Mathf.Sin(wiggleSpeed * Time.time);
    }
}
