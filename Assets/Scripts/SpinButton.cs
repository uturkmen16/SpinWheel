using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SpinButton : MonoBehaviour
{
    [SerializeField]
    private Transform rotateTransform;
    // Start is called before the first frame update

    public void SpinWheel() {
        int randomInt = UnityEngine.Random.Range(0, 500);

        //Clamping
        randomInt -= randomInt % 45;
                
        rotateTransform.DORotate(new Vector3(0, 0, randomInt), 1, RotateMode.LocalAxisAdd);
        //GetComponent<Button>().interactable = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
