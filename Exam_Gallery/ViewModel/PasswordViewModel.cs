using Exam_Gallery.Command;
using Exam_Gallery.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Exam_Gallery.ViewModel
{
    public class PasswordViewModel : BaseViewModel
    {
        public bool IsAuthorisationSuccessful { get; private set; } = false;

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

        private string password;
        public string UserPassword
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged(nameof(UserPassword));
            }
        }

        private string onLoginMessage;
        public string OnLoginMessage
        {
            get => onLoginMessage;
            set
            {
                onLoginMessage = value;
                OnPropertyChanged(nameof(OnLoginMessage));
            }
        }


        private ICommand onLoginOk;
        public ICommand OnLoginOk
        {
            get
            {
                if (onLoginOk == null)
                    onLoginOk = new UserCommand(ExecuteOk, CanExecuteOk);
                return onLoginOk;
            }
        }
        private void ExecuteOk(object parameter)
        {
            DbModel db = new DbModel();
            IsAuthorisationSuccessful = db.IsUserValid(Login, UserPassword);
            Clear();
            SetTextMessage();
        }

        private bool CanExecuteOk(object parameter) => !HasEmptyField();
        
        private ICommand onLoginCancel;
        public ICommand OnLoginCancel
        {
            get
            {
                if (onLoginCancel == null)
                    onLoginCancel = new UserCommand(ExecuteCancel, CanExecuteCancel);
                return onLoginCancel;
            }
        }
        private void ExecuteCancel(object param) => Clear();
        private bool CanExecuteCancel(object param) => HasEmptyField() == true ? false : true;

        
        private void DebuggingAuthorization() => MessageBox.Show($"authorization value: {IsAuthorisationSuccessful}");

        //returns true, when both fields have length not less than one symbol
        private bool HasEmptyField() => (String.IsNullOrEmpty(Login) || String.IsNullOrEmpty(UserPassword)) == true ? true : false;
        private void Clear()
        {
            Login = null;
            UserPassword = null;
            onLoginMessage = null;
        }
        private void SetTextMessage()
        {
            if (IsAuthorisationSuccessful)
                OnLoginMessage = "login successful";
            else
                OnLoginMessage = "wrong login or password";
        }
    }
}