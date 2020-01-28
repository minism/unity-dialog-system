using UnityEngine;
using UnityEngine.UI;

namespace DialogSystem {

  public class Blink : MonoBehaviour {
    public float blinkSpeed = 1;

    private Graphic graphic;
    private float t;

    private void Awake() {
      graphic = GetComponent<Graphic>();
    }

    private void Update() {
      if ((t += Time.deltaTime) > blinkSpeed) {
        t -= blinkSpeed;
        graphic.enabled = !graphic.enabled;
      }
    }
  }

}
