using UnityEngine;

public class FallingBaller : MonoBehaviour
{
    float alive = 0f;
    float spf = 0f;
    float rot = 0f;
    float side = 0f;
    // Update is called once per frame
    private void Start()
    {
        side = Random.Range(-1f, 1f);
        rot = Random.Range(-45f, 45f);
    }
    void Update()
    {
        spf += Time.deltaTime * 3f;
        transform.position += new Vector3(side * Time.deltaTime, -spf * Time.deltaTime, 0);
        transform.Rotate(0, 0, rot * Time.deltaTime);
        if (alive >= 10) Destroy(gameObject);
    }
}
