using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanonBall : MonoBehaviour
{
    public Transform muzzlePoint;
    public GameObject ball;

    private void Update()
    {
        UserInput();
    }

    private void UserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject instatiatedBall = Instantiate(ball, muzzlePoint.position, Quaternion.identity);
            //instatiatedBall.GetComponent<Ball>().drawTrajectoryControl = true;
        }
    }
}
