using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<LevelSettings> levelSettings;
    // Start is called before the first frame update
    void Start() {
        //Bronze - 0, Silver - 1, Gold - 2
        int levelType = 0;

        levelSettings[levelType].ShuffleRewards();

        GameObject instance = Instantiate(levelSettings[levelType].Prefab, transform);
        Transform baseTransform = instance.transform.GetChild(0);

        for(int i = 0; i < baseTransform.childCount; i++) {
            Transform childTransform = baseTransform.GetChild(i);
            childTransform.GetComponentInChildren<Image>().sprite = levelSettings[levelType].Rewards[i].itemType.itemIcon;
            int amount = levelSettings[levelType].Rewards[i].amount;
            string suffix = "";
            if(amount > 999999) {
                //Million
                amount /= 1000000;
                suffix = "M";
            }
            else if(amount > 999) {
                //Thousand
                amount /= 1000;
                suffix = "K";
            }
            childTransform.GetComponentInChildren<TextMeshProUGUI>().text = "x" + amount.ToString() + suffix;
        }
    }
    // Update is called once per frame
    void Update() {
        
    }
}
