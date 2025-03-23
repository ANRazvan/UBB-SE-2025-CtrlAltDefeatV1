using SocialStuff.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.UI.Xaml;

namespace SocialStuff.ViewModel
{
    public class GenerateTransferViewModel : INotifyPropertyChanged
    {
        private string amountText;
        private string description;
        private string selectedTransferType;
        private int transferTypeIndex = -1;
        private int currencyIndex = -1;
        private bool isFormValid;
        private bool hasSufficientFunds = true;
        private bool isCheckingFunds = false;
        private int ChatID;

        private readonly ChatService chatService;

        public GenerateTransferViewModel(ChatService chatService, int ChatID)
        {
            this.chatService = chatService;
            this.ChatID = ChatID;
            SendMessageCommand = new RelayCommand(ExecuteSendMessage);

            // Set default values
            Description = "";
            AmountText = "";
            SelectedTransferType = "";
        }

        public string AmountText
        {
            get => amountText;
            set
            {
                amountText = value;
                OnPropertyChanged();
                ValidateForm();
                CheckFunds();
            }
        }

        public float Amount
        {
            get
            {
                if (float.TryParse(AmountText, out float result))
                {
                    return result;
                }
                return 0f;
            }
        }

        public string Description
        {
            get => description;
            set
            {
                description = value;
                OnPropertyChanged();
                ValidateForm();
            }
        }

        public string SelectedTransferType
        {
            get => selectedTransferType;
            set
            {
                selectedTransferType = value;
                OnPropertyChanged();
                ValidateForm();
                CheckFunds();
            }
        }

        public int TransferTypeIndex
        {
            get => transferTypeIndex;
            set
            {
                transferTypeIndex = value;
                OnPropertyChanged();

                // Update the SelectedTransferType based on index
                switch (transferTypeIndex)
                {
                    case 0:
                        SelectedTransferType = "Transfer Money";
                        break;
                    case 1:
                        SelectedTransferType = "Request Money";
                        break;
                    case 2:
                        SelectedTransferType = "Split Bill";
                        break;
                    default:
                        SelectedTransferType = null;
                        break;
                }

                ValidateForm();
            }
        }

        public int CurrencyIndex
        {
            get => currencyIndex;
            set
            {
                currencyIndex = value;
                OnPropertyChanged();
                ValidateForm();
                CheckFunds();
            }
        }

        public string Currency
        {
            get
            {
                switch (CurrencyIndex)
                {
                    case 0:
                        return "USD";
                    case 1:
                        return "EUR";
                    case 2:
                        return "RON";
                    default:
                        return null;
                }
            }
        }

        public bool IsFormValid
        {
            get => isFormValid;
            set
            {
                if (isFormValid != value)
                {
                    isFormValid = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand SendMessageCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ValidateForm()
        {
            IsFormValid =
                TransferTypeIndex >= 0 &&
                CurrencyIndex >= 0 &&
                !string.IsNullOrWhiteSpace(AmountText) &&
                float.TryParse(AmountText, out float parsedAmount) &&
                parsedAmount > 0 &&
                (SelectedTransferType != "Transfer Money" || HasSufficientFunds);
        }

        private void ExecuteSendMessage()
        {
            try
            {

                switch (SelectedTransferType)
                {
                    case "Transfer Money":
                        chatService.sendMoneyViaChat(Amount, Currency, Description, this.ChatID);
                        break;
                    case "Request Money":
                        chatService.requestMoneyViaChat(Amount, Currency, this.ChatID, Description);
                        break;
                    case "Split Bill":
                        float SplitAmount = Amount / (chatService.getNumberOfParticipants(ChatID));
                        chatService.requestMoneyViaChat(SplitAmount, Currency, this.ChatID, description);
                        break;
                    default:
                        throw new InvalidOperationException("Invalid transfer type selected.");
                }

                // Reset form after successful operation
                ResetForm();
            }
            catch (Exception ex)
            {
                // Handle exception (in a real app, you would show a message to the user)
                System.Diagnostics.Debug.WriteLine($"Error sending message: {ex.Message}");
            }
        }

        private void ResetForm()
        {
            AmountText = "";
            Description = "";
            HasSufficientFunds = true;
            // Optionally reset other fields if needed
        }


        public bool HasSufficientFunds
        {
            get => hasSufficientFunds;
            set
            {
                if (hasSufficientFunds != value)
                {
                    hasSufficientFunds = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ShowInsufficientFundsError));
                }
            }
        }

        public bool IsCheckingFunds
        {
            get => isCheckingFunds;
            set
            {
                if (isCheckingFunds != value)
                {
                    isCheckingFunds = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool ShowInsufficientFundsError
        {
            get
            {
                // Only show error for Transfer Money operations
                return !HasSufficientFunds &&
                       SelectedTransferType == "Transfer Money" &&
                       !string.IsNullOrWhiteSpace(AmountText) &&
                       float.TryParse(AmountText, out float amount) &&
                       amount > 0 &&
                       CurrencyIndex >= 0;
            }
        }

        private void CheckFunds()
        {
            // Only check funds for transfer money operations
            if (SelectedTransferType != "Transfer Money")
            {
                HasSufficientFunds = true;
                return;
            }

            // Return if any required fields are not set
            if (string.IsNullOrWhiteSpace(AmountText) ||
                !float.TryParse(AmountText, out float amount) ||
                amount <= 0 ||
                CurrencyIndex < 0)
            {
                HasSufficientFunds = true;
                return;
            }

            IsCheckingFunds = true;

            try
            {
                int chatID = chatService.getCurrentChatID();
                int currentUserID = chatService.GetCurrentUserID();

                // Calculate total amount based on number of participants
                int participantCount = chatService.getNumberOfParticipants(chatID);
                float totalAmount = amount * (participantCount - 1);

                // Check if user has enough funds for the total amount
                HasSufficientFunds = chatService.enoughFunds(totalAmount, Currency, currentUserID);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error checking funds: {ex.Message}");
                HasSufficientFunds = false;
            }
            finally
            {
                IsCheckingFunds = false;
                OnPropertyChanged(nameof(ShowInsufficientFundsError));
                ValidateForm();
            }
        }
    }
}