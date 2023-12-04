using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PS4Controller : MonoBehaviour
{

    GameObject cube = null;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < Gamepad.all.Count; i++)
        {
            Debug.Log(Gamepad.all[i].name);
        }

        cube = GameObject.Find("Cube");
    }

    // Update is called once per frame
    void Update()
    {
        //  if (Input.GetButtonDown("buttonNorth"))
        // {
        //     // X button is pressed, perform your action here
        //     Debug.Log("X button pressed");
        // }
        // if (Gamepad.all.Count > 0)
        // {
        //     if(Gamepad.all[0].rightStick.right.isPressed)
        //     {
        //         cube.transform.position += Vector3.left * Time.deltaTime * 5f;
        //     }
        // }
    }
}
