using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelNum : MonoBehaviour
{

    public SpriteRenderer numSprite;

    public Sprite[] digits;

    public static string levelKey = "current_level";

    public static int GetCurrentLevel()
    {
        return PlayerPrefs.GetInt(levelKey, 1);
    }
    // Use this for initialization
    void Start()
    {
        UpdateLevelNum(GetCurrentLevel());
    }

    private void UpdateLevelNum(int v)
    {
        if (v > 9)
        {
            numSprite.sprite = digits[v % 10];
        }
        else
        {
            numSprite.sprite = digits[v];
        }
    }

    public void AddLevel()
    {
        int level = PlayerPrefs.GetInt(levelKey, 1);
        PlayerPrefs.SetInt(levelKey, ++level);
        UpdateLevelNum(level);
    }

    public void DecLevel()
    {
        int level = PlayerPrefs.GetInt(levelKey, 1);
        PlayerPrefs.SetInt(levelKey, --level);
        UpdateLevelNum(level);
    }
}
