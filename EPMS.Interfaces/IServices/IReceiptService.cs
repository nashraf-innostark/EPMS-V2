using System.Collections.Generic;
using EPMS.Models.DomainModels;
using EPMS.Models.ResponseModels;

namespace EPMS.Interfaces.IServices
{
    public interface IReceiptService
    {
        IEnumerable<Receipt> GetAll();
        IEnumerable<Receipt> GetAll(string userId);
        long AddReceipt(Receipt receipt);
        Receipt FindReceiptById(long id);
        ReceiptResponse GetReceiptDetails(long id);
    }
}