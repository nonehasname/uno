using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

  private Button button;
  //private HandController handControl_;
  private float selected_position_y_;
  private float deselected_position_y_;
  private float select_lerp_ = 0.0f;
  private bool selecting_card_ = false;
  private float draw_speed_ = 5.0f;

  // draw the card into the deck the first time it is
  // instantiated.
  private bool start_draw_card_ = true;
  private float start_draw_card_lerp_ = 0.0f;


  private bool start_card_rearrange_ = false;
  private float start_card_rearrange_lerp_ = 0.0f;

  private float start_rearrange_position_x_;
  private float end_rearrange_position_x_;

  void Start() {
    // gameObject.SetActive(false);
    button = GetComponent<Button>();
    button.onClick.AddListener(SelectCard);

    float card_height = GetComponent<RectTransform>().rect.height;
    deselected_position_y_ = 0.0f;//transform.localPosition.y;

    selected_position_y_ = 0.4f * card_height;

  }

  void Update() {
    HandleInitCardDraw();
    HandleCardSelection();
    HandleCardRearrange();
  }


  public void SelectCard() {

    selecting_card_ = true;

  }

  public void DeselectCard() {

    selecting_card_ = false;

  }

  private void HandleInitCardDraw() {
    if (!start_draw_card_) return;
    if (start_draw_card_lerp_ == 0.0f) {
      
      //new cards do need to be set horizontally using rearrange
      start_card_rearrange_ = false; 

      transform.localPosition = CreateDrawStartPosition();
      gameObject.SetActive(true);
    } else {
      if (start_draw_card_lerp_ == 1.0f) {
        start_draw_card_ = false;
        transform.localPosition = CreateDeselectedVectorPosition();
      } else {
        Vector3 start = CreateDrawStartPosition();
        Vector3 end = CreateDeselectedVectorPosition();
        
        Vector3 newPosition = transform.localPosition;
        newPosition = Vector3.Lerp(start, end, start_draw_card_lerp_);
        transform.localPosition = newPosition;
      
      }
    }

    start_draw_card_lerp_ = Mathf.Clamp(
      start_draw_card_lerp_ + Time.deltaTime * draw_speed_,
      0.0f, 1.0f);
  }

  private void HandleCardRearrange(){

    if (start_card_rearrange_){

      if (start_card_rearrange_lerp_ == 1.0f){
        start_card_rearrange_ = false;
      }

      Vector3 newPosition = transform.localPosition;
      newPosition.x = Mathf.Lerp(start_rearrange_position_x_, end_rearrange_position_x_, start_card_rearrange_lerp_);
      transform.localPosition = newPosition;

      start_card_rearrange_lerp_ = Mathf.Clamp(start_card_rearrange_lerp_ + draw_speed_ * Time.deltaTime, 0.0f, 1.0f);
    }
  }


  private void HandleCardSelection() {
    if (selecting_card_) {
      //if (select_lerp_ == 1.0f) return;
      if (start_draw_card_) start_draw_card_ = false;
      Vector3 start = CreateDeselectedVectorPosition();
      Vector3 end = CreateSelectedVectorPosition();

      transform.localPosition = Vector3.Lerp(start, end, select_lerp_);
      select_lerp_ = Mathf.Clamp(select_lerp_ + draw_speed_ * Time.deltaTime, 0.0f, 1.0f);
    } else {
      //if (select_lerp_ == 0.0f) return;
      if (start_draw_card_) return;//start_draw_card_ = false;
      Vector3 start = CreateSelectedVectorPosition();
      Vector3 end = CreateDeselectedVectorPosition();

      transform.localPosition = Vector3.Lerp(start, end, 1 - select_lerp_);
      select_lerp_ = Mathf.Clamp(select_lerp_ - draw_speed_ * Time.deltaTime, 0.0f, 1.0f);
    }
  }

  private Vector3 CreateSelectedVectorPosition() {
    return new Vector3(
      transform.localPosition.x,
      selected_position_y_,
      transform.localPosition.z
    );
  }

  private Vector3 CreateDeselectedVectorPosition() {
    return new Vector3(
      transform.localPosition.x,
      deselected_position_y_,
      transform.localPosition.z
    );
  }

  private Vector3 CreateDrawStartPosition() {
    float card_height = GetComponent<RectTransform>().rect.height;
    return new Vector3(

      end_rearrange_position_x_,
      deselected_position_y_ - card_height * 1.5f,
      transform.localPosition.z
    );
  }




  public void SetCardPosition(float xPosition){
    start_rearrange_position_x_ = transform.localPosition.x;
    end_rearrange_position_x_ = xPosition;
    start_card_rearrange_ = true;

    start_card_rearrange_lerp_ = 0.0f;

  }

}
