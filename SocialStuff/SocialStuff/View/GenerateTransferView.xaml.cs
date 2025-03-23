using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using Microsoft.UI;
using Windows.Graphics;
using WinRT.Interop;
using SocialStuff.ViewModel;
using SocialStuff.Services;
using SocialStuff.Data;
using Microsoft.IdentityModel.Tokens;

namespace SocialStuff.View
{
    public sealed partial class GenerateTransferView : Page
    {
        public GenerateTransferViewModel ViewModel { get; }
        private ChatService chatService;
        private Page lastChat;
        private Frame rightFrame;

        public GenerateTransferView(GenerateTransferViewModel GenerateTransferViewModel, Page lastChat, Frame rightFrame, int ChatID, ChatService chatService)
        {
            // Create repository and services (this would typically be injected)
            this.chatService = chatService;

            // Initialize ViewModel
            ViewModel = GenerateTransferViewModel;
            this.lastChat = lastChat;
            this.rightFrame = rightFrame;
            this.InitializeComponent();
        }

        private void TransferTypeCombobox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TransferTypeComboBox.SelectedItem != null)
            {
                string selectedValue = ((ComboBoxItem)TransferTypeComboBox.SelectedItem).Content.ToString();
                ViewModel.SelectedTransferType = selectedValue;

                switch (selectedValue)
                {
                    case "Transfer Money":
                        TitleTextBlock.Text = "Make a Transfer";
                        TransferButton.Content = "Transfer Money";
                        break;
                    case "Request Money":
                        TitleTextBlock.Text = "Request Funds";
                        TransferButton.Content = "Request Money";
                        break;
                    case "Split Bill":
                        TitleTextBlock.Text = "Split Bill";
                        TransferButton.Content = "Split Bill";
                        break;
                    default:
                        TitleTextBlock.Text = "";
                        break;
                }
            }
        }

        private void AmountTextBox_BeforeTextChanging(TextBox sender, TextBoxBeforeTextChangingEventArgs args)
        {
            // Empty text is allowed
            if (string.IsNullOrEmpty(args.NewText))
                return;

            // Check if new text matches a valid numeric pattern (digits with optional single decimal point)
            bool isValid = System.Text.RegularExpressions.Regex.IsMatch(
                args.NewText,
                @"^\d*\.?\d{0,2}$"
            );

            // Cancel if invalid
            args.Cancel = !isValid;
        }
        private void AmountTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // This ensures the funds check happens when text changes through user input
            if (sender is TextBox textBox && textBox.FocusState != FocusState.Unfocused)
            {
                // The binding will update the ViewModel.AmountText which triggers CheckFunds
            }
        }

        private void CurrencyComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // This ensures the funds check happens when currency changes
            // The binding will update ViewModel.CurrencyIndex which triggers CheckFunds
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.rightFrame.Content = lastChat;
        }
    }
}