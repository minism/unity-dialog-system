using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleController : MonoBehaviour {
  private void Start() {
    FindObjectOfType<DialogSystem.DialogSystem>().ShowDialog("I really enjoy bananas.");
  }
}