using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameTable : MonoBehaviour {

  // Position where the cards should go when played onto the
  // board.
  private Vector3 card_play_stack_pos_ = new Vector3(0, 0, 0);
  public Vector3 CardPlayStackPos {
    get { return card_play_stack_pos_; }
  }

  // Start is called before the first frame update
  void Start() {

  }

  // Update is called once per frame
  void Update() {

  }
}
