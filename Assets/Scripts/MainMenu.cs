using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text energyText;
    [SerializeField] Button playButton;

    [SerializeField] AndroidNotificationHandler androidNotificationHandler;

    [SerializeField] int maxEnergy = 5;
    [SerializeField] int energyRechargeDuration = 1;

    const string EnergyKey = "Energy";
    const string EnergyReadyKey = "EnergyReady";

    int energy;


    void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
            return;

        CancelInvoke();

        int highScore = PlayerPrefs.GetInt(ScoreSystem.HighScoreKey, 0);
        // Debug.Log($"High Score {highScore}");

        energy = PlayerPrefs.GetInt(EnergyKey, maxEnergy);
        if (energy == 0)
        {
            string EnergyReadyString = PlayerPrefs.GetString(EnergyReadyKey, string.Empty);

            if (EnergyReadyString == string.Empty)
                return;

            DateTime energyReady = DateTime.Parse(EnergyReadyString);
            if(DateTime.Now > energyReady)
            {
                energy = maxEnergy;
                PlayerPrefs.SetInt(EnergyKey, energy);
            }
            else
            {
                playButton.interactable = false;
                Invoke(nameof(EnergyRecharged), (energyReady - DateTime.Now).Seconds);
            }
        }
        
        energyText.text = $"Play ({energy})";
    }

    void EnergyRecharged()
    {
        playButton.interactable = true;
        energy = maxEnergy;
        PlayerPrefs.SetInt(EnergyKey, energy);
        energyText.text = $"Play ({energy})";
    }

    public void Play()
    {
        // PlayerPrefs.DeleteAll();

        if (energy < 1)
            return;

        energy--;
        PlayerPrefs.SetInt(EnergyKey, energy);

        if(energy == 0)
        {
            DateTime energyReady = DateTime.Now.AddMinutes(energyRechargeDuration);
            PlayerPrefs.SetString(EnergyReadyKey, energyReady.ToString());

            #if UNITY_ANDROID
            androidNotificationHandler.ScheduleNotification(energyReady);
            #endif
        }

        SceneManager.LoadScene(1);
    }
}
