using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 

public class NPCInteractable : MonoBehaviour
{

    [SerializeField] private string interactText;
    [SerializeField] private string npcDialogue;
   public void Interact() {
    ChatBubble3D.Create(transform.transform, new Vector3(3f,1.7f,0f), this);
   }


   public string GetInteractText() {
    return interactText;
   }

     public string GetDialogue()
    {
    return npcDialogue;
    }
}
