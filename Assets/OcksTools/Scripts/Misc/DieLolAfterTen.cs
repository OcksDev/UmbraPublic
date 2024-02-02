using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieLolAfterTen : MonoBehaviour
{
    public float baller = 10f;
    void FixedUpdate()
    {
        baller -= Time.deltaTime;
        if(baller <= 0)
        {
            Destroy(gameObject);   
        }
    }
}
