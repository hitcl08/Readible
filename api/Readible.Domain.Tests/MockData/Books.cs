using Readible.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Readible.Domain.Tests.MockData
{
    internal static class Books
    {
        internal static Book GetValidBook()
        {
            return new Book
            {
                Id = 1,
                Name = "Dune"
            };
        }

        internal static List<Book> GetValidBooks()
        {
            return new List<Book>
            {
                new Book
                {
                    Id = 1,
                    Name = "Dune"
                },
                new Book
                {
                    Id = 2,
                    Name = "Dune 2"
                }
            };
        }

        internal static Book GetInvalidBook()
        {
            return null;
        }
    }
}
