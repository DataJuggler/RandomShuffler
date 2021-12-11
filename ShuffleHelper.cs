

#region using statements

using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using DataJuggler.UltimateHelper;

#endregion

namespace DataJuggler.RandomShuffler
{

    #region class ShuffleHelper
    /// <summary>
    /// This class is used to make shuffle operations easier to call from a client application.
    /// </summary>
    public static class ShuffleHelper
    {
        
        #region Methods

            #region Shuffle(this IList<T> list)
            /// <summary>
            /// This class is used to Shuffle a list of type 'T'.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="list"></param>
            public static List<T> Shuffle<T>(this IList<T> list)
            {
                // List
                List<T> shuffledList = new List<T>();

                // locals
                int randomIndex = -1;
                int cycles = 1;
                
                // create
                RandomNumberGenerator crypto = RandomNumberGenerator.Create();

                // Create the byte array that serves asa                               
                byte[] container = new byte[1];

                // if the list exists
                if ((list != null) && (list.Count > 0))
                {  
                    // we can't use the collection count it changes
                    int listCount = list.Count;

                    // set the cycles
                    cycles = (listCount / 255) + 1;

                    // now we have to 'Randomly' pull items and add them to the end results
                    for (int x = 0; x < listCount; x++)
                    {
                        // reset
                        randomIndex = -1;

                        // Fill the topOrBottom byteArray
                        crypto.GetBytes(container);

                        // iterate the cycles
                        for (int c = 0; c < cycles; c++)
                        {
                            // Get the value of topOrBottom
                            object randomByte = container.GetValue(0);

                            // if the randomByte exists
                            if (NullHelper.Exists(randomByte))
                            {  
                                // get a randomValue
                                int randomValue = NumericHelper.ParseInteger(randomByte.ToString(), -1, -1);

                                // set the randomIndex to the modulas of the the listCount
                                randomIndex += randomValue;
                            }
                        }

                        // ensure in range
                        randomIndex = (randomIndex % list.Count);

                        // verify the index is in range
                        if ((randomIndex < list.Count) && (randomIndex >= 0))
                        { 
                             // Add this integer 
                            shuffledList.Add(list[randomIndex]);

                            // if the index is in rage
                            if ((list.Count > 0) && (list.Count > randomIndex))
                            {
                                // Remove the item from the sourceList now that we have it
                                list.RemoveAt(randomIndex);
                            }
                        }
                    }
                }

                // return value
                return shuffledList;
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
