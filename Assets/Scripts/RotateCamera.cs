using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
  

    public void RotateLeft()
    {
        transform.RotateAround(transform.position, Vector3.up, 10f);
    }

    public void RotateRight()
    {
        transform.RotateAround(transform.position, Vector3.up, -10f);
    }

}
