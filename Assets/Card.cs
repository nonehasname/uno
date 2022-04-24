using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{


    private Button button;

    private HandController handControl;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();

        button.onClick.AddListener(CardClick);

    }



    public void SetHandController(HandController newController){
        handControl = newController;
    }

    public void CardClick(){
        if (handControl != null){
            handControl.SelectCard(transform);
        }
    }

}
