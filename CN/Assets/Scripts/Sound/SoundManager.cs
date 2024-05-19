
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;

public enum SoundEffects
{
	Scanner_Buzz,
	PC_Startup,
	Keyboard_Typing,
	UI_Confirm,
	UI_Select,
	UI_Pause,
	Printing

};

public class SoundManager : MonoBehaviour
{
	public static SoundManager main;

	[SerializeField] private AudioMixer musicMixer = null;
	[SerializeField] private AudioMixer sfxMixer = null;
	[SerializeField] private AudioMixer ambienceMixer = null;
	
	public float musicVolume;
	public float sfxVolume;
	public float ambienceVolume;
	
	[Space] [Header("Music")] 
	public AudioClip[] MainGameMusic;
	public AudioClip currentMusic;
	
	
	[Space] [Header("Ambience")] 
	public AudioClip Office;
	public AudioClip[] Town;
	public AudioClip currentTownAmbience;
	public float fadeDuration;

	
	[SerializeField] private bool isInMainMenuScene;
	[Space] [Header("Sounds")] 
	public AudioClip Scanner_Buzz;
	public AudioClip PC_Startup;
	public AudioClip Keyboard_Typing;
	public AudioClip[] UI_Confirm;
	public AudioClip[] UI_Select;
	public AudioClip UI_Pause;
	public AudioClip Printing;


	public AudioSource musicSource;
	public AudioSource sfxSource;
	public AudioSource ambienceSource;

	private EconomyManager econManager;


	private bool canPlay=true;
	
	#region Properties

	public float MusicVolume
	{
		get => musicVolume;
		set => musicVolume = value;
	}

	public float SfxVolume
	{
		get => sfxVolume;
		set => sfxVolume = value;
	}

	public AudioMixer MusicMixer
	{
		get => musicMixer;
	}

	public AudioMixer SfxMixer
	{
		get => sfxMixer;
	}

	#endregion

	private void Awake()
	{
		main = this;
		getMixerVolumes();
	}

	private void Start()
	{
		StartCoroutine(musicLoop(isInMainMenuScene));
	}

	private void getMixerVolumes()
	{
		musicMixer.GetFloat("MusicVolume", out musicVolume);
		sfxMixer.GetFloat("SfxVolume", out sfxVolume);
		ambienceMixer.GetFloat("ambienceVolume", out sfxVolume);
	}

	private void Update()
	{
		//if (Input.GetMouseButtonDown(0)) PlaySoundEffect(SoundEffects.UI_Select);
	}

	public void PlaySoundEffect(SoundEffects effect)
	{
		if (canPlay)
		{
			AudioClip clip=UI_Confirm[0];//dummy clip that will definitely not play

			switch (effect)
			{
				case SoundEffects.Scanner_Buzz:
					clip = Scanner_Buzz;
					//sfxSource.PlayOneShot(Scanner_Buzz);
					break;
				case SoundEffects.PC_Startup:
					clip = PC_Startup;
					//sfxSource.PlayOneShot(PC_Startup);
					break;
				case SoundEffects.Keyboard_Typing:
					clip = Keyboard_Typing;
					//sfxSource.PlayOneShot(Keyboard_Typing);
					break;
				case SoundEffects.Printing:
					clip = Printing;
					//sfxSource.PlayOneShot(Printing);
					break;
				case SoundEffects.UI_Select:
					clip = UI_Select[Random.Range(0, UI_Select.Length - 1)];
					break;
				case SoundEffects.UI_Confirm:
					clip = UI_Confirm[Random.Range(0, UI_Confirm.Length - 1)];
					break;
				case SoundEffects.UI_Pause:
					clip = UI_Pause;
					//sfxSource.PlayOneShot(UI_Pause);
					break;


				default: break;
			}

			if (canPlay)
			{
				sfxSource.PlayOneShot(clip);
				StartCoroutine(WaitForSFXToPlay(clip));
			}

		}
	}

	IEnumerator WaitForSFXToPlay(AudioClip clip)
	{
		canPlay = false;
		yield return new WaitForSeconds(clip.length/2);
		canPlay = true;
	}
	

	IEnumerator musicLoop(bool mainMenu)
	{
		int current = 0;
		if (!mainMenu)
		{
			yield return new WaitForSeconds(1);
		}
		while (gameObject.activeSelf)
		{
			if (!musicSource.isPlaying)
			{
				if (!mainMenu)
				{
					current++;
					if (current >= MainGameMusic.Length)
					{
						current = 0;
					}
					musicSource.clip = currentMusic;
				}
				else
				{
					//musicSource.clip = MainMenuMusic;
				}

				musicSource.Play();
			}
			yield return new WaitForEndOfFrame();
		}
	}
	
	public void PlayAmbientNoise(AudioClip ambientSound)
	{
		//Check if an audio is playing
		if (ambienceSource.isPlaying)
		{
			AudioClip currentClip = ambienceSource.clip;
			//Check if the clip we want to play is different
			if(currentClip == ambientSound)
			{
				return; //If clip is the same don't do anything
			}
			else
			{
                // Fade out the current sound, set the new clip, and fade in the new sound
                DOTween.To(() => ambienceSource.volume, x => ambienceSource.volume = x, 0f, fadeDuration).OnComplete(() =>
                {
                    ambienceSource.Stop();
                    ambienceSource.clip = ambientSound;
                    ambienceSource.Play();
                    DOTween.To(() => ambienceSource.volume, x => ambienceSource.volume = x, 1f, fadeDuration);
                });
			}
		}
		else
		{
            // Set the clip, start playing with volume set to 0, and fade in
            ambienceSource.clip = ambientSound;
            ambienceSource.volume = 0f;
            ambienceSource.Play();
            DOTween.To(() => ambienceSource.volume, x => ambienceSource.volume = x, 1f, fadeDuration);
		}
	}
	
	//function to set music. Town Calculate state calls this function.
	public void DetermineMusic(AudioClip music)
	{
		currentMusic = music;
	}
	public void DetermineTownAmbience(AudioClip ambience)
	{
		currentTownAmbience = ambience;
	}

	public void PlaySFXClip(AudioClip clip)
	{
		
		if (canPlay)
		{
			sfxSource.PlayOneShot(clip);
			StartCoroutine(WaitForSFXToPlay(clip));
		}
	}

	public void PlayConfirmSFX()
	{
		PlaySoundEffect(SoundEffects.UI_Confirm);
	}

	public void PlaySelectSFX()
	{
			PlaySoundEffect(SoundEffects.UI_Select);
	}
}
