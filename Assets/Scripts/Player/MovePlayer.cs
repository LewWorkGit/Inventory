using UnityEngine;


public class MovePlayer : MonoBehaviour, IMovePlayer
{
    private NewControls inputs;
    private Rigidbody2D rbPlayer;
    private Transform playerTrasform;
    private Vector2 moveInput;
    private Vector3 lookVector = Vector3.one;
    private Transform enemyTransform;
    [SerializeField] private float moveSpeed = 7;
    private void Awake()
    {
        inputs = new NewControls();
        rbPlayer = GetComponent<Rigidbody2D>();
        playerTrasform = transform;

    }

    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }

    private void LateUpdate()
    {
        //движение игрока
        moveInput = inputs.player.move.ReadValue<Vector2>();
        rbPlayer.velocity = moveInput * moveSpeed;

        LookVector();
    }

    //поворот игрока в сторону движения или врага
    private void LookVector()
    {
        if (enemyTransform == null)
        {
            if (inputs.player.move.ReadValue<Vector2>().x < 0)
            {
                lookVector.Set(-1, 1, 1);
                playerTrasform.localScale = lookVector;
            }
            else if (inputs.player.move.ReadValue<Vector2>().x > 0)
            {
                lookVector.Set(1, 1, 1);
                playerTrasform.localScale = lookVector;
            }
        }
        else
        {
            lookVector = enemyTransform.position;

            if (lookVector.x > rbPlayer.position.x)
            {
                playerTrasform.localScale = Vector3.one;
            }
            else
            {
                playerTrasform.localScale = new Vector3(-1, 1, 1);
            }
        }
    }

    public Transform GetEnemyTransform()
    {
        return enemyTransform;
    }

    public void SetEnemyTransform(Transform enemy)
    {
        enemyTransform = enemy;
    }

    public Vector3 GetLookSide()
    {
        return transform.localScale;
    }

    public void DisableInput()
    {
        inputs.Disable();
    }
}