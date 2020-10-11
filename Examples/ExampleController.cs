using UnityEngine;

public class ExampleController : MonoBehaviour {
  public DialogSystem.Dialog[] dialogs;

  private void Start() {
    FindObjectOfType<DialogSystem.DialogSystem>().ShowDialog(dialogs[0]);
  }
}