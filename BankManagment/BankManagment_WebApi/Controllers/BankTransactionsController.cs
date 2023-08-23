using AutoMapper;
using BankManagment_Domain.Entity;
using BankManagment_DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

public class BankTransactionsController : Controller
{
    private readonly IBankTransactionService _bankTransactionService;
    private readonly IMapper _mapper;

    public BankTransactionsController(IBankTransactionService bankTransactionService, IMapper mapper)
    {
        _bankTransactionService = bankTransactionService;
        _mapper = mapper;
    }

    [HttpGet("banktransactions")]
    public async Task<IActionResult> GetBankTransactions()
    {
        var bankTransactions = await _bankTransactionService.GetAllBankTransactionsAsync();
        var bankTransactionDTOs = _mapper.Map<List<BankTransactionDTO>>(bankTransactions);
        return Ok(bankTransactionDTOs);
    }

    [HttpPost("banktransactions")]
    public async Task<IActionResult> CreateBankTransaction([FromBody] BankTransactionDTO bankTransactionDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var bankTransaction = _mapper.Map<BankTransaction>(bankTransactionDTO);

        await _bankTransactionService.CreateBankTransactionAsync(bankTransaction);
        await _bankTransactionService.SaveChangesAsync();

        var createdDTO = _mapper.Map<BankTransactionDTO>(bankTransaction);
        return CreatedAtAction(nameof(GetBankTransactions), new { id = createdDTO.Id }, createdDTO);
    }

    [HttpPut("banktransactions/{id}")]
    public async Task<IActionResult> UpdateBankTransaction(Guid id, [FromBody] BankTransactionDTO updatedBankTransactionDTO)
    {
        if (!ModelState.IsValid || id != updatedBankTransactionDTO.Id)
            return BadRequest();

        var updatedBankTransaction = _mapper.Map<BankTransaction>(updatedBankTransactionDTO);

        await _bankTransactionService.UpdateBankTransactionAsync(id, updatedBankTransaction);
        await _bankTransactionService.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("banktransactions/{id}")]
    public async Task<IActionResult> DeleteBankTransaction(Guid id)
    {
        await _bankTransactionService.DeleteBankTransactionAsync(id);
        await _bankTransactionService.SaveChangesAsync();

        return NoContent();
    }
}
