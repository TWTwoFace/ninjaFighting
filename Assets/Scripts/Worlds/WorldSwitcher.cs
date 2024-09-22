using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput;
	
	[SerializeField] private LayerMask normalWorldCullingMask;
	[SerializeField] private LayerMask shadowWorldCullingMask;
	
	public static WorldType CurrentWorld { get; private set; }
	
	private Camera mainCamera;

	private void Awake()
	{
		mainCamera = Camera.main;
		SwitchWorld(WorldType.Normal);
	}

	private void Start()
	{
		playerInput.WorldSwitched += ToggleWorld;
	}

	public void SwitchWorld(WorldType worldType)
	{
		CurrentWorld = worldType;
		if (worldType == WorldType.Normal) 
			mainCamera.cullingMask = normalWorldCullingMask;
		else if (worldType == WorldType.Shadow) 
			mainCamera.cullingMask = shadowWorldCullingMask;
	}

	public void ToggleWorld()
	{
		if (CurrentWorld == WorldType.Normal) 
			SwitchWorld(WorldType.Shadow);
		else if (CurrentWorld == WorldType.Shadow) 
			SwitchWorld(WorldType.Normal);
	}
}

public enum WorldType
{
	Normal,
	Shadow,
}