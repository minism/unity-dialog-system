using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem {

  public class DialogBox : MonoBehaviour {
    public TMPro.TMP_Text textComponent;
    public UnityEngine.UI.Image backgroundComponent;

    private string parsedChunk;

    public void SetBackgroundColor(Color color) {
      backgroundComponent.color = color;
    }

    public IEnumerator LoadChunkAsync(string chunkText) {
      textComponent.maxVisibleCharacters = 0;
      textComponent.text = chunkText;
      yield return new WaitUntil(() => (parsedChunk = textComponent.GetParsedText()).Length > 0);
    }

    // Given some input text, this function takes into account the size of the dialog box and
    // font and constructs an array of dialog chunks with line breaks in the appropriate place.
    public string[] PrepareChunks(string rawText) {
      //Debug.Log(textComponent.GetPreferredValues(rawText));
      return new string[] { rawText };
    }

    // Reveals and return the next character.  Returns true if at the end of the current chunk.
    public Tuple<char, bool> RevealCharacter() {
      if (parsedChunk.Length == 0) {
        throw new System.ArgumentException("Can't advance a dialog chunk before it is prepared.");
      }
      var finished = ++textComponent.maxVisibleCharacters >= parsedChunk.Length;
      var c = parsedChunk[textComponent.maxVisibleCharacters - 1];
      return Tuple.Create(c, finished);
    }
  }

}
