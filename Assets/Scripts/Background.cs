using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private CameraController cam;

    // Update is called once per frame
    void Update()
    {
        if (cam.Freeze) return;
        
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
