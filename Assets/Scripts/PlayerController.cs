using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private MobileController mobContrlRot;
    [SerializeField] private MobileController mobContrlMove;
    [SerializeField] private Button jump;
    private CharacterController player;

    private float playerSpeed = 8f;
    private float jumpSpeed = 12f;
    private float rotationSpeed = 5f;

    private float minimumVert = -30f;
    private float maximumVert = 40f;
    private float rotationX = 0;
    private float rotationY = 0;

    private float horInput = 0;
    private float vertInput = 0;

    private float gravity = Physics.gravity.y;
    private float terminalVelocity = -10f;
    private float minimumFall = -1.5f;
    private float verticalSpeed;

    private void Awake()
    {
        player = GetComponent<CharacterController>();
        rotationY = transform.localEulerAngles.y;
    }   

    // Update is called once per frame
    void Update()
    {
        // ABSTRACTION player move
        if (!GameManager.gameManager.gamePause)
        {
            PlayerMove();
            PlayerRotation();
            PlayerJump();
        }        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Obstacle")) hit.gameObject.GetComponent<Obstacle>().Hit();
    }

    public void PlayerMove()
    {
    #if (UNITY_EDITOR)
        horInput = Input.GetAxis("Horizontal");
        vertInput = Input.GetAxis("Vertical");
    #else
        horInput = mobContrlMove.inputHorizontal();
        vertInput = mobContrlMove.inputVertical();
    #endif



        Vector3 move = new Vector3(horInput, 0, vertInput);        
        move *= playerSpeed;
        move.y = verticalSpeed;
        move *= Time.deltaTime;
        move = Vector3.ClampMagnitude(move, playerSpeed);
        move = transform.TransformDirection(move);

        player.Move(move);
    }

    private void PlayerRotation()
    {
    #if (UNITY_EDITOR)
        rotationY += Input.GetAxis("Mouse X");
        rotationX -= Input.GetAxis("Mouse Y");
    #else
        rotationY += mobContrlRot.inputHorizontal();
        rotationX -= mobContrlRot.inputVertical();
    #endif

        
        rotationX = Mathf.Clamp(rotationX, minimumVert, maximumVert);
        transform.localEulerAngles = Vector3.up * rotationY  * rotationSpeed;
        cam.transform.localEulerAngles = Vector3.right * rotationX;
    }


    public void PlayerJump(bool button = false)
    {
        if (player.isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.Space) || button)
            {
                verticalSpeed = jumpSpeed;
            }
            else
            {
                verticalSpeed = minimumFall;
            }
        }
        else
        {
            verticalSpeed += gravity * 5 * Time.deltaTime;
            if (verticalSpeed < terminalVelocity) verticalSpeed = terminalVelocity;
        }        
    }
}
