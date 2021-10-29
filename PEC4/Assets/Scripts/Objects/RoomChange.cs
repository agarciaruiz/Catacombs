using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomChange : MonoBehaviour
{
    public Vector2 camChangeMin;
    public Vector2 camChangeMax;
    public Vector3 playerChange;

    private CameraSystem cam;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            cam.minPos += camChangeMin;
            cam.maxPos += camChangeMax;
            collision.transform.position += playerChange;
        }
    }
}
