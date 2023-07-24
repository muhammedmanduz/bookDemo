using bookDemo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace bookDemo.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllBooks()
        {
            var books = ApplicationContext.Books;
            return Ok(books);
        }
        [HttpGet("{id:int}")]
        public IActionResult GetOneBooks([FromRoute(Name = "id")] int id)
        {
            var book = ApplicationContext
                .Books
                .Where(b => b.Id.Equals(id))
                .SingleOrDefault();
            if (book is null)
            {
                return NotFound();//404
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult CreateOneBook([FromBody] Book book)
        {
            try
            {
                if (book is null)
                    return BadRequest();//404

                ApplicationContext.Books.Add(book);
                return StatusCode(201, book);



            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateOneBook([FromRoute(Name = "id")] int id, [FromBody] Book book)
        {

            //check  book
            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));
            if (entity is null)
                return NotFound();//404

            //check İd
            if (id != book.Id)
                return BadRequest();//400
            ApplicationContext.Books.Remove(book);
            book.Id = entity.Id;
            ApplicationContext.Books.Add(book);
            return Ok(book);


        }
        [HttpDelete]
        public IActionResult DeleteBooks()
        {
            ApplicationContext.Books.Clear();
            return NoContent();//204
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeleteOneBook([FromRoute(Name ="id")] int id)
        {
            var entity = ApplicationContext
                .Books
                .Find(b => b.Id.Equals(id));
            if (entity is null)
                return NotFound(new
                {
                    statousCode=404,
                    message = $"Book with id:{id} could not found"
                });
           ApplicationContext.Books.Remove(entity);
            return NoContent();
        }
        [HttpPatch]
        public IActionResult PartiallyUpdateOneBook([FromRoute(Name ="id")]int id,
            [FromBody]JsonPatchDocument<Book> bookPatch)
        {
            //check Entity
            var entity=ApplicationContext.Books.Find(b=>b.Id.Equals(id));
            if(entity is null)
                return NotFound();//404
            bookPatch.ApplyTo(entity);
            return NoContent();//202
        }

    }
}
