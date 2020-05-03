using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class CharacterController2D : MonoBehaviour
{
    public static CharacterController2D Instance { get; set; }
    [SerializeField] private float MOVE_SPEED = 10f;

    private enum State
    {
        Normal,
        Rolling,
        Idle
    }

    [SerializeField] private LayerMask dashLayerMask;
    [SerializeField] private float rollSpeedValue = 100f;

    private Rigidbody2D rigidbody2D;
    private Vector3 moveDir;
    private Vector3 rollDir;
    private Vector3 lastMoveDir;
    private float rollSpeed;
    private bool isDashButtonDown;
    public bool  isMoving = false;
    private State state;
 

    public float lookSpeed = 10;
    private Vector3 curLoc;
    private Vector3 prevLoc;

    public float rotSpeed = 100f;

    public GameObject bullet;

    private BulletManager bulletManager;


    private void Awake()
    {
        Instance = this;
        rigidbody2D = GetComponent<Rigidbody2D>();
        state = State.Normal;
        isMoving = false;
        bulletManager = GetComponent<BulletManager>();
    }

    private void Update()
    {


        switch (state)
        {
            case State.Normal:
                float moveX = 0f;
                float moveY = 0f;

                if (Input.GetKey(KeyCode.W))
                {
                    moveY = +1f;
          
                }
                if (Input.GetKey(KeyCode.S))
                {
                    moveY = -1f;
                }
                if (Input.GetKey(KeyCode.A))
                {
                    moveX = -1f;


                }
                if (Input.GetKey(KeyCode.D))
                {
                    moveX = +1f;

                }
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    if ( bulletManager != null)
                    {
                        bulletManager.AddBullet(bullet);
                    }
                }

                moveDir = new Vector3(moveX, moveY).normalized;
                if (moveX != 0 || moveY != 0)
                {
                    // Not idle

                    lastMoveDir = moveDir;
                    isMoving = true;

                }
                else
                {
                    isMoving = false;
                }


                if (Input.GetKeyDown(KeyCode.F))
                {
                    isDashButtonDown = true;
                }

                /*
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rollDir = lastMoveDir;
                    rollSpeed = rollSpeedValue;
                    state = State.Rolling;
                }
                */

                break;
            case State.Rolling:
                float rollSpeedDropMultiplier = 5f;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;

                float rollSpeedMinimum = 50f;
                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                }
                break;
        }
    }
    private void setLookDir()
    {
        if (isMoving)
        {
            float angle = Mathf.Atan2(moveDir.y, moveDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90));
        }


    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                rigidbody2D.velocity = moveDir * MOVE_SPEED;
                setLookDir();
                

                if (isDashButtonDown)
                {
                    float dashAmount = 50f;
                    Vector3 dashPosition = transform.position + lastMoveDir * dashAmount;

                    RaycastHit2D raycastHit2d = Physics2D.Raycast(transform.position, lastMoveDir, dashAmount, dashLayerMask);
                    if (raycastHit2d.collider != null)
                    {
                        dashPosition = raycastHit2d.point;
                    }

                    // Spawn visual effect
                    // DashEffect.CreateDashEffect(transform.position, lastMoveDir, Vector3.Distance(transform.position, dashPosition));

                    rigidbody2D.MovePosition(dashPosition);
                    isDashButtonDown = false;
                    
                    
                }
                break;
            case State.Rolling:
                rigidbody2D.velocity = rollDir * rollSpeed;
                break;
        }
    }

}
