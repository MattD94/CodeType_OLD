using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTypeApp {
    /// <summary>
    /// Container that holds all data pertaining to a lesson, which is a series of consecutive code fragments (usually from the same source)
    /// as well as the users scores and progress towards completion.
    /// </summary>
    [Serializable]
    class Lesson {
        // fields for lesson content and score information
        private int highestCompleted; // how many fragments of the lesson have been completed
        private int totalCompletions; // how many times a fragment has been completed
        private Score[] scores;       // scores for each fragment
        private List<ICodeFragment> fragments; // fragments in the lesson

        // freely changable descriptive fields
        public string CodeLanguage { get; set; } // language(s) of the code in the fragments
        public string Description { get; set; } // optional description of the lesson or code
        public string Name { get; set; } // name of the lesson

        /// <summary>
        /// Basic constructor to create a new lesson based on the given fragments.
        /// </summary>
        /// <param name="fragments">code fragments to use for the lesson</param>
        public Lesson(IEnumerable<ICodeFragment> fragments) {
            // create the fragment list and the corresponding score array
            this.fragments = new List<ICodeFragment>(fragments);
            this.scores = new Score[this.fragments.Count];

            //initialize highestCompleted to -1 since no fragments have been completed yet
            this.highestCompleted = -1;
        }

        /// <summary>
        /// Constructor used when loading a lesson from a save.
        /// Takes parameters for all fields
        /// </summary>
        /// <param name="f">fragments</param>
        /// <param name="tc">total completions</param>
        /// <param name="s">scores</param>
        /// <param name="hc">highestCompleted</param>
        /// <param name="l">Language</param>
        /// <param name="d">Description</param>
        /// <param name="n">Name</param>
        public Lesson(List<ICodeFragment> f, int tc, Score[] s, int hc, string l, string d, string n) {
            this.fragments = f;
            this.totalCompletions = tc;
            this.scores = s;
            this.highestCompleted = hc;
            this.CodeLanguage = l;
            this.Description = d;
            this.Name = n;
        }

        /// <summary>
        /// Method used to update the lesson progress by updating the fragment scores.
        /// Only completed fragments or the fragment following the completed fragment can be updated
        /// </summary>
        /// <param name="fragment"></param>
        /// <param name="score"></param>
        public void UpdateScore(int fragment, long time, int typed, int erased) {
            // throw an exception if the fragment is too far ahead in the lesson
            if (fragment - highestCompleted > 1 || fragment >= fragments.Count)
                throw new ArgumentOutOfRangeException("fragment index too large!");
            // throw an exception if fragment is less than 0
            if (fragment < 0)
                throw new ArgumentOutOfRangeException("fragment index cannot be less than zero!");

            // incriment the number of completions for the lesson:
            totalCompletions++;

            // if this fragment hasn't been completed before, create a new score
            if (fragment > highestCompleted) {
                highestCompleted++;
                scores[fragment] = new Score(time, fragments[fragment].GetCodeCount(), typed, erased);
            }
            // otherwise update the fragment score
            else {
                scores[fragment].Update(time, typed, erased);
            }
        }

        /*
            Todo:
            
            GetScore
            GetFragment
            ClearScores
            CalcLessonWPM
            CalcLessonCPM
            CalcLessonError
        */
    }

}
