
using System;
using System.Collections;
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
	
	
	[Space] [Header("Ambience")] 
	public AudioClip Office;
	public AudioClip[] Town;

	
	[SerializeField] private bool isInMainMenuScene;
	[Space] [Header("Sounds")] 
	public AudioClip Scanner_Buzz;
	public AudioClip PC_Startup;
	public AudioClip Keyboard_Typing;
	public AudioClip UI_Confirm;
	public AudioClip UI_Select;
	public AudioClip UI_Pause;
	public AudioClip Printing;


	public AudioSource musicSource;
	public AudioSource sfxSource;
	public AudioSource ambienceSource;

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
		
		switch (effect)
		{
			case SoundEffects.Scanner_Buzz:
				sfxSource.PlayOneShot(Scanner_Buzz);
				break;
			case SoundEffects.PC_Startup:
				sfxSource.PlayOneShot(PC_Startup);
				break;
			case SoundEffects.Keyboard_Typing:
				sfxSource.PlayOneShot(Keyboard_Typing);
				break;
			case SoundEffects.Printing:
				sfxSource.PlayOneShot(Printing);
				break;
			case SoundEffects.UI_Select:
				sfxSource.PlayOneShot(UI_Select);
				break;
			case SoundEffects.UI_Confirm:
				sfxSource.PlayOneShot(UI_Confirm);
				break;
			case SoundEffects.UI_Pause:
				sfxSource.PlayOneShot(UI_Pause);
				break;
			
			default: break;
		}
	}

	IEnumerator musicLoop(bool mainMenu)
	{
		int current = 0;
		if (!mainMenu)
		{
			yield return new WaitForSeconds(5);
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
					musicSource.clip = MainGameMusic[Random.Range(0,MainGameMusic.Length)];
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
}
