using UnityEngine;
using Random = UnityEngine.Random;

public class PlayerAudio : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	
	[SerializeField] private AudioClip[] attackClips;
	[SerializeField] private AudioClip[] hittedClips;
	[SerializeField] private AudioClip[] deathClips;
	[SerializeField] private AudioClip[] footstepSounds;
	[SerializeField] private AudioClip[] dashSounds;
	[SerializeField] private AudioClip[] switchWorldSounds;

	private PlayerHealth health;
	private PlayerInput input;

	private void Awake()
	{
		health = GetComponent<PlayerHealth>();
		input = GetComponent<PlayerInput>();
	}

	private void Start()
	{
		input.WorldSwitched += PlaySwitchWorldClip;
		health.Hitted += PlayHittedClip;
		health.Dead += PlayDeathClip;
	}

	private void PlaySwitchWorldClip()
	{
		PlayRandomSound(switchWorldSounds);
	}
	
	public void PlayAttackClip()
	{
		PlayRandomSound(attackClips);
	}
	
	private void PlayHittedClip()
	{
		PlayRandomSound(hittedClips);
	}
	
	private void PlayDeathClip()
	{
		PlayRandomSound(deathClips);
	}
	
	public void PlayFootstepClip()
	{
		PlayRandomSound(footstepSounds);
	}

	public void PlayDashSound()
	{
		PlayRandomSound(dashSounds);
	}
	
	private void PlayRandomSound(AudioClip[] clips)
	{
		var clip = clips[Random.Range(0, clips.Length)];
		audioSource.PlayOneShot(clip);
	}
}
