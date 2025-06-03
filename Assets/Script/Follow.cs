using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Follow : MonoBehaviour
{
    public Transform target;
    public Vector3 offset;
    public float smoothSpeed;


    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
