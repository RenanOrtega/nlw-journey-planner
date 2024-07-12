using System.Net;

namespace Exception.ExceptionsBase;

public abstract class JourneyException : SystemException
{
    public JourneyException(string messsage) : base(messsage)
    {

    }

    public abstract HttpStatusCode GetStatusCode();
    public abstract IList<string> GetErrorMessages();
}
