using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform m_target;
    public float m_speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        Vector3 targetPos = m_target.position;
        targetPos.z = transform.position.z;
        transform.position = Vector3.Lerp(transform.position, targetPos, m_speed);
    }
}
