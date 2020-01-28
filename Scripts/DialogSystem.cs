using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DialogSystem {

  public class DialogSystem : MonoBehaviour {
    public DialogSettings baseSettings;

    [Tooltip("Words which are always displayed with a given color.")]
    public Dictionary<string, Color> colorDictionary;

    [Header("Dialog object references")]
    public DialogBox dialogBox;
    public AudioSource printAudioSource;

    public void ShowDialog(string text) {
      StartCoroutine(DialogRoutine(text));
    }

    private IEnumerator DialogRoutine(string text) {
      // TODO: Support per-dialog settings.
      var settings = baseSettings;

      // Setup global dialog box settings.
      dialogBox.gameObject.SetActive(true);
      dialogBox.SetBackgroundColor(settings.backgroundColor);

      // Character loop.
      var builder = new StringBuilder();
      for (int i = 0; i < text.Length; i++) {
        var character = text[i];
        builder.Append(character);
        dialogBox.SetText(builder.ToString());
        if (!char.IsWhiteSpace(character) && settings.printSound != null) {
          printAudioSource.PlayOneShot(settings.printSound);
        }
        yield return new WaitForSeconds(baseSettings.dialogSpeed);
      }
      dialogBox.gameObject.SetActive(false);
    }
  }

}