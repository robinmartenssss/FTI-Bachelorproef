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
        if(collision.gameObject.tag == "Game2_Option1" || collision.gameObject.tag ==  "Game2_Option2") {
            transform.position = new Vector3(589f, 5f, 438f);
            
            ShowFeedback();

            GameManager.Instance.FreezeGame();
        }
        if(collision.gameObject.tag == "Game2_Option3" || collision.gameObject.tag == "Game2_2_Option4" || collision.gameObject.tag == "Game2_2_Option1" || collision.gameObject.tag == "Game2_3_Option1"|| collision.gameObject.tag == "Game2_3_Option4" || collision.gameObject.tag == "Game2_4_Option1"|| collision.gameObject.tag == "Game2_4_Option4" || collision.gameObject.tag == "Game2_5_Option1"|| collision.gameObject.tag == "Game2_5_Option4")
        {
            collision.gameObject.SetActive(false);
        }
        if(collision.gameObject.tag == "Game2_2_Option2" || collision.gameObject.tag == "Game2_2_Option3") {
            transform.position = new Vector3(554f, 5f, 487f);
        }
         if(collision.gameObject.tag == "Game2_3_Option2" || collision.gameObject.tag == "Game2_3_Option3") {
            transform.position = new Vector3(500f, 5f, 534f);
        }
        if(collision.gameObject.tag == "Game2_4_Option2" || collision.gameObject.tag == "Game2_4_Option3") {
            transform.position = new Vector3(450f, 5f, 480f);
        }
         if(collision.gameObject.tag == "Game2_5_Option2" || collision.gameObject.tag == "Game2_5_Option3") {
            transform.position = new Vector3(450f, 5f, 390f);
        }
        if(collision.gameObject.tag == "Game2_6_Option2" || collision.gameObject.tag == "Game2_6_Option3") {
            transform.position = new Vector3(503, 5f, 340f);
        }
    }

    public void ShowFeedback()
    {
        respawnFeedback.SetActive(true);
        IsRespawnFeedbackVisible = true;
    }

   
}
