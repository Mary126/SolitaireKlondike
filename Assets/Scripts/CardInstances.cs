using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardInstances : MonoBehaviour
{
    public enum Type
    {
        clubs, diamonds, hearts, spades
    }
    public class CardType 
    {
        public Sprite sprite;
        public Type type;
        public int number;

        public CardType(Sprite spriteToAdd, Type typeToAdd, int numberToAdd)
        {
            sprite = spriteToAdd;
            type = typeToAdd;
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
    public CardType info;
    public Position position;
    public bool isOpen = false;
    public void SetPosition(string row, int num)
    {
        position = new Position(row, num);
    }
    public void SetCardType(Sprite sprite)
    {
        switch (sprite.name)
        {
            case "card-deck_0": info = new CardType(sprite, Type.diamonds, 2); break;
            case "card-deck_1": info = new CardType(sprite, Type.diamonds, 3); break;
            case "card-deck_2": info = new CardType(sprite, Type.diamonds, 4); break;
            case "card-deck_3": info = new CardType(sprite, Type.diamonds, 5); break;
            case "card-deck_4": info = new CardType(sprite, Type.diamonds, 6); break;
            case "card-deck_5": info = new CardType(sprite, Type.diamonds, 7); break;
            case "card-deck_6": info = new CardType(sprite, Type.diamonds, 8); break;
            case "card-deck_7": info = new CardType(sprite, Type.diamonds, 9); break;
            case "card-deck_8": info = new CardType(sprite, Type.diamonds, 10); break;
            case "card-deck_9": info = new CardType(sprite, Type.diamonds, 11); break;
            case "card-deck_10": info = new CardType(sprite, Type.diamonds, 12); break;
            case "card-deck_11": info = new CardType(sprite, Type.diamonds, 13); break;
            case "card-deck_12": info = new CardType(sprite, Type.diamonds, 1); break;
            case "card-deck_14": info = new CardType(sprite, Type.clubs, 2); break;
            case "card-deck_15": info = new CardType(sprite, Type.clubs, 3); break;
            case "card-deck_16": info = new CardType(sprite, Type.clubs, 4); break;
            case "card-deck_17": info = new CardType(sprite, Type.clubs, 5); break;
            case "card-deck_18": info = new CardType(sprite, Type.clubs, 6); break;
            case "card-deck_19": info = new CardType(sprite, Type.clubs, 7); break;
            case "card-deck_20": info = new CardType(sprite, Type.clubs, 8); break;
            case "card-deck_21": info = new CardType(sprite, Type.clubs, 9); break;
            case "card-deck_22": info = new CardType(sprite, Type.clubs, 10); break;
            case "card-deck_23": info = new CardType(sprite, Type.clubs, 11); break;
            case "card-deck_24": info = new CardType(sprite, Type.clubs, 12); break;
            case "card-deck_25": info = new CardType(sprite, Type.clubs, 13); break;
            case "card-deck_26": info = new CardType(sprite, Type.clubs, 1); break;
            case "card-deck_28": info = new CardType(sprite, Type.hearts, 2); break;
            case "card-deck_29": info = new CardType(sprite, Type.hearts, 3); break;
            case "card-deck_30": info = new CardType(sprite, Type.hearts, 4); break;
            case "card-deck_31": info = new CardType(sprite, Type.hearts, 5); break;
            case "card-deck_32": info = new CardType(sprite, Type.hearts, 6); break;
            case "card-deck_33": info = new CardType(sprite, Type.hearts, 7); break;
            case "card-deck_34": info = new CardType(sprite, Type.hearts, 8); break;
            case "card-deck_35": info = new CardType(sprite, Type.hearts, 9); break;
            case "card-deck_36": info = new CardType(sprite, Type.hearts, 10); break;
            case "card-deck_37": info = new CardType(sprite, Type.hearts, 11); break;
            case "card-deck_38": info = new CardType(sprite, Type.hearts, 12); break;
            case "card-deck_39": info = new CardType(sprite, Type.hearts, 13); break;
            case "card-deck_40": info = new CardType(sprite, Type.hearts, 1); break;
            case "card-deck_42": info = new CardType(sprite, Type.spades, 2); break;
            case "card-deck_43": info = new CardType(sprite, Type.spades, 3); break;
            case "card-deck_44": info = new CardType(sprite, Type.spades, 4); break;
            case "card-deck_45": info = new CardType(sprite, Type.spades, 5); break;
            case "card-deck_46": info = new CardType(sprite, Type.spades, 6); break;
            case "card-deck_47": info = new CardType(sprite, Type.spades, 7); break;
            case "card-deck_48": info = new CardType(sprite, Type.spades, 8); break;
            case "card-deck_49": info = new CardType(sprite, Type.spades, 9); break;
            case "card-deck_50": info = new CardType(sprite, Type.spades, 10); break;
            case "card-deck_51": info = new CardType(sprite, Type.spades, 11); break;
            case "card-deck_52": info = new CardType(sprite, Type.spades, 12); break;
            case "card-deck_53": info = new CardType(sprite, Type.spades, 13); break;
            case "card-deck_54": info = new CardType(sprite, Type.spades, 1); break;
            default: break;
        }
    }
}
