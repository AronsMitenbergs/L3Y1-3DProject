using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayAndNight : MonoBehaviour
{
    Vector3 rot=Vector3.zero;
    float degpersec=1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        rot.x=degpersec*Time.deltaTime;
        transform.Rotate(rot,Space.World);
        
    }
}
