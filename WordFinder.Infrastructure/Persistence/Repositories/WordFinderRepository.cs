using System.Text;
using WordFinder.Application.Interface.Persistence;

namespace WordFinder.Infrastructure.Persistence.Repositories
{
    public class WordFinderRepository: IWordFinderRepository
    {
        /// <summary>
        /// This method takes a matrix and a wordstream as input and returns the list of the 10 more repeated words found in the matrix.
        /// </summary>
        /// <param name="matrix">Is an array of strings representing the matrix.</param>
        /// <param name="wordstream">Is an array of strings representing the words to search for.</param>
        /// <returns></returns>
        public Task<IEnumerable<string>> Find(IEnumerable<string> matrix, IEnumerable<string> wordstream)
        {
            // Run FindHorizontal and FindVertical in parallel
            var horizontalTask = Task.Run(() => FindHorizontal(matrix.ToArray(), wordstream.ToArray()));
            var verticalTask = Task.Run(() => FindVertical(matrix.ToArray(), wordstream.ToArray()));

            // Wait for both tasks to complete
            Task.WaitAll(horizontalTask, verticalTask);

            var words = new List<string>();
            words.AddRange(horizontalTask.Result);
            words.AddRange(verticalTask.Result);

            //return the 10 most repeated words
            var result = words.GroupBy(x => x)
                .OrderByDescending(x => x.Count())
                .Take(10)
                .Select(x => $"{x.Count()}x {x.Key}");
            return Task.FromResult(result);
        }

        private string[] GetColumn(string[] matrix, int index)
        {
            return matrix.Select(row => row[index].ToString()).ToArray();
        }

        /// <summary>
        /// This method finds horizontal words in the matrix and returns them.
        /// </summary>
        /// <param name="matrixArray">An array of strings representing the matrix.</param>
        /// <param name="wordstreamArray">An array of strings representing the words to search for.</param>
        private IEnumerable<string> FindHorizontal(string[] matrixArray, string[] wordstreamArray)
        {
            List<string> words = new List<string>();
            // Create a HashSet of lowercase and trimmed words for efficient search
            HashSet<string> wordSet = new HashSet<string>(wordstreamArray.Select(w => w.ToLower().Trim()));
            StringBuilder wordFound = new StringBuilder();

            for (int m = 0; m < matrixArray.Length; m++)
            {
                // Get the current row and convert it to lowercase and trim
                string row = matrixArray[m].ToLower().Trim();
                foreach (string word in wordSet)
                {
                    foreach (char rowElement in row)
                    {
                        // Check if the constructed word matches the current word in the wordSe
                        if (wordFound.Length == word.Length)
                        {
                            wordFound.Clear();
                        }
                        else if (wordFound.Length < word.Length && word[wordFound.Length].ToString() == rowElement.ToString())
                        {
                            // Append the character to the wordFound StringBuilder if it matches
                            wordFound.Append(rowElement);
                            if (wordFound.Length == word.Length)
                            {
                                // If the word is complete, add it to the list of words and clear the StringBuilder
                                words.Add(wordFound.ToString().ToUpperInvariant());
                                wordFound.Clear();
                            }
                        }
                        else
                        {
                            wordFound.Clear();
                        }
                    }
                }
            }

            return words;
        }

        private IEnumerable<string> FindVertical(string[] matrixArray, string[] wordstreamArray)
        {
            List<string> words = new List<string>();
            // Create a HashSet of lowercase and trimmed words for efficient search
            HashSet<string> wordSet = new HashSet<string>(wordstreamArray.Select(w => w.ToLower().Trim()));
            StringBuilder wordFound = new StringBuilder();

            for (int m = 0; m < matrixArray.Length; m++)
            {
                // Get the current column
                string[] column = GetColumn(matrixArray, m);

                foreach (string word in wordSet)
                {
                    foreach (string columnElement in column)
                    {
                        // Check if the constructed word matches the current word in the wordSe
                        if (wordFound.Length == word.Length)
                        {
                            wordFound.Clear();
                        }
                        else if (wordFound.Length < word.Length && word[wordFound.Length].ToString() == columnElement.ToLower().Trim())
                        {
                            // Append the character to the wordFound StringBuilder if it matches
                            wordFound.Append(columnElement.ToLower().Trim());
                            if (wordFound.Length == word.Length)
                            {
                                // If the word is complete, add it to the list of words and clear the StringBuilder
                                words.Add(wordFound.ToString().ToUpperInvariant());
                                wordFound.Clear();
                            }
                        }
                        else
                        {
                            wordFound.Clear();
                        }
                    }
                }
            }
            return words;
        }

        /// <summary>
        /// This method generates a matrix with words placed randomly in horizontal or vertical directions.
        /// </summary>
        /// <param name="words">The list of words to be placed in the matrix</param>
        /// <param name="matrixSize">The size of the square matrix</param>
        /// <returns>The generated matrix as a list of strings</returns>

        public Task<IEnumerable<string>> GenerateMatrix(IEnumerable<string> words, int matrixSize)
        {
            Random random = new Random();
            char[][] matrix = new char[matrixSize][];

            // Initialize the matrix with spaces
            for (int i = 0; i < matrixSize; i++)
            {
                matrix[i] = new char[matrixSize];
                for (int j = 0; j < matrixSize; j++)
                {
                    matrix[i][j] = ' '; // Initialize with spaces
                }
            }
            // Place words in the matrix
            foreach (string word in words)
            {
                bool placed = false;
                while (!placed)
                {
                    int direction = random.Next(2);
                    int row = random.Next(matrixSize);
                    int column = random.Next(matrixSize - word.Length + 1);

                    // Check if word can be placed in the selected position
                    if ((direction == 0 && column >= 0 && column + word.Length <= matrixSize &&
                        matrix[row].Skip(column).Take(word.Length).All(c => c == ' ')) ||
                        (direction == 1 && row + word.Length <= matrixSize &&
                        Enumerable.Range(0, word.Length).All(i => matrix[row + i][column] == ' ')))
                    {
                        for (int i = 0; i < word.Length; i++)
                        {
                            if (direction == 0)
                            {
                                matrix[row][column + i] = word.ToUpper()[i]; // Horizontal placement
                            }
                            else
                            {
                                matrix[row + i][column] = word.ToUpper()[i]; // Vertical placement
                            }
                        }
                        placed = true;
                    }
                }
            }

            // Fill remaining empty cells with random letters
            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    if (matrix[i][j] == ' ')
                    {
                        matrix[i][j] = (char)('A' + random.Next(26)); // Random letter
                    }
                }
            }

            // Convert matrix to list of strings and return
            var result = matrix.Select(row => new string(row));
            return Task.FromResult(result);
        }
    }

}
