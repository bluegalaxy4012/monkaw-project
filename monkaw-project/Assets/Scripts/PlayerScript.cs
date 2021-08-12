using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public GameObject flashlight;
    private GameObject door;
    public PhotonView photonview;
    private bool nearDoor = false;
    private Animator animator;
    
    private int ok = 0;
    PhotonView pv;
    Camera cam;
    [SerializeField] LayerMask doorMask;
    [SerializeField] TMP_Text UsernameText;
    RaycastHit doorHit;
    private void Awake()
    {
        cam = GetComponentInChildren<Camera>();
        pv = GetComponent<PhotonView>();
        animator = GetComponent<Animator>(); 
        ok = 0;
        if(!pv.IsMine)
            UsernameText.text = pv.Owner.NickName;
        else
        UsernameText.text = PhotonNetwork.NickName;
    }




    private void Update()
    {
        if (!pv.IsMine)
            return;


        //for animator
        bool wPressed = Input.GetKey(KeyCode.W);
        bool aPressed = Input.GetKey(KeyCode.A);
        bool dPressed = Input.GetKey(KeyCode.D);
        bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
        bool isrunning = animator.GetBool("isRunning");
        bool iswalkingforward = animator.GetBool("isWalkingForward");
        bool iswalkingleft = animator.GetBool("isWalkingLeft");
        bool iswalkingright = animator.GetBool("isWalkingRight");
        bool iswalking = iswalkingforward || iswalkingleft || iswalkingright;




        nearDoor = Physics.CheckSphere(transform.position + new Vector3(0,0.81f,0), 3f, doorMask);

        if (!iswalkingforward && wPressed)
        {
                animator.SetBool("isWalkingForward", true);
        }
        if (iswalkingforward && !wPressed)
        {
            animator.SetBool("isWalkingForward", false);
        }
        if (!iswalkingleft && aPressed)
        {
            animator.SetBool("isWalkingLeft", true);
        }
        if (iswalkingleft && !aPressed)
        {
            animator.SetBool("isWalkingLeft", false);
        }
        if (!iswalkingright && dPressed)
        {
            animator.SetBool("isWalkingRight", true);
        }
        if (iswalkingright && !dPressed)
        {
            animator.SetBool("isWalkingRight", false);
        }


        if (Input.GetKeyDown(KeyCode.E) && nearDoor && Physics.Raycast(cam.transform.position,cam.transform.forward, out doorHit, 2))
        {
            Debug.Log("ddd0");
            if (doorHit.collider != null)
            {

                if (doorHit.collider.gameObject.tag == "Door")
                {
                    doorHit.collider.gameObject.transform.rotation = Quaternion.Euler(0, -85f, 0);
                    doorHit.collider.gameObject.tag = "OpenDoor";
                }
                else if (doorHit.collider.gameObject.tag == "OpenDoor")
                {
                    doorHit.collider.gameObject.transform.rotation = Quaternion.Euler(0, 0f, 0);
                    doorHit.collider.gameObject.tag = "Door";
                }


            }
        }

            if (Input.GetKeyDown(KeyCode.F))
            {
                if (ok == 0)
                {
                    flashlight.SetActive(true);
                    ok = 1;
                }
                else
                {
                    flashlight.SetActive(false);
                    ok = 0;
                }

            }
        
    }
}
