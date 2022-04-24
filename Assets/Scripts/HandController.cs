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

  private bool frameWait;

  // Start is called before the first frame update
  void Start() {
    handLayout = transform;
    frameWait = false;

    cardWidth = cardPrefab.transform.GetComponent<RectTransform>().rect.width; //handLayout * cardPrefab.transform.width
    cardHeight = cardPrefab.transform.GetComponent<RectTransform>().rect.height;
    handWidth = maxHandWidth * cardWidth;
  }

  // Update is called once per frame
  void Update() {
    //we wait one frame before disabling the grid layout so that it could space them out properly
    if (frameWait) {
      frameWait = false;
    } else {
      //disable the gridlayout so that horizontal spacing is done automatically and we can still move cards vertically
      GetComponent<GridLayoutGroup>().enabled = false;
    }

  }

  //currently only draws cards visually
  public void DrawCard() {
    //Instantiate a new card under the handlayout
    GameObject drawnCard = Instantiate(cardPrefab, handLayout);
    drawnCard.transform.GetComponent<Card>().SetHandController(handLayout.GetComponent<HandController>());
    UpdateCardSpacing();
  }

  //ideally a drag and drop but will just use a play card button to play currently selected card
  public void PlayCard() {
    if (selectedCard_ != null) {
      Destroy(selectedCard_.gameObject); //hand is only tracked by the objects - will need to do more when we have more of a hand
      selectedCard_ = null;
      UpdateCardSpacing();
    }
  }

  //This occurs when a card is drawn or a card is played
  public void UpdateCardSpacing() {
    GetComponent<GridLayoutGroup>().enabled = true;
    frameWait = true;
    cardCount = handLayout.childCount;
    if (cardCount * cardWidth > handWidth) {
      GetComponent<GridLayoutGroup>().spacing = new Vector2(-(cardWidth * cardCount - handWidth) / cardCount, 0.0f);
    } else {
      GetComponent<GridLayoutGroup>().spacing = new Vector2(0.0f, 0.0f);
    }
  }

}




