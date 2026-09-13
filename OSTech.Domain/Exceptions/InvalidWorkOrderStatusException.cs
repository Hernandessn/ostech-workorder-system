using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSTech.Domain.Exceptions;

public class InvalidWorkOrderStatusException : DomainException
{
    public InvalidWorkOrderStatusException(string message) : base(message) { }
}
