using CorePro.SDK.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorePro.SDK.Utils
{
    public class RequestErrorEventArgs : EventArgs
    {
        public dynamic Envelope { get; set; }  // note this guy is dynamic to ease caller's implementation.  Otherwise everything must be generic types, and that gets difficult when trying to handle all different types.
        public bool IsHandled {  get; set; }

        public int Status
        {
            get
            {
                if (this.Envelope != null)
                {
                    return this.Envelope.Status;
                }
                return -1;
            }
        }
        public string RawRequest
        {
            get
            {
                if (this.Envelope != null)
                {
                    return this.Envelope.RawRequestBody;
                }
                return null;
            }
        }
        public string RawResponse
        {
            get
            {
                if (this.Envelope != null)
                {
                    return this.Envelope.RawResponseBody;
                }
                return null;
            }
        }
        public int FirstErrorCode
        {
            get
            {
                if (this.Envelope != null && this.Envelope.Errors != null && this.Envelope.Errors.Count > 0)
                {
                    return this.Envelope.Errors[0].Code;
                }
                return -1;
            }
        }
        public string FirstErrorMessage
        {
            get
            {
                if (this.Envelope != null && this.Envelope.Errors != null && this.Envelope.Errors.Count > 0)
                {
                    return this.Envelope.Errors[0].Message;
                }
                return null;
            }
        }

        public List<Error> Errors
        {
            get
            {
                if (this.Envelope != null)
                {
                    return (List<Error>)this.Envelope.Errors;
                }
                return new List<Error>();
            }
        }

    }
}
