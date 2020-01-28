using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleController : MonoBehaviour {
  [TextArea]
  public string[] pages;

  private void Start() {
    FindObjectOfType<DialogSystem.DialogSystem>().ShowDialog(pages);
  }
}