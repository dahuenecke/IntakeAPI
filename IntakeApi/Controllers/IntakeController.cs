using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntakeApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IntakeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntakeController : ControllerBase
    {
        private readonly IntakeContext _context;

        public IntakeController(IntakeContext context)
        {
            _context = context;

            if (_context.IntakeItems.Count() == 0)
            {
                _context.IntakeItems.Add(new IntakeItem { ClientId = "BU", DocumentName = "UnresolvedAccounts.xlsx", DocumentType = DocType.ClientReport, ReceiveDate = DateTime.UtcNow.ToShortDateString(), IsCompleted = false });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "BU", DocumentName = "Payments.pdf", DocumentType = DocType.EOB, ReceiveDate = DateTime.UtcNow.AddDays(-4).ToShortDateString(), IsCompleted = true });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "PHS", DocumentName = "20180731_UHC_57483922.dat", DocumentType = DocType.E835, ReceiveDate = DateTime.UtcNow.AddDays(-10).ToShortDateString(), IsCompleted = false });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "HHMS", DocumentName = $"{DateTime.UtcNow.ToShortDateString().Replace("/", "")}_AET_9987242527_0585746.dat", DocumentType = DocType.E837P, ReceiveDate = DateTime.UtcNow.ToShortDateString(), IsCompleted = false });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "HHMS", DocumentName = $"{DateTime.UtcNow.AddDays(-7).ToShortDateString().Replace("/", "")}_HOSP_CC_776483.pdf", DocumentType = DocType.HospitalCharges, ReceiveDate = DateTime.UtcNow.AddDays(-5).ToShortDateString(), IsCompleted = true });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "PHS", DocumentName = $"PT_Questions_{DateTime.UtcNow.AddDays(-1).ToShortDateString().Replace("/", "")}.msg", DocumentType = DocType.ClientQuestion, ReceiveDate = DateTime.UtcNow.AddDays(-1).ToShortDateString(), IsCompleted = false });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "GBN", DocumentName = $"6754_609_45_{DateTime.UtcNow.AddDays(-2).ToShortDateString().Replace("/","")}.txt", DocumentType = DocType.E837P, ReceiveDate = DateTime.UtcNow.AddDays(-2).ToShortDateString(), IsCompleted = true });
                _context.IntakeItems.Add(new IntakeItem { ClientId = "GBN", DocumentName = $"{DateTime.UtcNow.AddDays(-4).ToShortDateString().Replace("/", "")}_MCS_6678_94848333.csv", DocumentType = DocType.ClinicCharges, ReceiveDate = DateTime.UtcNow.AddDays(-3).ToShortDateString(), IsCompleted = false });

                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<IntakeItem>>> GetAll()
        {
            return await _context.IntakeItems.ToListAsync();
        }

        [HttpGet("{id}", Name = "GetIntake")]
        public async Task<ActionResult<IntakeItem>> GetById(long id)
        {
            var item = await _context.IntakeItems.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }

        [HttpPost]
        public async Task<IActionResult> Create(IntakeItem item)
        {
            bool exists = await _context.IntakeItems.AnyAsync(i => i.DocumentName == item.DocumentName);
            if (exists)
            {
                Exception ex = new Exception("Document already exists");
                ex.Data.Add("DocumentName", item.DocumentName);
                return BadRequest(new Result { Success = false, Exceptions = new List<Exception>() { ex } });
            }
            _context.IntakeItems.Add(item);
            await _context.SaveChangesAsync();

            return CreatedAtRoute("GetIntake", new { id = item.Id }, item);
        }
    }
}