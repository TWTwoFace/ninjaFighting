using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
	[SerializeField] private AudioSource audioSource;
	
	[SerializeField] private AudioClip[] attackClips;
	[SerializeField] private AudioClip[] hittedClips;
	[SerializeField] private AudioClip[] deathClips;
	[SerializeField] private AudioClip[] footstepSounds;

	public void PlayAttackClip()
	{
		PlayRandomSound(attackClips);
	}
	
	public void PlayHittedClip()
	{
		PlayRandomSound(hittedClips);
	}
	
	public void PlayDeathClip()
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
