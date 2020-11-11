using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // earth's axis tilt
        this.gameObject.transform.Rotate(-23, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // rotate anticlockwise
        this.gameObject.transform.Rotate(0, -20 * Time.deltaTime, 0);
    }
}
