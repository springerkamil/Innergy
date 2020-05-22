using Innergy.Parsing.Models;
using System.Collections.Generic;

namespace Innergy.Parsing.Interfaces
{
    public interface IInputParser
    {
        List<Warehouse> Parse(string[] inputLines);
    }
}
