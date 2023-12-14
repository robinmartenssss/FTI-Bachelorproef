using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class RespawnPoints : MonoBehaviour
{
    // Start is called before the first frame update
    
    public GameObject respawnFeedback;
    private bool IsRespawnFeedbackVisible = false;
    void Update()
    {
        if(IsRespawnFeedbackVisible && Input.GetButtonDown("buttonSouth"))
            {
            if(Input.GetButtonDown("buttonSouth")){
                respawnFeedback.SetActive(false);
                IsRespawnFeedbackVisible = false;

                GameManager.Instance.UnfreezeGame();
            }
        }
    }

      public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "Game2_Option1") {
            transform.position = new Vector3(589f, 5f, 438f);
            
            ShowFeedback();

            GameManager.Instance.FreezeGame();
        }
    }

    public void ShowFeedback()
    {
        respawnFeedback.SetActive(true);
        IsRespawnFeedbackVisible = true;
    }

   
}
