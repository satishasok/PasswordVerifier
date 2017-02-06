using System;

namespace PasswordVerifierErrors
{

    /// <summary>
    /// Summary description for InputError
    /// </summary>
    public class InputError : Exception
    {
        public InputError(string message)
        {
            this.m_strErrorMsg = message; 
        }

        public string m_strErrorMsg;
    }
}
