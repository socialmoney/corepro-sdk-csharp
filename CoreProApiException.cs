using CorePro.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK
{
    public class CoreProApiException : Exception
    {
        /// <summary>
        /// List of errors that caused this exception.  Typically business rule failures, but could contain more catastrophic error information as well.  i.e. inspect each one based on Code to determine if it should be displayed to your end users.
        /// </summary>
        public List<Error> Errors { get; private set; }

        /// <summary>
        /// Status of 200 (OK) or 201 (Created) indicate success.  All other values indicate error.  These values align exactly with HTTP status codes. i.e. 400 = BadRequest, 500 = Internal Server Error, etc.
        /// </summary>
        public int Status { get; private set; }

        /// <summary>
        /// The RequestId (aka "IncidentId") associated with the request.  This is used primarily for troubleshooting purposes.  Any errors within CorePro for a given request will capture this value, allowing you to tie your system's logs with CorePro's logs for a specific request.  If no value is provided at request time, one is automatically assigned.
        /// </summary>
        public Guid? RequestId { get; private set; }

        /// <summary>
        /// Gets the exact string of characters returned in the body portion of the HTTP response from the CorePro server.
        /// </summary>
        public string RawResponseBody { get; private set; }
        public CoreProApiException()
            : base()
        {
            this.Errors = new List<Error>();
            this.Status = -1;
        }

        public CoreProApiException(List<Error> errors, int status, string body, Guid? requestId = null)
            : base(errors?.Count > 0 ? errors[0].Message : "No error message specified.")
        {
            this.Errors = errors;
            this.Status = status;
            this.RawResponseBody = body;
            this.RequestId = requestId;
        }

        public string GetFirstErrorMessage()
        {
            var err = Errors.FirstOrDefault();
            if (err == null)
                return null;
            else
                return err.Message;
        }

        public int? GetFirstErrorCode()
        {
            var err = Errors.FirstOrDefault();
            if (err == null)
                return null;
            else
                return err.Code;
        }

        public string GetAllErrorInfo(bool includeRawResponse = false)
        {
            var rv = new StringBuilder();
            foreach (var e in Errors)
            {
                rv.AppendLine(e.ToString());
            }
            if (includeRawResponse)
            {
                rv.AppendLine().AppendLine().AppendLine(this.RawResponseBody);
            }
            return rv.ToString();
        }
    }
}
