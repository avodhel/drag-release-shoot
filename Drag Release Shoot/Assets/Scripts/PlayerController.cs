using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private GameObject mousePointA;
    private GameObject mousePointB;
    private GameObject arrow;
    private GameObject circle;
    private Rigidbody rb;

    private float currentDistance;
    public float maxDistance = 3f;
    private float safeDistance;
    private float shootPower;

    private Vector3 shootDirection;

    private void Awake()
    {
        mousePointA = GameObject.FindGameObjectWithTag("pointA");
        mousePointB = GameObject.FindGameObjectWithTag("pointB");
        arrow = GameObject.FindGameObjectWithTag("arrow");
        circle = GameObject.FindGameObjectWithTag("circle");
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDrag()
    {
        currentDistance = Vector3.Distance(mousePointA.transform.position, transform.position);

        if (currentDistance <= maxDistance)
        {
            safeDistance = currentDistance;
        }
        else
        {
            safeDistance = maxDistance;
        }

        DoArrowAndCircleStuff();

        //calc power and direction
        shootPower = Mathf.Abs(safeDistance) * 13;

        Vector3 dimXY = mousePointA.transform.position - transform.position;
        float difference = dimXY.magnitude;
        mousePointB.transform.position = transform.position + ((dimXY / difference) * currentDistance * -1);
        mousePointB.transform.position = new Vector3(mousePointB.transform.position.x, mousePointB.transform.position.y, -0.5f);

        shootDirection = Vector3.Normalize(mousePointA.transform.position - transform.position);
    }

    private void OnMouseUp()
    {
        arrow.GetComponent<Renderer>().enabled = false;
        circle.GetComponent<Renderer>().enabled = false;

        Vector3 push = shootDirection * shootPower * -1;
        rb.AddForce(push, ForceMode.Impulse);
    }

    private void DoArrowAndCircleStuff()
    {
        arrow.GetComponent<Renderer>().enabled = true;
        circle.GetComponent<Renderer>().enabled = true;

        //calc position
        if (currentDistance <= maxDistance)
        {
            arrow.transform.position = new Vector3((2 * transform.position.x) - mousePointA.transform.position.x,
                                                   (2 * transform.position.y) - mousePointA.transform.position.y,
                                                    -1.5f);
        }
        else
        {
            Vector3 dimXY = mousePointA.transform.position - transform.position;
            float difference = dimXY.magnitude;
            arrow.transform.position = transform.position + ((dimXY / difference) * maxDistance * -1);
            arrow.transform.position = new Vector3(arrow.transform.position.x, arrow.transform.position.y, -1.5f);
        }

        circle.transform.position = transform.position + new Vector3(0, 0, 0.05f);
        Vector3 dir = mousePointA.transform.position - transform.position;
        float rot;

        if (Vector3.Angle(dir, transform.forward) > 90)
        {
            rot = Vector3.Angle(dir, transform.right);
        }
        else
        {
            rot = Vector3.Angle(dir, transform.right) * -1;
        }

        arrow.transform.eulerAngles = new Vector3(0, 0, rot);

        float scaleX = Mathf.Log(1 + safeDistance / 2, 2) * 2.2f;
        float scaleY = Mathf.Log(1 + safeDistance / 2, 2) * 2.2f;

        arrow.transform.localScale = new Vector3(1 + scaleX, 1 + scaleY, 0.001f);
        circle.transform.localScale = new Vector3(1 + scaleX, 1 + scaleY, 0.001f);
    }
}
