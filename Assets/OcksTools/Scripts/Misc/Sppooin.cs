using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sppooin : MonoBehaviour 
{
    void Update()
    {
        transform.Rotate(0, 0, 180*Time.deltaTime);
    }
}
