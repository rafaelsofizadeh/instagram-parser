using System;
using System.Security.Cryptography;
using System.Text;

namespace InstagramParser
{
    public class XInstagramGisHash
    {
        private readonly string _gis;
        
        private readonly string _id;
        private readonly string _first;
        private readonly string _after;
            
        private const string VariablesTemplate = "{{\"id\":\"{0}\",\"first\":{1},\"after\":\"{2}\"}}";
        private readonly string _xInstagramGisTemplate = "{0}:{1}";


        
        public XInstagramGisHash(string gis, string id, string first, string after = "")
        {
            _gis = gis;

            _id = id;
            _first = first;
            _after = after;
        }
        
        

        //CHAIN 3 (END)
        /// <summary>
        /// Generates an MD5 hash of x-instagram-gis header, recognized by Instagram security services
        /// </summary>
        public string GenerateXInstagramGisHash()
        {
            string xInstagramGis = GenerateXInstagramGis();
            
            return GenerateMd5Hash(xInstagramGis);
        }
        
        //CHAIN 2
        /// <summary>
        /// Generates a x-instagram-gis header value by templateXInstagramGis from gis and variables, generated by GenerateVariablesFromTemplate()
        /// </summary>
        private string GenerateXInstagramGis()
        {
            string templateVariables = GenerateVariablesFromTemplate();
            
            return String.Format(_xInstagramGisTemplate, _gis, templateVariables);
        }
        
        //CHAIN 1 (START)
        /// <summary>
        /// Generates variables part of the XInstagramGis template
        /// </summary>
        private string GenerateVariablesFromTemplate()
        {
            return String.Format(VariablesTemplate, _id, _first, _after);
        }
        
        /// <summary>
        /// Generate an MD5 hash
        /// </summary>
        /// <param name="toEncrypt">String to be encrypted</param>
        public static string GenerateMd5Hash(string toEncrypt)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5Provider.ComputeHash(new UTF8Encoding().GetBytes(toEncrypt));

            foreach (byte t in bytes)
                hash.Append(t.ToString("x2"));        

            return hash.ToString();
        }
    }
}