using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCameraMover : MonoBehaviour {

    [Header("Settings")]
    public float speed;

    [Header("Technical")]
    public float moveDist;

    private Vector3 target;
    private Vector3 mainPosition;
    private Vector3 levelSelectPosition;

    private void Awake()
    {
        mainPosition = transform.position;
        target = mainPosition;
        levelSelectPosition = mainPosition + (new Vector3(moveDist, 0, 0));
    }

    private void Update()
    {
        if (target != transform.position)
        {
            transform.position = Vector3.Lerp(transform.position, target, Time.deltaTime * speed);
        }
    }

    public void MoveCamera(bool toLevels)
    {
        if (toLevels)
            target = levelSelectPosition;
        else
            target = mainPosition;
    }
}
