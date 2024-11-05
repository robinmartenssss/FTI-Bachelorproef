// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class VirtualMouseClicks : MonoBehaviour
// {

//    void Update()
// {
//     if (Input.GetButtonDown("buttonSouth"))
//     {
//         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

//         // Log the ray's origin and direction
//         Debug.Log("Ray Origin: " + ray.origin);
//         Debug.Log("Ray Direction: " + ray.direction);

//         RaycastHit hit;
//         if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("Clickable")))
//         {
//             Debug.Log("Clickable");
//             Debug.Log("Clicked object: " + hit.collider.gameObject.name);
//             Debug.Log("Clicked object layer: " + hit.collider.gameObject.layer);

//             MainImageScript script = hit.collider.GetComponent<MainImageScript>();
//             if (script != null)
//             {
//                 script.SimulateClick();
//                 Debug.Log("SimulateClick called on " + script.gameObject.name);
//             }
//             else
//             {
//                 Debug.Log("MainImageScript not found on clicked object. Collider name: " + hit.collider.name);
//             }
//         }
//     }
// }



// }
