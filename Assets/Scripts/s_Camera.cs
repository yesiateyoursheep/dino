using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_Camera : MonoBehaviour
{
    private Camera cam;
    public float defaultAspect;
    // Start is called before the first frame update
    void Start()
    {
        cam = gameObject.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        cam.fieldOfView = (cam.pixelRect.height/cam.pixelRect.width+1)/2*defaultAspect;
    }
}
