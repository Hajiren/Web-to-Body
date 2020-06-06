using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class HandController : MonoBehaviour
{   
    [SerializeField]
    private GameObject left_hand_rotation;
    [SerializeField]
    private GameObject left_hand_transition;
    [SerializeField]
    private GameObject right_hand_rotation;
    [SerializeField]
    private GameObject right_hand_transition;

    private Vector2 preMousePos;
    private Vector2 MousePos;
    public float speed = 0.1f;
    public float tranlationSpeed = 0.2f;
    private Vector2 offset;
    private bool movingLeft = false;
    private bool movingRight = false;

    void Start()
    {
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            movingLeft = !movingLeft;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            movingRight = !movingRight;
        }

        if (Input.GetMouseButtonDown(0))
        {
            preMousePos = Input.mousePosition;
            MousePos = preMousePos;
        }
        if (Input.GetMouseButton(0))
        {
            MousePos = Input.mousePosition;
            offset = (MousePos - preMousePos)*speed;
            if (!movingLeft)
            {
                float tempOffset = offset.x;
                left_hand_rotation.transform.Rotate(Vector3.right, -tempOffset, Space.Self);
            }
            else
            {
                offset *= tranlationSpeed;
                left_hand_transition.transform.Translate(new Vector3(offset.x, 0, offset.y));
            }
            preMousePos = MousePos;
        }

        if (Input.GetMouseButtonDown(1))
        {
            preMousePos = Input.mousePosition;
            MousePos = preMousePos;
        }
        if (Input.GetMouseButton(1))
        {
            MousePos = Input.mousePosition;
            offset = (MousePos - preMousePos)*speed;
            if (!movingRight)
            {
                float tempOffset = offset.x;
                right_hand_rotation.transform.Rotate(Vector3.right, -tempOffset, Space.Self);
            }
            else
            {
                offset *= tranlationSpeed;
                right_hand_transition.transform.Translate(new Vector3(offset.y,0, -offset.x));
            }
            preMousePos = MousePos;
        }
    }

}
