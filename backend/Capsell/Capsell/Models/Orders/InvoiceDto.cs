using System;
namespace Capsell.Models.Orders
{
    public class InvoiceDto
	{
		public string? Name { get; set; }
        public List<ProductItemModel> ProductListInInvoice { get; set; }
		public string? TotalPrice { get; set; }
    }
}

