using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem {

  [System.Serializable]
  public struct ColorEntry {
    public string text;
    public Color color;
  }

  [System.Serializable]
  public class DialogSettings {
    [Tooltip("Background color for the dialog.")]
    public Color backgroundColor = new Color(0.3f, 0.3f, 0.8f);

    [Tooltip("9-sliced frame image to use for the dialog.")]
    public Sprite frameSprite;

    [Tooltip("Whether to show the dialog frame.")]
    public bool showFrame = true;

    [Tooltip("The sound effect to play when printing a character.")]
    public AudioClip printSound;

    [Tooltip("The sound effect to play when confirming or advancing the dialog.")]
    public AudioClip confirmSound;

    [Tooltip("The dialog speed in characters per second.")]
    public float dialogSpeed = 0.1f;

    [Tooltip("The speed at which the next cursor blinks, in seconds.")]
    public float cursorBlinkSpeed = 0.5f;

    [Tooltip("Whether the character print sound should play for whitespace.")]
    public bool soundOnWhitespace = false;

    [Tooltip("Words which are always displayed with a given color.")]
    public ColorEntry[] colorDictionary;
  }

}