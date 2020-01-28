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

    [Tooltip("The sound effect to play when printing a character")]
    public AudioClip printSound;

    [Tooltip("The dialog speed in characters per second.")]
    public float dialogSpeed = 0.1f;

    [Tooltip("Whether the character print sound should play for whitespace.")]
    public bool soundOnWhitespace = false;

    [Tooltip("Words which are always displayed with a given color.")]
    public ColorEntry[] colorDictionary;
  }

}