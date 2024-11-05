using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainImageScript : MonoBehaviour
{
    [SerializeField] private GameObject image_unknown;
    [SerializeField] private GameController gameController;

    public int _spriteId; 


    public void OnButtonRegular()
    {
        Debug.Log("OnButtonRegular method called");
        if (image_unknown.activeSelf && gameController.canOpen)
        {
            image_unknown.SetActive(false);
            gameController.imageOpened(this);
        }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _spriteId = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }


    public void Close()
    {
        image_unknown.SetActive(true);
    }
}
