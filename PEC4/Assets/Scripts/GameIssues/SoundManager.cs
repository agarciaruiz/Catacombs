using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager soundManager;

    //public List<AudioClip> sfxSounds;
    public AudioClip button, sword, chest, enemy_died, player_died, boss_appear,
        boss_dissapear, boss_attack, boss_died, fireball;
    public AudioClip menu_ost, level_ost, boss_ost;
    public GameObject soundObj;
    public GameObject currentMusicObject;

    private void Awake()
    {
        soundManager = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void PlaySfx(string sfxName)
    {
        switch (sfxName)
        {
            case "button":
                SoundObjectCreation(button);
                break;
            case "sword":
                SoundObjectCreation(sword);
                break;
            case "chest":
                SoundObjectCreation(chest);
                break;
            case "enemy_died":
                SoundObjectCreation(enemy_died);
                break;
            case "player_died":
                SoundObjectCreation(player_died);
                break;
            case "boss_appear":
                SoundObjectCreation(boss_appear);
                break;
            case "boss_disappear":
                SoundObjectCreation(boss_dissapear);
                break;
            case "boss_attack":
                SoundObjectCreation(boss_attack);
                break;
            case "boss_died":
                SoundObjectCreation(boss_died);
                break;
            case "fireball":
                SoundObjectCreation(fireball);
                break;
            default:
                break;
        }
    }

    private void SoundObjectCreation(AudioClip clip)
    {
        GameObject newObj = Instantiate(soundObj, transform);
        newObj.GetComponent<AudioSource>().clip = clip;
        newObj.GetComponent<AudioSource>().Play();
    }

    public void PlayMusic(string musicName)
    {
        switch (musicName)
        {
            case "menu_ost":
                MusicObjectCreation(menu_ost);
                break;
            case "level_ost":
                MusicObjectCreation(level_ost);
                break;
            case "boss_ost":
                MusicObjectCreation(boss_ost);
                break;
            default:
                break;
        }
    }

    private void MusicObjectCreation(AudioClip clip)
    {
        if (currentMusicObject)
        {
            Destroy(currentMusicObject);
        }
        currentMusicObject = Instantiate(soundObj, transform);
        currentMusicObject.GetComponent<AudioSource>().clip = clip;
        currentMusicObject.GetComponent<AudioSource>().loop = true;
        currentMusicObject.GetComponent<AudioSource>().Play();
    }
}
