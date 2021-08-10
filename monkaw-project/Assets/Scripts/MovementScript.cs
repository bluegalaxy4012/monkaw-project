using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MovementScript : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float rbDrag = 6f;
    [SerializeField] private float moveBoost = 9f;
    [SerializeField] private float airAntiBoost = 0.05f;
    [SerializeField] LayerMask groundMask;
    PhotonView pv;
    float playerHeight = 1.68f;

    float horizontalMovement;
    float verticalMovement;
    float jumpForce = 600;

    bool isGrounded;

    Vector3 moveDirection;
    Vector3 slopemoveDirection;
    Rigidbody rb;

    RaycastHit slopeHit;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        pv = GetComponent<PhotonView>();
    }
    private void Start()
    {
        if(!pv.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(rb);
        }
    }
    void MyInput()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        verticalMovement = Input.GetAxisRaw("Vertical");
        moveDirection = transform.forward * verticalMovement + transform.right * horizontalMovement;
    }
    private bool OnSlope()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, 0.5f))
        {
            if(slopeHit.normal != Vector3.up)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }
    private void Update()
    {
        if (!pv.IsMine)
            return;
   
            isGrounded = Physics.CheckSphere(transform.position, 0.25f, groundMask);
            MyInput();
          

            if (isGrounded)
            {
                rb.drag = rbDrag;

            }
            else
            {
                rb.drag = 1.75f;

            }

            if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
            {
                Jump();
            }
            slopemoveDirection = Vector3.ProjectOnPlane(moveDirection, slopeHit.normal);
        
        }
        void Jump()
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
        private void FixedUpdate()
        {
        if (!pv.IsMine)
            return;
            MovePlayer();
        }
        void MovePlayer()
        {
            if (isGrounded && !OnSlope())
                rb.AddForce(moveDirection.normalized * moveSpeed * moveBoost, ForceMode.Acceleration);
            else if (isGrounded && OnSlope())
                rb.AddForce(slopemoveDirection.normalized * moveSpeed * moveBoost, ForceMode.Acceleration);
            else
                rb.AddForce(moveDirection.normalized * moveSpeed * airAntiBoost * moveBoost, ForceMode.Acceleration);
        }
    }

