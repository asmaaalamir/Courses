
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class ResultViewModel
    {
        public object Data { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
        public bool Authorized { get; set; }

        public ResultViewModel()
        {
            Data = null;
            Success = true;
            Message = string.Empty;
            Authorized = true;
        }

        public ResultViewModel(object data, string message = "", bool success = true, bool authorized = true)
        {
            Data = data;
            Success = success;
            Message = message;
            Authorized = authorized;
        }
        public ResultViewModel Create(bool IsSucceeded, string message, object data = null)
        {
            return new ResultViewModel() { Success = IsSucceeded, Message = message, Data = data };
        }
        public ResultViewModel Create(Exception exception)
        {
            ResultViewModel result = new ResultViewModel();
            result.Success = false;
           if (exception.Message != null && exception != null)
            {
                var ex = exception;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                }
                result.Message = ex.Message;
            }
            else
                result.Message = "Error";

            return result;
        }

        public ResultViewModel Create(bool v, object errorOccurred)
        {
            throw new NotImplementedException();
        }
    }
}
