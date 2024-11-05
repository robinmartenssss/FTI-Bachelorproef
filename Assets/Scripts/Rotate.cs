using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    // Start is called before the first frame update
    public float amp = 0.6f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0f, 20f * Time.deltaTime, 0f, Space.Self);
        transform.position = new Vector3(transform.position.x, Mathf.Sin(Time.time) * amp + 5, transform.position.z);


    }
}
