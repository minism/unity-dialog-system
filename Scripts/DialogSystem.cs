using System;
using System.Collections;
using System.Text;
using UnityEngine;

namespace DialogSystem {

  public class DialogSystem : MonoBehaviour {
    public DialogSettings baseSettings;

    [Header("Dialog object references")]
    public DialogBox dialogBox;
    public AudioSource printAudioSource;

    public void ShowDialog(string text) {
      if (text.Length < 1) {
        Debug.LogWarning("DialogSystem: Ignoring empty text for ShowDialog()");
        return;
      }
      StartCoroutine(DialogRoutine(text));
    }

    private IEnumerator DialogRoutine(string fullText) {
      // TODO: Support per-dialog settings.
      var settings = baseSettings;

      // Setup global dialog box settings.
      dialogBox.gameObject.SetActive(true);
      dialogBox.SetBackgroundColor(settings.backgroundColor);

      // Apply any preprocessing to the text.
      fullText = TextUtil.PreprocessText(fullText, settings);

      // Start the page reveal routine.
      yield return PageRoutine(fullText, settings);

      dialogBox.gameObject.SetActive(false);
    }

    private IEnumerator PageRoutine(string pageText, DialogSettings settings) {
      // Setup the page and wait until TMP processes it.
      yield return dialogBox.LoadPageAsync(pageText);

      // Progressively reveal characters.
      Debug.Log("Revealing");
      Tuple<char, bool> result;
      while (!(result = dialogBox.Advance()).Item2) {
        if (!char.IsWhiteSpace(result.Item1) && settings.printSound != null) {
          printAudioSource.PlayOneShot(settings.printSound);
        }
        yield return new WaitForSeconds(baseSettings.dialogSpeed);
      }
    }
  }

}