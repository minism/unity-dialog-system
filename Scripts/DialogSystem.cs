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

      // Break the page into visible chunks.
      var chunks = dialogBox.PrepareChunks(fullText);
      foreach (var chunkText in chunks) {
        yield return CharacterRevealRoutine(chunkText, settings);
      }

      dialogBox.gameObject.SetActive(false);
    }

    private IEnumerator CharacterRevealRoutine(string chunkText, DialogSettings settings) {
      // Setup the chunk and wait until TMP processes it.
      yield return dialogBox.LoadChunkAsync(chunkText);

      // Progressively reveal characters.
      Debug.Log("Revealing");
      Tuple<char, bool> result;
      while (!(result = dialogBox.RevealCharacter()).Item2) {
        if (!char.IsWhiteSpace(result.Item1) && settings.printSound != null) {
          printAudioSource.PlayOneShot(settings.printSound);
        }
        yield return new WaitForSeconds(baseSettings.dialogSpeed);
      }
    }
  }

}