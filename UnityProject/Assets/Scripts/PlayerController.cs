using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static bool isStarted = false;
    public static PlayerController player;
    [Header("Paddle Settings")]
    [SerializeField]
    private float acceleration = 20;
    [SerializeField]
    private float moveSpeed = 20;
    [Space]
    [Header("Assignment")]
    [SerializeField]
    private Transform ballPool;
    private OptSystem optSystem = new OptSystem();
    private bool isBoosting = true;
    private float maxX = 13.3f;
    private float minX = -13.3f;
    private float maxY = -3.5f;
    private float minY = -4.5f;
    private Vector3 returnPos = new Vector3(0, -6, 0);
    private Vector2 velocity;
    public Text scoreText;
    private double score = 0;
    private double scoreDifference;
    private double scoreDisplayIncrease;
    private double scoreDisplay = 0;
    private const string _format = "{0:#,###0}";
    private void Start()
    {
        player = this;
        optSystem.GameMouseActive(false, CursorLockMode.Locked);
        SetupPaddle();
    }

    private void Update()
    {
        Move();
        if (optSystem.Input.GetButton("Boost")) isBoosting = true;
        else if (optSystem.Input.GetButtonUp("Boost")) isBoosting = false;
        if (optSystem.Input.GetButtonDown("Start") && !isStarted) StartPaddle();
        if (optSystem.Input.GetButtonDown("A") && isStarted) SetupPaddle();

        if (scoreDisplay < score)
        {
            scoreDifference = score - scoreDisplay;
            scoreDisplayIncrease = scoreDifference / 10;
            if (scoreDisplayIncrease == 0) scoreDisplayIncrease = 1;
            scoreDisplay += scoreDisplayIncrease;
            scoreText.text = string.Format(_format, scoreDisplay);
        }

    }
    public void AddScore(int amt)
    {
        score += amt;
    }
    private void Move()
    {
        float inputX = optSystem.Input.GetAxisRaw("Horizontal");
        //float inputY = optSystem.Input.GetAxisRaw("Vertical");
        transform.Translate(Time.deltaTime * velocity);
        if(inputX != 0)
        {
            if (isBoosting)
                velocity.x = Mathf.MoveTowards(velocity.x, (moveSpeed * 2f) * inputX, (acceleration * 2));
            else
                velocity.x = Mathf.MoveTowards(velocity.x, moveSpeed * inputX, acceleration);
        }
        else if(inputX == 0)
            velocity.x = 0;

        //if (inputY != 0)
        //{
        //    if (isBoosting)
        //        velocity.y = Mathf.MoveTowards(velocity.y, (moveSpeed * 1.5f) * inputY, acceleration);
        //    else
        //        velocity.y = Mathf.MoveTowards(velocity.y, moveSpeed * inputY, acceleration);
        //}
        //else if (inputY == 0)
        //    velocity.y = 0;

        //if (transform.localPosition.y < minY)
        //    transform.localPosition = optSystem.Vector3(transform.localPosition.x, minY, transform.localPosition.z);
        //else if (transform.localPosition.y > maxY)
        //    transform.localPosition = optSystem.Vector3(transform.localPosition.x, maxY, transform.localPosition.z);
        if (transform.localPosition.x < minX)
            transform.localPosition = optSystem.Vector3(minX, transform.localPosition.y, transform.localPosition.z);
        else if (transform.localPosition.x > maxX)
            transform.localPosition = optSystem.Vector3(maxX, transform.localPosition.y, transform.localPosition.z);
    }
    public void StartPaddle()
    {
        transform.GetChild(1).transform.SetParent(ballPool);
        isStarted = true;
        
    }
    public void SetupPaddle()
    {
        isStarted = false;
        transform.localPosition = returnPos;
        if (ballPool.transform.childCount > 0)
        {
            ballPool.transform.GetChild(0).transform.SetParent(transform);
            transform.GetChild(1).transform.localPosition = optSystem.Vector3(0, 0.7f, 0);
            transform.GetChild(1).transform.localRotation = Quaternion.identity;
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawLine(new Vector3(-14.5f, -6, 0), new Vector3(-14.5f, 22, 0));
        Gizmos.DrawLine(new Vector3(14.5f, -6, 0), new Vector3(14.5f, 22, 0));
        Gizmos.DrawLine(new Vector3(14.5f, 22, 0), new Vector3(-14.5f, 22, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(14.5f, -6, 0), new Vector3(-14.5f, -6, 0));
    }
}
