using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UIElements;
using UnityEngine.Networking;
using Unity.VisualScripting;
using System;

public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI tmp;
    public TextMeshProUGUI notif;
    public TMP_InputField input;
    public GameObject submitButton;
    void Start(){
        tmp.text = "Score: " + PlayerPrefs.GetInt("Score").ToString();
    }
    public void Retry(){
        SceneManager.LoadScene(1);
    }
    public void MainM(){
        SceneManager.LoadScene(0);
    }

    public void Submit(){
        String toSubmit = input.text;
        if (toSubmit.Length > 20){
            notif.text = "Name too long!";
        }
        else if (toSubmit.Contains(",")){
            notif.text = "No commas in name";
        }
        else if (toSubmit == ""){
            notif.text = "Please enter a name";
        }
        else{
            submitButton.SetActive(false);
            StartCoroutine(AddScore(toSubmit));
        }
    }

    System.Collections.IEnumerator AddScore(string name){
        WWWForm form = new WWWForm();
        form.AddField("scoresetter", name);
        form.AddField("numCoins", PlayerPrefs.GetInt("Score"));

        using (UnityWebRequest www = UnityWebRequest.Post("http://localhost/DodgeTheBulletsBackend/InsertLeaderboard.php", form))
        {
            yield return www.SendWebRequest();
            if (www.result != UnityWebRequest.Result.Success)
            {
                notif.text = "Server Error";
                submitButton.SetActive(true);
            }
            else{
                notif.text = "Score Saved";
            }
        }
    }
}
