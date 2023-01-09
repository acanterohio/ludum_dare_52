using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioSource menuTheme, introTheme;
    [SerializeField] private List<AudioSource> gameThemes;
    private AudioSource current;
    private bool playingGameThemes;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void PlayMenu()
    {
        StopAll();
        menuTheme.Play();
    }

    public void PlayIntro()
    {
        StopAll();
        introTheme.Play();
    }

    public void PlayGameTheme()
    {
        StopAll();
        playingGameThemes = true;
        StartCoroutine(PlayGameThemes());
    }

    private IEnumerator PlayGameThemes()
    {
        while (playingGameThemes)
        {
            current = gameThemes[Random.Range(0, gameThemes.Count)];
            current.Play();
            while (current.isPlaying && playingGameThemes)
            {
                yield return null;
            }
            current.Stop();
        }
    }

    private void StopAll()
    {
        playingGameThemes = false;
        menuTheme.Stop();
        introTheme.Stop();
        foreach (var a in gameThemes)
        {
            a.Stop();
        }
    }
}
