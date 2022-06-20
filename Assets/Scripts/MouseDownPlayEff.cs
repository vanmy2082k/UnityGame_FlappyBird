using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDownPlayEff : MonoBehaviour {

    private void OnMouseDown()
    {
        Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = 0;

        GamePlayController.instance.playEff(0, target);
    }
}
