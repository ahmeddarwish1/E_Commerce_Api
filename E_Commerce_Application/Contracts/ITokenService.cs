using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Application.Contracts
{
    public interface ITokenService
    {
        string createToken(string userid, string email, string username ,IEnumerable<string> roles);
    }
}
