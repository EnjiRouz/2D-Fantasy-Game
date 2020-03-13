using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    /// настройки размеров камеры (используется для настройки 
    /// сцен разных размеров, чтобы не выходить за рамки карты)
    public float xMax=32;
    public float xMin=-32; 
    public float yMax = 16;
    public float yMin=-16; 

    /// настройки скорости слежения камеры и объекта слежения
    public GameObject objectToFollow;
    public float speed = 2.0f;

    /// слежка за объектом в пределах разрешённой настройками области
    void Update()
    {
        float interpolation = speed * Time.deltaTime;
        Vector3 position = transform.position;

        position.y = Mathf.Clamp(Mathf.Lerp(transform.position.y,
            objectToFollow.transform.position.y, interpolation), yMin, yMax);
        position.x = Mathf.Clamp(Mathf.Lerp(transform.position.x, 
            objectToFollow.transform.position.x, interpolation), xMin, xMax);

        transform.position = position;
    }
}
