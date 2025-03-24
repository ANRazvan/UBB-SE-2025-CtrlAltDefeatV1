using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using SocialStuff.Model;
using SocialStuff.Services;
using System;
using System.Collections.Generic;

namespace SocialStuff.Views
{
    public sealed partial class ReportWindow : Window
    {
        private readonly UserService userService;
        private readonly ReportService reportService;
        private readonly int reportedUserId;
        private readonly int messageId;

        internal ReportWindow(UserService userService, ReportService reportService, int reportedUserId, int messageId)
        {
            this.InitializeComponent();
            this.userService = userService;
            this.reportService = reportService;
            this.reportedUserId = reportedUserId;
            this.messageId = messageId;
        }

        private void CategoryComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryComboBox.SelectedItem is ComboBoxItem selectedItem && selectedItem.Content.ToString() == "Other")
            {
                OtherReasonTextBox.Visibility = Visibility.Visible;
            }
            else
            {
                OtherReasonTextBox.Visibility = Visibility.Collapsed;
            }
        }

        private async void SubmitButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedCategory = (CategoryComboBox.SelectedItem as ComboBoxItem)?.Content.ToString();
            string reason = selectedCategory == "Other" ? OtherReasonTextBox.Text : selectedCategory;

            if (string.IsNullOrEmpty(reason))
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "Please provide a reason for reporting.",
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await dialog.ShowAsync();
                return;
            }

            // Create a new report
            Report report = new Report(messageId, reportedUserId, "Pending", reason, string.Empty);
            reportService.AddReport(report);
            reportService.LogReportedMessages(new List<Report> { report });

            // Increase the reported count for the user
            User reportedUser = userService.GetUserById(reportedUserId);
            string reportMessage = reportedUser.IncreaseReportCount();

            userService.MarkUserAsDangerousAndGiveTimeout(reportedUser);

            // Show report message if not empty
            if (!string.IsNullOrEmpty(reportMessage))
            {
                var reportDialog = new ContentDialog
                {
                    Title = "User Reported",
                    Content = reportMessage,
                    CloseButtonText = "OK",
                    XamlRoot = this.Content.XamlRoot
                };
                await reportDialog.ShowAsync();
            }

            var successDialog = new ContentDialog
            {
                Title = "Success",
                Content = "Report submitted successfully.",
                CloseButtonText = "OK",
                XamlRoot = this.Content.XamlRoot
            };
            await successDialog.ShowAsync();
            this.Close();
        }


        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
