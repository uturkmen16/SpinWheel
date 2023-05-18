using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace SpinWheel {

    public class MainMenuManager : MonoBehaviour
    {
        void Start()
        {
            this.transform.Find("Play_button").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("SpinWheelScreen");
            });
            this.transform.Find("Inventory_button").GetComponent<Button>().onClick.AddListener(() => {
                SceneManager.LoadScene("InventoryScreen");
            });
            this.transform.Find("Exit_button").GetComponent<Button>().onClick.AddListener(() => {
                Application.Quit();
            });
        }
    }

}