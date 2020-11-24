using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform bot;
    Vector3 initialPos;
    void Start()
    {
        initialPos = gameObject.transform.position;
    }
    void Update()
    {
        if(bot.position.y > transform.position.y)
        {
            Vector3 newPos = new Vector3(transform.position.x, bot.position.y, transform.position.z);
            transform.position = newPos;
        }
    }
    public void resetCamera()
    {
        transform.position = initialPos;
    }
}
