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

  private GameObject opponent_side_table_;
  private GameObject player_side_table_;
  private GameObject player_side_table_drape_;
  // Start is called before the first frame update
  void Start() {
    opponent_side_table_ = transform.Find("OpponentSideTable").gameObject;
    player_side_table_ = transform.Find("PlayerSideTable").gameObject;
    player_side_table_drape_ = transform.Find("PlayerSideTableSide").gameObject;

    Debug.Assert(opponent_side_table_ != null);
    Debug.Assert(player_side_table_ != null);
    Debug.Assert(player_side_table_drape_ != null);
    Debug.Assert(opponent_side_table_.GetComponent<SpriteRenderer>() != null);
    Debug.Assert(player_side_table_.GetComponent<SpriteRenderer>() != null);
    Debug.Assert(player_side_table_drape_.GetComponent<SpriteRenderer>() != null);

    CloneAndReplaceSpriteTexture(opponent_side_table_.GetComponent<SpriteRenderer>());
    CloneAndReplaceSpriteTexture(player_side_table_.GetComponent<SpriteRenderer>());
    CloneAndReplaceSpriteTexture(player_side_table_drape_.GetComponent<SpriteRenderer>());

    // Set color ..
    Color a = new Color(
      Random.Range(0.0f, 1.0f),
      Random.Range(0.0f, 1.0f),
      Random.Range(0.0f, 1.0f));
    Color b = new Color(
      Random.Range(0.0f, 1.0f),
      Random.Range(0.0f, 1.0f),
      Random.Range(0.0f, 1.0f));
    SetPlayerTableColor(a);
    SetOpponentTableColor(b);
  }

  void Update() {
  }

  public void SetPlayerTableColor(in Color color) {
    SpriteRenderer renderer = player_side_table_.GetComponent<SpriteRenderer>();
    SetSpriteColor(renderer.sprite, color);

    renderer = player_side_table_drape_.GetComponent<SpriteRenderer>();
    Color dimmed_color = new Color(
      Mathf.Clamp(color.r - 0.15f, 0.0f, 1.0f),
      Mathf.Clamp(color.g - 0.15f, 0.0f, 1.0f),
      Mathf.Clamp(color.b - 0.15f, 0.0f, 1.0f));
    SetSpriteColor(renderer.sprite, dimmed_color);
  }

  public void SetOpponentTableColor(in Color color) {
    SpriteRenderer renderer = opponent_side_table_.GetComponent<SpriteRenderer>();
    SetSpriteColor(renderer.sprite, color);
  }

  private void CloneAndReplaceSpriteTexture(SpriteRenderer renderer) {
    Sprite sprite = renderer.sprite;
    Texture2D original_tex = sprite.texture;
    Texture2D tex_copy = new Texture2D(original_tex.width, original_tex.height);
    tex_copy.SetPixels(original_tex.GetPixels());
    tex_copy.Apply();

    Sprite sprite_copy = Sprite.Create(
      tex_copy,
      new Rect(0.0f, 0.0f, tex_copy.width, tex_copy.height),
      new Vector2(0.5f, 0.5f));
    renderer.sprite = sprite_copy;
  }

  private void SetSpriteColor(Sprite s, Color c) {
    Texture2D tex = s.texture;
    for (int i = 0; i < tex.width; ++i) {
      for (int j = 0; j < tex.height; ++j) {
        tex.SetPixel(i, j, c);
      }
    }
    tex.Apply();
  }
}
