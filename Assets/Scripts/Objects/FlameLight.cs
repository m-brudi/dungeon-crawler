using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;

public class FlameLight : MonoBehaviour
{
    Light light2d;

    // Start is called before the first frame update
    void Start()
    {
        light2d = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
