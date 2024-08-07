using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class Leaderboard : MonoBehaviour
{

    public List<TextMeshProUGUI> ranks = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> names = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> coins = new List<TextMeshProUGUI>();
    public List<TextMeshProUGUI> dates = new List<TextMeshProUGUI>();

    public void Refresh()
    {
        for (int i = 0; i < 10; i++){
            ranks[i].text = "";
            names[i].text = "";
            coins[i].text = "";
            dates[i].text = "";
        }
        StartCoroutine(GetLeaderboard());
    }

    private void UpdateRow(int i, string[] data){
        ranks[i].text = data[0];
        names[i].text = data[1];
        coins[i].text = data[2];
        dates[i].text = data[3];
    }

    System.Collections.IEnumerator GetLeaderboard(){
        using (UnityWebRequest www = UnityWebRequest.Get("http://localhost/DodgeTheBulletsBackend/GetLeaderboard.php")){
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Error: " + www.error);
            }
            else
            {
                // Successfully received a response
                string responseText = www.downloadHandler.text;
                String[] entries = responseText.Split("\n");
                for (int i = 0; i < entries.Length - 1; i++){
                    string[] curRow = entries[i].Split(",");
                    UpdateRow(i, curRow);
                }
            }
        }
    }
}
