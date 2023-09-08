using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using BankManagment_Infrastructure.UnitOfWork;

namespace Services
{
    public class BankTransactionService : IBankTransactionService
    {
        private readonly IRepository<BankTransaction> _bankTransactionRepository;
        private readonly IRepository<BankAccountPosting> _bankAccountPostingRepository;
        private readonly IRepository<BankAccount> _bankAccountRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BankTransactionService(IUnitOfWork unitOfWork,
                                      IRepository<BankTransaction> bankTransactionRepository,
                                      IRepository<BankAccountPosting> bankAccountPostingRepository,
                                      IRepository<BankAccount> bankAccountRepository)
        {
            _unitOfWork = unitOfWork;
            _bankTransactionRepository = bankTransactionRepository;
            _bankAccountPostingRepository = bankAccountPostingRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        public async Task<IEnumerable<BankTransaction>> GetAllBankTransactionsAsync()
        {
            return await _bankTransactionRepository.GetAllAsync();
        }

        public async Task CreateBankTransactionAsync(BankTransaction bankTransaction)
        {
            await _bankTransactionRepository.AddAsync(bankTransaction);

            if (bankTransaction.Category == "Bank Interest" || bankTransaction.Category == "Bank Charges")
            {
                var bankAccountPosting = new BankAccountPosting
                {
                    TransactionPersonFirstName = bankTransaction.TransactionPersonFirstName,
                    TransactionPersonMiddleName = bankTransaction.TransactionPersonMiddleName,
                    TransactionPersonLastName = bankTransaction.TransactionPersonLastName,
                    TransactionType = bankTransaction.TransactionType,
                    Category = bankTransaction.Category,
                    Amount = bankTransaction.Amount,
                    TransactionDate = bankTransaction.TransactionDate,
                    PaymentMethodId = bankTransaction.PaymentMethodID,
                    BankAccountId = bankTransaction.BankAccountID
                };

                await _bankAccountPostingRepository.AddAsync(bankAccountPosting);
            }

            var bankAccount = await _bankAccountRepository.GetByIdAsync(bankTransaction.BankAccountID);
            if (bankAccount != null)
            {
                if (bankTransaction.TransactionType == "Credit")
                {
                    bankAccount.TotalBalance += bankTransaction.Amount;
                }
                else if (bankTransaction.TransactionType == "Debit")
                {
                    bankAccount.TotalBalance -= bankTransaction.Amount;
                }
                await _bankAccountRepository.UpdateAsync(bankAccount);
            }
        }

        public async Task UpdateBankTransactionAsync(Guid id, BankTransaction updatedBankTransaction)
        {
            var existingBankTransaction = await _bankTransactionRepository.GetByIdAsync(id);
            if (existingBankTransaction == null)
                throw new ArgumentException("Bank transaction not found.");

            existingBankTransaction.TransactionPersonFirstName = updatedBankTransaction.TransactionPersonFirstName;
            existingBankTransaction.TransactionPersonMiddleName = updatedBankTransaction.TransactionPersonMiddleName;
            existingBankTransaction.TransactionPersonLastName = updatedBankTransaction.TransactionPersonLastName;
            existingBankTransaction.TransactionType = updatedBankTransaction.TransactionType;
            existingBankTransaction.Category = updatedBankTransaction.Category;
            existingBankTransaction.Amount = updatedBankTransaction.Amount;
            existingBankTransaction.TransactionDate = updatedBankTransaction.TransactionDate;

            await _bankTransactionRepository.UpdateAsync(existingBankTransaction);
        }

        public async Task DeleteBankTransactionAsync(Guid id)
        {
            var bankTransaction = await _bankTransactionRepository.GetByIdAsync(id);
            if (bankTransaction == null)
                throw new ArgumentException("Bank transaction not found.");

            await _bankTransactionRepository.DeleteAsync(bankTransaction);
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveAsync();
        }
    }
}
