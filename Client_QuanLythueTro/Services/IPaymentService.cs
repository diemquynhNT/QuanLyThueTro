using Client_QuanLythueTro.Models;

namespace Client_QuanLythueTro.Services
{
    public interface IPaymentService
    {
        string CreateVNPaymentUrl(VNPayInformationModel model, HttpContext context);
        VNPayResponseModel VNPaymentExecute(IQueryCollection collections);
    }
}
