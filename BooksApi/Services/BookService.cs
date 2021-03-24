using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using BooksApi.Models;
using MongoDB.Driver;

namespace BooksApi.Services
{
    public class BookService
    {
        private readonly IMongoCollection<Book> _books;

        public BookService(IBookstoreDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            _books = database.GetCollection<Book>(settings.BooksCollectionName);
        }

        public List<Book> Get()
        {
            return _books.Find(book => true).ToList();
        }

        public Book Get(string id)
        {
            return _books.Find(book => book.Id == id).FirstOrDefault();
        }

        public Book Create(Book book)
        {
            _books.InsertOne(book);
            return book;
        }

        public void Update(string id,Book bookIn)
        {
            _books.ReplaceOne(book => book.Id == id, bookIn);
        }

        public void Remove(string id)
        {
            _books.DeleteOne(book => book.Id == id);
        }

        public void Remove(Book bookIn)
        {
            _books.DeleteOne(book=>book.Id==bookIn.Id);
        }
    }
}
