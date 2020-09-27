using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskWebsites.Services
{
    public interface IPasswordSafe
    {
        string Encrypt(string password);
        string Decrypt(string encryptedPassword);
    }
}
