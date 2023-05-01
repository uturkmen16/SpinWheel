using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    List<LevelSettings> levelSettings;
    // Start is called before the first frame update
    void Start()
    {
        GameObject instance = Instantiate(levelSettings[0].Prefab, this.transform);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
