using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpinWheel {

    public class InventoryScreenManager : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            transform.Find("Button_back").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("MainMenuScreen");
            });
        }
    }
    
}
