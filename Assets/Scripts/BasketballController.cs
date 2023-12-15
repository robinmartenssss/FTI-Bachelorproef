using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NewBehaviourScript : MonoBehaviour
{

    public float MoveSpeed = 10;
    public Transform Ball;
    public Transform Arms;
    public Transform PosOverHead;
    public Transform PosDribble;
    public Transform Target1;
    public Transform Target2;
    public Transform Target3;
    public Transform Target4;

    public GameObject scoringCylinderGreenNorth; 
    public GameObject scoringCylinderGreenEast; 
    public GameObject scoringCylinderGreenSouth; 
    public GameObject scoringCylinderGreenWest; 
    public GameObject scoringTextDoctype;
    public GameObject scoringTextHtml;
    public GameObject scoringTextBody;
    public GameObject scoringTextHtmlclose;

    private GameObject scoringCylinder;
    private GameObject scoringText;
    

    private bool IsBallInHands = true;

    private bool IsBallFlyingNorth = false;
    private bool IsBallFlyingEast = false;
    private bool IsBallFlyingSouth = false;
    private bool IsBallFlyingWest = false;
    
    private bool isButtonHeld = false;
    private float T = 0;
    private Direction currentDirection;

    private static float timeSinceMovement = 0f;

    public GameObject startGameBasket;
    public GameObject startGameOverlay;

    // Start is called before the first frame update
    void Start()
    {
    
    }
    // Update is called once per frame
    void Update()
    {
         //walking
          if (!GameManager.Instance.IsGameFrozen)
        {

        startGameBasket.SetActive(false);
        startGameOverlay.SetActive(false);

        float horizontalInput = Input.GetAxisRaw("Horizontal"); 
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);

      
    
        if (IsBallInHands) {
            if (Input.GetButtonDown("Shoot")) {
                isButtonHeld = true;
            }
            if (isButtonHeld) {
                Ball.position = PosOverHead.position;
                Arms.localEulerAngles = Vector3.right * 180;
            if (Input.GetButtonDown("buttonSouth")) {
                transform.LookAt(Target1.parent.position);
                IsBallInHands = false;
                IsBallFlyingSouth = true;
                T = 0;
                isButtonHeld = false;
            }
            if (Input.GetButtonDown("buttonWest")) {
                transform.LookAt(Target2.parent.position);
                IsBallInHands = false;
                IsBallFlyingWest = true;
                T = 0;
                isButtonHeld = false;
            }
            if (Input.GetButtonDown("buttonNorth")) {
                transform.LookAt(Target3.parent.position);
                IsBallInHands = false;
                IsBallFlyingNorth = true;
                T = 0;
                isButtonHeld = false;
            }
            if (Input.GetButtonDown("buttonEast")) {
                transform.LookAt(Target4.parent.position);
                IsBallInHands = false;
                IsBallFlyingEast = true;
                T = 0;
                isButtonHeld = false;
            }

            if (Input.GetButtonUp("Cancel")) { 
                   isButtonHeld = false;
                }
            }
            else  {
                Ball.position = PosDribble.position + Vector3.up * Mathf.Abs(Mathf.Sin(Time.time *5));
                Arms.localEulerAngles = Vector3.right * 0;
            }
           
        }

        // Check if there is movement other wise reset scene 

        timeSinceMovement += Time.deltaTime;
        Debug.Log(timeSinceMovement);

        if ( direction != Vector3.zero) {
            transform.position += direction * MoveSpeed * Time.deltaTime;
            transform.LookAt(transform.position + direction);
            timeSinceMovement = 0f;
        } if (timeSinceMovement >= 5f && timeSinceMovement <= 5.025f){
                Debug.Log("hello");
                ResetScene();
        } else if (timeSinceMovement >= 8f){
                Debug.Log("helloreset");
                ResetGame();
        }
           
    

        if (IsBallFlyingSouth) {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t01 = T / duration;


            Vector3 A = PosOverHead.position;
            Vector3 B = Target1.position;
            Vector3 pos = Vector3.Lerp(A,B,t01);

            SetCurrentDirection(Direction.South);

            // Arc of ball 
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t01 * 3.14f);

            Ball.position = pos + arc;

            Arms.localEulerAngles = Vector3.right * 0;

            if (t01 >= 1) {
                IsBallFlyingSouth = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }

            CheckScoring();
        }

         if (IsBallFlyingWest) {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t02 = T / duration;


            Vector3 A = PosOverHead.position;
            Vector3 B = Target2.position;
            Vector3 pos = Vector3.Lerp(A,B,t02);

            SetCurrentDirection(Direction.West);

            // Arc of ball 
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t02 * 3.14f);

            Ball.position = pos + arc;

            Arms.localEulerAngles = Vector3.right * 0;

            if (t02 >= 1) {
                IsBallFlyingWest = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }

            CheckScoring();
        }

         if (IsBallFlyingNorth) {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t03 = T / duration;


            Vector3 A = PosOverHead.position;
            Vector3 B = Target3.position;
            Vector3 pos = Vector3.Lerp(A,B,t03);

            SetCurrentDirection(Direction.North);

            // Arc of ball 
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t03 * 3.14f);

            Ball.position = pos + arc;

            Arms.localEulerAngles = Vector3.right * 0;

            if (t03 >= 1) {
                IsBallFlyingNorth = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }

            CheckScoring();
            
        }

         if (IsBallFlyingEast) {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t04 = T / duration;


            Vector3 A = PosOverHead.position;
            Vector3 B = Target4.position;
            Vector3 pos = Vector3.Lerp(A,B,t04);

            SetCurrentDirection(Direction.East);
            // Arc of ball 
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t04 * 3.14f);

            Ball.position = pos + arc;

            Arms.localEulerAngles = Vector3.right * 0;

            if (t04 >= 1) {
                IsBallFlyingEast = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }

             CheckScoring();
        }

        }

    }

    private bool IsBallFlyingInDirection(Direction direction) {
    switch (direction) {
        case Direction.North:
            return IsBallFlyingNorth;
        case Direction.East:
            return IsBallFlyingEast;
        case Direction.South:
            return IsBallFlyingSouth;
        case Direction.West:
            return IsBallFlyingWest;
        default:
            return false;
    }   
}

 private void SetIsBallFlying(Direction direction, bool value) {
    switch (direction) {
        case Direction.North:
            IsBallFlyingNorth = value;
            break;
        case Direction.East:
            IsBallFlyingEast = value;
            break;
        case Direction.South:
            IsBallFlyingSouth = value;
            break;
        case Direction.West:
            IsBallFlyingWest = value;
            break;
    }
 }

 private void CheckScoring()
{
    // Check if the ball is close enough to the target points
    float scoringDistanceThreshold = 1.0f; // Adjust this value based on your game's scale

    Debug.Log("Checking scoring...");

    if (Vector3.Distance(Ball.position, Target1.position) < scoringDistanceThreshold)
    {
        HandleScoring(Direction.North);
    }
    else if (Vector3.Distance(Ball.position, Target2.position) < scoringDistanceThreshold)
    {
        HandleScoring(Direction.West);
    }
    else if (Vector3.Distance(Ball.position, Target3.position) < scoringDistanceThreshold)
    {
        HandleScoring(Direction.South);
    }
    else if (Vector3.Distance(Ball.position, Target4.position) < scoringDistanceThreshold)
    {
        HandleScoring(Direction.East);
    }
}


public enum Direction {
    North,
    East,
    South,
    West
}

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("ScoringArea"))
    {
        Debug.Log("Scoring Trigger entered!");
        HandleScoring(currentDirection);
    }
    else
    {
        Debug.Log("Ball picked up Trigger entered!");
        HandleBallPickup();
    }
}


private void HandleScoring(Direction scoringDirection)
{
    Debug.Log("Scoring Logic for Direction: " + scoringDirection);

    // Show scoring cylinder at the ball's current position
    ShowScoringCylinder(Ball.position, scoringDirection);

    // Pass scoringDirection to the DeactivateScoringCylinderAfterDelay method
    StartCoroutine(DeactivateScoringCylinderAfterDelay(scoringDirection));
}


private void HandleBallPickup()
{
    Debug.Log("Ball picked up Logic");
    if (!IsBallInHands && !IsBallFlyingInDirection(currentDirection))
    {
        IsBallInHands = true;
        Ball.GetComponent<Rigidbody>().isKinematic = true;
        SetIsBallFlying(currentDirection, true);
    }
}


private void ShowScoringCylinder(Vector3 position, Direction scoringDirection)
    {
        
        Debug.Log("Showing scoring cylinder at position: " + position);

        GameObject scoringCylinder = null;
        GameObject scoringText = null;
        

        switch (scoringDirection)
        {
            case Direction.North:
                scoringCylinder = scoringCylinderGreenNorth;
                scoringText = scoringTextDoctype;
                break;
            case Direction.East:
                scoringCylinder = scoringCylinderGreenEast;
                scoringText = scoringTextHtml;
                break;
            case Direction.South:
                scoringCylinder = scoringCylinderGreenSouth;
                scoringText = scoringTextHtmlclose;
                break;
            case Direction.West:
                scoringCylinder = scoringCylinderGreenWest;
                scoringText = scoringTextBody;
                break;
            default:
                Debug.LogError("Invalid scoring direction!");
                return;
        }

      if (scoringCylinder != null)

            {
                scoringCylinder.SetActive(true);
                scoringText.SetActive(true);
                StartCoroutine(DeactivateScoringCylinderAfterDelay(scoringDirection));
            }
        else
            {
                 Debug.LogError("Scoring cylinder is null!");
            }
    }
private IEnumerator DeactivateScoringCylinderAfterDelay(Direction direction)
{
    yield return new WaitForSeconds(2f);

   switch (direction)
    {
        case Direction.North:
            scoringCylinderGreenNorth.SetActive(false);
            break;
        case Direction.West:
            scoringCylinderGreenWest.SetActive(false);
            break;
        case Direction.South:
            scoringCylinderGreenSouth.SetActive(false);
            break;
        case Direction.East:
            scoringCylinderGreenEast.SetActive(false);
            break;
    }
}


private void SetCurrentDirection(Direction direction) {
    currentDirection = direction;
}

public void OnCollisionEnter(Collision collision){
        if(collision.gameObject.tag == "SceneTransitionBack") {
            GameBehaviour.Instance.sceneToMoveBackTo();
        }
    }

private void ResetScene(){
    GameBehaviour.Instance.ReloadCurrentScene();
}

private void ResetGame(){
    GameBehaviour.Instance.ReloadMainArea();

    timeSinceMovement = 0f;
}
   

}
