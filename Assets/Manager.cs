using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public GameObject card;
    public TMP_Text gameOverText;
    public GameObject retryButton;
    List<int> faceIndexes = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 0, 1, 2, 3, 4, 5, 6, 7 };
    public int shuffleNumber = 0;
    cardBack cardUp1 = null;
    cardBack cardUp2 = null;
    int winNum = 8;
    int matchNum = 0;

    private void Start()
    {

        gameOverText.color = new Color(0, 0, 0, 0);

        int origLength = faceIndexes.Count;
        float yPos = 3;
        float xPos = -3;
        
        for(int a = 1; a <= 4; a++)
        {
            for(int b = 1; b <= 4; b++)
            {
                shuffleNumber = Random.Range(0, (faceIndexes.Count));
                var tempCard = Instantiate(card, new Vector3(xPos, yPos, 0), Quaternion.identity);
                tempCard.GetComponent<cardBack>().faceIndex = faceIndexes[shuffleNumber];
                faceIndexes.Remove(faceIndexes[shuffleNumber]);
                xPos = xPos + 2;
            }
            yPos = yPos - 2;
            xPos = -3;
        }
        
    }

    public void CardDown(cardBack card)
    {
        if(cardUp1 == card)
        {
            cardUp1 = null;
        } else if(cardUp2 == card)
        {
            cardUp2 = null;
        }
    }

    public bool CardUp(cardBack card)
    {
        if(cardUp1 == null)
        {
            cardUp1 = card;
        } else if (cardUp2 == null)
        {
            cardUp2 = card;
        } else
        {
            return false;
        }
        return true;
    }

    public void CheckCards()
    {
        if (cardUp1 != null && cardUp2 != null && cardUp1.faceIndex == cardUp2.faceIndex)
        {
            cardUp1.matched = true;
            cardUp2.matched = true;
            cardUp1 = null;
            cardUp2 = null;
            matchNum++;
            if(matchNum == winNum)
            {
                gameOverText.color = new Color(0, 0, 0, 1);
                retryButton.transform.Translate(new Vector3(0, -411, 0));
            }
        } else if (cardUp1 != null && cardUp2 != null)
        {
            StartCoroutine(WaitASecond());
        }
    }

    IEnumerator WaitASecond()
    {
        cardUp1.matched = true;
        cardUp2.matched = true;

        yield return new WaitForSeconds(1);

        cardUp1.spriteRenderer.sprite = cardUp1.back;
        cardUp2.spriteRenderer.sprite = cardUp2.back;

        cardUp1.matched = false;
        cardUp2.matched = false;

        CardDown(cardUp1);
        CardDown(cardUp2);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
