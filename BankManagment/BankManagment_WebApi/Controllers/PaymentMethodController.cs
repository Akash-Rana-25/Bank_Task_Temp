using AutoMapper;
using BankManagment_Domain.Entity;
using BankManagment_DTO;
using Microsoft.AspNetCore.Mvc;
using Services;

public class PaymentMethodController : Controller
{
    private readonly IPaymentMethodService _paymentMethodService;
    private readonly IMapper _mapper;

    public PaymentMethodController(IPaymentMethodService paymentMethodService, IMapper mapper)
    {
        _paymentMethodService = paymentMethodService;
        _mapper = mapper;
    }

    [HttpGet("paymentmethods")]
    public async Task<IActionResult> GetPaymentMethods()
    {
        var paymentMethods = await _paymentMethodService.GetAllPaymentMethodsAsync();
        var paymentMethodDTOs = _mapper.Map<List<PaymentMethodDTO>>(paymentMethods);
        return Ok(paymentMethodDTOs);
    }

    [HttpPost("paymentmethods")]
    public async Task<IActionResult> CreatePaymentMethod([FromBody] PaymentMethodDTO paymentMethodDTO)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodDTO);

        await _paymentMethodService.CreatePaymentMethodAsync(paymentMethod);
        await _paymentMethodService.SaveChangesAsync();

        var createdDTO = _mapper.Map<PaymentMethodDTO>(paymentMethod);
        return CreatedAtAction(nameof(GetPaymentMethods), new { id = createdDTO.Id }, createdDTO);
    }

    [HttpPut("paymentmethods/{id}")]
    public async Task<IActionResult> UpdatePaymentMethod(Guid id, [FromBody] PaymentMethodDTO updatedPaymentMethodDTO)
    {
        if (!ModelState.IsValid || id != updatedPaymentMethodDTO.Id)
            return BadRequest();

        var updatedPaymentMethod = _mapper.Map<PaymentMethod>(updatedPaymentMethodDTO);

        await _paymentMethodService.UpdatePaymentMethodAsync(id, updatedPaymentMethod);
        await _paymentMethodService.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("paymentmethods/{id}")]
    public async Task<IActionResult> DeletePaymentMethod(Guid id)
    {
        await _paymentMethodService.DeletePaymentMethodAsync(id);
        await _paymentMethodService.SaveChangesAsync();

        return NoContent();
    }
}
