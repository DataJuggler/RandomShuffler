# RandomShuffler
RandomShuffler is a .NET 7 Nuget Package and project uses even random distribution and has built in methods for shuffling Cards and integers for Dice games and other uses.

Random Shuffler comes with two shufflers. For small values (less than 100), the RandomShuffler class is well suited for continually drawing numbers or cards, and reshuffling when the reshuffle threshhold is reached. True random numbers can take a very long time to even out, where as an even distrution system will distribute evenly.

I just released a demo project: Random Bytes

Random Bytes - Example Win Forms app
https://github.com/DataJuggler/RandomBytes 

Create a random shuffler for cards:

  // create the RandomShuffler object for cards
  int numberDecks = 1;
  int initialShuffles = ShufflesControl.IntValue;
  
  // Create a new RandomShuffler for Cards
  RandomShuffler shuffler = new RandomShuffler(numberDecks, BlackJackCardValueManager, initialShuffles);

using DataJuggler.RandomShuffler;
using DataJuggler.RandomShuffler.Enumerations;

Create a random shuffler for integers

    // How many random sets to create
    int setsToInitialize = 10;

    // Create a RandomShuffler
    RandomShuffler shuffler = new RandomShuffler(MinControl.IntValue, MaxControl.IntValue, setsToInitialize, initialShuffles);
    

    
Create a LargeNumberShuffler to draw numbers between 1 and 1 million


    // Create a new instance of a 'LargeNumberShuffler' object.
    int digits = 7;
    int min = 1;
    int max = 1000000;
    LargeNumberShuffler Shuffler = new LargeNumberShuffler(digits, min, max, NumberOutOfRangeOptionEnum.ReturnModulus);

To pull items:

    value = Shuffler.PullNumber();
    
You can also create random numbers on my site: https://datajuggler.com/Random (coming soon).
