﻿using SocialStuff.Services;
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
    public class GenerateRequestViewModel : INotifyPropertyChanged
    {
        private string amountText;
        private string description;
        private string selectedTransferType;
        private int transferTypeIndex = -1;
        private int currencyIndex = -1;
        private bool isFormValid;

        private readonly ChatService chatService;

        public GenerateRequestViewModel(ChatService chatService)
        {
            this.chatService = chatService;
            SendMessageCommand = new RelayCommand(ExecuteSendMessage);

            // Set default values
            Description = "";
            AmountText = "";
        }

        public string AmountText
        {
            get => amountText;
            set
            {
                amountText = value;
                OnPropertyChanged();
                ValidateForm();
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

        public event PropertyChangedEventHandler PropertyChanged;

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
                parsedAmount > 0;
        }

        private void ExecuteSendMessage(object parameter)
        {
            try
            {
                int chatID = chatService.getCurrentChatID();

                switch (SelectedTransferType)
                {
                    case "Transfer Money":
                        chatService.sendMoneyViaChat(Amount, Currency, Description, chatID);
                        break;
                    case "Request Money":
                        chatService.requestMoneyViaChat(Amount, Currency, chatID, Description);
                        break;
                    case "Split Bill":
                        chatService.requestMoneyViaChat(Amount / (chatService.getNumberOfParticipants(chatService.getCurrentChatID())), Currency, chatID, description);
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
            // Optionally reset other fields if needed
        }
    }
}