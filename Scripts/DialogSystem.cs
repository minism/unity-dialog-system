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

    public bool IsDialogActive {
      get {
        return state != State.OFF;
      }
    }

    // Dialog state.
    private enum State {
      OFF,
      ON,
      WAITING_FOR_INPUT,
    }
    private State state = State.OFF;

    private void Awake() {
      dialogBox.gameObject.SetActive(false);
    }

    public void ShowDialog(string text) {
      ShowDialog(new string[] { text });
    }

    public void ShowDialog(IList<string> pages, DialogSettings settings = null) {
      if (settings == null) {
        settings = baseSettings;
      }
      if (state != State.OFF) {
        Debug.LogWarning("Dialog already active, ignoring (Did you forget to check IsDialogActive?).");
        return;
      }
      if (pages.Count < 1 || pages[0].Length < 1) {
        Debug.LogWarning("DialogSystem: Ignoring empty text for ShowDialog()");
        return;
      }
      StartCoroutine(DialogRoutine(pages, settings));
    }

    private IEnumerator DialogRoutine(IList<string> pages, DialogSettings settings) {
      state = State.ON;
      dialogBox.gameObject.SetActive(true);

      // Apply visual settings to the dialog box.
      dialogBox.ApplySettings(settings);

      // Apply any preprocessing to the text.
      pages = pages.Select(p => TextUtil.PreprocessText(p, settings)).ToArray();

      // Start the page reveal routine for each page.
      foreach (var pageText in pages) {
        dialogBox.HideNextCursor();
        yield return PageRoutine(pageText, settings);

        // Wait until we've consumed an input to advance the page.
        dialogBox.ShowNextCursor();
        state = State.WAITING_FOR_INPUT;
        yield return new WaitUntil(() => Input.GetButton(advanceButtonName));
        state = State.ON;

        if (settings.confirmSound != null) {
          audioSource.PlayOneShot(settings.confirmSound);
        }
      }

      dialogBox.gameObject.SetActive(false);
      state = State.OFF;
    }

    private IEnumerator PageRoutine(string pageText, DialogSettings settings) {
      // Setup the page and wait until TMP processes it.
      yield return dialogBox.LoadPageAsync(pageText);

      // Progressively reveal characters.
      Tuple<char, bool> result;
      while (!(result = dialogBox.Advance()).Item2) {
        if (settings.printSound != null) {
          if (settings.soundOnWhitespace || !char.IsWhiteSpace(result.Item1)) {
            audioSource.PlayOneShot(settings.printSound);
          }
        }
        yield return new WaitForSeconds(baseSettings.dialogSpeed);
      }
    }
  }

}