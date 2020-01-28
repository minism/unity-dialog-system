using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DialogSystem {

  public class DialogBox : MonoBehaviour {
    public TMPro.TMP_Text textComponent;
    public UnityEngine.UI.Image backgroundComponent;

    public void SetBackgroundColor(Color color) {
      backgroundComponent.color = color;
    }

    public void SetText(string text) {
      textComponent.text = text;
    }
  }

}
