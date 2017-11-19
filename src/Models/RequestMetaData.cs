using System;

namespace CorePro.SDK.Models
{
    public class RequestMetaData
    {
        /// <summary>
        /// The globally unique value logged in CorePro tied to the request.  Primarily for ticket reporting and troubleshooting purposes.
        /// </summary>
        public Guid? RequestId { get; set; }

        /// <summary>
        /// The end user's IP Address at request time
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// The end user's User Agent as reported by their browser / UI application at request time
        /// </summary>
        public string UserAgent { get; set; }

        /// <summary>
        /// The end user's latitude at request time
        /// </summary>
        public decimal? Latitude { get; set; }
        /// <summary>
        /// The end user's longitude at request time
        /// </summary>
        public decimal? Longitude { get; set; }
        /// <summary>
        /// The end user's elevation at request time
        /// </summary>
        public decimal? Elevation { get; set; }


        /// <summary>
        /// The city the end user is in at request time
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// The state the end user is in at request time (2 character state abbreviation)
        /// </summary>
        public string StateCode { get; set; }
        /// <summary>
        /// The zipcode the end user is in at request time
        /// </summary>
        public string PostalCode { get; set; }
        /// <summary>
        /// The country the end user is in at request time
        /// </summary>
        public string CountryCode { get; set; }


        public string CustomField1 { get; set; }
        public string CustomField2 { get; set; }
        public string CustomField3 { get; set; }
        public string CustomField4 { get; set; }
        public string CustomField5 { get; set; }

        public override string ToString()
        {
            return RequestId == null ? "" : RequestId.ToString();
        }
    }
}