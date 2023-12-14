using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FirstPersonController : MonoBehaviour
{
    public bool CanMove { get;private set;} = true;
    private bool IsSprinting => canSprint && Input.GetKey(sprintKey) && !isCrouching;
    private bool ShouldJump => Input.GetKeyDown(jumpKey) && characterController.isGrounded && !isCrouching;
    private bool ShouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && characterController.isGrounded;
    private bool isWalking => !IsSprinting && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D));
  
    [Header("Function Options")]
    [SerializeField] private bool canSprint = true;
    [SerializeField] private bool canJump = true;
    public bool canCrouch = true;
    [SerializeField] private bool canUseHeadbob = true;
    [SerializeField] private bool WilSlideOnSlopes = true;

    [Header("Controls")]
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode jumpKey = KeyCode.Space;
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Movement Parameters")]
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 11f;
    [SerializeField] private float crouchspeed = 4f;
    [SerializeField] private float slopeSpeed = 10f;


    [Header("Look Parameters")]
    [SerializeField, Range(1, 10)] private float lookSpeedX = 2.0f;
    [SerializeField, Range(1, 10)] private float lookSpeedY = 2.0f;
    [SerializeField, Range(1, 180)] public float upperLookLimit = 80.0f;
    [SerializeField, Range(1, 180)] public float lowerLookLimit = 25.0f;

    [Header("Jumping Parameter")]
    [SerializeField] private float jumpForce = 8.0f;
    [SerializeField] private float gravity = 30.0f;

    [Header("Crouch Parameters")]
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float standingHeight = 3.8f;
    [SerializeField] private float timeToCrouch = 0.25f;
    [SerializeField] private Vector3 crouchingCenter = new Vector3(0, 1.17f, 0);
    [SerializeField] private Vector3 standingCenter = new Vector3(0, 2f, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;

    [Header("Headbob Parameters")]
    [SerializeField] private float walkBobSpeed = 10f;
    [SerializeField] private float walkBobAmount = 0.05f;
    [SerializeField] private float sprintBobSpeed = 18f;
    [SerializeField] private float sprintBobAmount = 0.11f;
    [SerializeField] private float crouchBobSpeed = 8f;
    [SerializeField] private float crouchBobAmount = 0.025f;
    private float defaultYPos = 0;
    private float timer;

    [Header("UI")]
    [SerializeField] private float stamina = 100f;
    [SerializeField] private float staminaMax = 100f;
    [SerializeField] private float Destamina = 25f;
    [SerializeField] private float Instamina = 9f;
    [SerializeField] private Slider staminaSlider;


    //slide
    private Vector3 hitPointNormal;
    private bool Issliding
    {
        get
        {
            Debug.DrawRay(transform.position, Vector3.down, Color.red);
            if(characterController.isGrounded && Physics.Raycast(transform.position, Vector3.down, out RaycastHit slopeHit, 3f))
            {
                hitPointNormal = slopeHit.normal;
                return Vector3.Angle(hitPointNormal, Vector3.up) > characterController.slopeLimit;
            }
            else
            {
                return false;
            }
        }
    }

    public Camera playerCamera;
    private CharacterController characterController;

    private Vector3 moveDirection;
    private Vector2 currentInput;

    private float rotationX = 0;

    private Animator animator;

    void Awake()
    {
        stamina = staminaMax;
        staminaSlider.GetComponent<Slider>().maxValue = staminaMax;
        staminaSlider.GetComponent<Slider>().value = stamina;
        animator = GetComponent<Animator>();
        playerCamera = GetComponentInChildren<Camera>();
        characterController = GetComponent<CharacterController>();
        defaultYPos = playerCamera.transform.localPosition.y;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        staminaSlider.GetComponent<Slider>().value = stamina;
        StaminaSystem();
        Debug.DrawRay(playerCamera.transform.position, Vector3.up, Color.red);
        if(CanMove)
        {
            HandleMovementInput();
            HandleMouseLook();

            if(canJump)
                HandleJump();

            if(canCrouch)
                HandleCrouch();

            if(canUseHeadbob)
                HandleHeadbob();

            ApplyFinalMovements();
            Animation();
        }
    }

    private void HandleMovementInput()
    {
        currentInput = new Vector2((isCrouching ? crouchspeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Vertical"), (isCrouching ? crouchspeed : IsSprinting ? sprintSpeed : walkSpeed) * Input.GetAxisRaw("Horizontal"));

        float moveDirectionY = moveDirection.y;
        moveDirection = (transform.TransformDirection(Vector3.forward) * currentInput.x) + (transform.TransformDirection(Vector3.right) * currentInput.y);
        moveDirection.y = moveDirectionY;
    }

    private void HandleMouseLook()
    {
        rotationX -= Input.GetAxis("Mouse Y") * lookSpeedY;
        rotationX = Mathf.Clamp(rotationX, -upperLookLimit, lowerLookLimit);
        playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeedX, 0);


    }

    private void HandleJump()
    {
        if(ShouldJump)
            moveDirection.y = jumpForce;
    }

    private void HandleCrouch()
    {
        if(ShouldCrouch)
            StartCoroutine(CrouchStand());

        if(isCrouching)
        {
            lowerLookLimit = 50f;
            playerCamera.transform.localPosition = new Vector3(
            0f,
            -0.8f,
            0.7f);
        }
        if(!isCrouching)
        {
            lowerLookLimit = 23f;
            playerCamera.transform.localPosition = new Vector3(
            0f,
            0f,
            0f);
        }
    }

    private void HandleHeadbob()
    {
        if(!characterController.isGrounded) return;

        if(Mathf.Abs(moveDirection.x) > 0.1f || Mathf.Abs(moveDirection.z) > 0.1f)
        {
            timer += Time.deltaTime * (isCrouching ? crouchBobSpeed : IsSprinting ? sprintBobSpeed : walkBobSpeed);
            
            if(isCrouching)
            {
                playerCamera.transform.localPosition = new Vector3(
                0f,
                -0.8f + Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                0.7f);
            }
            else
            {
                playerCamera.transform.localPosition = new Vector3(
                playerCamera.transform.localPosition.x,
                Mathf.Sin(timer) * (isCrouching ? crouchBobAmount : IsSprinting ? sprintBobAmount : walkBobAmount),
                playerCamera.transform.localPosition.z);
            }
        }
    }

    private void ApplyFinalMovements()
    {
        if(!characterController.isGrounded);
            moveDirection.y -= gravity * Time.deltaTime;

        if(moveDirection.y <= -15f)
            moveDirection.y = -15f;
        
        if(WilSlideOnSlopes && Issliding)
            moveDirection += new Vector3(hitPointNormal.x, -hitPointNormal.y, hitPointNormal.z) * slopeSpeed;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    private IEnumerator CrouchStand()
    {
        if(isCrouching && Physics.Raycast(playerCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standingHeight : crouchHeight;
        float currentHeight = characterController.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = characterController.center;

        while(timeElapsed < timeToCrouch)
        {
            characterController.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed/timeToCrouch);
            characterController.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed/timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        characterController.height = targetHeight;
        characterController.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    void Animation()
    {

        if(isWalking && !isCrouching) //Walk
        {
            animator.SetBool("IsWalking", true);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }

        if(IsSprinting && !isCrouching) //Sprint
        {
            animator.SetBool("IsRunning", true);
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }

        if(isCrouching && !isWalking &&!IsSprinting) //Crouch
        {
            animator.SetBool("IsCrouching", true);
        }
        if(!isCrouching)
        {

            animator.SetBool("IsCrouching", false);
        }
        if(isCrouching && isWalking && !IsSprinting) //CrouchMove
        { 
            animator.SetBool("IsCrouching", true);
            animator.SetBool("IsCrouchMove", true);
        }
        else
        {
            animator.SetBool("IsCrouchMove", false);
        }


    }

    void StaminaSystem()
    {

        if(stamina <= 0)
        {
            stamina = 0;
            canSprint = false;
        }

        if(stamina <= 10)
        {
            canSprint = false;
            StartCoroutine(WaitWalk());
        }
        if(IsSprinting)
        {
            stamina -= Destamina * Time.deltaTime;
        }

        if(stamina < staminaMax)
        {
            stamina += Instamina * Time.deltaTime;
        }
            

    }

    IEnumerator WaitWalk()
    {
        yield return new WaitForSeconds(1);
        if(stamina > 10)
        {
            canSprint = true;
        }
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(1);
    }
    


}
