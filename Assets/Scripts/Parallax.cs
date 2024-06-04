using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera cam;
    public Transform followTarget;
    private float startingZ;
    private Vector2 startingPos;
    private Vector2 camMoveSinceStart => (Vector2)cam.transform.position - startingPos;
    private float zDistanceFromTarget => transform.position.z - followTarget.position.z;

    private float clippingPlane =>
        (cam.transform.position.z + (zDistanceFromTarget > 0 ? cam.farClipPlane : cam.nearClipPlane));

    private float parallaxFactor => Mathf.Abs(zDistanceFromTarget) / clippingPlane;

    private void Start()
    {
        startingZ = transform.position.z;
        startingPos = transform.position;
    }

    private void Update()
    {
        Vector2 newPos = startingPos + camMoveSinceStart * parallaxFactor;
        transform.position = new Vector3(newPos.x, newPos.y, startingZ);
    }
}