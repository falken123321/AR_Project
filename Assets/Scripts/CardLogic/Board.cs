using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private Deck DealerDeck;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        this.DealerDeck = new Deck();
        this.player = new Player();
    }

    void RegisterCard()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
