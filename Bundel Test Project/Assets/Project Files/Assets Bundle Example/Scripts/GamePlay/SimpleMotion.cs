using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMotion : MonoBehaviour
{
    private enum Motion_Mode
    { 
        movement,
        Rotation
    }


    [SerializeField] Motion_Mode MotionMode = Motion_Mode.movement;
    [SerializeField, Range(0.5f, 5f)] private float Amplitude = 1; //sin wave amplitude
#pragma warning disable 649
    [SerializeField] private Vector3 Direction; //movement direction
#pragma warning restore 649
    private Vector3 initialpos;



    private void Start()
    {
        initialpos = transform.position; 
    }
    void Update()
    {
        Vector3 dir = Direction * Amplitude;
        switch (MotionMode)
        {
            case Motion_Mode.movement:
                movement(dir);
                break;
            case Motion_Mode.Rotation:
                rotate(dir);
                break;
        }

    }


    private void movement(Vector3 direction)
    {
        transform.position = initialpos + direction * Mathf.Sin(Time.time);
    }

    private void rotate(Vector3 direction)
    {
        transform.Rotate(direction);
    }

}
