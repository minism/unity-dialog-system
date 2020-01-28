using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleController : MonoBehaviour {
  [TextArea]
  public string exampleText;

  private void Start() {
    FindObjectOfType<DialogSystem.DialogSystem>().ShowDialog(exampleText);
  }
}