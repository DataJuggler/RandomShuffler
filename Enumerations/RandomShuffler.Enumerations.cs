

#region using statements

#endregion

namespace DataJuggler.RandomShuffler.Enumerations
{

    #region CardEnum : int
    /// <summary>
	/// This enum is used to indicate the name of the Card.
	/// </summary>
	public enum CardEnum : int
	{
        Unknown = 0,
		Ace = 1,
		Two = 2,
		Three = 3,
		Four = 4,
		Five = 5,
		Six = 6,
		Seven = 7,
		Eight = 8,
		Nine = 9,
		Ten = 10,
		Jack = 11,
		Queen = 12,
		King = 13
	}
    #endregion

    #region NumberOutOfRangeOptionEnum : int
    /// <summary>
    /// This enumeration is used to determine how the LargeNumberShuffler should handle a number outside of the Min and Max.
    /// </summary>
    public enum NumberOutOfRangeOptionEnum : int
    {
        Repull = 0,
        ReturnModulus = 1,
        RaiseError =2
    }
    #endregion

    #region RandomModeEnum
    /// <summary>
    /// This enumeration is used for the type of storage being used.
    /// For now only Integers and Cards are implemented.
    /// </summary>
    public enum RandomModeEnum
    {
        Integers = 0,
        Cards = 1,
        Dice = 2
    }
    #endregion

    #region SuitEnum : int
    /// <summary>
	/// This enum is used to indicate the Suit of the card.
	/// </summary>
	public enum SuitEnum : int
    {
        Unknown = 0,
		Clubs = 1,
		Diamonds = 2,
		Hearts = 3,
		Spades = 4
	}
	#endregion
    
}
