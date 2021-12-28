using AspNetCore.AsyncInitialization;
using InvoiceGenerator.Common.Constants;
using InvoiceGenerator.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceGenerator.Repository
{
    public class Initializer : IAsyncInitializer
    {
        private readonly InGenDbContext _dbContext;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<Initializer> _logger;

        public Initializer(InGenDbContext dbContext, UserManager<User> userManager, RoleManager<Role> roleManager, IUnitOfWork unitOfWork, ILogger<Initializer> logger)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Initiales the database asynchrously.
        /// </summary>
        /// <returns></returns>
        public async Task InitializeAsync()
        {
            try
            {
                _logger.LogInformation("Initializing the database.");
                await _dbContext.Database.MigrateAsync();

                if (!await _dbContext.Roles.AnyAsync())
                {
                    await CreateRoles();
                    await _unitOfWork.SaveAsync();
                }

                if (!(await _dbContext.Users.AnyAsync()))
                {
                    await CreateAdmin();
                    await _unitOfWork.SaveAsync();
                }

                var invoice = new Invoice();
                if (!(await _dbContext.Invoices.AnyAsync()))
                {
                    invoice = CreateInvoice();
                    await _unitOfWork.AddAsync(invoice);
                    await _unitOfWork.SaveAsync();
                }

                if (!await _dbContext.Comments.AnyAsync() && invoice != null)
                {
                    var comments = CreateComments(invoice.Id);
                    await _unitOfWork.AddRangeAsync(comments);
                    await _unitOfWork.SaveAsync();
                }

                if (!await _dbContext.Items.AnyAsync() && invoice != null)
                {
                    var items = CreateItems(invoice.Id);
                    await _unitOfWork.AddRangeAsync(items);
                    await _unitOfWork.SaveAsync();
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
            }
        }

        private async Task CreateRoles()
        {
            await _roleManager.CreateAsync(new Role { Name = Roles.Admin });
            await _roleManager.CreateAsync(new Role { Name = Roles.User });
        }

        private async Task CreateAdmin()
        {
            const string email = "admin@email.com";
            const string userName = "niceadmin";
            const string password = "aA!12345";
            const string firstName = "Admin";
            const string lastName = "Adminton";
            const string companyName = "AdminLtd.";
            const string contactNo = "0507562785";
            const string companyLogo = "/src/destination";
            const string address = "1, Admin Rd, Adminopolis, Administan";
            const decimal vat = 2.5M;
            var roles = new List<string> { "Admin", "User" };

            await CreateUser(roles: roles, firstName: firstName, lastName: lastName, email: email, userName: userName, password: password, companyName: companyName, contactNo: contactNo, companyLogo: companyLogo, address: address, vat: vat);
        }

        //TODO refactor
        private async Task CreateUser(List<string> roles, string firstName, string lastName, string email, string userName, string password,
            string companyName, string contactNo, string companyLogo, string address, decimal vat) //remove the parameter and use the injected one
        {
            _logger.LogInformation($"Create user with email `{email}` for application");
            var user = new User
            {
                Email = email,
                BusinessEmail = email,
                NormalizedEmail = email.ToUpper(),
                UserName = userName,
                NormalizedUserName = userName.ToUpper(),
                Password = password,
                AccessFailedCount = 0,
                EmailConfirmed = true,
                FirstName = firstName,
                LastName = lastName,
                CompanyName = companyName,
                ContactNo = contactNo,
                Address = address,
                VAT = vat,
                CompanyLogo = new Image { CreatedAt = DateTime.Now, ImageName = "image-test", IsDeleted = false },
                LockoutEnabled = false
            };

            var ir = await _userManager.CreateAsync(user, password);
            if (ir.Succeeded)
            {
                _logger.LogInformation($"Created user `{email}` successfully");
            }
            else
            {
                //var exception = new ApplicationException($"Default user `{email}` cannot be created");
                var exception = new Exception($"Default user `{email}` cannot be created");
                _logger.LogError("Log in failed", exception);
                throw exception;
            }

            var createdUser = await _userManager.FindByEmailAsync(email);
            await _userManager.AddToRoleAsync(createdUser, Roles.Admin);
            await _userManager.AddToRoleAsync(createdUser, Roles.User);
        }

        private Invoice CreateInvoice()
        {
            return new Invoice
            {
                EmailAddress = "client@email.com",
                IsDeleted = false,
                TotalFee = 101,
                PhoneNumber = "9021212121212",
                Vat = 21,
                Address = "one, Derby st, derbany",
                ClientName = "Herby Derpy",
                CompanyName = "Herby Industries",
                CreatedAt = DateTime.Now,
                DueDate = DateTime.Now.AddDays(5)
            };
        }

        private List<Comment> CreateComments(long invoiceId)
        {
            var comments = new List<Comment>();
            comments.Add(new Comment { Value = "Nothing personal, just business.", InvoiceNo = invoiceId });
            comments.Add(new Comment { Value = "Pay whenever.", InvoiceNo = invoiceId });
            return comments;
        }

        private List<Item> CreateItems(long invoiceId)
        {
            var items = new List<Item>();
            items.Add(new Item { InvoiceNo = invoiceId, Name = "Item A", Quantity = 1, UnitPrice = 10 });
            items.Add(new Item { InvoiceNo = invoiceId, Name = "Item B", Quantity = 2, UnitPrice = 10 });
            items.Add(new Item { InvoiceNo = invoiceId, Name = "Item C", Quantity = 3, UnitPrice = 10 });
            return items;
        }
    }
}

