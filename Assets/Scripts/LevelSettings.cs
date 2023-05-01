using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu(menuName = "SpinWheel/LevelSettings")]
public class LevelSettings : ScriptableObject
{
    [Serializable]
    private struct SlotValue {
        public ItemType itemtype;
        public int amount;
    }

    const int size = 8;
    [SerializeField] private String topText;
    [SerializeField] private String bottomText;
    [SerializeField] private Sprite baseSprite;
    [SerializeField] private Sprite indicatorSprite;
    [SerializeField] private Sprite buttonSprite;
    [SerializeField] private SlotValue[] rewards = new SlotValue[size];

    void OnValidate()
    {
        if (rewards.Length != size) {
            Debug.LogWarning("Don't change the rewards array size!");
            Array.Resize(ref rewards, size);
        }

        for(int i = 0; i < rewards.Length; i++) {
            if(rewards[i].amount < 1) {
                rewards[i].amount = 1;
            }
        } 
    }
}