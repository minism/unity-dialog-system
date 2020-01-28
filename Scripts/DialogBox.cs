using System;
using System.Collections;
using UnityEngine;

namespace DialogSystem {

  public class DialogBox : MonoBehaviour {
    public TMPro.TMP_Text textMesh;
    public UnityEngine.UI.Image frame;
    public UnityEngine.UI.Image background;
    public Blink nextCursor;

    private string parsedPageText;
    private TMPro.TMP_TextInfo textInfo;

    public void ApplySettings(DialogSettings settings) {
      background.color = settings.backgroundColor;
      frame.sprite = settings.frameSprite;
      nextCursor.blinkSpeed = settings.cursorBlinkSpeed;
    }

    public void ShowNextCursor() {
      nextCursor.gameObject.SetActive(true);
    }

    public void HideNextCursor() {
      nextCursor.gameObject.SetActive(false);
    }

    public IEnumerator LoadPageAsync(string pageText) {
      textMesh.maxVisibleCharacters = 0;
      textMesh.text = pageText;
      textMesh.pageToDisplay = 1;

      // Wait for TMP to process the page text.
      yield return new WaitUntil(
          () => (textInfo = textMesh.GetTextInfo(pageText)).characterCount > 0);
      parsedPageText = textMesh.GetParsedText();
    }

    // Reveals and return the next character.  Returns true if at the end of the current page.
    public Tuple<char, bool> Advance() {
      if (textInfo == null || textInfo.characterCount == 0) {
        throw new ArgumentException("Can't advance a dialog chunk before it is prepared.");
      }
      var finished = ++textMesh.maxVisibleCharacters >= textInfo.characterCount;
      var c = parsedPageText[textMesh.maxVisibleCharacters - 1];

      // Check if we need to flip to the next page.
      if (textMesh.maxVisibleCharacters >
          textInfo.pageInfo[textMesh.pageToDisplay - 1].lastCharacterIndex + 1) {
        textMesh.pageToDisplay++;
      }

      return Tuple.Create(c, finished);
    }
  }

}
