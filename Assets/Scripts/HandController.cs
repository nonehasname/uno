using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandController : MonoBehaviour {

  private Transform handLayout;

  private float cardCount;

  private const float maxHandWidth = 6.0f; //the max number of cards before we start overlapping cards
  private float cardWidth;

  private float cardHeight;

  private float handWidth; //the calculated max hand width

  public GameObject cardPrefab;

  private Transform selectedCard_;


  // Start is called before the first frame update
  void Start() {
    handLayout = transform;

    cardWidth = cardPrefab.transform.GetComponent<RectTransform>().rect.width; //handLayout * cardPrefab.transform.width
    cardHeight = cardPrefab.transform.GetComponent<RectTransform>().rect.height;
    handWidth = maxHandWidth * cardWidth;
  }

  // Update is called once per frame
  void Update() {



  }

  //currently only draws cards visually
  public void DrawCard() {
    //Calculate positions of current cards based on the new amount of cards
    int newCardCount = handLayout.childCount + 1; //newest card count


    float spacingIncrement = 0.0f;

    float leftEdge = -(newCardCount - 1) * 0.5f * cardWidth;

    if (newCardCount * cardWidth > handWidth) {
      spacingIncrement = cardWidth - ((cardWidth * newCardCount - handWidth) / newCardCount); 

      leftEdge = -handWidth / 2.0f + cardWidth * 0.5f;
    } else {
      spacingIncrement = cardWidth;
    }

    //Each existing card will be moved to their new positions
    foreach(Transform child in handLayout){
        
        child.GetComponent<Card>().SetCardPosition(leftEdge);
        leftEdge+= spacingIncrement;
    }

    //Add the new card
    GameObject drawnCard = Instantiate(cardPrefab, handLayout);
    drawnCard.transform.localPosition = new Vector3(leftEdge, -cardHeight * 1.5f, 0.0f);
    drawnCard.transform.GetComponent<Card>().SetCardPosition(leftEdge);
    drawnCard.transform.GetComponent<Card>().SetHandController(handLayout.GetComponent<HandController>());
    cardCount = newCardCount;
    
  }

  //ideally a drag and drop but will just use a play card button to play currently selected card
  public void PlayCard() {
    if (selectedCard_ != null) {
      Destroy(selectedCard_.gameObject); //hand is only tracked by the objects - will need to do more when we have more of a hand
      selectedCard_ = null;
      UpdateCardSpacing();
    }
  }

  public void RemoveCard(Transform removedCard){
    //Calculate positions of current cards based on the new amount of cards
    int newCardCount = handLayout.childCount - 1; //newest card count


    float spacingIncrement = 0.0f;

    float leftEdge = -(newCardCount - 1) * 0.5f * cardWidth;

    if (newCardCount * cardWidth > handWidth) {
      spacingIncrement = cardWidth - ((cardWidth * newCardCount - handWidth) / newCardCount); 

      leftEdge = -handWidth / 2.0f + cardWidth * 0.5f;
    } else {
      spacingIncrement = cardWidth;
    }

    Destroy(removedCard.gameObject);

    //Each existing card will be moved to their new positions
    foreach(Transform child in handLayout){
        

        if (child != removedCard){
          child.GetComponent<Card>().SetCardPosition(leftEdge);
          leftEdge+= spacingIncrement;
        }
    }

    cardCount = newCardCount;
  }

  //This occurs when a card is drawn or a card is played
  public void UpdateCardSpacing() {

    cardCount = handLayout.childCount; //newest card count

    float spacingIncrement = 0.0f;

    float leftEdge = -(cardCount - 1) * 0.5f * cardWidth;

    if (cardCount * cardWidth > handWidth) {
      spacingIncrement = cardWidth - ((cardWidth * cardCount - handWidth) / cardCount); 

      leftEdge = -handWidth / 2.0f + cardWidth * 0.5f;
    } else {
      spacingIncrement = cardWidth;
    }

    foreach(Transform child in handLayout){

        child.GetComponent<Card>().SetCardPosition(leftEdge);

        leftEdge+= spacingIncrement;
    }


  }

}




