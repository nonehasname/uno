using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameDeck : MonoBehaviour {

  private Sprite base_card_sprite_ = null;
  private Dictionary<float, Sprite> card_sprites_ = new Dictionary<float, Sprite>();

  void Start() {
    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
    Debug.Assert(renderer != null);
    Debug.Assert(renderer.sprite);

    base_card_sprite_ = renderer.sprite;
    InitializeCardSpriteStates();

    const int kSpriteIndex = 3;
    Debug.Log("Sprite Count: " + card_sprites_.Count);
    int ct = 0;
    foreach (KeyValuePair<float, Sprite> entry in card_sprites_) {
      if (ct == kSpriteIndex) {
        Debug.Log("Sepecting sprite with fullness: " + entry.Key);
        var sprite = entry.Value;

        renderer.sprite = sprite;

        break;
      }
      ++ct;
    }
  }

  // @desc Create different sprites for the card deck depending on how
  // many cards are left in the deck.
  // The less cards in the deck, the smaller the deck should look.
  private void InitializeCardSpriteStates() {
    const int kFullnessStates = 4;
    for (int i = 0; i < kFullnessStates; ++i) {
      float fullness = ((float)i) / kFullnessStates;
      card_sprites_.Add(fullness, CreateDeckSprite(fullness));
    }
  }

  // @desc Create deck sprite dependin on how full the deck is.
  // @param [fullness] >= 0 and <= 1.0
  private Sprite CreateDeckSprite(float fullness) {
    Debug.Assert(base_card_sprite_ != null);
    fullness = Mathf.Clamp(fullness, 0.0f, 1.0f);
    const int kThickness = 8;

    int offset = (int)(kThickness * fullness);
    Texture2D base_tex = base_card_sprite_.texture;
    Texture2D tex = new Texture2D(base_tex.width, base_tex.height + offset);

    for (int i = 0; i < base_tex.width; ++i) {
      for (int j = 0; j < base_tex.height; ++j) {
        tex.SetPixel(i, offset + j, base_tex.GetPixel(i, j));
      }
    }

    // fill in the offset
    Color c = new Color(0.75f, 0.75f, 0.75f);
    for (int i = 0; i < tex.width; ++i) {
      for (int j = 0; j < offset; ++j) {
        tex.SetPixel(i, j, c);
      }
    }

    tex.filterMode = FilterMode.Point;
    tex.Apply();
    // construct a sprite using this new texture
    // Set the pivot to the center fo rnow.
    return Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f));
  }

  private void PrintTexture(in Texture2D tex) {
    for (int i = 0; i < tex.width; ++i) {
      string tex_str = "";
      for (int j = 0; j < tex.height; ++j) {
        tex_str += tex.GetPixel(i, j);
      }
      Debug.Log(tex_str);
    }
  }
}
