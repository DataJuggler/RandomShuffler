

#region using statements

using DataJuggler.RandomShuffler.Enumerations;
using DataJuggler.RandomShuffler.Interfaces;
using System.Collections.Generic;

#endregion

namespace DataJuggler.RandomShuffler.Objects
{

    #region class Card
    /// <summary>
    /// This class represents a card in a 'Deck' of cards.
    /// </summary>
    public class Card
    {
        
        #region Private Variables
        private SuitEnum suit;
        private CardEnum cardName;
        private int cardValue;
        private List<int> cardValues;
        #endregion

        #region Parameterized Constructor(SuitEnum suit, CardEnum cardName, int cardValue)
        /// <summary>
        /// Create a new instance of a Card object.
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="cardName"></param>
        public Card(SuitEnum suit, CardEnum cardName, int cardValue)
        {
            // Store the parameters
            this.Suit = suit;
            this.CardName = cardName;
        }
        #endregion
        
        #region Properties
            
            #region CardFullName
            /// <summary>
            /// This read only property returns the value for 'CardFullName'.
            /// This value includes the CardName and the Suit.
            /// Example: TwoHearts
            /// </summary>
            public string CardFullName
            {
                get
                {
                    // initial value
                    string fullName = this.CardName.ToString() + this.Suit.ToString();
                    
                    // return value
                    return fullName;
                }
            }
            #endregion
            
            #region CardName
            /// <summary>
            /// This property gets or sets the value for 'Card'.
            /// </summary>
            public CardEnum CardName
            {
                get { return cardName; }
                set { cardName = value; }
            }
            #endregion
            
            #region CardValue
            /// <summary>
            /// This property gets or sets the value for 'CardValue'.
            /// </summary>
            public int CardValue
            {
                get { return cardValue; }
                set { cardValue = value; }
            }
            #endregion
            
            #region CardValues
            /// <summary>
            /// This property gets or sets the value for 'CardValues'.
            /// </summary>
            public List<int> CardValues
            {
                get { return cardValues; }
                set { cardValues = value; }
            }
            #endregion
            
            #region HasCardValues
            /// <summary>
            /// This property returns true if this object has a 'CardValues'.
            /// </summary>
            public bool HasCardValues
            {
                get
                {
                    // initial value
                    bool hasCardValues = (CardValues != null);

                    // return value
                    return hasCardValues;
                }
            }
            #endregion
            
            #region Suit
            /// <summary>
            /// This property gets or sets the value for 'Suit'.
            /// </summary>
            public SuitEnum Suit
            {
                get { return suit; }
                set { suit = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
