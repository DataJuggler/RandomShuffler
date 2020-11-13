using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataJuggler.RandomShuffler.Objects
{

    #region class Shoe
    /// <summary>
    /// This class is used to represent a collection of Decks of cards.
    /// This is used in BlackJack; to deal 'Single Deck' black jack you 
    /// can use a Deck or a Shoe. A shoe could be used in poker
    /// but using a 'Deck' object better represents the real world.
    /// </summary>
    public class Shoe
    {
        #region Private Variables
        private int numberDecks;
        private List<Card> cards;
        #endregion

        #region Properties

            #region Cards
            /// <summary>
            /// This property gets or sets the value for 'Cards'.
            /// </summary>
            public List<Card> Cards
            {
                get { return cards; }
                set { cards = value; }
            }
            #endregion
            
            #region HasCards
            /// <summary>
            /// This property returns true if this object has a 'Cards'.
            /// </summary>
            public bool HasCards
            {
                get
                {
                    // initial value
                    bool hasCards = (this.Cards != null);
                    
                    // return value
                    return hasCards;
                }
            }
            #endregion
            
            #region NumberDecks
            /// <summary>
            /// This property gets or sets the value for 'NumberDecks'.
            /// </summary>
            public int NumberDecks
            {
                get { return numberDecks; }
                set { numberDecks = value; }
            }
            #endregion
            
        #endregion
    }
    #endregion

}
