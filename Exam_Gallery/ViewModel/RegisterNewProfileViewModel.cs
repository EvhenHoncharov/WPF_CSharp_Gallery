using Exam_Gallery.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exam_Gallery.ViewModel
{
    public class RegisterNewProfileViewModel : BaseViewModel
    {
        private string login;
        public string Login
        {
            get => login;
            set
            {
                login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string userPassword;
        public string UserPassword
        {
            get => userPassword;
            set
            {
                userPassword = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        private string userPassword2;
        public string UserPassword2
        {
            get => userPassword2;
            set
            {
                userPassword2 = value;
                OnPropertyChanged(nameof(UserPassword2));
            }
        }

        private string name;
        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(Name);
            }
        }

        private string surname;
        public string Surname
        {
            get => surname;
            set
            {
                surname = value;
                OnPropertyChanged(Surname);
            }
        }


        private string onRegisterMessage;
        public string OnRegisterMessage
        {
            get => onRegisterMessage;
            set
            {
                onRegisterMessage = value;
                OnPropertyChanged(nameof(OnRegisterMessage));
            }
        }

        private UserCommand onRegisterCommand;
        public UserCommand OnRegisterCommand
        {
            get
            {
                if (onRegisterCommand == null)
                    onRegisterCommand = new UserCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
                return onRegisterCommand;
            }
        }
        private void ExecuteRegisterCommand(object parameter)
        {
            try
            {
                using (ImageGalleryEntities2 db = new ImageGalleryEntities2())
                {
                    using (DbContext)
                    {

                    }
                }
            }
            catch (Exception ex)
            {
                OnRegisterMessage = ex.Message;
            }
        }
        private bool CanExecuteRegisterCommand(object parameter) => HasNullField() ? false : true;




        private bool HasNullField()
        {
            if (String.IsNullOrEmpty(Login) ||
                String.IsNullOrEmpty(UserPassword) ||
                String.IsNullOrEmpty(UserPassword2) ||
                String.IsNullOrEmpty(Name) ||
                String.IsNullOrEmpty(Surname))
                return true;
            return false;
        }
        private void ClearAll() => Login = UserPassword = UserPassword2 = Name = Surname = OnRegisterMessage = null;
    }
}