using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Models.Invoice;

namespace InvoiceGenerator.Common.Helpers
{
    public static class TemplateHelper
    {
        public static string BuildInvoiceHtml(InvoiceModel invoice)
        {
            var invoiceHtml = TemplateConstants.BaseHtml;
            if (!string.IsNullOrEmpty(invoice.UserLogo))
            {
                var logo = TemplateConstants.Logo;
                logo = logo.Replace("{{LogoUrl}}", invoice.UserLogo);
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
            invoiceHtml = invoiceHtml.Replace("{{InvoiceNo}}", invoice.InvoiceNo.ToString("D6"));
            invoiceHtml = invoiceHtml.Replace("{{CreatedDate}}", invoice.CreatedDate);
            invoiceHtml = invoiceHtml.Replace("{{DueDate}}", invoice.DueDate);

            var items = "";
            var itemNo = 0;
            foreach(var item in invoice.Items)
            {
                itemNo++;
                var itemRow = TemplateConstants.Item;
                itemRow = itemRow.Replace("{{itemNo}}", $"{itemNo}");
                itemRow = itemRow.Replace("{{itemName}}", $"{item.Name}");
                itemRow = itemRow.Replace("{{itemQty}}", $"{item.Quantity}");
                itemRow = itemRow.Replace("{{itemPrice}}", item.UnitPrice.ToString("F2"));
                itemRow = itemRow.Replace("{{itemTotalPrice}}", item.TotalPrice.ToString("F2"));
                items += itemRow;
            }
            invoiceHtml = invoiceHtml.Replace("{{Item}}", items);
            invoiceHtml = invoiceHtml.Replace("{{SubTotalFee}}", invoice.SubTotalFee);
            invoiceHtml = invoiceHtml.Replace("{{VAT}}", invoice.Vat.ToString("F2"));
            invoiceHtml = invoiceHtml.Replace("{{TotalFee}}", invoice.TotalFee);
            if(!string.IsNullOrEmpty(invoice.Comment))
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

            return invoiceHtml;
        }
    }
}
