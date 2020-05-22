using Innergy.Input.Interfaces;
using System.IO;

namespace Innergy.Input
{
    public class InputReader: IInputReader
    {
        private const string INPUT_FILE_PATH = "Input\\input.txt";

        public string[] Read()
        {
            if(!File.Exists(INPUT_FILE_PATH))
            {
                throw new FileNotFoundException("File with input doesn't exist.");
            }

            return File.ReadAllLines(INPUT_FILE_PATH);
        }
    }
}
