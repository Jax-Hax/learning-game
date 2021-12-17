using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashcardSet
{
    public Dictionary<int, Flashcard> flashcards = new Dictionary<int, Flashcard>();
    public string nameOfSet;
    public string subject;
    public int amOfCards;
}
