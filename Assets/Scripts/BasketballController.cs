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
    public Transform Target5;
    public Transform Target6;

    public GameObject scoringCylinderGreenNorth; 
    public GameObject scoringCylinderGreenEast; 
    public GameObject scoringCylinderGreenSouth; 
    public GameObject scoringCylinderGreenWest; 
    public GameObject scoringCylinderGreenOther1; 
    public GameObject scoringCylinderGreenOther2; 

    private GameObject scoringCylinder;

    

    private bool IsBallInHands = true;

    private bool IsBallFlyingNorth = false;
    private bool IsBallFlyingEast = false;
    private bool IsBallFlyingSouth = false;
    private bool IsBallFlyingWest = false;

    private bool IsBallFlyingOther1 = false;
    private bool IsBallFlyingOther2 = false;
    
    private bool isButtonHeld = false;
    private float T = 0;
    private Direction currentDirection;

    private static float timeSinceMovement = 0f;

    public GameObject startGameBasket;
    public GameObject startGameOverlay;

    public GameObject scoredSphere;
     public GameObject destroyObject;
     public GameObject placeObject;
    public float yourSphereMovementSpeed = 20.0f;
    public float distancePerMovement = 4.0f;

    private Vector3 lastPosition;

    // Start is called before the first frame update

    public static NewBehaviourScript Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

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
             if (Input.GetButtonDown("buttonOther1")) {
                transform.LookAt(Target5.parent.position);
                IsBallInHands = false;
                IsBallFlyingOther1 = true;
                T = 0;
                isButtonHeld = false;
            }
            if (Input.GetButtonDown("buttonOther2")) {
                transform.LookAt(Target6.parent.position);
                IsBallInHands = false;
                IsBallFlyingOther2 = true;
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

        if ( direction != Vector3.zero) {
            transform.position += direction * MoveSpeed * Time.deltaTime;
            transform.LookAt(transform.position + direction);
            timeSinceMovement = 0f;
        } if (timeSinceMovement >= 5f && timeSinceMovement <= 5.025f){
                ResetScene();
        } else if (timeSinceMovement >= 8f){
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

          if (IsBallFlyingOther1) {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t05 = T / duration;


            Vector3 A = PosOverHead.position;
            Vector3 B = Target5.position;
            Vector3 pos = Vector3.Lerp(A,B,t05);

            SetCurrentDirection(Direction.Other1);
            // Arc of ball 
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t05 * 3.14f);

            Ball.position = pos + arc;

            Arms.localEulerAngles = Vector3.right * 0;

            if (t05 >= 1) {
                IsBallFlyingOther1 = false;
                Ball.GetComponent<Rigidbody>().isKinematic = false;
            }

             CheckScoring();
        }

         if (IsBallFlyingOther2) {
            T += Time.deltaTime;
            float duration = 0.5f;
            float t06 = T / duration;


            Vector3 A = PosOverHead.position;
            Vector3 B = Target6.position;
            Vector3 pos = Vector3.Lerp(A,B,t06);

            SetCurrentDirection(Direction.Other2);
            // Arc of ball 
            Vector3 arc = Vector3.up * 5 * Mathf.Sin(t06 * 3.14f);

            Ball.position = pos + arc;

            Arms.localEulerAngles = Vector3.right * 0;

            if (t06 >= 1) {
                IsBallFlyingOther2 = false;
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
        case Direction.Other1:
            return IsBallFlyingOther1;
        case Direction.Other2:
            return IsBallFlyingOther2;
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
        case Direction.Other1:
            IsBallFlyingOther1 = value;
            break;
         case Direction.Other2:
            IsBallFlyingOther2 = value;
            break;
    }
 }

 private void CheckScoring()
{
    // Check if the ball is close enough to the target points
    float scoringDistanceThreshold = 1.0f; // Adjust this value based on your game's scale


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
     else if (Vector3.Distance(Ball.position, Target5.position) < scoringDistanceThreshold)
    {
        HandleScoring(Direction.Other1);
    }
    else if (Vector3.Distance(Ball.position, Target6.position) < scoringDistanceThreshold)
    {
        HandleScoring(Direction.Other2);
    }
}


public enum Direction {
    North,
    East,
    South,
    West,
    Other1,
    Other2
}

private void OnTriggerEnter(Collider other)
{
    if (other.CompareTag("ScoringArea"))
    {
        HandleScoring(currentDirection);
    }
    else
    {
        HandleBallPickup();
    }
}


private void HandleScoring(Direction scoringDirection)
{
    Debug.Log("Scoring Logic for Direction: " + scoringDirection);
    
       switch (scoringDirection)
    {
        case Direction.North:
            // Implement movement logic for scoring in the North target
            MoveCharacterForward();
            break;
        case Direction.West:
            // Implement movement logic for scoring in the West target
            MoveCharacterLeft();
            break;
        case Direction.South:
            // Implement movement logic for scoring in the South target
            MoveCharacterBackward();
            break;
        case Direction.East:
            // Implement movement logic for scoring in the East target
            MoveCharacterRight();
            break;
        case Direction.Other1:
            // Implement movement logic for scoring in the East target
            MoveCharacterDestroy();
            break;
        case Direction.Other2:
            // Implement movement logic for scoring in the East target
            MoveCharacterPlace();
            break;    
        default:
            Debug.LogError("Invalid scoring direction!");
            return;
    }

    // Show scoring cylinder at the ball's current position
    ShowScoringCylinder(Ball.position, scoringDirection);

    // Pass scoringDirection to the DeactivateScoringCylinderAfterDelay method
    StartCoroutine(DeactivateScoringCylinderAfterDelay(scoringDirection));
}

private void MoveCharacterForward()
{
    if (!IsBallInHands)
    {
        lastPosition = scoredSphere.transform.position;
        scoredSphere.transform.Translate(Vector3.forward * distancePerMovement);
    }
}

private void MoveCharacterLeft()
{
    if (!IsBallInHands)
    {
        lastPosition = scoredSphere.transform.position;
        scoredSphere.transform.Translate(Vector3.left * distancePerMovement);
    }
}

private void MoveCharacterBackward()
{
    if (!IsBallInHands)
    {
        lastPosition = scoredSphere.transform.position;
        scoredSphere.transform.Translate(Vector3.back * distancePerMovement);
    }
}

private void MoveCharacterRight()
{
    if (!IsBallInHands)
    {
        lastPosition = scoredSphere.transform.position;
        scoredSphere.transform.Translate(Vector3.right * distancePerMovement);
    }
}

private void MoveCharacterDestroy()
{
    if (!IsBallInHands)
    {
        destroyObject.SetActive(false);
    }
    StartCoroutine(ResetStateAfterDelayDestroy());
}

private void MoveCharacterPlace()
{
    if (!IsBallInHands)
    {
        placeObject.SetActive(true);
    }
     StartCoroutine(ResetStateAfterDelayPlaced());
}

// ResetScene
private IEnumerator ResetStateAfterDelayDestroy()
{
    yield return new WaitForSeconds(5f);

    destroyObject.SetActive(true); 
}

private IEnumerator ResetStateAfterDelayPlaced()
{
    yield return new WaitForSeconds(5f);

    placeObject.SetActive(false);  // Assuming placeObject is a reference to the placed object
}


public void ResetToLastPosition(){
    Debug.Log("where here");
    Debug.Log(lastPosition);
    scoredSphere.transform.position = lastPosition;
}

private void HandleBallPickup()
{
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
        

        switch (scoringDirection)
        {
            case Direction.North:
                scoringCylinder = scoringCylinderGreenNorth;
                break;
            case Direction.East:
                scoringCylinder = scoringCylinderGreenEast;
                break;
            case Direction.South:
                scoringCylinder = scoringCylinderGreenSouth;
                break;
            case Direction.West:
                scoringCylinder = scoringCylinderGreenWest;
                break;
            case Direction.Other1:
                scoringCylinder = scoringCylinderGreenOther1;
                break;
            case Direction.Other2:
                scoringCylinder = scoringCylinderGreenOther2;
                break;
            default:
                Debug.LogError("Invalid scoring direction!");
                return;
        }

      if (scoringCylinder != null && !IsBallInHands)

            {
                scoringCylinder.SetActive(true);
                StartCoroutine(DeactivateScoringCylinderAfterDelay(scoringDirection));
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
        case Direction.Other1:
            scoringCylinderGreenOther1.SetActive(false);
            break;
        case Direction.Other2:
            scoringCylinderGreenOther2.SetActive(false);
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
