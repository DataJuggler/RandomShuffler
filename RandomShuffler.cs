

#region using statements

using System;
using System.Text;
using DataJuggler.UltimateHelper;
using System.Collections.Generic;
using DataJuggler.RandomShuffler.Objects;
using DataJuggler.RandomShuffler.Enumerations;
using DataJuggler.RandomShuffler.Interfaces;

#endregion

namespace DataJuggler.RandomShuffler
{

    #region class RandomShuffler
    ///<summary>
    /// This class is used to create large sets of random numbers for RandomMode of Integers, Cards and someday Dice.
    /// 
    /// RandomMode.Cards
    /// There is a special Constructor for RandomMode.Cards, you can create RandomShuffler(6) to create a six deck shoe.
    /// The MinValue is 1 and the MaxValue is set to 52 when you use RandomMode.Cards
    /// See the sample project CardCounter located in the SamplesFolder for a working example.
    /// Note that this value of 1 - 52 is only used for creating and filling the RandomCardStorage (a List of 'Card' objects).
    /// This does not affect the playing card value for the game or simulation you are running, as each Card object contains
    /// a CardValueManager that implements ICardValues that you set when you 
    /// 
    /// RandomMode.Integers
    /// For Integers you must set the MinValue and MaxValue and the SetsToFill property, see the Sample project ThreeDoorsGame
    /// located in the 'Samples' folder for a working example.
    ///</summary>
    public class RandomShuffler : IDisposable
    {

        #region Private Variables
        private List<int> randomIntStorage;
        private List<Card> randomCardStorage;
        private int minValue;
        private int maxValue;
        private int setsToInitialize;
        private bool shuffled;
        private int reshuffleMin;
        private ShuffleOptionManager shuffleOptions;
        private RandomModeEnum randomMode;
        private const int DefaultShuffleCount = 3;
        private const int CardMinValue = 1;
        private const int CardMaxValue = 52;
        private ICardValueManager cardValueManager;
        private int randomStorageCount;
        private const int NumberSuitsInDeck = 4;
        #endregion

        #region Constructors

            #region RandomShuffler(int numberDecks, ICardValueManager cardValueManager, int initialShuffles = DefaultShuffleCount, int beforeItemIsPulledShuffles = 0, int afterItemIsPulledShuffles = 0)
            /// <summary>
            /// Create a new instance of a RandomShuffler object; this constructor is for initializing RandomCardStorage.
            /// </summary>
            /// <param name="minValue"></param>
            /// <param name="maxValue"></param>
            /// <param name="numberToInitialize"></param>
            public RandomShuffler(int numberDecks, ICardValueManager cardValueManager, int initialShuffles = DefaultShuffleCount, int beforeItemIsPulledShuffles = 0, int afterItemIsPulledShuffles = 0)
            {
                // This is Cards Mode
                this.RandomMode = RandomModeEnum.Cards;

                // Store the CardValueManager
                this.CardValueManager = cardValueManager;
                
                // store the parameters
                this.MinValue = CardMinValue;
                this.MaxValue = CardMaxValue;
                this.SetsToInitialize = numberDecks;
            
                // Create the ShuffleOptions
                this.ShuffleOptions = new ShuffleOptionManager(initialShuffles, beforeItemIsPulledShuffles, afterItemIsPulledShuffles);

                // Perform initializations for this object
                Init();
            }
            #endregion

            #region Parameterized Constructor(int minValue, int maxValue, int setsToInitialize, int initialShuffles = DefaultShuffleCount, int beforeItemIsPulledShuffles = 0, int afterItemIsPulledShuffles = 0)
            /// <summary>
            /// Create a new instance of a RandomShuffler object; this constructor is for initializing RandomIntStorage.
            /// </summary>
            /// <param name="minValue"></param>
            /// <param name="maxValue"></param>
            /// <param name="numberToInitialize"></param>
            public RandomShuffler(int minValue, int maxValue, int setsToInitialize, int initialShuffles = DefaultShuffleCount, int beforeItemIsPulledShuffles = 0, int afterItemIsPulledShuffles = 0, int stopPercentage = 0)
            {
                // This is IntegerMode
                this.RandomMode = RandomModeEnum.Integers;

                // Create a new collection of 'int' objects.
                this.RandomIntStorage = new List<int>();

                // store the parameters
                this.MinValue = minValue;
                this.MaxValue = maxValue;
                this.SetsToInitialize = setsToInitialize;
            
                // Create the ShuffleOptions
                this.ShuffleOptions = new ShuffleOptionManager(initialShuffles, beforeItemIsPulledShuffles, afterItemIsPulledShuffles);

                // Perform initializations for this object
                Init();
            }
            #endregion

        #endregion

        #region Methods

            #region CanPullNextItem(int seed = 0)
            /// <summary>
            /// This item returns true if an item can be pulled using the (optional) seed given
            /// </summary>
            /// <param name="seed">This value is used to determine where the next time should
            /// be pulled from the 'RandomIntStorage'.</param>
            /// <param name="requiredMinimumItemsRemaining">Set this value to 10 if you need at least five cards
            /// for two hands of black jack for instance.</param>
            /// <returns></returns>
            public bool CanPullNextItem(int seed = 0, int requiredMinimumItemsRemaining = 0)
            {
                // initial value
                bool canPullNextItem = false;

                // If the RandomIntStorage object exists
                if (this.HasRandomIntStorage)
                {                    
                    // if there will be the required minimum in storage after one is taken out
                    if (this.RandomIntStorage.Count > (requiredMinimumItemsRemaining + 1))
                    {
                        // Set the value to true
                        canPullNextItem = true;
                    }
                }

                // return value
                return canPullNextItem;
            }
            #endregion

            #region Dispose()
            /// <summary>
            /// method Dispose
            /// </summary>
            public void Dispose()
            {
                // destroy the lists and objects that might be created
                randomIntStorage = null;
                randomCardStorage = null;
                cardValueManager = null;
                shuffleOptions = null;
            }
            #endregion
            
            #region GetCardName(int cardNumber)
            /// <summary>
            /// This method returns the Card Name for the cardNumber.
            /// This is used for new deck initialization only and this is not inteneded to be called after a deck is shuffled.
            /// <param name="cardNumber">The ordinal position of the card where 1 is the first card and 52 is the last card.</param>
            /// </summary>
            private CardEnum GetCardName(int cardNumber)
            {
               // initial value
                CardEnum cardName = CardEnum.Unknown;
                
                // If the value for cardNumber is between 1 and 52
                if ((cardNumber >=1) && (cardNumber <= 52))
                {
                    // set the return value
                    cardName = (CardEnum) ((cardNumber % 13) + 1);    
                }
                else
                {
                    // Raise an excpetion
                    throw new Exception("GetCardName was called with an invalid cardNumber.");
                }

               // return value
                return cardName;
            }
            #endregion
            
            #region GetSuit(int cardNumber)
            /// <summary>
            /// This method returns the Suit for the cardNumber given.
            /// This is used for new deck initialization only and this is not inteneded to be called after a deck is shuffled.
            /// <param name="cardNumber">The ordinal position of the card where 1 is the first card and 52 is the last card.</param>
            /// </summary>
            private SuitEnum GetSuit(int cardNumber)
            {
                // initial value
                SuitEnum suit = SuitEnum.Unknown;

                // if the cardNumber is higher than where Spades start
                if (cardNumber > 39)
                {
                    // set to spades
                    suit = SuitEnum.Spades;
                }
                else if (cardNumber > 26)
                {
                    // set to Hearts
                    suit = SuitEnum.Hearts;
                }
                else if (cardNumber > 13)
                {
                    // Set to Diamonds
                    suit = SuitEnum.Diamonds;
                }
                else if (cardNumber > 0)
                {
                    // Must be Clubs
                    suit = SuitEnum.Clubs;
                }

                // return value
                return suit;
            }
            #endregion
            
            #region Init()
            /// <summary>
            /// This method performs initializations for this object.
            /// </summary>
            public void Init()
            {
                // locals
                int milliSecond = DateTime.Now.Millisecond;
                
                // If this is Cards mode
                if (this.RandomMode == RandomModeEnum.Cards)
                {
                    // Initialize the RandomCardStorage
                    InitializeCards();
                }
                // if this IntegerMode
                else if (this.RandomMode == RandomModeEnum.Integers)
                {
                    // Initialize the integers
                    InitializeIntegers();
                }

                // if the StopPercentage is set and we have items in randomStorage
                if ((this.ShuffleOptions.StopPercentage > 0) && (randomStorageCount > 0))
                {
                    // Set the StopAtCount
                    this.ShuffleOptions.StopAtCount = (int)((randomStorageCount / 100) * this.ShuffleOptions.StopPercentage);
                }

                // If the ShuffleOptions object exists
                if ((this.HasShuffleOptions) && (this.ShuffleOptions.InitializationShuffles > 0))
                {
                    // Call the override for this method that repeats itself x number of times
                    Shuffle(this.ShuffleOptions.InitializationShuffles);

                    // The items have been shuffled
                    this.Shuffled = true;
                }
            }
            #endregion

            #region InitializeCards()
            /// <summary>
            /// This method Initialize Cards
            /// </summary>
            public void InitializeCards()
            {
                // locals
                SuitEnum suit = SuitEnum.Unknown;
                CardEnum cardName = CardEnum.Unknown;
                Card card = null;

                // If the CardValueManager object exists
                if (this.HasCardValueManager)
                {
                    // Create a new collection of Card' objects.
                    this.RandomCardStorage = new List<Card>();

                    // iterate the values up to the sets to initialize
                    for (int a = 0; a < this.SetsToInitialize; a++)
                    {
                        // iterate the values in between min and max (add all the card, dice, etc. possibilities)
                        for (int x = minValue; x <= maxValue; x++)
                        {
                            // Find the Suit based upon the cardNumber (x)
                            suit = GetSuit(x);

                            // Get the name of this card
                            cardName = GetCardName(x);

                            // Create a new instance of a 'Card' object.
                            card = new Card(suit, cardName, this.CardValueManager);

                            // Add the value for x
                            this.RandomCardStorage.Add(card);
                        }
                    }

                    // Set the Count
                    this.RandomStorageCount = this.RandomCardStorage.Count;
                }
                else
                {
                    // raise an exception
                    throw new Exception("The 'CardValueManager' must exist in the Init() method of a RandomShuffler object when using RandomMode.Cards.");
                }
            }
            #endregion
            
            #region InitializeIntegers()
            /// <summary>
            /// This method Initialize Integers
            /// </summary>
            private void InitializeIntegers()
           {
                // iterate the values up to the sets to initialize
                for (int a = 0; a < this.SetsToInitialize; a++)
                {
                    // iterate the values in between min and max (add all the card, dice, etc. possibilities)
                    for (int x = minValue; x <= maxValue; x++)
                    {
                        // Add the value for x
                        this.RandomIntStorage.Add(x);
                    }
                }

                // Set the Count
                this.RandomStorageCount = this.RandomIntStorage.Count;
            }
            #endregion

            #region PullNextCard(bool remove = true, bool ignoreShuffle = false, bool ignoreException = false
            /// <summary>
            /// This method returns the Next Card from the top of the 'Deck'.
            /// </summary>
            public Card PullNextCard(bool remove = true, bool ignoreShuffle = false, bool ignoreException = false)
            {
                // initial value
                Card nextCard = null;

                // If the RandomIntStorage object exists
                if (ListHelper.HasOneOrMoreItems(this.RandomCardStorage))
                {
                    // if ignreShuffle is false and the ShuffleOptions exist and have one or more shuffles required                                       
                    if ((!ignoreShuffle) && (this.HasShuffleOptions) && (this.ShuffleOptions.HasBeforeItemIsPulledShuffles))
                    {
                        // Shuffle the deck
                        this.Shuffle(this.ShuffleOptions.BeforeItemIsPulledShuffles);
                    }

                    // pull out the next Card off the top of the deck
                    nextCard = this.RandomCardStorage[0];

                    // if the value for remove is true
                    if (remove)
                    {
                        // Remove the Card that was just pulled
                        this.RandomCardStorage.RemoveAt(0);
                    }
                }
                else if (!this.HasRandomCardStorage)
                {
                    // if the value for ignoreException is false
                    if (!ignoreException)
                    {
                        // raise an exception
                        throw new Exception("The 'RandomCardStorage' property is not set in the 'PullNextCard' method.");
                    }
                }
                else 
                {
                    // if the value for ignoreException is false
                    if (!ignoreException)
                    {
                        // raise an exception
                        throw new Exception("The 'RandomCardStorage' property is empty in the 'PullNextItem' method.");
                    }
                }

                // If the nextCard object exists
                if (NullHelper.Exists(nextCard))
                {
                    // Uncomment this to send a report to the console
                    // LogReport(nextCard);

                    // if ignreShuffle is false and the ShuffleOptions exist and have one or more shuffles required                    
                    if ((!ignoreShuffle) && (this.HasShuffleOptions) && (this.ShuffleOptions.HasAfterItemIsPulledShuffles))
                    {
                        // Shuffle the deck
                        this.Shuffle(this.ShuffleOptions.AfterItemIsPulledShuffles);
                    }
                }

                // return value
                return nextCard;
            }
            #endregion
            
            #region PullNextItem(bool remove = true, bool ignoreShuffle = false, bool ignoreException = false)
            ///<summary>
            /// Returns a random number within the specified range.
            ///</summary>
            /// <param name="seed">An optional value, if present it is used to find where to pull the next item from randomly
            /// else if pulls the first item (index zero)</param>
            /// <param name="ignoreShuffle">If set to true this will override the settings for 
            /// 'AfterItemIsPulledShuffles' and 'BeforeItemIsPulledShuffles'.</param>
            /// <param name="ignoreException"></param>
            public int PullNextItem(bool remove = true, bool ignoreShuffle = false, bool ignoreException = false)
            {
                // initial value
                int nextItem = -1;

                // If the RandomIntStorage object exists
                if (ListHelper.HasOneOrMoreItems(RandomIntStorage))
                {
                    // if ignreShuffle is false and the ShuffleOptions exist and have one or more shuffles required                                       
                    if ((!ignoreShuffle) && (this.HasShuffleOptions) && (this.ShuffleOptions.HasBeforeItemIsPulledShuffles))
                    {                        
                        // Shuffle the deck
                        this.Shuffle(this.ShuffleOptions.BeforeItemIsPulledShuffles);
                    }

                    // pull out the next item
                    nextItem = this.RandomIntStorage[0];
                }
                else
                {
                    // if the value for ignoreException is false
                    if (!ignoreException)
                    {
                        // raise an exception
                        throw new Exception("The 'RandomIntStorage' property is empty in the 'PullNextItem' method.");
                    }
                }

                // If the nextItem object exists
                if (NullHelper.Exists(nextItem))
                {
                    // if remove is true
                    if (remove)
                    {
                        // Remove the item that was just pulled
                        this.RandomIntStorage.RemoveAt(0);
                    }

                    // if we have reached the ReshuffleMin
                    if (RandomIntStorage.Count <= ReshuffleMin)
                    {
                        // Reinitialize Integerse
                        InitializeIntegers();

                        // Reshuffle
                        Shuffle();
                    }

                    // if ignreShuffle is false and the ShuffleOptions exist and have one or more shuffles required                    
                    if ((!ignoreShuffle) && (this.HasShuffleOptions) && (this.ShuffleOptions.HasAfterItemIsPulledShuffles))
                    {
                        // Shuffle the deck
                        this.Shuffle(this.ShuffleOptions.AfterItemIsPulledShuffles);
                    }
                }

                // return value
                return nextItem;
            }
            #endregion

            #region Shuffle(int shuffleCount)
            /// <summary>
            /// This method shuffles the 'RandomIntStorage' as many times as the shuffleCount indicates.
            /// </summary>
            public void Shuffle(int shuffleCount)
            {
                // Shuffle as many times as the InitializationShuffles indicates
                for (int x = 0; x < shuffleCount; x++)
                {
                    // Shuffle the contents of RandomIntStorage
                    Shuffle();
                }

                // We have shuffled
                this.Shuffled = true;
            }
            #endregion
            
            #region Shuffle()
            /// <summary>
            /// This method Shuffles the 'RandomIntStorage' one time.
            /// </summary>
            public void Shuffle()
            {  
                // If the RandomCardStorage object exists
                if ((this.RandomMode == RandomModeEnum.Cards) && (this.HasRandomCardStorage))
                {
                    // Shuffle the list of integers
                    this.RandomCardStorage = ShuffleHelper.Shuffle<Card>(this.RandomCardStorage);
                }
                // If the RandomIntStorage object exists
                else if ((this.RandomMode == RandomModeEnum.Integers) && (this.HasRandomIntStorage))
                {
                    // Shuffle the list of integers
                    this.RandomIntStorage = ShuffleHelper.Shuffle<int>(this.RandomIntStorage);
                }                
            }
        #endregion

        #endregion

        #region Properties

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
     
            #region HasRandomCardStorage
            /// <summary>
            /// This property returns true if this object has a 'RandomCardStorage'.
            /// </summary>
            public bool HasRandomCardStorage
            {
                get
                {
                    // initial value
                    bool hasRandomCardStorage = (this.RandomCardStorage != null);
                    
                    // return value
                    return hasRandomCardStorage;
                }
            }
            #endregion
            
            #region HasRandomIntStorage
            /// <summary>
            /// This property returns true if this object has a 'RandomIntStorage'.
            /// </summary>
            public bool HasRandomIntStorage
            {
                get
                {
                    // initial value
                    bool hasRandomIntStorage = (this.RandomIntStorage != null);
                    
                    // return value
                    return hasRandomIntStorage;
                }
            }
            #endregion
            
            #region HasRandomStorageCount
            /// <summary>
            /// This property returns true if the 'RandomStorageCount' is set.
            /// </summary>
            public bool HasRandomStorageCount
            {
                get
                {
                    // initial value
                    bool hasRandomStorageCount = (this.RandomStorageCount > 0);
                    
                    // return value
                    return hasRandomStorageCount;
                }
            }
            #endregion
            
            #region HasShuffleOptions
            /// <summary>
            /// This property returns true if this object has a 'ShuffleOptions'.
            /// </summary>
            public bool HasShuffleOptions
            {
                get
                {
                    // initial value
                    bool hasShuffleOptions = (this.ShuffleOptions != null);
                    
                    // return value
                    return hasShuffleOptions;
                }
            }
            #endregion
            
            #region MaxValue
            /// <summary>
            /// This property gets or sets the value for 'MaxValue'.
            /// </summary>
            public int MaxValue
            {
                get { return maxValue; }
                set { maxValue = value; }
            }
            #endregion

            #region MinValue
            /// <summary>
            /// This property gets or sets the value for 'MinValue'.
            /// </summary>
            public int MinValue
            {
                get { return minValue; }
                set { minValue = value; }
            }
            #endregion

            #region RandomCardStorage
            /// <summary>
            /// This property gets or sets the value for 'RandomCardStorage'.
            /// </summary>
            public List<Card> RandomCardStorage
            {
                get { return randomCardStorage; }
                set { randomCardStorage = value; }
            }
            #endregion
            
            #region RandomIntStorage
            /// <summary>
            /// This property gets or sets the value for 'RandomIntStorage'.
            /// </summary>
            public List<int> RandomIntStorage
            {
                get { return randomIntStorage; }
                set { randomIntStorage = value; }
            }
            #endregion

            #region RandomMode
            /// <summary>
            /// This property gets or sets the value for 'RandomMode'.
            /// </summary>
            public RandomModeEnum RandomMode
            {
                get { return randomMode; }
                set { randomMode = value; }
            }
            #endregion
            
            #region RandomStorageCount
            /// <summary>
            /// This property gets or sets the value for 'RandomStorageCount'.
            /// </summary>
            public int RandomStorageCount
            {
                get { return randomStorageCount; }
                set { randomStorageCount = value; }
            }
            #endregion

            #region CardsInDeck
            /// <summary>
            /// This static property returns the number of cards in a deck
            /// </summary>
            public static int CardsInDeck
            {
                get
                {
                    // return the number of cards in a deck
                    return CardMaxValue;
                }
            }
            #endregion
            
            #region ReshuffleMin
            /// <summary>
            /// This property gets or sets the value for 'ReshuffleMin'.
            /// </summary>
            public int ReshuffleMin
            {
                get { return reshuffleMin; }
                set { reshuffleMin = value; }
            }
            #endregion
            
            #region SetsToInitialize
            /// <summary>
            /// This property gets or sets the value for 'SetsToInitialize'.
            /// </summary>
            public int SetsToInitialize
            {
                get { return setsToInitialize; }
                set { setsToInitialize = value; }
            }
            #endregion

            #region Shuffled
            /// <summary>
            /// This property gets or sets the value for 'Shuffled'.
            /// </summary>
            public bool Shuffled
            {
                get { return shuffled; }
                set { shuffled = value; }
            }
            #endregion

            #region ShuffleOptions
            /// <summary>
            /// This property gets or sets the value for 'ShuffleOptions'.
            /// </summary>
            public ShuffleOptionManager ShuffleOptions
            {
                get{ return shuffleOptions; }
                set { shuffleOptions = value; }
            }
            #endregion

            #region SuitsInDeck
            /// <summary>
            /// This static property returns the number of suits in a deck (4).
            /// </summary>
            public static int SuitsInDeck
            {
                get
                {
                    // return the number of suits in a deck
                    return NumberSuitsInDeck;
                }
            }
            #endregion
            
        #endregion


    }
    #endregion

}
