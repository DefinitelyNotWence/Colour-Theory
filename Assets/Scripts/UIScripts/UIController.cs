using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public PlayerController playerController;

    [SerializeField] private TextMeshProUGUI level;
    [SerializeField] private TextMeshProUGUI dash;
    [SerializeField] private TextMeshProUGUI deathCounter;
    [SerializeField] private TextMeshProUGUI collectableCounter;
    [SerializeField] private GameObject Diamond;
    [SerializeField] private GameObject Coin;
    [SerializeField] private GameObject Mushroom;
    [SerializeField] private GameObject Clover;

    // Start is called before the first frame update
    void Start()
    {
        Diamond.SetActive(false);
        Coin.SetActive(false);
        Mushroom.SetActive(false);
        Clover.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        level.text = "Level " + playerController.level;

        if (playerController.canDash == true)
        {
            dash.text = "Dash : Ready";
        }
        else
        {
            dash.text = "Dash : Recharging";
        }
    }

    // This function changes what is displayed in the end screen
    public void GameFinish()
    {
        deathCounter.text = playerController.failCounter + "DEATHS";
        collectableCounter.text = playerController.collectables + "/4 Collectables";
        if (playerController.diamond == true)
        {
            Diamond.SetActive(true);
        }
        if (playerController.coin == true)
        {
            Coin.SetActive(true);
        }
        if (playerController.mushroom == true)
        {
            Mushroom.SetActive(true);
        }
        if (playerController.clover == true)
        {
            Clover.SetActive(true);
        }
    }

    // This function calls the main game scene to start which restarts the game
    public void Restart()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    
}
