using System;
using System.Collections.Generic;
using UnityEngine;

namespace Card.ScripctsObject
{
    [CreateAssetMenu(fileName = "CardLibrarySO", menuName = "Card/CardLibrarySO")]
    public class CardLibrarySO : ScriptableObject
    {
        public List<CardLibraryEntry> cardLibraries;

        public void AddCard(CardLibraryEntry card)
        {
            cardLibraries.Add(card);
        }

        public void RemoveCard(CardLibraryEntry card)
        {
            cardLibraries.Remove(card);
        }
    }

    [System.Serializable]
    public struct CardLibraryEntry : IEquatable<CardLibraryEntry>
    {
        public CardDataSO cardData;
        public int amount;

        public bool Equals(CardLibraryEntry other)
        {
            return Equals(cardData, other.cardData) && amount == other.amount;
        }

        public override bool Equals(object obj)
        {
            return obj is CardLibraryEntry other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(cardData, amount);
        }
    }
}