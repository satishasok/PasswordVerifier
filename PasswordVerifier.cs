using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using PasswordVerifierErrors;

namespace PasswordVerifier
{
    class PasswordVerifier
    {
        public PasswordVerifier(string inputFilePath)
        {
            m_strInputFilePath = inputFilePath;

            LoadInputFile();

        }

        private void LoadInputFile()
        {
            try
            {
                this.m_arrInputPasswords = File.ReadAllLines(this.m_strInputFilePath);

                if (this.m_arrInputPasswords == null || this.m_arrInputPasswords.Length == 0)
                {
                    throw new Exception("Input File is empty");
                }
            }
            catch (Exception ex)
            {
                throw new InputError(ex.Message);
            }
        }

        public void VerifyPasswordAndOutputResults(string outputFileName)
        {
            // going thru input passwords to see if they valid.
            string[] arrOutputResults = new string[m_arrInputPasswords.Length];
            long index = 0;
            foreach (string password in m_arrInputPasswords)
            {
                if (index == m_arrInputPasswords.Length - 1 && password == "end")
                {
                    continue;
                }
                string outResult = "<" + password + "> is ";
                if (IsValidPassword(password))
                {
                    outResult += "acceptable.";
                }
                else
                {
                    outResult += "not acceptable.";
                }
                arrOutputResults[index++] = outResult;
            }

                // outputing the results.
            File.WriteAllLines(outputFileName, arrOutputResults);

            System.Console.WriteLine("PasswordVerifier: Password verification results are output to \"" + outputFileName + "\"");
        }

        public bool IsValidPassword(string password)
        {
            bool isValid = false;

                // does not contain vowels
            if (password.IndexOfAny(m_arrVowels) < 0)
            {
                return isValid;
            }

                // does chars repeat except for 'e' and 'o'
            if (DoesCharsRepeat(password))
            {
                return isValid;
            }

            if (DoesVowelsOrconsonantsRepeat(password))
            {
                return isValid;
            }
            isValid = true;

            return isValid;
        }

        private bool DoesCharsRepeat(string password)
        {
            char[] arrPassword = password.ToCharArray();
            for (int i = 0; i < arrPassword.Length - 1; i++)
            {
                string tempString = new string(arrPassword[i], 1);
                    // char is not 'e' or 'o'
                if (tempString.IndexOfAny(m_arrRepeatingVowels) < 0)
                {
                    if (arrPassword[i] == arrPassword[i + 1])
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private bool DoesVowelsOrconsonantsRepeat(string password)
        {
            char[] arrPassword = password.ToCharArray();
            for (int i = 0; i < arrPassword.Length - 2; i++)
            {
                string tempString = new string(arrPassword[i], 1);
                string tempString1 = new string(arrPassword[i + 1], 1);
                string tempString2 = new string(arrPassword[i + 2], 1);
                    // char is not 'e' or 'o'
                if (tempString.IndexOfAny(m_arrVowels) < 0)
                {
                        // found consonants, so check if the next 2 chars are consonants
                    if (tempString1.IndexOfAny(m_arrVowels) < 0 && tempString2.IndexOfAny(m_arrVowels) < 0)
                    {
                        return true;
                    }
                }
                else
                {
                        // found a vowel so check if the next 2 chars are vowels
                    if (tempString1.IndexOfAny(m_arrVowels) == 0 && tempString2.IndexOfAny(m_arrVowels) == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private string m_strInputFilePath = "";
        private string[] m_arrInputPasswords = null;

        private char[] m_arrVowels = { 'a', 'e', 'i', 'o', 'u' };
        private char[] m_arrRepeatingVowels = { 'e', 'o' };

    }
}
