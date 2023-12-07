using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject scene_Tutorial;
    public GameObject scene_Main;

    public List<Goal> tutorialGoals;
    public List<Goal> mainGoals;

    [Space]
    public Player player;

    public void Start()
    {
        if (Statics.tutorialMode) player.goals = tutorialGoals;
        else player.goals = mainGoals;

        scene_Tutorial.SetActive(Statics.tutorialMode);
        scene_Main.SetActive(!Statics.tutorialMode);

        player.NextGoal();
    }
}
