

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
        private ICardValueManager cardValueManager;
        #endregion

        #region Parameterized Constructor(SuitEnum suit, CardEnum cardName, ICardValueManager cardValueManager)
        /// <summary>
        /// Create a new instance of a Card object.
        /// </summary>
        /// <param name="suit"></param>
        /// <param name="cardName"></param>
        /// <param name="cardValueManager"></param>
        public Card(SuitEnum suit, CardEnum cardName, ICardValueManager cardValueManager)
        {
            // Store the parameters
            this.Suit = suit;
            this.CardName = cardName;
            this.CardValueManager = cardValueManager;
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
                get 
                {
                    // initial value
                    int cardValue = 0;

                    // if the CardValueManager exists
                    if (HasCardValueManager)
                    {
                        // Set the return value
                        cardValue = this.CardValueManager.GetCardValue(this);
                    }

                    // return value
                    return cardValue;
                }
            }
            #endregion
            
            #region CardValueManager
            /// <summary>
            /// This property gets or sets the value for 'CardValueManager'.
            /// </summary>
            public ICardValueManager CardValueManager
            {
                get { return cardValueManager; }
                set { cardValueManager = value; }
            }
            #endregion

            #region CardValues
            /// <summary>
            /// This property returns the possible CardValues for a Card. In most circumstances you will
            /// use the CardValue (singular) but in the case of Ace in Black Jack this is needed.
            /// You can just stub out the method and throw a Not Implemented Exception if this
            /// method does not apply to a particular game or simulation.
            /// </summary>
            public List<int> CardValues
            {
                get
                {
                    // initial value
                    List<int> cardValues = null;

                    // if the CardValueManager exists
                    if (HasCardValueManager)
                    {
                        // Set the return value
                        cardValues = this.CardValueManager.GetCardValues(this);
                    }

                    // return value
                    return cardValues;
                }
            }
            #endregion
            
            #region HasCardValueManager
            /// <summary>
            /// This property returns true if this object has a 'CardValueManager'.
            /// </summary>
            public bool HasCardValueManager
            {
                get
                {
                    // initial value
                    bool hasCardValueManager = (this.CardValueManager != null);
                    
                    // return value
                    return hasCardValueManager;
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
