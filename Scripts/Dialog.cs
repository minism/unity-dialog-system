using UnityEngine;

namespace DialogSystem {
  [CreateAssetMenu(menuName = "Dialog System/Dialog")]
  public class Dialog : ScriptableObject {
    [Tooltip("List of pages of text.")]
    [TextArea]
    public string[] pages;

    [Tooltip("If enabled, overrides the base dialog settings for this dialog.")]
    public bool useOverrideSettings;
    public DialogSettings overrideSettings;
  }

}
