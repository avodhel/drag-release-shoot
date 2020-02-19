using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject trajectoryDot;
    public float forceFactor;
    public int number;

    //private GameObject muzzlePoint;
    private Vector3 startPos;
    private Vector3 endPos;
    private Vector3 initPos;
    private Rigidbody rb;
    private Vector3 forceAtPlayer;
    private GameObject[] trajectoryDots;

    //[HideInInspector]
    //public bool drawTrajectoryControl = false;

    //private void Awake()
    //{
    //    muzzlePoint = GameObject.FindGameObjectWithTag("canonball").gameObject;
    //}

    private void Start()
    {
        initPos = gameObject.transform.position;
        rb = GetComponent<Rigidbody>();
        trajectoryDots = new GameObject[number];
    }

    private void Update()
    {
        //if (drawTrajectoryControl)
        //{
            UserInput();
        //}
    }

    private void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        { //click
            startPos = gameObject.transform.position;
            //startPos = muzzlePoint.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
                //trajectoryDots[i] = Instantiate(trajectoryDot, muzzlePoint.transform);
            }

        }
        if (Input.GetMouseButton(0))
        { //drag
            var mousePos = Input.mousePosition;
            mousePos.z = 10; // select distance = 10 units from the camera

            endPos = Camera.main.ScreenToWorldPoint(mousePos) + new Vector3(0, 0, -10);
            forceAtPlayer = endPos - startPos;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        { //leave
            rb.useGravity = true;
            rb.velocity = new Vector3(-forceAtPlayer.x * forceFactor,
                                             -forceAtPlayer.y * forceFactor,
                                             -forceAtPlayer.z * forceFactor);

            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
        }
    }

    private Vector3 calculatePosition(float elapsedTime)
    {
        return gameObject.transform.position +
               new Vector3(-forceAtPlayer.x * forceFactor, 
                           -forceAtPlayer.y * forceFactor,
                           -forceAtPlayer.z * forceFactor) * elapsedTime + 0.5f * Physics.gravity * elapsedTime * elapsedTime;
    }
}
