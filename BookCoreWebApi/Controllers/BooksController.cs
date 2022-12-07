using AutoMapper;
using BookCoreWebApi.Models.Domain;
using BookCoreWebApi.Models.DTO;
using BookCoreWebApi.Repositories.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookCoreWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;
        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async  Task<IActionResult> GetBooksAsync()
        {
            var allbooks = await _bookRepository.GetAllAsync();
            var booksDto = _mapper.Map<List<BookDTO>>(allbooks);
            return Ok(booksDto);
        }
        [HttpGet]
        [Route("{id}")]
        [ActionName("GetBookByIdAsync")]
        public async Task<IActionResult> GetBookByIdAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var book = await _bookRepository.GetByIdAsync(id);
            var bookDto = _mapper.Map<BookDTO>(book);
            return Ok(bookDto);
        }
        [HttpPost]
        public async Task<IActionResult> AddBookAsync(AddBookRequest addBookRequest)
        {
            var book = new Book()
            {
                Name = addBookRequest.Name,
                DisplayOrder = addBookRequest.DisplayOrder,
            };
            book = await _bookRepository.AddBookAsync(book);
            var bookDto = _mapper.Map<BookDTO>(book);
            //return Ok(bookDto);
            return CreatedAtAction(nameof(GetBookByIdAsync), new { Id = bookDto.Id }, bookDto);

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBookAsync(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var book = await _bookRepository.DeleteBookAsync(id);
            var bookDto = _mapper.Map<BookDTO>(book);
            return Ok(bookDto);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBookAsync([FromRoute] int id,[FromBody]UpdateBookRequest updateBookRequest)
        {
            var book = new Book()
            {
                Name=updateBookRequest.Name,
                DisplayOrder=updateBookRequest.DisplayOrder,
            };

             book = await _bookRepository.UpdateBookAsync(id,book);
            if(book == null)
            {
                return NotFound();
            }
            var bookDto = _mapper.Map<BookDTO>(book);
            return Ok(bookDto);
        }
    }
}
