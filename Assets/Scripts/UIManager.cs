using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<LevelSettings> levelSettings;
    private GameObject currentLevelInstance;
    
    //Type of the current level such as: Bronze - 0, Silver - 1, Gold - 2
    private int currentLevelType;

    //Level number that increases each level starting from level 1
    private int currentLevelNo;
    void Start() {

        int currentLevelNo = 1;

        int currentLevelType = 0;

        InitLevel(currentLevelType);
    }

    void Update() {
    }

    private void InitLevel(int levelType) {

        levelSettings[levelType].ShuffleRewards();

        currentLevelInstance = Instantiate(levelSettings[levelType].Prefab, transform);
        Transform baseTransform = currentLevelInstance.transform.GetChild(0);

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

        //Add spin button callback
        currentLevelInstance.GetComponentInChildren<Button>().onClick.AddListener(SpinWheel);
    }

    private void SpinAnimationOverCallback() {
        currentLevelNo++;
        Destroy(currentLevelInstance);
        InitLevel(currentLevelType);
    }

    public void SpinWheel() {
        int randomInt = UnityEngine.Random.Range(levelSettings[currentLevelType].MinimumRoll, levelSettings[currentLevelType].MaximumRoll);
        currentLevelInstance.transform.GetChild(0).transform.DORotate(new Vector3(0, 0, randomInt * 45), 1 + randomInt / 5, RotateMode.LocalAxisAdd)
        .OnComplete(SpinAnimationOverCallback);
        transform.GetChild(0).GetComponentInChildren<Button>().interactable = false;
    }

    private void getLevelType(int currentLevelNo) {}

}
