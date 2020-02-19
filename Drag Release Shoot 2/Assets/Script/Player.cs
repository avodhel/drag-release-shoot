using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    public Vector3 initPos;
    private Rigidbody rigidbody;
    private Vector3 forceAtPlayer;
    public float forceFactor;

    public GameObject trajectoryDot;
    private GameObject[] trajectoryDots;
    public int number;



    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        trajectoryDots = new GameObject[number];
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        { //click
            startPos = gameObject.transform.position;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i] = Instantiate(trajectoryDot, gameObject.transform);
            }

        }
        if (Input.GetMouseButton(0))
        { //drag
            var mousePos = Input.mousePosition;
            mousePos.z = 10; // select distance = 10 units from the camera

            endPos = Camera.main.ScreenToWorldPoint(mousePos) + new Vector3(0, 0, -10);
            //gameObject.transform.position = endPos;
            forceAtPlayer = endPos - startPos;
            for (int i = 0; i < number; i++)
            {
                trajectoryDots[i].transform.position = calculatePosition(i * 0.1f);
            }
        }
        if (Input.GetMouseButtonUp(0))
        { //leave
            rigidbody.useGravity = true;
            rigidbody.velocity = new Vector3(-forceAtPlayer.x * forceFactor, 
                                             -forceAtPlayer.y * forceFactor, 
                                             -forceAtPlayer.z * forceFactor);

            for (int i = 0; i < number; i++)
            {
                Destroy(trajectoryDots[i]);
            }
        }
        if (Input.GetKey(KeyCode.Space))
        {
            rigidbody.useGravity = false;
            rigidbody.velocity = Vector3.zero;
            gameObject.transform.position = initPos;

        }
    }

    private Vector3 calculatePosition(float elapsedTime)
    {
        //return new Vector3(endPos.x, endPos.y, endPos.z) + //X0
        return gameObject.transform.position +
               new Vector3(-forceAtPlayer.x * forceFactor, 
                           -forceAtPlayer.y * forceFactor,
                           -forceAtPlayer.z * forceFactor) * elapsedTime + 0.5f * Physics.gravity * elapsedTime * elapsedTime;
    }
}
