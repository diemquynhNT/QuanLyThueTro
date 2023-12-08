using Client_QuanLythueTro.Models;

namespace Client_QuanLythueTro.Services
{
    public interface IPaymentService
    {
        #region VNPay
        string CreateVNPaymentUrl(VNPayInformationModel model, HttpContext context);
        VNPayResponseModel VNPaymentExecute(IQueryCollection collections);
        #endregion

        #region MOMO
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(MomoInfoModel model);
        MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
        #endregion
    }
}
