namespace E_Tracker.Application.Exceptions.UserException;

public class UserCreatedFailException: Exception
{
    public UserCreatedFailException():base("user created fail")
    {
        
    }

    public UserCreatedFailException(string message):base(message)
    {
        
    }

    public UserCreatedFailException(string message, Exception innerException):base(message,innerException)
    {
        
    }
    
}