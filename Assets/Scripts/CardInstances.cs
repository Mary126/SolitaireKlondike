using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstances : MonoBehaviour
{
    public class CardSuit 
    {
        public Sprite sprite;
        public string suit;
        public int number;

        public CardSuit(Sprite spriteToAdd, string SuitToAdd, int numberToAdd)
        {
            sprite = spriteToAdd;
            suit = SuitToAdd;
            number = numberToAdd;
        }
    }
    public class Position
    {
        public string row;
        public int number;

        public Position(string rowToAdd, int numberToAdd)
        {
            row = rowToAdd;
            number = numberToAdd;
        }
    }
    public CardSuit cardSuit;
    public Position position;
    public bool isOpen = false;
    public void SetPosition(string row, int num)
    {
        position = new Position(row, num);
    }
    public void SetCardSuit(Sprite sprite)
    {
        switch (sprite.name)
        {
            case "card-deck_0": cardSuit = new CardSuit(sprite, "diamonds", 2); break;
            case "card-deck_1": cardSuit = new CardSuit(sprite, "diamonds", 3); break;
            case "card-deck_2": cardSuit = new CardSuit(sprite, "diamonds", 4); break;
            case "card-deck_3": cardSuit = new CardSuit(sprite, "diamonds", 5); break;
            case "card-deck_4": cardSuit = new CardSuit(sprite, "diamonds", 6); break;
            case "card-deck_5": cardSuit = new CardSuit(sprite, "diamonds", 7); break;
            case "card-deck_6": cardSuit = new CardSuit(sprite, "diamonds", 8); break;
            case "card-deck_7": cardSuit = new CardSuit(sprite, "diamonds", 9); break;
            case "card-deck_8": cardSuit = new CardSuit(sprite, "diamonds", 10); break;
            case "card-deck_9": cardSuit = new CardSuit(sprite, "diamonds", 11); break;
            case "card-deck_10": cardSuit = new CardSuit(sprite, "diamonds", 12); break;
            case "card-deck_11": cardSuit = new CardSuit(sprite, "diamonds", 13); break;
            case "card-deck_12": cardSuit = new CardSuit(sprite, "diamonds", 1); break;
            case "card-deck_14": cardSuit = new CardSuit(sprite, "clubs", 2); break;
            case "card-deck_15": cardSuit = new CardSuit(sprite, "clubs", 3); break;
            case "card-deck_16": cardSuit = new CardSuit(sprite, "clubs", 4); break;
            case "card-deck_17": cardSuit = new CardSuit(sprite, "clubs", 5); break;
            case "card-deck_18": cardSuit = new CardSuit(sprite, "clubs", 6); break;
            case "card-deck_19": cardSuit = new CardSuit(sprite, "clubs", 7); break;
            case "card-deck_20": cardSuit = new CardSuit(sprite, "clubs", 8); break;
            case "card-deck_21": cardSuit = new CardSuit(sprite, "clubs", 9); break;
            case "card-deck_22": cardSuit = new CardSuit(sprite, "clubs", 10); break;
            case "card-deck_23": cardSuit = new CardSuit(sprite, "clubs", 11); break;
            case "card-deck_24": cardSuit = new CardSuit(sprite, "clubs", 12); break;
            case "card-deck_25": cardSuit = new CardSuit(sprite, "clubs", 13); break;
            case "card-deck_26": cardSuit = new CardSuit(sprite, "clubs", 1); break;
            case "card-deck_28": cardSuit = new CardSuit(sprite, "hearts", 2); break;
            case "card-deck_29": cardSuit = new CardSuit(sprite, "hearts", 3); break;
            case "card-deck_30": cardSuit = new CardSuit(sprite, "hearts", 4); break;
            case "card-deck_31": cardSuit = new CardSuit(sprite, "hearts", 5); break;
            case "card-deck_32": cardSuit = new CardSuit(sprite, "hearts", 6); break;
            case "card-deck_33": cardSuit = new CardSuit(sprite, "hearts", 7); break;
            case "card-deck_34": cardSuit = new CardSuit(sprite, "hearts", 8); break;
            case "card-deck_35": cardSuit = new CardSuit(sprite, "hearts", 9); break;
            case "card-deck_36": cardSuit = new CardSuit(sprite, "hearts", 10); break;
            case "card-deck_37": cardSuit = new CardSuit(sprite, "hearts", 11); break;
            case "card-deck_38": cardSuit = new CardSuit(sprite, "hearts", 12); break;
            case "card-deck_39": cardSuit = new CardSuit(sprite, "hearts", 13); break;
            case "card-deck_40": cardSuit = new CardSuit(sprite, "hearts", 1); break;
            case "card-deck_42": cardSuit = new CardSuit(sprite, "spades", 2); break;
            case "card-deck_43": cardSuit = new CardSuit(sprite, "spades", 3); break;
            case "card-deck_44": cardSuit = new CardSuit(sprite, "spades", 4); break;
            case "card-deck_45": cardSuit = new CardSuit(sprite, "spades", 5); break;
            case "card-deck_46": cardSuit = new CardSuit(sprite, "spades", 6); break;
            case "card-deck_47": cardSuit = new CardSuit(sprite, "spades", 7); break;
            case "card-deck_48": cardSuit = new CardSuit(sprite, "spades", 8); break;
            case "card-deck_49": cardSuit = new CardSuit(sprite, "spades", 9); break;
            case "card-deck_50": cardSuit = new CardSuit(sprite, "spades", 10); break;
            case "card-deck_51": cardSuit = new CardSuit(sprite, "spades", 11); break;
            case "card-deck_52": cardSuit = new CardSuit(sprite, "spades", 12); break;
            case "card-deck_53": cardSuit = new CardSuit(sprite, "spades", 13); break;
            case "card-deck_54": cardSuit = new CardSuit(sprite, "spades", 1); break;
            default: break;
        }
    }
}
