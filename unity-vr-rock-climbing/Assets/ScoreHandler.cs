using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;


public class ScoreHandler : MonoBehaviour
{
    public TMPro.TextMeshProUGUI scoreTracker;
    private int[] handlesFired = new int[10];

    // Update is called once per frame
    public void HandleLaunched(int user)
    {
        handlesFired[user]++;
        scoreTracker.text = GeneratePlayerStats(handlesFired);
    }

    string GeneratePlayerStats(int[] playerScores)
    {
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < playerScores.Length; i++)
        {
            if (playerScores[i] != 0)
            {
                sb.Append($"Player {i + 1}: {playerScores[i]}\n");
            }
        }

        return sb.ToString();
    }
}
