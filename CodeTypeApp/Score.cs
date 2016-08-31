using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTypeApp {
    /// <summary>
    /// Object to hold a user's scoring data for a fragment.
    /// Also includes methods to calculate some statistics regarding performance.
    /// </summary>
    class Score {
        // constant to define the length of a word for calculations
        public const double Chars_Per_Word = 5;

        // fields holding raw performance data:
        private long bestTime;       // shortest time taken to finish fragment, in ms
        private int neededChars;     // number of code characters in the fragment (minimum number of typed character possible)
        private int bestTyped;       // number of characters the user typed for best time
        private int bestErased;      // number of times the user erased a character they typed for best time

        // fields for cumulative perfomance data
        private int completions;     // total times completed
        private double totalTime;    // total time taken
        private ulong totalTyped;    // total number of characters typed
        private ulong totalErased;   // total number of characters erased
        private double avgWPM;       // cumulative moving average of WPM
        private double avgCPM;       // cumulative moving average of CPM
        private double avgErr;       // cumulative moving average of % error
         
        // accessor fields to get the data values:
        public long BestTime { get { return bestTime; } }
        public int Needed { get { return neededChars; } }
        public int BestTyped { get { return bestTyped; } }
        public int BestErased { get { return bestErased; } }
        public double BestCPM { get { return this.CalcCPM(bestTime); } }
        public double BestWPM { get { return this.CalcWPM(bestTime); } }
        public double BestError { get { return this.CalcError(bestTyped); } }
        public double AverageWPM { get { return this.avgWPM; } }
        public double AverageCPM { get { return this.avgCPM; } }
        public double AverageError { get { return this.avgErr; } }


        /// <summary>
        /// Basic constructor that sets the data fields to the corresponding parameters for a new score.
        /// </summary>
        /// <param name="time"></param>
        /// <param name="needed"></param>
        /// <param name="typed"></param>
        /// <param name="backspaces"></param>
        public Score(long time, int needed, int typed, int backspaces) {
            // set the highscore fields
            this.bestTime = time;
            this.neededChars = needed;
            this.bestTyped = typed;
            this.bestErased = backspaces;

            // set the cumulative fields to the same values since there has only been one completion
            this.totalErased = (ulong)this.bestErased;
            this.totalTime = (ulong)this.bestTime;
            this.totalTyped = (ulong)this.bestTyped;
            this.avgCPM = this.CalcCPM(time);
            this.avgWPM = this.CalcWPM(time);
            this.avgErr = this.CalcError(typed);

            // set the number of completions to 1
            this.completions = 1;
        }

        /// <summary>
        /// Calculate and return the characters per minute (CPM) for the given time
        /// </summary>
        /// <param name="time">completion time in ms</param>
        /// <returns>CPM of the given completion time</returns>
        public double CalcCPM(long time) {
            return neededChars / (time / 60000.0);
        }

        /// <summary>
        /// Calculate and return the words per minute (WPM) for given time
        /// </summary>
        /// <param name="time">completion time in ms</param>
        /// <returns>WPM of the fastest completion</returns>
        public double CalcWPM(long time) {
            return (neededChars / Chars_Per_Word) / (time / 60000.0);
        }

        /// <summary>
        /// Calculate and return the error percentage for number of characters typed
        /// </summary>
        /// <returns>% Error for the given number of characters typed</returns>
        public double CalcError(int typed) {
            return (typed - neededChars) * 100.0 / neededChars;
        }

        /// <summary>
        /// Add the new data to the score and update the values as applicable
        /// </summary>
        /// <param name="newTime"></param>
        /// <param name="newTyped"></param>
        /// <param name="newErased"></param>
        public void Update(long newTime, int newTyped, int newErased) {
            // accumulate the totals
            completions++;
            totalTime += (ulong)newTime;
            totalTyped += (ulong)newTyped;
            totalErased += (ulong)newErased;

            // update the averages
            avgCPM += (this.CalcCPM(newTime) - avgCPM) / completions;
            avgWPM += (this.CalcWPM(newTime) - avgWPM) / completions;
            avgErr += (this.CalcError(newTyped) - avgErr) / completions;

            // update the highscore, only if the newTime is shorter than the current highscore
            if (newTime < bestTime) {
                bestTime = newTime;
                bestTyped = newTyped;
                bestErased = newErased;
            }
        }

    }
}
