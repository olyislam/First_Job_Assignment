using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{

    [SerializeField, Range(0.5f, 5f)] private float RunSpeed = 0.5f;
#pragma warning disable 649
    [SerializeField, Header("Input Devices Setup")] private InputDevices input; //controller input devices
#pragma warning restore 649


    private Rigidbody Pbody;
    private Vector3 initialpos;

    // Start is called before the first frame update
    void Start()
    {
        Pbody = GetComponent<Rigidbody>();
        initialpos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move(input.Direction);
    }

    //change player velocity from direction
    private void move(Vector3 direction)
    {
        Pbody.velocity = direction * RunSpeed;
    }


    private void OnCollisionEnter(Collision col)
    {
        switch (col.gameObject.tag)
        {
            case "Finish":
                transform.position = initialpos;
                GameObject.FindObjectOfType<PlayerScore>().AddScore(1);
                break;
            case "Enemy":
                transform.position = initialpos;
                GameObject.FindObjectOfType<PlayerScore>().AddScore(-1);
                break;
        }
    }



    [System.Serializable]
    private class InputDevices
    {
        private enum Input_Devides
        {
            TouchJoystic,
            Keyboard
        }
        [SerializeField, Header("Select Input Devices")] Input_Devides _InputDevices = Input_Devides.TouchJoystic;

#pragma warning disable 649
        // touch input device
        [SerializeField, Header("Touch Joysric")] FixedJoystick Touchjoystic;

        [Header("keyboard Input")]
        [SerializeField] private KeyCode UpKey;
        [SerializeField] private KeyCode DownKey;
        [SerializeField] private KeyCode LeftKey;
        [SerializeField] private KeyCode RightKey;
#pragma warning restore 649
        
       

        /// <summary>
        /// return direction W.R.To input 
        /// </summary>
        public Vector3 Direction => new Vector3(Horizontal, 0, Vertical);

        /// <summary>
        ///  return vertical value W.R.To input 
        /// </summary>
        public float Vertical
        {
            get
            {

                switch (_InputDevices)
                {
                    case Input_Devides.Keyboard:
                        if (Input.GetKey(UpKey))
                            return 1f;
                        else if (Input.GetKey(DownKey))
                            return -1f;
                        else
                            return 0;

                    case Input_Devides.TouchJoystic:
                        return Touchjoystic.Vertical;
                    default:
                        return 0f;

                }
            }
        }

        /// <summary>
        /// return horizontal value W.R.To input 
        /// </summary>
        public float Horizontal
        {
            get 
            {
                switch (_InputDevices)
                {
                    case Input_Devides.Keyboard:
                        if (Input.GetKey(RightKey))
                            return 1f;
                        else if (Input.GetKey(LeftKey))
                            return -1f;
                        else 
                            return 0f;

                    case Input_Devides.TouchJoystic:
                        return Touchjoystic.Horizontal;
                    default:
                        return 0f;

                }
            }
        }


    }
}
