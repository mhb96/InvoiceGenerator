using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Common.Exception;
using InvoiceGenerator.Common.Extensions;
using InvoiceGenerator.Common.Helpers;
using InvoiceGenerator.Common.Helpers.Interfaces;
using InvoiceGenerator.Common.Models.Invoice;
using InvoiceGenerator.Common.Models.Item;
using InvoiceGenerator.Repository.DataServices.Interfaces;
using InvoiceGenerator.Repository.Models.Invoice;
using InvoiceGenerator.Services.Models.Invoice;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Services
{
    public class InvoiceService : BaseService, IInvoiceService
    {
        private readonly IItemService _itemService;
        private readonly IFileHelper _fileHelper;
        private readonly IUserService _userService;
        private readonly IImageService _imageService;
        private readonly IInvoiceDataService _invoiceDataService;
        public InvoiceService(IItemService itemService, ILogger<InvoiceService> logger, IFileHelper fileHelper, IUserService userService, IImageService imageService, IInvoiceDataService invoiceDataService) : base(logger)
        {
            _itemService = itemService;
            _fileHelper = fileHelper;
            _userService = userService;
            _imageService = imageService;
            _invoiceDataService = invoiceDataService;
        }

        public async Task<List<InvoiceSummaryModel>> GetForDashboardAsync(long userId) =>
            await _invoiceDataService.GetForDashboardAsync(userId);

        public async Task<InvoiceModel> GetAsync(long invoiceId, bool isForPdf)
        {
            var invoice = await _invoiceDataService.GetAsync(invoiceId);
            if (invoice is null)
            {
                Logger.LogError($"Invoice with id: {invoiceId} was not found.");
                throw new IGException("Invoice does not exist.");
            }

            var image = await _imageService.GetAsync(invoice.UserCompanyLogoId);
            invoice.UserCompanyLogo = invoice.UserCompanyLogoId != 0 ? isForPdf ? _fileHelper.GetImageAddress(image.ImageName, true) : _fileHelper.GetImageAddress(image.ImageName, false) : null;

            var items = await _itemService.GetAsync(invoiceId);

            decimal subTotal = 0M;
            foreach (var item in items)
                subTotal += item.Quantity * item.UnitPrice;

            decimal totalFee = Math.Round(subTotal + (subTotal * invoice.Vat / 100), 2, MidpointRounding.AwayFromZero);
            subTotal = Math.Round(subTotal, 2, MidpointRounding.AwayFromZero);

            invoice.TotalFee = totalFee;
            invoice.SubTotalFee = subTotal;
            invoice.Items = items;
            invoice.TotalFeeDue = totalFee - invoice.FeePaid;

            return invoice;
        }

        public async Task DeleteAsync(long invoiceId, long userId) =>
            await _invoiceDataService.DeleteAsync(invoiceId, userId);

        public async Task EditAsync(EditInvoiceModel input)
        {
            input.UserCompanyName.ValidateString("Your company name", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            input.UserAddress.ValidateString("Your address", RegexStrings.ObjectName, InputLengthLimits.Address);
            Validate.ValidateEmail(input.UserEmailAddress);
            input.UserPhoneNumber.ValidateString("Your contact no.", RegexStrings.ContactNo, InputLengthLimits.ContactNo);

            input.ClientName.ValidateString("Client name", RegexStrings.Name, InputLengthLimits.ObjectName);
            input.ClientCompanyName.ValidateString("Client company name", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            input.ClientAddress.ValidateString("Client address", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            Validate.ValidateEmail(input.ClientEmailAddress);
            input.ClientPhoneNumber.ValidateString("Client phone number", RegexStrings.ContactNo, InputLengthLimits.ContactNo);
            input.Comment.ValidateString("Comment", RegexStrings.ObjectName, InputLengthLimits.Comment);

            ValidateItems(input.Items);

            if (input.TotalFee > 1000000000)
                throw new IGException("Total fee cannot exceed 1,000,000,000");

            VerifyTotalFee(input.Items, input.TotalFee, input.Vat);

            await _invoiceDataService.EditAsync(input.InvoiceId, input.UserId, new EditInvoiceDataModel
            {
                CreatedDate = DateTime.Parse(input.CreatedDate),
                DueDate = DateTime.Parse(input.DueDate),
                UserAddress = input.UserAddress,
                UserCompanyName = input.UserCompanyName,
                UserContactNo = input.UserPhoneNumber,
                UserEmail = input.UserEmailAddress,
                ClientName = input.ClientName,
                ClientAddress = input.ClientAddress,
                ClientCompanyName = input.ClientCompanyName,
                ClientEmailAddress = input.ClientEmailAddress,
                ClientPhoneNumber = input.ClientPhoneNumber,
                TotalFee = input.TotalFee,
                FeePaid = input.FeePaid,
                Vat = input.Vat,
                CurrencyId = input.CurrencyId,
                Comment = input.Comment,
            });

            await _itemService.DeleteAllAsync(input.InvoiceId);
            await _itemService.AddAsync(input.Items, input.InvoiceId);
        }

        public async Task<long> CreateAsync(CreateInvoiceModel input)
        {
            input.ClientName.ValidateString("Client name", RegexStrings.Name, InputLengthLimits.ObjectName);
            input.ClientCompanyName.ValidateString("Client company name", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            input.ClientAddress.ValidateString("Client address", RegexStrings.ObjectName, InputLengthLimits.ObjectName);
            Validate.ValidateEmail(input.ClientEmailAddress);
            input.ClientPhoneNumber.ValidateString("Client phone number", RegexStrings.ContactNo, InputLengthLimits.ContactNo);
            input.Comment.ValidateString("Comment", RegexStrings.ObjectName, InputLengthLimits.Comment);

            ValidateItems(input.Items);

            VerifyTotalFee(input.Items, input.TotalFee, input.Vat);

            var user = await _userService.GetAsync(input.UserId);

            long invoiceId = await _invoiceDataService.CreateAsync(new CreateInvoiceDataModel
            {
                UserId = user.Id,
                UserAddress = user.Address,
                UserCompanyName = user.CompanyName,
                UserContactNo = user.ContactNo,
                UserEmail = user.Email,
                UserCompanyLogoId = user.CompanyLogoId,

                ClientAddress = input.ClientAddress,
                ClientName = input.ClientName,
                Comment = input.Comment,
                ClientCompanyName = input.ClientCompanyName,
                CreatedDate = DateTime.Parse(input.CreatedDate),
                DueDate = DateTime.Parse(input.DueDate),
                TotalFee = input.TotalFee,
                ClientEmailAddress = input.ClientEmailAddress,
                ClientPhoneNumber = input.ClientPhoneNumber,
                CurrencyId = input.CurrencyId,
                FeePaid = input.FeePaid,
                Vat = input.Vat,
            });

            Logger.LogInformation($"Added new invoice: {invoiceId}.");

            await _itemService.AddAsync(input.Items, invoiceId);

            return invoiceId;
        }

        public async Task<string> CreateInvoicePdf(long invoiceId)
        {
            InvoiceModel invoice = await GetAsync(invoiceId, true);
            var invoiceHtml = TemplateHelper.BuildInvoiceHtml(invoice);
            var fileName = _fileHelper.CreatePdf(invoiceHtml);
            return fileName;
        }

        private void VerifyTotalFee(List<ItemInputModel> items, decimal expectedTotalFee, decimal vat)
        {
            Logger.LogInformation($"Verifying total fee.");

            decimal subTotal = 0M;
            foreach (var item in items)
                subTotal += item.Quantity * item.UnitPrice;

            if (subTotal > 1000000000)
                throw new IGException("SubTotal fee cannot exceed 1,000,000,000");

            decimal totalFee = Math.Round(subTotal + (subTotal * vat / 100), 2, MidpointRounding.AwayFromZero);
            if (totalFee != expectedTotalFee)
                throw new IGException("Total fee is not equal to total price of items plus vat.");
        }

        private void ValidateItems(List<ItemInputModel> items)
        {
            foreach (var item in items)
            {
                if (string.IsNullOrEmpty(item.Name))
                    throw new IGException("Item names cannot be empty.");

                if (item.Quantity == 0)
                    throw new IGException("Item quantities cannot be 0.");

                if (item.UnitPrice == 0)
                    throw new IGException("Item prices cannot be 0.");

                item.Name.ValidateString(item.Name, RegexStrings.ObjectName, 35);
            }
        }
    }
}