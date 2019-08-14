using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DemoGSE.Models;
using DemoGSE.Services;
using DemoGSE.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoGSE.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;

        #region Ctor

        public BookController(IBookService bookService)
        {
            this._bookService = bookService;
        }

        #endregion

        #region Action Index/Create/Edit/Delete
        
        public async Task<ActionResult> Index( string message)
        {
            var books =  await _bookService.GetAllBooks();
            if (!String.IsNullOrEmpty(message))
            {
                TempData["Message"] = message;
            }            
            return View(books);
        }        
       
        public ActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BookCreateViewModel book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _bookService.AddBook(book);
                    var success = "The book " + book.Title.ToString() + " has been added successfully !!";
                    return RedirectToAction("Index", new { message = success });
                }
                return View();                
            }
            catch
            {
                var error = "An error occurred while processing your request. !!";
                TempData["Message"] = error;
                return View();
            }
        }
     
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                var book = await _bookService.GetBook(id);
                if (book != null)
                {
                    return View(book);
                }
                var error = "There isn't record with Id : " + id.ToString();
                TempData["Message"] = error;
                return RedirectToAction("Index", new { message = error });
            }
            catch (Exception)
            {
                var error = "An error occurred while processing your request. !!";
                TempData["Message"] = error;
                return View();
            }           
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BookViewModel book)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var b = await _bookService.GetBook(id);
                    if (b != null)
                    {
                        book.Id = id;
                        if (await _bookService.EditBook(book))
                        {
                            var success = book.Title.ToString() + " has been updated successfully !!";
                            return RedirectToAction("Index", new { message = success });
                        }
                    }
                    var error = "There isn't record with Id : " + id.ToString();
                    TempData["Message"] = error;
                    return View();
                }
                else { return View(); }
            }
            catch(Exception ex)
            {
                var error = "An error occurred while processing your request. !!";
                TempData["Message"] = error;
                return View();
            }
        }
        
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var b = await _bookService.GetBook(id);
                if (b != null)
                {
                    if (await _bookService.DeleteBook(id))
                    {
                        var success = b.Title.ToString() + " has been deleted successfully !!";
                        return RedirectToAction("Index", new { message = success });
                    }
                    else {
                        var errorResult = "An error occurred while processing your request. !!";                        
                        return RedirectToAction("Index", new { message = errorResult });
                    }
                }
                var error = "There isn't record with Id : " + id.ToString();                
                return RedirectToAction("Index", new { message = error });
            }
            catch (Exception ex)
            {
                var error = "An error occurred while processing your request. !!";                
                return RedirectToAction("Index", new { message = error });
            }
        }

        #endregion
    }
}