using AutoMapper;
using Badminton.Web.DTO;
using Badminton.Web.Interfaces;
using Badminton.Web.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Badminton.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoiceRepository _invoiceRepo;
        private readonly IMapper _mapper;

        public InvoiceController(IInvoiceRepository invoiceRepo, IMapper mapper)
        {
            _invoiceRepo = invoiceRepo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var invoices = await _invoiceRepo.GetAllAsync();
            var invoiceDTO = _mapper.Map<List<InvoiceDTO>>(invoices);
            return Ok(new ApiResponse
            {
                Success = true,
                Data = invoiceDTO
            });
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var invoice = await _invoiceRepo.GetByIdAsync(id);
            if(invoice == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Invoice not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success= true,
                Data = _mapper.Map<InvoiceDTO>(invoice)
            });
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var invoice = await _invoiceRepo.DeleteAsync(id);
            if(invoice == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Invoice does not exist!"
                });
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateInvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var invoiceModel = _mapper.Map<Invoice>(invoiceDTO);
            DateTime currentTime = DateTime.Now;
            invoiceModel.InvoiceDate = currentTime;
            await _invoiceRepo.CreateAsync(invoiceModel);

            return CreatedAtAction(nameof(GetById), new { id = invoiceModel.InvoiceId }, new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<InvoiceDTO>(invoiceModel)
            });
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateInvoiceDTO invoiceDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Invalid data",
                    Data = ModelState
                });
            }

            var invoiceModel = await _invoiceRepo.UpdateAsync(id, invoiceDTO);
            if(invoiceModel == null)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Invoice not found!"
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Data = _mapper.Map<InvoiceDTO>(invoiceModel)
            });
        }
    }
}
