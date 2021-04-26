using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVars : MonoBehaviour
{
    private static int score = 0;
    private static float depth = 0;
    private static bool isWinner = false;

    private static int totalScore = 0;
    private static float totalDepth = 0;

    public static void UpdateScore(int value)
    {
        score += value;
    }

    public static int GetScore() { return score; }

    public static void ResetScore() { score = 0; }

    public static void SetIsWinner(bool w) { isWinner = w; } 

    public static bool GetIsWinner() { return isWinner; }

    public static void IncreaseDepth() { 
        depth += 1 * Time.deltaTime;
    }

    public static void AddToTotalScore() { totalScore += score; }
    public static void AddToTotalDepth() { totalDepth += depth; }

    public static float GetTotalScore() { return totalScore; }
    public static float GetTotalDepth() { return totalDepth; }

    public static void ResetTotalScore() { totalScore = 0; }
    public static void ResetTotalDepth() { totalDepth = 0; }

    public static void ResetDepth() { depth = 0; }

    public static float GetDepth() { return depth; }
}
