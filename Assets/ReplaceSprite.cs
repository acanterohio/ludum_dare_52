using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReplaceSprite : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite newSprite;

    public void Replace()
    {
        image.sprite = newSprite;
        GameObject.Find("SoundManager").GetComponent<MusicManager>().PlayGameTheme();
    }
}
