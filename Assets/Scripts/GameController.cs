using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public const int columns = 4;
    public const int rows = 2;

    public const float Xspace = 4f;
    public const float Yspacee = -5f;

    [SerializeField] private MainImageScript startObject;
    [SerializeField] private Sprite[] images;

    [SerializeField] private TextMesh scoreText;
    [SerializeField] private TextMesh attemptsText;

    private MainImageScript firstOpen;
    private MainImageScript secondOpen;
    private int score = 0;
    private int attempts = 0;

    private HashSet<int> matchedPairs = new HashSet<int>();

    public bool canOpen
    {
        get { return secondOpen == null; }
    }

   private List<int> Randomiser(int[] location)
{
    List<int> array = new List<int>(location);
    for (int i = 0; i < array.Count; i++)
    {
        int newArray = array[i];
        int j = Random.Range(i, array.Count);
        array[i] = array[j];
        array[j] = newArray;
    }
    return array;
}

    private void Start()
    {
        GameObject yourCanvasGameObject = GameObject.Find("Canvas");
        RectTransform canvasRectTransform = yourCanvasGameObject.GetComponent<RectTransform>();

        Vector3 startPosition = startObject.transform.localPosition;

        List<int> indices = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7 };
        indices = Randomiser(indices.ToArray());

        for (int i = 0; i < columns; i++)
        {
            for (int j = 0; j < rows; j++)
            {
                MainImageScript gameImage;

                if (i == 0 && j == 0)
                {
                    gameImage = startObject;
                }
                else
                {
                    GameObject gameImageObject = Instantiate(startObject.gameObject, canvasRectTransform);
                    gameImage = gameImageObject.GetComponent<MainImageScript>();
                    gameImageObject.transform.SetParent(canvasRectTransform, false);
                }

                if (gameImage == null)
                {
                    Debug.LogError("gameImage is null");
                    continue;
                }

                int index = j * columns + i;
                int id = indices[index];
                gameImage.ChangeSprite(id, images[id]);

                Debug.Log($"Image at ({i}, {j}) - ID: {id}");

                float positionX = (Xspace * i);
                float positionY = (Yspacee * j);

                gameImage.transform.position = new Vector3(positionX, positionY, startPosition.z);
            }
        }
    }

    public void imageOpened(MainImageScript startObject)
    {
        if (firstOpen == null)
        {
            firstOpen = startObject;
        }
        else
        {
            secondOpen = startObject;
            StartCoroutine(CheckGuessed());
        }
    }

    private IEnumerator CheckGuessed()
    {
        int pairId1 = firstOpen._spriteId;
        int pairId2 = secondOpen._spriteId;

        if (!matchedPairs.Contains(pairId1) && !matchedPairs.Contains(pairId2) && (pairId2 == (pairId1 + 4) % 8 || pairId1 == (pairId2 + 4) % 8))
        {
            matchedPairs.Add(pairId1);
            matchedPairs.Add(pairId2);
            score++;
            scoreText.text = "Score: " + score;
        }
        else
        {
            yield return new WaitForSeconds(0.5f);
            firstOpen.Close();
            secondOpen.Close();
        }

        attempts++;
        attemptsText.text = "Attempts: " + attempts;

        firstOpen = null;
        secondOpen = null;
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game3");
    }

    public void GoHome()
    {
        SceneManager.LoadScene("MainAreaScene");
    }
}
