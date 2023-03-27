using Flunt.Validations;
using MauiSampleLogin.Helper;
using MauiSampleLogin.Models.CreateAccount;

namespace MauiSampleLogin.Contracts.CreateAccount
{
    public class CreateAccountContract : Contract<CreateAccountRequest>
    {
        public CreateAccountContract(CreateAccountRequest createAccount)
        {
            Requires()
                .IsNotNullOrEmpty(createAccount.Name, nameof(createAccount.Name), Messages.NameIsInvalid);

            Requires()
                .IsEmail(createAccount.Email, nameof(createAccount.Email), Messages.EmailIsInvalid)
                .IsNotNullOrEmpty(createAccount.Email, nameof(createAccount.Email), Messages.EmailIsInvalid);

            Requires()
                .IsNotNullOrEmpty(createAccount.Password, nameof(createAccount.Password), Messages.PasswordIsInvalid);
        }
    }
}
