using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace CodeTypeApp {
    /// <summary>
    /// Interface to define the minimum required functionality of a CodeFragment object,
    /// which is meant as a container for a section of code and can identify which characters are
    /// meant to be typed (code) and which characters should be skipped (comment).
    /// </summary>
    interface ICodeFragment : ISerializable {
        /// <summary>
        /// Returns the index of the first character that is not part of a comment, relative to the
        /// beginning of the fragment.
        /// </summary>
        /// <returns>index of first non-comment character</returns>
        int FirstCodePos();

        /// <summary>
        /// Returns the character at the specified position within the fragment.
        /// </summary>
        /// <param name="pos"></param>
        /// <returns>character at the position</returns>
        char GetChar(int pos);

        /// <summary>
        /// Returns the number of code characters in the fragment
        /// </summary>
        /// <returns>number of code characters</returns>
        int GetCodeCount();

        /// <summary>
        /// Returns the fragment as a string with all non-code characters omitted.
        /// </summary>
        /// <returns>string containing only the code characters</returns>
        string GetCodeOnly();

        /// <summary>
        /// Returns the size, in characters, of the fragment
        /// </summary>
        /// <returns>number of characters in fragment</returns>
        int GetSize();

        /// <summary>
        /// Finds and returns the index of the first encountered code character following the specified position.
        /// </summary>
        /// <param name="pos">index to begin search at</param>
        /// <returns>index of first encountered code character</returns>
        int NextCodePos(int pos);

        /// <summary>
        /// Finds and returns the index of the first encountered code character preceding the specified position.
        /// </summary>
        /// <param name="pos">index to begin search at</param>
        /// <returns>index of first encountered code character</returns>
        int PrevCodePos(int pos);

        /// <summary>
        /// Check if the character at the specified position is a code character or not.
        /// </summary>
        /// <param name="pos">position of character in the fragment</param>
        /// <returns>false if character is non-code, true otherwise</returns>
        bool IsCode(int pos);
    }
}
