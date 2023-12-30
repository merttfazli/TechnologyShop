namespace AdminUI.Models
{
    public class LoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Message { get; set; }
        public ResultModel Result { get; set; }

    }
}
