using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HitINdie : MonoBehaviour
{
    public TextMeshProUGUI texty;
    float side;
    float upval;
    float rot;
    public void Start()
    {
        upval = 0.6f;
        side = Random.Range(-1f, 1f);
        rot = Random.Range(-1f, 1f);
    }
    float life = 0;
    public void Update()
    {
        transform.position += new Vector3((side / 3) * Time.deltaTime, upval * Time.deltaTime, 0);
        transform.Rotate(new Vector3(0, 0, 45 * Time.deltaTime * rot));
        life += Time.deltaTime;
        transform.localScale -= transform.localScale * 0.5f * Time.deltaTime;

        if (life >= 3f)
        {
            Destroy(gameObject);
        }
    }
}
