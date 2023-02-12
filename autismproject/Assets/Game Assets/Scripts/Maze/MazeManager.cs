using System.Linq;
using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

public class MazeManager : MonoBehaviour
{
	[InfoBox("MAKE SURE YOUR PLAYER HAS TAG \"PLAYER\"")]
	[Required][SerializeField] MazeSpawner mazeSpawner;
	[Required][SerializeField] GameObject mazeGoalPrefab;
	[Required][SerializeField] Material mazeGoalMaterial;
	[Required][SerializeField] Camera mazeCamera;
	
	[HorizontalLine(height: 7,color:EColor.Black)]
	[Required][SerializeField] Rigidbody player;
	[MinValue(0)] public float playerSpeed = 2;
	[Required][SerializeField] FloatingJoystick joystick;
	public bool allowMovement;
	
	[HorizontalLine(height: 7,color:EColor.Black)]
	public UnityEvent OnReachEnd;
	[HorizontalLine(height: 7,color:EColor.Black)]
	
	public MazeDifficulty[] difficultyList;
	[Required][Dropdown("GetDifficulties")] public MazeDifficulty currentDifficulty;
	
	MazeGoal mazeGoal;
	bool reachedEnd;
	bool reachedEndUpdate = true;
	Vector2 joystickPosition;

	void Start()
	{
		player.GetComponent<MeshRenderer>().enabled = false;
	}


	public void StartGame(int index)
	{
		currentDifficulty = GetDifficulties()[index];
		InitDimensions();

		mazeSpawner.Generate();
		SetupEndGoal();
		CameraSetup();

		player.GetComponent<MeshRenderer>().enabled = true;
	}

	void Update()
	{
		joystickPosition = new Vector2(joystick.Horizontal, joystick.Vertical);
		
		MazeEndUpdate();
	}
	void FixedUpdate()
	{
		if(allowMovement)
			player.velocity = new Vector3(joystickPosition.x,0,joystickPosition.y)
			* Time.fixedDeltaTime * playerSpeed;
	}
	
	void MazeEndUpdate()
	{
		if(reachedEndUpdate)
		{
			reachedEnd = mazeGoal? mazeGoal.finished : false;
			if(reachedEnd)
			{
				OnReachEnd.Invoke();
				reachedEndUpdate = false;
			}
		}
	}
	
	void SetupEndGoal()
	{
		mazeGoal = Instantiate(mazeGoalPrefab.gameObject, 
					new Vector3((currentDifficulty.rows-1)*mazeSpawner.CellWidth,
								0,
								(currentDifficulty.columns-1)*mazeSpawner.CellHeight), Quaternion.identity).
								GetComponent<MazeGoal>();
								
		mazeSpawner.transform.GetComponentsInChildren<Transform>().
		ToList<Transform>().FindLast(x=>x.name.Contains("Floor")).
		GetComponent<MeshRenderer>().material = mazeGoalMaterial;
	}
	
	void CameraSetup()
	{
		mazeCamera.transform.position = new Vector3(((currentDifficulty.rows-1)*mazeSpawner.CellWidth),
													30*2,
													((currentDifficulty.columns-1)*mazeSpawner.CellHeight))/2;
		mazeCamera.orthographicSize = currentDifficulty.rows * 3;
	}
	
	void InitDimensions()
	{
		mazeSpawner.Rows = currentDifficulty.rows;
		mazeSpawner.Columns = currentDifficulty.columns;
	}
	
	MazeDifficulty[] GetDifficulties() => difficultyList;	
}