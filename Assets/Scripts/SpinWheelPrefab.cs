using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SpinWheel {

    public class SpinWheelPrefab : MonoBehaviour
    {
        [SerializeField]
        GameObject objectToSpin;

        public delegate void RotationOverCallback();

        public void RotateWheel(RotationOverCallback callback, int randomInt) {
            objectToSpin.transform.DORotate(new Vector3(0, 0, randomInt * 45), 1 + randomInt / 5, RotateMode.LocalAxisAdd).OnComplete(() => {
                callback();
            });
        }
    }

}
