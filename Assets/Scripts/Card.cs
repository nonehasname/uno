using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour {

  private Button button;
  private HandController handControl_;
  private float selected_position_y_;
  private float deselected_position_y_;
  private float select_lerp_ = 0.0f;
  private bool selecting_card_ = false;
  private float draw_speed_ = 5.0f;

  // draw the card into the deck the first time it is
  // instantiated.
  private bool start_draw_card_ = true;
  private float start_draw_card_lerp_ = 0.0f;

  void Start() {
    // gameObject.SetActive(false);
    button = GetComponent<Button>();
    button.onClick.AddListener(SelectCard);

    float card_height = GetComponent<RectTransform>().rect.height;
    deselected_position_y_ = transform.position.y;
    selected_position_y_ = transform.position.y;
    selected_position_y_ += 1.4f * card_height;

    transform.position = CreateDrawStartPosition();
  }

  void Update() {
    HandleInitCardDraw();
    HandleCardSelection();
  }

  public void SetHandController(HandController newController) {
    handControl_ = newController;
  }

  public void SelectCard() {
    selecting_card_ = true;
    transform.position = new Vector3(
      transform.position.x,
      selected_position_y_,
      transform.position.z
    );
  }

  public void DeselectCard() {
    selecting_card_ = false;
    transform.position = new Vector3(
      transform.position.x,
      deselected_position_y_,
      transform.position.z
    );
  }

  private void HandleInitCardDraw() {
    if (!start_draw_card_) return;
    if (start_draw_card_lerp_ == 0.0f) {
      transform.position = CreateDrawStartPosition();
      gameObject.SetActive(true);
    } else {
      if (start_draw_card_lerp_ == 1.0f) {
        start_draw_card_ = false;
      } else {
        Vector3 start = CreateDrawStartPosition();
        Vector3 end = CreateDeselectedVectorPosition();
        transform.position = Vector3.Lerp(start, end, start_draw_card_lerp_);
      }
    }

    start_draw_card_lerp_ = Mathf.Clamp(
      start_draw_card_lerp_ + Time.deltaTime * draw_speed_,
      0.0f, 1.0f);
  }

  private void HandleCardSelection() {
    if (selecting_card_) {
      if (select_lerp_ == 1.0f) return;
      if (start_draw_card_) start_draw_card_ = false;
      Vector3 start = CreateDeselectedVectorPosition();
      Vector3 end = CreateSelectedVectorPosition();

      transform.position = Vector3.Lerp(start, end, select_lerp_);
      select_lerp_ = Mathf.Clamp(select_lerp_ + draw_speed_ * Time.deltaTime, 0.0f, 1.0f);
    } else {
      if (select_lerp_ == 0.0f) return;
      if (start_draw_card_) start_draw_card_ = false;
      Vector3 start = CreateSelectedVectorPosition();
      Vector3 end = CreateDeselectedVectorPosition();

      transform.position = Vector3.Lerp(start, end, 1 - select_lerp_);
      select_lerp_ = Mathf.Clamp(select_lerp_ - draw_speed_ * Time.deltaTime, 0.0f, 1.0f);
    }
  }

  private Vector3 CreateSelectedVectorPosition() {
    return new Vector3(
      transform.position.x,
      selected_position_y_,
      transform.position.z
    );
  }

  private Vector3 CreateDeselectedVectorPosition() {
    return new Vector3(
      transform.position.x,
      deselected_position_y_,
      transform.position.z
    );
  }

  private Vector3 CreateDrawStartPosition() {
    return new Vector3(
      transform.position.x,
      transform.position.y - 2.0f,
      transform.position.z
    );
  }

}
