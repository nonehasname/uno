using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardSpacing : MonoBehaviour
{

    private Transform handLayout;

    private float cardCount;

    private const float handWidth = 1.32f; //currently set to not overlap cards until 6 cards

    private const float cardSize = 0.22f;

    public GameObject cardPrefab;
    // Start is called before the first frame update
    void Start()
    {
        handLayout = transform;
    }

    // Update is called once per frame
    void Update()
    {
        



    }

    //currently only draws cards visually
    public void DrawCard(){


        //Instantiate a new card under the handlayout
        Instantiate(cardPrefab, handLayout); 

        UpdateCardSpacing();
    }

    //when hovering over a card it should move up
    public void SelectCard(){

    }

    //ideally a drag and drop but will just use a play card button to play currently selected card
    public void PlayCard(){ 

    }

    //This occurs when a card is drawn or a card is played
    public void UpdateCardSpacing(){


        cardCount = handLayout.childCount;
        if (cardCount * cardSize > handWidth){

            GetComponent<GridLayoutGroup>().spacing = new Vector2( -(cardSize * cardCount - handWidth) / cardCount, 0.0f) ;
        } else {


            GetComponent<GridLayoutGroup>().spacing = new Vector2(0.0f, 0.0f);
        }


    }
}
