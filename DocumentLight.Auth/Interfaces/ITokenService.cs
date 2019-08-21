using DocumentLight.Auth.Models;
using DocumentLight.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DocumentLight.Auth.Interfaces
{
    public interface ITokenService
    {
        AuthorizationToken CreateToken(User user);
    }
}
