
using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using BankManagment_Infrastructure.UnitOfWork;
using Services;

public class BankAccountService : IBankAccountService
{
    private readonly IRepository<BankAccount> _bankAccountRepository;
    private readonly IUnitOfWork _unitOfWork;

    public BankAccountService(IUnitOfWork unitOfWork, IRepository<BankAccount> bankAccountRepository)
    {
        _unitOfWork = unitOfWork;
        _bankAccountRepository = bankAccountRepository;
    }

    public async Task<IEnumerable<BankAccount>> GetAllBankAccountsAsync()
    {
        return await _bankAccountRepository.GetAllAsync();
    }

    public async Task CreateBankAccountAsync(BankAccount bankAccount)
    {
        await _bankAccountRepository.AddAsync(bankAccount);
    }

    public async Task UpdateBankAccountAsync(Guid id, BankAccount updatedBankAccount)
    {
        var existingBankAccount = await _bankAccountRepository.GetByIdAsync(id);
        if (existingBankAccount == null)
            throw new ArgumentException("Bank account not found.");

        existingBankAccount.FirstName = updatedBankAccount.FirstName;
        existingBankAccount.MiddleName = updatedBankAccount.MiddleName;
        existingBankAccount.LastName = updatedBankAccount.LastName;
        existingBankAccount.ClosingDate = updatedBankAccount.ClosingDate;

        await _bankAccountRepository.UpdateAsync(existingBankAccount);
    }

    public async Task DeleteBankAccountAsync(Guid id)
    {
        var bankAccount = await _bankAccountRepository.GetByIdAsync(id);
        if (bankAccount == null)
            throw new ArgumentException("Bank account not found.");

        await _bankAccountRepository.DeleteAsync(bankAccount);
    }

    public async Task SaveChangesAsync()
    {
        await _unitOfWork.SaveAsync();
    }
}
