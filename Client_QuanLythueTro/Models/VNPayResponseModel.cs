namespace Client_QuanLythueTro.Models
{
    public class VNPayResponseModel
    {
        public string OrderDescription { get; set; }
        public string Username { get; set; }
        public string InfoDichVu { get; set; }
        public string Amount { get; set; }
        public string TransactionId { get; set; }
        public string OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public string IdDichVu { get; set; }
    }
}
