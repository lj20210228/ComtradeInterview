using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Exceptions
{
    public class CampaignLimitException(string message) : Exception(message);
    public class NotFoundException(string message) : Exception(message);
}
