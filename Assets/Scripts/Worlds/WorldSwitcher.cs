using UnityEngine;

public class WorldSwitcher : MonoBehaviour
{
	[SerializeField] private PlayerInput playerInput;
	
	[SerializeField] private LayerMask normalWorldCullingMask;
	[SerializeField] private LayerMask shadowWorldCullingMask;
	
	private WorldType currentWorld;
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
		currentWorld = worldType;
		if (worldType == WorldType.Normal) 
			mainCamera.cullingMask = normalWorldCullingMask;
		else if (worldType == WorldType.Shadow) 
			mainCamera.cullingMask = shadowWorldCullingMask;
	}

	public void ToggleWorld()
	{
		if (currentWorld == WorldType.Normal) 
			SwitchWorld(WorldType.Shadow);
		else if (currentWorld == WorldType.Shadow) 
			SwitchWorld(WorldType.Normal);
	}
}

public enum WorldType
{
	Normal,
	Shadow,
}