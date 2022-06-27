namespace Shop.Query.Users._DTOs;

public class LoginNextStep
{
    public bool UserExists { get; set; }
    public NextSteps NextStep { get; set; }

    public enum NextSteps
    {
        Password,
        Register,
        RegisterWithPhone
    }
}