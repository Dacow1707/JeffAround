using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitAround : MonoBehaviour
{

    public GameObject cube;
    Transform center;

    public Vector3 desiredPosition;
    public float radius = 2.0f;
    public float anglePerSecond = Mathf.PI; 
    public float distance = 2f;



    public float currentAngle
    {
        get; set;
    } = -1;
   

    void Start()
    {
        center = GameObject.Find("Player").transform;
        transform.position = (transform.position - center.position).normalized * radius + center.position;
        radius = 2.0f;

    }

    void FixedUpdate()
    {
        if (currentAngle == -1) return;
        currentAngle += Mathf.PI * Time.deltaTime;
        Vector3 newPos = new Vector3();
        newPos.x = center.transform.position.x + Mathf.Cos(currentAngle) * radius;
        newPos.y = center.transform.position.y + Mathf.Sin(currentAngle) * radius;
        transform.position = newPos;
    }
}