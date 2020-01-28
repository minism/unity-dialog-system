using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace DialogSystem {

  public class DialogSystem : MonoBehaviour {
    public DialogSettings baseSettings;
    public DialogBox dialogBox;

    [Tooltip("Words which are always displayed with a given color.")]
    public Dictionary<string, Color> colorDictionary;

    public void ShowDialog(string text) {
      StartCoroutine(DialogRoutine(text));
    }

    private IEnumerator DialogRoutine(string text) {
      // Setup dialog box color and position.
      dialogBox.gameObject.SetActive(true);
      dialogBox.SetBackgroundColor(baseSettings.backgroundColor);

      // Character loop.
      var builder = new StringBuilder();
      for (int i = 0; i < text.Length; i++) {
        builder.Append(text[i]);
        dialogBox.SetText(builder.ToString());
        yield return new WaitForSeconds(baseSettings.dialogSpeed);
      }
      dialogBox.gameObject.SetActive(false);
    }
  }

}