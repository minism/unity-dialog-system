using UnityEngine;

namespace DialogSystem {

  [System.Serializable]
  public class DialogSettings {
    [Tooltip("Background color for the dialog.")]
    public Color backgroundColor = new Color(0.3f, 0.3f, 0.8f);

    [Tooltip("The sound effect to play when printing a character")]
    public AudioClip printSound;

    [Tooltip("The dialog speed in characters per second.")]
    public float dialogSpeed = 0.25f;

    [Tooltip("Whether the character print sound should play for whitespace.")]
    public bool soundOnWhitespace = false;
  }

}