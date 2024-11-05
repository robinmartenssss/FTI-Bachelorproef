using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSphere : MonoBehaviour
{
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "destroyobject") {
            Debug.Log("hello reloaddd");
            GameBehaviour.Instance.ReloadCurrentScene();
        }
        else if (other.gameObject.tag == "wall")
        {
            Debug.Log("hello hit");
            NewBehaviourScript.Instance.ResetToLastPosition();
        }
    }
}
