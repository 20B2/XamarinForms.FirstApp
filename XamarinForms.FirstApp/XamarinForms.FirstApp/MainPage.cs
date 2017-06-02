using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinForms.FirstApp
{
    public class MainPage:ContentPage
    {
        //Content = new StackLayout
        //{
        //    VerticalOptions = LayoutOptions.Center,
        //    Children =
        //    {
        //        new Label
        //        {
        //            HorizontalTextAlignment=TextAlignment.Center,
        //            Text="Welcome to Xamarin Forms!"
        //        }
        //    }
        //};

        Entry phoneNumberText;
        Button translateButton;
        Button callButton;

        string translatedNumber;

        public MainPage()
        {
            this.Padding = new Thickness(20, 20, 20, 20);
            StackLayout panel = new StackLayout
            {
                Spacing = 15
            };

            panel.Children.Add(new Label
            {
                Text="Enter a PhoneWord",
                FontSize=Device.GetNamedSize(NamedSize.Large,typeof(Label))
            });

            panel.Children.Add(phoneNumberText = new Entry
            {
                Text = "1-855-XAMARIN",
            });

            panel.Children.Add(translateButton = new Button
            {
                Text="Translate"
            });

            panel.Children.Add(callButton = new Button
            {
                Text="Call",
                IsEnabled=false,
            });

            translateButton.Clicked += OnTranslate;
            callButton.Clicked += OnCall;

            this.Content = panel;
        }

        private void OnTranslate(object sender, EventArgs e)
        {
            string enteredNumber = phoneNumberText.Text;
            translatedNumber = PhonewordTranslator.ToNumber(enteredNumber);
            if(!string.IsNullOrEmpty(translatedNumber))
            {
                callButton.IsEnabled = true;
                callButton.Text = "Call " + translatedNumber;
            }
            else
            {
                callButton.IsEnabled = false;
                callButton.Text = "Call";
            }
        }
        
        private async void OnCall(object sender, EventArgs e)
        {
            if(await this.DisplayAlert(
                "Dial a Number",
                "Wouldyou like to call " + translatedNumber + "?",
                "Yes",
                "No"))
            {
                //dial a phone
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    await dialer.DialAsync(translatedNumber);
                }
            }
        }
    }
}
