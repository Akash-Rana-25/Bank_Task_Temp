using BankManagment_Domain.Entity;
using BankManagment_Infrastructure.Repository;
using BankManagment_Infrastructure.UnitOfWork;

namespace Services
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IRepository<PaymentMethod> _paymentMethodRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentMethodService(IUnitOfWork unitOfWork, IRepository<PaymentMethod> paymentMethodRepository)
        {
            _unitOfWork = unitOfWork;
            _paymentMethodRepository = paymentMethodRepository;
        }

        public async Task<IEnumerable<PaymentMethod>> GetAllPaymentMethodsAsync()
        {
            return await _paymentMethodRepository.GetAllAsync();
        }

        public async Task CreatePaymentMethodAsync(PaymentMethod paymentMethod)
        {
            await _paymentMethodRepository.AddAsync(paymentMethod);
        }

        public async Task UpdatePaymentMethodAsync(Guid id, PaymentMethod updatedPaymentMethod)
        {
            var existingPaymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            if (existingPaymentMethod == null)
                throw new ArgumentException("Payment method not found.");

            existingPaymentMethod.Name = updatedPaymentMethod.Name;

            await _paymentMethodRepository.UpdateAsync(existingPaymentMethod);
        }

        public async Task DeletePaymentMethodAsync(Guid id)
        {
            var paymentMethod = await _paymentMethodRepository.GetByIdAsync(id);
            if (paymentMethod == null)
                throw new ArgumentException("Payment method not found.");

            await _paymentMethodRepository.DeleteAsync(paymentMethod);
        }

        public async Task SaveChangesAsync()
        {
            await _unitOfWork.SaveAsync();
        }
    }
}
