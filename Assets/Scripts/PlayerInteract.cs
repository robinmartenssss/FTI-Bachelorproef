using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteract : MonoBehaviour
{
   

   private void Update() {
    if( Input.GetButtonDown("buttonSouth")){
    float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange );
        foreach (Collider collider in colliderArray) {
            if(collider.TryGetComponent(out NPCInteractable npcInteractable)) {
                npcInteractable.Interact();
            }
        }
   }
   }

   public NPCInteractable GetInteractableObject(){
    float interactRange = 2f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange );
        foreach (Collider collider in colliderArray) {
            if(collider.TryGetComponent(out NPCInteractable npcInteractable)) {
               return npcInteractable;
            }
        }
    return null;
   }
}
