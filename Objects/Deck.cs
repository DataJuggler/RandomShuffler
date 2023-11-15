

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.RandomShuffler.Objects
{

    #region class Deck
    /// <summary>
    /// This class represents a 'Deck' of cards.
    /// </summary>
    public class Deck
    {
        
        #region Private Variables
        private List<Card> cards;
        #endregion

        #region Methods

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
            
        #endregion
        
    }
    #endregion

}
