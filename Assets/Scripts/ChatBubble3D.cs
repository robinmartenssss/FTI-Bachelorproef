 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChatBubble3D : MonoBehaviour
{
    private SpriteRenderer backgroundSpriteRenderer;
    private SpriteRenderer iconSpriteRenderer;
    private TextMeshPro textMeshPro;



    public static void Create(Transform parent, Vector3 localPosition, NPCInteractable npcInteractable ){

        //check if there is a n existing chat bubble
        ChatBubble3D exisitingChatBubble = parent.GetComponentInChildren<ChatBubble3D>();
        if (exisitingChatBubble != null)
        {
            Destroy(exisitingChatBubble.gameObject);
        }

        Transform chatBubbleTransform = Instantiate(GameAssets.i.pfChatBubble, parent);
        chatBubbleTransform.localPosition = localPosition;

        
        Vector3 playerForward = Camera.main.transform.forward;
        playerForward.y = 0f;

        chatBubbleTransform.rotation = Quaternion.LookRotation(playerForward);

        ChatBubble3D chatBubble = chatBubbleTransform.GetComponent<ChatBubble3D>();
        chatBubble.Setup(npcInteractable.GetDialogue());

        chatBubbleTransform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        chatBubbleTransform.localPosition = new Vector3(2.2f, 2.8f, 0f);

    }
  private void Awake() {
       Debug.Log("ChatBubble3D Awake");
    backgroundSpriteRenderer = transform.Find("Background").GetComponent<SpriteRenderer>();
    iconSpriteRenderer = transform.Find("Icon").GetComponent<SpriteRenderer>();
    textMeshPro = transform.Find("Text").GetComponent<TextMeshPro>();
 }

private void Start() {
    // string text = npcInteractable.GetDialogue();
    // Setup(GetDialogue());
}


 private void Setup(string text)
{
    // Set the text
    textMeshPro.SetText(text);
    textMeshPro.ForceMeshUpdate();

    // Set the desired fixed width
    float maxWidth = 48f;

    // Set the text width to the fixed width
    textMeshPro.rectTransform.sizeDelta = new Vector2(maxWidth, textMeshPro.rectTransform.sizeDelta.y);

    // Set the vertical alignment to top
    textMeshPro.alignment = TextAlignmentOptions.TopLeft;

    // Calculate the number of lines based on the text and the desired width
    int lineCount = textMeshPro.textInfo.lineCount;

    // Adjust the background height based on the number of lines and padding
    float paddingBetweenTextAndContainer = 6f; 
    float lineHeight = textMeshPro.textBounds.size.y / lineCount;
    float backgroundHeight = (lineCount * lineHeight) + paddingBetweenTextAndContainer;

    
    backgroundSpriteRenderer.size = new Vector2(maxWidth + 5f, backgroundHeight);

    float paddingInsideContainer = 6f; // Adjust as needed
    float textVerticalOffset = (backgroundHeight - textMeshPro.preferredHeight) ;
    textMeshPro.rectTransform.localPosition = new Vector3(maxWidth / 2f + 2f, textVerticalOffset + paddingInsideContainer, 0f);

    // Set the background position
    backgroundSpriteRenderer.transform.localPosition = new Vector3(maxWidth / 2f, 6f, 0f);
}


}
