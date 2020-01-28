using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DialogSystem {

  public class DialogSystem : MonoBehaviour {
    public DialogSettings baseSettings;

    [Tooltip("The name of the input action which advances / accepts dialogs.")]
    public string advanceButtonName = "Jump";

    [Header("Dialog object references")]
    public DialogBox dialogBox;
    public AudioSource audioSource;

    // Advance page input state.
    private bool waitingForAdvanceInput;
    private bool pendingAdvanceInput;

    private void Update() {
      if (waitingForAdvanceInput && Input.GetButton(advanceButtonName)) {
        pendingAdvanceInput = true;
      }
    }

    public void ShowDialog(string text) {
      ShowDialog(new string[] { text });
    }

    public void ShowDialog(IList<string> pages) {
      if (pages.Count < 1 || pages[0].Length < 1) {
        Debug.LogWarning("DialogSystem: Ignoring empty text for ShowDialog()");
        return;
      }
      StartCoroutine(DialogRoutine(pages));
    }

    private IEnumerator DialogRoutine(IList<string> pages) {
      dialogBox.gameObject.SetActive(true);

      // TODO: Support per-dialog settings.
      var settings = baseSettings;
      dialogBox.ApplySettings(settings);

      // Apply any preprocessing to the text.
      pages = pages.Select(p => TextUtil.PreprocessText(p, settings)).ToArray();

      // Start the page reveal routine for each page.
      foreach (var pageText in pages) {
        dialogBox.HideNextCursor();
        yield return PageRoutine(pageText, settings);

        // Wait until we've consumed an input to advance the page.
        dialogBox.ShowNextCursor();
        waitingForAdvanceInput = true;
        yield return new WaitUntil(() => pendingAdvanceInput);
        waitingForAdvanceInput = pendingAdvanceInput = false;

        if (settings.confirmSound != null) {
          audioSource.PlayOneShot(settings.confirmSound);
        }
      }

      dialogBox.gameObject.SetActive(false);
    }

    private IEnumerator PageRoutine(string pageText, DialogSettings settings) {
      // Setup the page and wait until TMP processes it.
      yield return dialogBox.LoadPageAsync(pageText);

      // Progressively reveal characters.
      Tuple<char, bool> result;
      while (!(result = dialogBox.Advance()).Item2) {
        if (!char.IsWhiteSpace(result.Item1) && settings.printSound != null) {
          audioSource.PlayOneShot(settings.printSound);
        }
        yield return new WaitForSeconds(baseSettings.dialogSpeed);
      }
    }
  }

}