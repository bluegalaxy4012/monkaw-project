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

        nearDoor = Physics.CheckSphere(transform.position + new Vector3(0,0.81f,0), 3f, doorMask);

           

        if(Input.GetKeyDown(KeyCode.E) && nearDoor && Physics.Raycast(cam.transform.position,cam.transform.forward, out doorHit, 2))
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
