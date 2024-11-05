using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicMovement : MonoBehaviour
{
    [Header("Movement")]
    public float MoveSpeed = 10;

    void Start()
    {
       
    }

    private void FixedUpdate()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.Instance.IsGameFrozen)
        {
        float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0 , verticalInput);
        transform.position += direction * MoveSpeed * Time.deltaTime;
        transform.LookAt(transform.position + direction);
        }
    }

    

public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "SceneTransitionBack") {
            GameBehaviour.Instance.sceneToMoveBackTo();
        }
    }



}
