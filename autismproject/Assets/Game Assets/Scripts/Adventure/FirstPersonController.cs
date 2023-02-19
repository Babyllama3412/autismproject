using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour
{
	[Header("PLEASE SET PLAYER TAG")]
	[Space(20)]
	public float walkSpeed = 5;
	public float runSpeed = 10;
	public KeyCode runKey = KeyCode.LeftShift;

	[Header("PLEASE SET GROUND TAGS")]
	[Space(20)]
	public float jumpStrength = 5;
	public float gravity = -20;
	public KeyCode jumpKey = KeyCode.Space;

	[Space(20)]
	public float camSensitivity = 1;
	public float camSmoothing = 2;
	public float camSensitivityStick = 0.7f;
	public float camSmoothingStick = 1;

	[Space(20)]
	public bool allowMove = true;
	public bool allowJump = true;
	public bool allowLook = true;
	public bool pcMode = true;

	CharacterController cc;
	float speed;
	bool isGrounded = true;

	public Transform charCamera;
	public FixedJoystick moveJoystick;
	public FixedJoystick lookJoystick;

	Vector2 currentMouseLook;
	Vector2 appliedMouseDelta;
	Vector3 moveDirection = Vector3.zero;
	float yVel;
	bool activateMouse;

	public static FirstPersonController Instance;

	void Awake()
	{
		Instance = this;
	}

	void Start()
	{
		cc = GetComponent<CharacterController>();
	}
	void Update()
	{	
		if(Input.GetKeyDown(KeyCode.T)) activateMouse = !activateMouse;
		if(Input.GetMouseButtonDown(0) && activateMouse) activateMouse = false;
		if(pcMode)
		{
			Cursor.lockState = DialoguePanel.Instance.dialogeActive || GoodsManager.Instance.ranOut || activateMouse? CursorLockMode.None: CursorLockMode.Locked;
			Cursor.visible = DialoguePanel.Instance.dialogeActive || GoodsManager.Instance.ranOut || activateMouse? true : false;
		} else
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

		if(allowJump)
			Jump();
		if(allowLook && !activateMouse)
			Look();
		if(allowMove)
			Move();
	}

	void Move()
	{
		speed = Input.GetKey(runKey) ? runSpeed : walkSpeed;

		Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Press Left Shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        float curSpeedX = pcMode? speed * Input.GetAxis("Vertical") : speed * moveJoystick.Vertical;
        float curSpeedY = pcMode? speed * Input.GetAxis("Horizontal") : speed * moveJoystick.Horizontal;
        moveDirection = (forward * curSpeedX) + (right * curSpeedY);
		moveDirection.y = yVel;

        // Move the controller
        cc.Move(moveDirection * Time.deltaTime);
	}

	void Jump()
	{	
		isGrounded = cc.isGrounded;

		if(yVel > gravity + 1)
			yVel += gravity * Time.deltaTime;
		else
			yVel = gravity;
			
		if (Input.GetKeyDown(jumpKey) && isGrounded)
		{
           yVel = Mathf.Sqrt(jumpStrength * -2f * (gravity ));
		}
	}

	void Look()
	{
		Vector2 smoothMouseDelta =  pcMode? Vector2.Scale(new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")), Vector2.one * camSensitivity * camSmoothing):
		Vector2.Scale(new Vector2(lookJoystick.Horizontal, lookJoystick.Vertical), Vector2.one * camSensitivityStick * camSmoothingStick);
		appliedMouseDelta = Vector2.Lerp(appliedMouseDelta, smoothMouseDelta, 1 / camSmoothing);
		currentMouseLook += appliedMouseDelta;
		currentMouseLook.y = Mathf.Clamp(currentMouseLook.y, -90, 90);

		charCamera.localRotation = Quaternion.AngleAxis(-currentMouseLook.y, Vector3.right);
		transform.localRotation = Quaternion.AngleAxis(currentMouseLook.x, Vector3.up);
	}
	
}
