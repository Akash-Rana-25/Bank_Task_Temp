using AutoMapper;
using BankManagment_Domain.Entity;
using BankManagment_DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

public class AccountTypeController : Controller
{
    private readonly IAccountTypeService _accountTypeService;
    private readonly IMapper _mapper;

    public AccountTypeController(IAccountTypeService accountTypeService, IMapper mapper)
    {
        _accountTypeService = accountTypeService;
        _mapper = mapper;
    }

    [HttpGet("accounttypes")]
    public async Task<IActionResult> GetAccountTypes()
    {
        var accountTypes = await _accountTypeService.GetAllAccountTypesAsync();
        var accountTypeDTOs = _mapper.Map<List<AccountTypeDTO>>(accountTypes);
        return Ok(accountTypeDTOs);
    }

    [HttpPost("accounttypes")]
    public async Task<IActionResult> CreateAccountType([FromBody] AccountTypeDTO accountTypeDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var accountType = _mapper.Map<AccountType>(accountTypeDTO);

        await _accountTypeService.CreateAccountTypeAsync(accountType);
        await _accountTypeService.SaveChangesAsync();

        var createdDTO = _mapper.Map<AccountTypeDTO>(accountType);
        return CreatedAtAction(nameof(GetAccountTypes), new { id = createdDTO.Id }, createdDTO);
    }

    [HttpPut("accounttypes/{id}")]
    public async Task<IActionResult> UpdateAccountType(Guid id, [FromBody] AccountTypeDTO updatedAccountTypeDTO)
    {
        if (!ModelState.IsValid || id != updatedAccountTypeDTO.Id)
            return BadRequest();

        var updatedAccountType = _mapper.Map<AccountType>(updatedAccountTypeDTO);

        await _accountTypeService.UpdateAccountTypeAsync(id, updatedAccountType);
        await _accountTypeService.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("accounttypes/{id}")]
    public async Task<IActionResult> DeleteAccountType(Guid id)
    {
        await _accountTypeService.DeleteAccountTypeAsync(id);
        await _accountTypeService.SaveChangesAsync();

        return NoContent();
    }
}
