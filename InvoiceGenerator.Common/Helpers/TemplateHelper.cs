using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Extensions;
using InvoiceGenerator.Common.Models.Invoice;

namespace InvoiceGenerator.Common.Helpers
{
    /// <summary>
    /// The template helper static class.
    /// </summary>
    public static class TemplateHelper
    {
        /// <summary>
        /// Builds invoice html.
        /// </summary>
        /// <param name="invoice"></param>
        /// <returns></returns>
        public static string BuildInvoiceHtml(InvoiceModel invoice)
        {
            var invoiceHtml = TemplateConstants.BaseHtml;
            if (!string.IsNullOrEmpty(invoice.UserCompanyLogo))
            {
                var logo = TemplateConstants.Logo;
                logo = logo.Replace("{{LogoUrl}}", invoice.UserCompanyLogo);
                invoiceHtml = invoiceHtml.Replace("{{Logo}}", logo);
                invoiceHtml = invoiceHtml.Replace("{{UserInfo}}", TemplateConstants.UserInfoWithLogo);
            }
            else
            {
                invoiceHtml = invoiceHtml.Replace("{{Logo}}", "");
                invoiceHtml = invoiceHtml.Replace("{{UserInfo}}", TemplateConstants.UserInfoWithoutLogo);
            }
            invoiceHtml = invoiceHtml.Replace("{{UserCompanyName}}", invoice.UserCompanyName);
            invoiceHtml = invoiceHtml.Replace("{{UserAddress}}", invoice.UserAddress);
            invoiceHtml = invoiceHtml.Replace("{{UserContactNumber}}", invoice.UserContactNo);
            invoiceHtml = invoiceHtml.Replace("{{UserCompanyEmail}}", invoice.UserEmail);
            invoiceHtml = invoiceHtml.Replace("{{ClientName}}", invoice.ClientName);
            invoiceHtml = invoiceHtml.Replace("{{ClientCompanyName}}", invoice.ClientCompanyName);
            invoiceHtml = invoiceHtml.Replace("{{ClientAddress}}", invoice.ClientAddress);
            invoiceHtml = invoiceHtml.Replace("{{ClientContactNumber}}", invoice.ClientPhoneNumber);
            invoiceHtml = invoiceHtml.Replace("{{ClientEmailAddress}}", invoice.ClientEmailAddress);
            invoiceHtml = invoiceHtml.Replace("{{InvoiceNo}}", invoice.InvoiceNo);
            invoiceHtml = invoiceHtml.Replace("{{CreatedDate}}", invoice.CreatedDate);
            invoiceHtml = invoiceHtml.Replace("{{DueDate}}", invoice.DueDate);

            var items = "";
            var itemNo = 0;
            foreach (var item in invoice.Items)
            {
                var quantityText = 
                itemNo++;
                var itemRow = TemplateConstants.Item;
                itemRow = itemRow.Replace("{{itemNo}}", $"{itemNo}");
                itemRow = itemRow.Replace("{{itemName}}", $"{item.Name}");
                itemRow = itemRow.Replace("{{itemQty}}", item.Quantity.FormatToQuantityString());
                itemRow = itemRow.Replace("{{itemPrice}}", item.UnitPrice.FormatTo2DpMoneyString());
                itemRow = itemRow.Replace("{{itemTotalPrice}}", item.TotalPrice.FormatTo2DpMoneyString());
                items += itemRow;
            }
            invoiceHtml = invoiceHtml.Replace("{{Item}}", items);
            invoiceHtml = invoiceHtml.Replace("{{SubTotalFee}}", invoice.SubTotalFee.FormatTo2DpMoneyString());
            invoiceHtml = invoiceHtml.Replace("{{VAT}}", invoice.Vat.ToString("F2"));
            invoiceHtml = invoiceHtml.Replace("{{TotalFee}}", invoice.TotalFee.FormatTo2DpMoneyString());
            invoiceHtml = invoiceHtml.Replace("{{FeePaid}}", invoice.FeePaid.FormatTo2DpMoneyString());
            invoiceHtml = invoiceHtml.Replace("{{TotalFeeDue}}", invoice.TotalFeeDue.FormatTo2DpMoneyString());
            if (!string.IsNullOrEmpty(invoice.Comment))
            {
                var comment = TemplateConstants.Comment;
                comment = comment.Replace("{{Comment}}", invoice.Comment);
                invoiceHtml = invoiceHtml.Replace("{{Comment}}", comment);
            }
            else
            {
                invoiceHtml = invoiceHtml.Replace("{{Comment}}", "");
            }

            invoiceHtml = invoiceHtml.Replace("{{PdfPreview}}", "");

            invoiceHtml = invoiceHtml.Replace("{{currencyCode}}", invoice.CurrencyCode);

            return invoiceHtml;
        }
    }
}
