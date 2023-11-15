

#region using statements

using System;
using System.Collections.Generic;
using DataJuggler.RandomShuffler.Objects;

#endregion

namespace DataJuggler.RandomShuffler.Interfaces
{

    #region interface ICardContainer
    /// <summary>
    /// This interface is used by Decks or Shoe to contain a List of 'Card' objects
    /// and they must have a value for Penetration indicating when they will
    /// be reshuffled. For Black Jack this value should be in the range of 40 - 80, but 
    /// for poker this value will be zero.
    /// </summary>
    public interface ICardContainer
    {

        #region Methods

        #endregion

        #region Properties

            #region Cards
            /// <summary>
            /// This property gets or sets the value for 'Cards'.
            /// This is used by either a 'Deck' object or a 
            /// 'Shoe' object. 
            /// </summary>
            List<Card> Cards
            {
                get;
                set;
            }
            #endregion

            #region Penetration
            /// <summary>
            /// This property is used to indicate when a deck or a shoe should be reshuffled.
            /// </summary>
            int Penetration
            {
                get;
                set;
            }
            #endregion

            #region ReshuffleCardIndex
            /// <summary>
            /// This read only property returns the CardIndex that
            /// indicates a reshuffle must occur before another hand
            /// can be dealt
            /// </summary>
            int ReshuffleCardIndex
            {
                get;
            }
            #endregion

            #region HasCards;
            /// <summary>
            /// Tihis read only property will return true if the Cards collection exists.
            /// </summary>
            bool HasCards
            {
                get;
            }
            #endregion

            #region HasPenetration
            /// <summary>
            /// Tihis read only property will return true if the Penetration value is set.
            /// </summary>
            bool HasPenetration
            {
                get;
            }
            #endregion

        #endregion

    }
    #endregion

}
