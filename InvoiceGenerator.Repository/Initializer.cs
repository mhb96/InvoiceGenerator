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

                if (!(await _dbContext.Currencies.AnyAsync()))
                {
                    List<Currency> currencies = AddCurrencies();
                    await _unitOfWork.AddRangeAsync(currencies);
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

        private List<Currency> AddCurrencies()
        {
            var currencies = new List<Currency>();
            currencies.Add(new Currency { Name = "Afghan afghani", Code = "AFG" });
            currencies.Add(new Currency { Name = "Albanian lek", Code = "ALL" });
            currencies.Add(new Currency { Name = "Algerian dinar", Code = "DZD" });
            currencies.Add(new Currency { Name = "United States dollar", Code = "USD" });
            currencies.Add(new Currency { Name = "European euro", Code = "EUR" });
            currencies.Add(new Currency { Name = "East Caribbean dollar", Code = "XCD" });
            currencies.Add(new Currency { Name = "Angolan kwanza", Code = "AOA" });
            currencies.Add(new Currency { Name = "Argentina peso", Code = "ARS" });
            currencies.Add(new Currency { Name = "Armenia dram", Code = "AMD" });
            currencies.Add(new Currency { Name = "Aruban florin", Code = "AWG" });
            currencies.Add(new Currency { Name = "Saint Helena pound", Code = "SHP" });
            currencies.Add(new Currency { Name = "Australian dollar", Code = "AUD" });
            currencies.Add(new Currency { Name = "Azerbaijan manat", Code = "AZN" });
            currencies.Add(new Currency { Name = "Bahamian dollar", Code = "BSD" });
            currencies.Add(new Currency { Name = "Bahraini dinar", Code = "BHD" });
            currencies.Add(new Currency { Name = "Bangladeshi taka", Code = "BDT" });
            currencies.Add(new Currency { Name = "Barbadian dollar", Code = "BBD" });
            currencies.Add(new Currency { Name = "Belarusian rube", Code = "BYN" });
            currencies.Add(new Currency { Name = "Belize dollar", Code = "BZD" });
            currencies.Add(new Currency { Name = "West African CFA franc", Code = "XOF" });
            currencies.Add(new Currency { Name = "Bermudian dollar", Code = "BMD" });
            currencies.Add(new Currency { Name = "Bhutanese ngultrum", Code = "BTN" });
            currencies.Add(new Currency { Name = "Bolivian boliviano", Code = "BOB" });
            currencies.Add(new Currency { Name = "Bosnia and Herzegovina convertible mark", Code = "BAM" });
            currencies.Add(new Currency { Name = "Botswana pula", Code = "BWP" });
            currencies.Add(new Currency { Name = "Brazilian real", Code = "BRL" });
            currencies.Add(new Currency { Name = "Brunei dollar", Code = "BND" });
            currencies.Add(new Currency { Name = "Bulgarian lev", Code = "BGN" });
            currencies.Add(new Currency { Name = "Burundi franc", Code = "BIF" });
            currencies.Add(new Currency { Name = "Cabo Verdean escudo", Code = "CVE" });
            currencies.Add(new Currency { Name = "Cambodian riel", Code = "KHR" });
            currencies.Add(new Currency { Name = "Canadian dollar", Code = "CAD" });
            currencies.Add(new Currency { Name = "Cayman Islands dollar", Code = "KYD" });
            currencies.Add(new Currency { Name = "New Zealand dollar", Code = "NZD" });
            currencies.Add(new Currency { Name = "Chilean peso", Code = "CLP" });
            currencies.Add(new Currency { Name = "Chinese Yuan Renminbi", Code = "CNY" });
            currencies.Add(new Currency { Name = "Colombian peso", Code = "COP" });
            currencies.Add(new Currency { Name = "Comorian franc", Code = "KMF" });
            currencies.Add(new Currency { Name = "Congolese franc", Code = "CDF" });
            currencies.Add(new Currency { Name = "Costa Rican colon", Code = "CRC" });
            currencies.Add(new Currency { Name = "Croatian kuna", Code = "HRK" });
            currencies.Add(new Currency { Name = "Cuban peso", Code = "CUP" });
            currencies.Add(new Currency { Name = "Netherlands Antillean guilder", Code = "ANG" });
            currencies.Add(new Currency { Name = "Czech koruna", Code = "CZK" });
            currencies.Add(new Currency { Name = "Djiboutian franc", Code = "DJF" });
            currencies.Add(new Currency { Name = "Dominican peso", Code = "DOP" });
            currencies.Add(new Currency { Name = "Egyptian pound", Code = "EGP" });
            currencies.Add(new Currency { Name = "Eritrean nakfa", Code = "ERN" });
            currencies.Add(new Currency { Name = "Swazi lilangeni", Code = "SZL" });
            currencies.Add(new Currency { Name = "Ethiopian birr", Code = "ETB" });
            currencies.Add(new Currency { Name = "Falkland Islands pound", Code = "FKP" });
            currencies.Add(new Currency { Name = "Fijian dollar", Code = "FJD" });
            currencies.Add(new Currency { Name = "CFP franc", Code = "XPF" });
            currencies.Add(new Currency { Name = "Gambian dalasi", Code = "GMD" });
            currencies.Add(new Currency { Name = "Georgian lari", Code = "GEL" });
            currencies.Add(new Currency { Name = "Ghanaian cedi", Code = "GHS" });
            currencies.Add(new Currency { Name = "Gibraltar pound", Code = "GIP" });
            currencies.Add(new Currency { Name = "Danish krone", Code = "DKK" });
            currencies.Add(new Currency { Name = "Guatemalan quetzal", Code = "GTQ" });
            currencies.Add(new Currency { Name = "Guernsey Pound", Code = "GGP" });
            currencies.Add(new Currency { Name = "Guinean franc", Code = "GNF" });
            currencies.Add(new Currency { Name = "Guyanese dollar", Code = "GYD" });
            currencies.Add(new Currency { Name = "Haitian gourde", Code = "HTG" });
            currencies.Add(new Currency { Name = "Honduran lempira", Code = "HNL" });
            currencies.Add(new Currency { Name = "Hong Kong dollar", Code = "HKD" });
            currencies.Add(new Currency { Name = "Hungarian forint", Code = "HUF" });
            currencies.Add(new Currency { Name = "Icelandic krona", Code = "ISK" });
            currencies.Add(new Currency { Name = "Indian rupee", Code = "INR" });
            currencies.Add(new Currency { Name = "Indonesian rupiah", Code = "IDR" });
            currencies.Add(new Currency { Name = "Iranian rial", Code = "IRR" });
            currencies.Add(new Currency { Name = "Iraqi dinar", Code = "IQD" });
            currencies.Add(new Currency { Name = "Manx pound", Code = "IMP" });
            currencies.Add(new Currency { Name = "Jamaican dollar", Code = "JMD" });
            currencies.Add(new Currency { Name = "Japanese yen", Code = "JPY" });
            currencies.Add(new Currency { Name = "Jersey pound", Code = "JEP" });
            currencies.Add(new Currency { Name = "Jordanian dinar", Code = "JOD" });
            currencies.Add(new Currency { Name = "Kazakhstani tenge", Code = "KZT" });
            currencies.Add(new Currency { Name = "Kenyan shilling", Code = "KES" });
            currencies.Add(new Currency { Name = "Kuwaiti dinar", Code = "KWD" });
            currencies.Add(new Currency { Name = "Kyrgyzstani som", Code = "KGS" });
            currencies.Add(new Currency { Name = "Lao kip", Code = "LAK" });
            currencies.Add(new Currency { Name = "Lebanese pound", Code = "LBP" });
            currencies.Add(new Currency { Name = "Lesotho loti", Code = "LSL" });
            currencies.Add(new Currency { Name = "Liberian dollar", Code = "LRD" });
            currencies.Add(new Currency { Name = "Libyan dinar", Code = "LYD" });
            currencies.Add(new Currency { Name = "Swiss franc", Code = "CHF" });
            currencies.Add(new Currency { Name = "Macanese pataca", Code = "MOP" });
            currencies.Add(new Currency { Name = "Malagasy ariary", Code = "MGA" });
            currencies.Add(new Currency { Name = "Malawian kwacha", Code = "MWK" });
            currencies.Add(new Currency { Name = "Malaysian ringgit", Code = "MYR" });
            currencies.Add(new Currency { Name = "Maldivian rufiyaa", Code = "MVR" });
            currencies.Add(new Currency { Name = "Mauritanian ouguiya", Code = "MRU" });
            currencies.Add(new Currency { Name = "Mauritian rupee", Code = "MUR" });
            currencies.Add(new Currency { Name = "Mexican peso", Code = "MXN" });
            currencies.Add(new Currency { Name = "Moldovan leu", Code = "MDL" });
            currencies.Add(new Currency { Name = "Mongolian tugrik", Code = "MNT" });
            currencies.Add(new Currency { Name = "Moroccan dirham", Code = "MAD" });
            currencies.Add(new Currency { Name = "Mozambican metical", Code = "MZN" });
            currencies.Add(new Currency { Name = "Myanmar kyat", Code = "MMK" });
            currencies.Add(new Currency { Name = "Namibian dollar", Code = "NAD" });
            currencies.Add(new Currency { Name = "Nepalese rupee", Code = "NPR" });
            currencies.Add(new Currency { Name = "CFP franc", Code = "XPF" });
            currencies.Add(new Currency { Name = "New Zealand dollar", Code = "NZD" });
            currencies.Add(new Currency { Name = "Nigerian naira", Code = "NGN" });
            currencies.Add(new Currency { Name = "North Korean won", Code = "KPW" });
            currencies.Add(new Currency { Name = "Macedonian denar", Code = "MKD" });
            currencies.Add(new Currency { Name = "Norwegian krone", Code = "NOK" });
            currencies.Add(new Currency { Name = "Omani rial", Code = "OMR" });
            currencies.Add(new Currency { Name = "Pakistani rupee", Code = "PKR" });
            currencies.Add(new Currency { Name = "Papua New Guinean kina", Code = "PGK" });
            currencies.Add(new Currency { Name = "Paraguayan guarani", Code = "PYG" });
            currencies.Add(new Currency { Name = "Peruvian sol", Code = "PEN" });
            currencies.Add(new Currency { Name = "Philippine peso", Code = "PHP" });
            currencies.Add(new Currency { Name = "Polish zloty", Code = "PLN" });
            currencies.Add(new Currency { Name = "Qatari riyal", Code = "QAR" });
            currencies.Add(new Currency { Name = "Romanian leu", Code = "RON" });
            currencies.Add(new Currency { Name = "Russian ruble", Code = "RUB" });
            currencies.Add(new Currency { Name = "Rwandan franc", Code = "RWF" });
            currencies.Add(new Currency { Name = "Saint Helena pound", Code = "SHP" });
            currencies.Add(new Currency { Name = "East Caribbean dollar", Code = "XCD" });
            currencies.Add(new Currency { Name = "Samoan tala", Code = "WST" });
            currencies.Add(new Currency { Name = "Sao Tome and Principe dobra", Code = "STN" });
            currencies.Add(new Currency { Name = "Saudi Arabian riyal", Code = "SAR" });
            currencies.Add(new Currency { Name = "Serbian dinar", Code = "RSD" });
            currencies.Add(new Currency { Name = "Seychellois rupee", Code = "SCR" });
            currencies.Add(new Currency { Name = "Sierra Leonean leone", Code = "SLL" });
            currencies.Add(new Currency { Name = "Singapore dollar", Code = "SGD" });
            currencies.Add(new Currency { Name = "Netherlands Antillean guilder", Code = "ANG" });
            currencies.Add(new Currency { Name = "Solomon Islands dollar", Code = "SBD" });
            currencies.Add(new Currency { Name = "Somali shilling", Code = "SOS" });
            currencies.Add(new Currency { Name = "South African rand", Code = "ZAR" });
            currencies.Add(new Currency { Name = "Pound sterling", Code = "GBP" });
            currencies.Add(new Currency { Name = "South Korean won", Code = "KRW" });
            currencies.Add(new Currency { Name = "South Sudanese pound", Code = "SSP" });
            currencies.Add(new Currency { Name = "Sri Lankan rupee", Code = "LKR" });
            currencies.Add(new Currency { Name = "Sudanese pound", Code = "SDG" });
            currencies.Add(new Currency { Name = "Surinamese dollar", Code = "SRD" });
            currencies.Add(new Currency { Name = "Norwegian krone", Code = "NOK" });
            currencies.Add(new Currency { Name = "Swedish krona", Code = "SEK" });
            currencies.Add(new Currency { Name = "Swiss franc", Code = "CHF" });
            currencies.Add(new Currency { Name = "Syrian pound", Code = "SYP" });
            currencies.Add(new Currency { Name = "New Taiwan dollar", Code = "TWD" });
            currencies.Add(new Currency { Name = "Tajikistani somoni", Code = "TJS" });
            currencies.Add(new Currency { Name = "Tanzanian shilling", Code = "TZS" });
            currencies.Add(new Currency { Name = "Thai baht", Code = "THB" });
            currencies.Add(new Currency { Name = "Tongan pa’anga", Code = "TOP" });
            currencies.Add(new Currency { Name = "Trinidad and Tobago dollar", Code = "TTD" });
            currencies.Add(new Currency { Name = "Tunisian dinar", Code = "TND" });
            currencies.Add(new Currency { Name = "Turkish lira", Code = "TRY" });
            currencies.Add(new Currency { Name = "Turkmen manat", Code = "TMT" });
            currencies.Add(new Currency { Name = "Ugandan shilling", Code = "UGX" });
            currencies.Add(new Currency { Name = "Ukrainian hryvnia", Code = "UAH" });
            currencies.Add(new Currency { Name = "UAE dirham", Code = "AED" });
            currencies.Add(new Currency { Name = "Uruguayan peso", Code = "UYU" });
            currencies.Add(new Currency { Name = "Uzbekistani som", Code = "UZS" });
            currencies.Add(new Currency { Name = "Vanuatu vatu", Code = "VUV" });
            currencies.Add(new Currency { Name = "Venezuelan bolivar", Code = "VES" });
            currencies.Add(new Currency { Name = "Vietnamese dong", Code = "VND" });
            currencies.Add(new Currency { Name = "Zambian kwacha", Code = "ZMW" });

            return currencies;
        }
    }
}

