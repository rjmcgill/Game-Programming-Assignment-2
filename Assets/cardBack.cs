using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardBack : MonoBehaviour
{
    GameObject gameManager;
    public SpriteRenderer spriteRenderer;
    public Sprite[] faces;
    public Sprite back;
    public int faceIndex;
    public bool matched = false;

    public void OnMouseDown()
    {
        if(matched == false)
        {
            Manager managerScript = gameManager.GetComponent<Manager>();
            if (spriteRenderer.sprite == back)
            {
                if (managerScript.CardUp(this))
                {
                    spriteRenderer.sprite = faces[faceIndex];
                    managerScript.CheckCards();
                }
            }
            else
            {
                spriteRenderer.sprite = back;
                managerScript.CardDown(this);
            }
        }
    }

    private void Awake()
    {
        gameManager = GameObject.Find("GameManager");

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Sliced;
        spriteRenderer.size = new Vector2(2.6f, 2.6f);

    }
}
