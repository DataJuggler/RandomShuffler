

#region using statements

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace DataJuggler.RandomShuffler
{

    #region class ShuffleOptionManager
    /// <summary>
    /// This class is used to keep track of options used by the RandomShuffler.
    /// </summary>
    public class ShuffleOptionManager
    {
        
        #region Private Variables
        private int initializationShuffles;
        private int beforeItemIsPulledShuffles;
        private int afterItemIsPulledShuffles;
        private double stopPercentage;
        private int stopAtCount;
        private const int DefaultInitializationShuffles = 3;
        #endregion

        #region Parameterized Constructor(int initializationShuffles = DefaultInitializationShuffles, int beforeItemIsPulledShuffles = 0, afterItemIsPulledShuffles = 0)
        /// <summary>
        /// Create a new instance of a ShuffleOptionManager
        /// </summary>
        /// <param name="initializationShuffles"></param>
        /// <param name="beforeItemIsPulledShuffles"></param>
        /// <param name="?"></param>
        public ShuffleOptionManager(int initializationShuffles = DefaultInitializationShuffles, int beforeItemIsPulledShuffles = 0, int afterItemIsPulledShuffles = 0)
        {
            // Store the parameters
            this.InitializationShuffles = initializationShuffles;
            this.BeforeItemIsPulledShuffles = beforeItemIsPulledShuffles;
            this.AfterItemIsPulledShuffles = afterItemIsPulledShuffles;
        }
        #endregion

        #region Properties
            
            #region AfterItemIsPulledShuffles
            /// <summary>
            /// This property gets or sets the value for 'AfterItemIsPulledShuffles'.
            /// </summary>
            public int AfterItemIsPulledShuffles
            {
                get { return afterItemIsPulledShuffles; }
                set { afterItemIsPulledShuffles = value; }
            }
            #endregion
            
            #region BeforeItemIsPulledShuffles
            /// <summary>
            /// This property gets or sets the value for 'BeforeItemIsPulledShuffles'.
            /// </summary>
            public int BeforeItemIsPulledShuffles
            {
                get { return beforeItemIsPulledShuffles; }
                set { beforeItemIsPulledShuffles = value; }
            }
            #endregion
            
            #region HasAfterItemIsPulledShuffles
            /// <summary>
            /// This property returns true if the 'AfterItemIsPulledShuffles' is set.
            /// </summary>
            public bool HasAfterItemIsPulledShuffles
            {
                get
                {
                    // initial value
                    bool hasAfterItemIsPulledShuffles = (this.AfterItemIsPulledShuffles > 0);
                    
                    // return value
                    return hasAfterItemIsPulledShuffles;
                }
            }
            #endregion
            
            #region HasBeforeItemIsPulledShuffles
            /// <summary>
            /// This property returns true if the 'BeforeItemIsPulledShuffles' is set.
            /// </summary>
            public bool HasBeforeItemIsPulledShuffles
            {
                get
                {
                    // initial value
                    bool hasBeforeItemIsPulledShuffles = (this.BeforeItemIsPulledShuffles > 0);
                    
                    // return value
                    return hasBeforeItemIsPulledShuffles;
                }
            }
            #endregion
            
            #region HasInitializationShuffles
            /// <summary>
            /// This property returns true if the 'InitializationShuffles' is set.
            /// </summary>
            public bool HasInitializationShuffles
            {
                get
                {
                    // initial value
                    bool hasInitializationShuffles = (this.InitializationShuffles > 0);
                    
                    // return value
                    return hasInitializationShuffles;
                }
            }
            #endregion
            
            #region InitializationShuffles
            /// <summary>
            /// This property gets or sets the value for 'InitializationShuffles'.
            /// </summary>
            public int InitializationShuffles
            {
                get {return initializationShuffles; }
                set { initializationShuffles = value; }
            }
            #endregion
            
            #region StopAtCount
            /// <summary>
            /// This property gets or sets the value for 'StopAtCount'.
            /// </summary>
            public int StopAtCount
            {
                get { return stopAtCount; }
                set { stopAtCount = value; }
            }
            #endregion
            
            #region StopPercentage
            /// <summary>
            /// This property gets or sets the value for 'StopPercentage'.
            /// </summary>
            public double StopPercentage
            {
                get { return stopPercentage; }
                set { stopPercentage = value; }
            }
            #endregion
            
        #endregion
        
    }
    #endregion

}
