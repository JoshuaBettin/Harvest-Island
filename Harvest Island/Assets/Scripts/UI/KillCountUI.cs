using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class KillCountUI : MonoBehaviour
{
    [SerializeField]
    private GameObject killCountUIPrefab;
    [SerializeField]
    private Transform killCountPanel;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InstantiateKillCountUI(string playerName)
    {
        GameObject killCountUI = Instantiate(killCountUIPrefab, killCountPanel);
        killCountUI.transform.GetChild(0).GetComponent<TMP_Text>().text = playerName;
        killCountUI.transform.GetChild(1).GetComponent<TMP_Text>().text = 0 + "";
    }

    public void UpdateKillCountUI()
    {
        // killcount ++
    }
}
