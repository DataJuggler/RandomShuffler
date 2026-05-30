

#region using statements

using DataJuggler.RandomShuffler.Enumerations;
using DataJuggler.RandomShuffler.Interfaces;
using System.Collections.Generic;
using System.Drawing;

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
        private string path;
        private Bitmap bitmap;
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
            this.CardValue = cardValue;
        }
        #endregion
        
        #region Properties
            
            #region Bitmap
            /// <summary>
            /// This property gets or sets the value for 'Bitmap'.
            /// </summary>
            public Bitmap Bitmap
            {
                get { return bitmap; }
                set { bitmap = value; }
            }
            #endregion
            
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
            
            #region HasBitmap
            /// <summary>
            /// This property returns true if this object has a 'Bitmap'.
            /// </summary>
            public bool HasBitmap
            {
                get
                {
                    // initial value
                    bool hasBitmap = (Bitmap != null);

                    // return value
                    return hasBitmap;
                }
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
            
            #region Path
            /// <summary>
            /// This property gets or sets the value for 'Path'.
            /// </summary>
            public string Path
            {
                get { return path; }
                set { path = value; }
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
