using Entities;
using System;
using System.Collections.Generic;

namespace ServiceContracts.CountryDTO
{
    /// <summary>
    /// DTO Class for adding a new Country
    /// </summary>
    public class CountryRequestDTO
    {
        public string? CountryName { get; set; }

        public Country ToCountry()
        {
            return new Country() { CountryName = CountryName };
        }
    }
}
