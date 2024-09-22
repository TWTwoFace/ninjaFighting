using UnityEngine;

public class EnemyAudio : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;

	[SerializeField] private AudioClip[] spawnClips;
	[SerializeField] private AudioClip[] attackClips;
	[SerializeField] private AudioClip[] hittedClips;
	[SerializeField] private AudioClip[] deathClips;
	[SerializeField] private AudioClip[] footstepSounds;

	private EnemyHealth health;

	private void Awake()
	{
		health = GetComponent<EnemyHealth>();
	}

	private void Start()
	{
		health.Damaged += PlayHittedClip;
		health.Dead += PlayDeathClip;
		
		PlayRandomSound(spawnClips);
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
	
	private void PlayRandomSound(AudioClip[] clips)
	{
		var clip = clips[Random.Range(0, clips.Length)];
		audioSource.PlayOneShot(clip);
	}
}