

#region using statements

using DataJuggler.RandomShuffler.Enumerations;
using DataJuggler.UltimateHelper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.RandomShuffler
{

    #region class LargeNumberShuffler
    /// <summary>
    /// This method is used to create large random numbers by creating each digit
    /// separetly, which is actually faster than shuffling numbers of 10,000 with RandomShuffler
    /// </summary>
    public class LargeNumberShuffler
    {
        
        #region Private Variables
        private int digits;
        private int minValue;
        private int maxValue;
        private NumberOutOfRangeOptionEnum numberOutOfRangeOption;
        private List<RandomShuffler> shufflers;
        private int repullNumber;
        #endregion

        #region Constructor
        /// <summary>
        /// Create a new instance of a LargeNumberShuffler
        /// </summary>
        /// <param name="digits"></param>
        /// <param name="minValue"></param>
        /// <param name="maxValue"></param>
        /// <param name="numberOutOfRangeOption"></param>
        public LargeNumberShuffler(int digits, int minValue, int maxValue, NumberOutOfRangeOptionEnum numberOutOfRangeOption)
        {
            // store the args
            this.Digits = digits;
            this.MinValue = minValue;
            this.MaxValue = maxValue;
            this.NumberOutOfRangeOption = numberOutOfRangeOption;

            // Create the Shufflers
            this.Shufflers = new List<RandomShuffler>();

            // if greater than 9
            if (digits > 9)
            {
                // Limit the number to 9 digits
                throw new Exception("Digits cannot be over 9 digits");
            }

            // If the value for digits is greater than zero
            if (digits > 0)
            {
                // iterate up to the number of digits
                for (int x = 1; x <= digits; x++)
                {
                    // Create a new Shuffler for this digit
                    RandomShuffler shuffler = new RandomShuffler(0, 9, 10);

                    // Add this shuffler
                    this.Shufflers.Add(shuffler);
                }
            }
        }
        #endregion

        #region Methods

            #region PullNumber()
            /// <summary>
            /// This method returns the Number
            /// </summary>
            public int PullNumber()
            {
                // initial value
                int pullNumber = -3;

                try
                {
                    // locals
                    bool firstNonZeroPulled = false;
                    int temp = 0;

                    // if the Shufflers exist
                    if (this.HasShufflers)
                    {
                        // Create a string builder
                        StringBuilder sb = new StringBuilder();

                        // Iterate the collection of RandomShuffler objects
                        foreach (RandomShuffler shuffler in Shufflers)
                        {
                            // Pull the next value
                            temp = shuffler.PullNextItem();

                            // if we already have the first non zero
                            if ((firstNonZeroPulled) || (MinValue == 0))
                            {
                                // Add this item
                                sb.Append(temp);
                            }
                            else
                            {
                                // If the value for temp is greater than zero
                                if (temp > 0)
                                {
                                    // Set to true
                                    firstNonZeroPulled = true;

                                    // Add this item
                                    sb.Append(temp);
                                }
                                else
                                {
                                    // do nothing for preceding zeros
                                }
                            }
                        }

                        // Get a temporary string to hold the number
                        string tempString = sb.ToString();

                        // Now set the return value
                        pullNumber = NumericHelper.ParseInteger(tempString, -1, -2);

                        // if this is not the defaultValue or the errorValue
                        if (MinValue == 1)
                        {
                            // Increment the value for pullNumber because the needed range is 1 - 1,000,000 and this code returns 0 - 999,999
                            pullNumber++;
                        }

                        // if the number is out of range
                        if ((pullNumber < this.MinValue) || (pullNumber > this.MaxValue))
                        {
                            // if raise error is supposed to happen
                            if (this.NumberOutOfRangeOption == NumberOutOfRangeOptionEnum.RaiseError)
                            {
                                // Raise the error
                                throw new Exception("The number generated " + pullNumber + " is out of range.");
                            }
                            else if (this.NumberOutOfRangeOption == NumberOutOfRangeOptionEnum.Repull)
                            {
                                // Increment the value for this
                                this.RepullNumber++;

                                // Do not try more than 10 times in case bad min and max values were set
                                if (this.RepullNumber < 11)
                                {
                                    // Call this method recursively
                                    pullNumber = PullNumber();
                                }
                            }
                            else if (this.NumberOutOfRangeOption == NumberOutOfRangeOptionEnum.ReturnModulus)
                            {
                                // if the MaxValue is set
                                if (this.MaxValue > 0)
                                {  
                                    // Get the modulusj + the MinValue
                                    pullNumber = (pullNumber % this.MaxValue) + MinValue;
                                }
                            }
                        }
                    }
                }
                catch (Exception error)
                {
                    // Set to a bad value to indicate an error
                    pullNumber = -4;

                    // Write to the error console
                    DebugHelper.WriteDebugError("PullNumber", "LargeNumberShuffler", error);
                }
                
                // return value
                return pullNumber;
            }
            #endregion
            
        #endregion

        #region Properties

            #region Digits
            /// <summary>
            /// This property gets or sets the value for 'Digits'.
            /// </summary>
            public int Digits
            {
                get { return digits; }
                set { digits = value; }
            }
            #endregion
            
            #region HasShufflers
            /// <summary>
            /// This property returns true if this object has a 'Shufflers'.
            /// </summary>
            public bool HasShufflers
            {
                get
                {
                    // initial value
                    bool hasShufflers = (this.Shufflers != null);
                    
                    // return value
                    return hasShufflers;
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
            
            #region NumberOutOfRangeOption
            /// <summary>
            /// This property gets or sets the value for 'NumberOutOfRangeOption'.
            /// </summary>
            public NumberOutOfRangeOptionEnum NumberOutOfRangeOption
            {
                get { return numberOutOfRangeOption; }
                set { numberOutOfRangeOption = value; }
            }
            #endregion
            
            #region RepullNumber
            /// <summary>
            /// This property gets or sets the value for 'RepullNumber'.
            /// </summary>
            public int RepullNumber
            {
                get { return repullNumber; }
                set { repullNumber = value; }
            }
            #endregion
            
            #region Shufflers
            /// <summary>
            /// This property gets or sets the value for 'Shufflers'.
            /// </summary>
            public List<RandomShuffler> Shufflers
            {
                get { return shufflers; }
                set { shufflers = value; }
            }
            #endregion
            
        #endregion

    }
    #endregion

}
