using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerinteractUI : MonoBehaviour
{
  [SerializeField] private GameObject containerGameobject;

  [SerializeField] private PlayerInteract playerInteract;
  [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;

  private void Update() {
    if (playerInteract.GetInteractableObject() != null) {
        Show(playerInteract.GetInteractableObject() );
    } else {
        Hide();
    }
  }

  private void Show(NPCInteractable npcInteractable) {
    containerGameobject.SetActive(true);
    interactTextMeshProUGUI.text = npcInteractable.GetInteractText();
  }

  private void Hide()  {
    containerGameobject.SetActive(false);
  }
}
