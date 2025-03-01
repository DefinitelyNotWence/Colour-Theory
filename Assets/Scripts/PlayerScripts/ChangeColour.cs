using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColour : MonoBehaviour
{
    public Animator animator;

    public GameObject BlueTiles;
    public GameObject RedTiles;
    public GameObject GreenTiles;
    public GameObject YellowTiles;

    public int colour;

    // Start is called before the first frame update
    void Start()
    {
        colour = 2;
    }

    // Update is called once per frame
    void Update()
    {
        Physics2D.IgnoreLayerCollision(9, 7);
        CheckColour();
        SwitchColour();
    }

    // This function checks the colour integer and changes the layers of the platforms accordingly, with layer 8 being the layer to be collided with and layer 9 to be ignored (phased through)
    private void CheckColour()
    {
        if (colour == 1)
        {
            animator.SetInteger("playerColour", 1);
            BlueTiles.layer = 8;
            RedTiles.layer = 9;
            GreenTiles.layer = 9;
            YellowTiles.layer = 9;
        }
        else if (colour == 2)
        {
            animator.SetInteger("playerColour", 2);
            BlueTiles.layer = 9;
            RedTiles.layer = 8;
            GreenTiles.layer = 9;
            YellowTiles.layer = 9;
        }
        else if (colour == 3)
        {
            animator.SetInteger("playerColour", 3);
            BlueTiles.layer = 9;
            RedTiles.layer = 9;
            GreenTiles.layer = 8;
            YellowTiles.layer = 9;
        }
        else if (colour == 4)
        {
            animator.SetInteger("playerColour", 4);
            BlueTiles.layer = 9;
            RedTiles.layer = 9;
            GreenTiles.layer = 9;
            YellowTiles.layer = 8;
        }
    }

    // This function checks which key has been pressed and changes the colour value accordingly
    private void SwitchColour()
    {
        if (Input.GetKeyDown(KeyCode.H)) // GetKeyDown checks for if the specified key on the keyboard has been pressed once
        {
            colour = 1;
            BlueTiles.layer = 8;
            RedTiles.layer = 9;
            GreenTiles.layer = 9;
            YellowTiles.layer = 9;
        }
        else if (Input.GetKeyDown(KeyCode.J))
        {
            colour = 2;
            BlueTiles.layer = 9;
            RedTiles.layer = 8;
            GreenTiles.layer = 9;
            YellowTiles.layer = 9;
        }
        else if (Input.GetKeyDown(KeyCode.K))
        {
            colour = 3;
            BlueTiles.layer = 9;
            RedTiles.layer = 9;
            GreenTiles.layer = 8;
            YellowTiles.layer = 9;
        }
        else if (Input.GetKeyDown(KeyCode.L))
        {
            colour = 4;
            BlueTiles.layer = 9;
            RedTiles.layer = 9;
            GreenTiles.layer = 9;
            YellowTiles.layer = 8;
        }
    }
}
